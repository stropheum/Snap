using System;
using Snap.Core;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snap.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class CardDragHandler : MonoBehaviour
    {
        public static event Action<CardDragHandler> DragStateChanged;
        private const float DragPlaneDepth = -2f;
        
        private bool IsHovered => _currentHoverHit.collider != null && _currentHoverHit.collider.gameObject == gameObject;

        public bool IsDragging
        {
            get => _isDragging;
            private set
            {
                if (_isDragging != value)
                {
                    _isDragging = value;
                    DragStateChanged?.Invoke(this);
                }
            }
        }

        [SerializeField] private LayerMask _defaultLayerMask;
        [SerializeField] private LayerMask _draggingLayerMask;
        [SerializeField] private float _moveSpeed = 12f;
        [SerializeField] private float _dragRotationSpeed = 12f;
        [SerializeField] private float _dragRotationDampening = 100f;
        [SerializeField] [Range(0f, 30f)] private float _maxDragRotationAngle = 30f;
        [SerializeField] private TextMeshPro _debugText;
        
        private Rigidbody _rigidbody;
        private Camera _mainCamera;
        private Vector2 _currentMousePosition;
        private RaycastHit _currentHoverHit;
        private Vector3 _clickOffsetFromCenter;
        private Vector3 _origin;
        private Quaternion _originRotation;
        private Vector3 _currentMoveTarget;
        private Quaternion _currentRotationTarget;
        private bool _isDragging;

        private void Awake()
        {
            _mainCamera = Camera.main;
            Debug.Assert(_mainCamera != null);
            _rigidbody = GetComponent<Rigidbody>();
            SetOrigin(transform.position);
            SetOriginRotation(transform.rotation.normalized);
        }

        private void Start()
        {
            BindInputCallbacks();
        }

        private void Update()
        {
            HandleDrag();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_currentMoveTarget);
            HandleDragRotation();
        }

        private void OnDestroy()
        {
            UnbindInputCallbacks();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.25f);
            if (IsDragging)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position + _clickOffsetFromCenter, 0.25f);   
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_currentMoveTarget, 0.25f);
            }
        }

        /// <summary>
        /// Sets the origin that the card will return to when released
        /// </summary>
        /// <param name="newOrigin">The new origin of the card (world space)</param>
        private void SetOrigin(Vector3 newOrigin)
        {
            _origin = newOrigin;
        }

        /// <summary>
        /// Sets the origin rotation that the card will return to when released
        /// </summary>
        /// <param name="newOriginRotation"></param>
        private void SetOriginRotation(Quaternion newOriginRotation)
        {
            _originRotation = newOriginRotation;
        }

        private void BindInputCallbacks()
        {
            InputManager.Point += InputManager_OnPoint;
            InputManager.Click += InputManager_OnClick;
            InputManager.Cancel += _ => Debug.Log("Canceled");
        }

        private void UnbindInputCallbacks()
        {
            InputManager.Point -= InputManager_OnPoint;            
            InputManager.Click -= InputManager_OnClick;
        }

        private void InputManager_OnPoint(InputAction.CallbackContext context)
        {
            if (_mainCamera == null) { return; }
            _currentMousePosition = context.ReadValue<Vector2>();

            Ray ray = _mainCamera.ScreenPointToRay(_currentMousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _isDragging ? _draggingLayerMask : _defaultLayerMask))
            {
                _currentHoverHit = hit;
                if (_debugText != null)
                {
                    _debugText.text = "Hit: " + _currentHoverHit.collider.gameObject.name;
                }
            }
        }

        private void InputManager_OnClick(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                IsDragging = false;
                return;
            }

            if (context.started)
            {
                if (!IsHovered) { return; }
                
                _clickOffsetFromCenter = _currentHoverHit.point - _currentHoverHit.collider.transform.position;
                IsDragging = true;
            }
        }

        private void HandleDrag()
        {
            if (!IsDragging)
            {
                _currentMoveTarget = Vector3.Lerp(_rigidbody.position, _origin, _moveSpeed * Time.fixedDeltaTime);
                return;
            }
            Vector3 moveTarget = _currentHoverHit.point - _clickOffsetFromCenter;
            Debug.Assert(CardHandPlane.Instance != null);
            moveTarget.z = CardHandPlane.Instance.CardPlanePosition.z;
            // moveTarget.z = DragPlaneDepth;
            _currentMoveTarget = Vector3.Lerp(_rigidbody.position, moveTarget, _moveSpeed * Time.fixedDeltaTime);
        }
        
        private void HandleDragRotation()
        {
            if (!_isDragging)
            {
                _rigidbody.MoveRotation(_originRotation);
                return;
            }

            float percentMaxSpeed = _rigidbody.linearVelocity.magnitude / _dragRotationDampening;
            float percent = Vector3.Dot(Vector3.right, _rigidbody.linearVelocity.normalized) * percentMaxSpeed;
            float direction = percent * _maxDragRotationAngle;
            Quaternion rot = Quaternion.Euler(0f, direction, 0f);
            _rigidbody.MoveRotation(rot);
        }
    }
}
