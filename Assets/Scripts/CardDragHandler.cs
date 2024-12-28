using Snap.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snap
{
    [RequireComponent(typeof(Rigidbody))]
    public class CardDragHandler : MonoBehaviour
    {
        private const float DragPlaneDepth = -2f;
        
        private bool IsHovered => _currentHoverHit.collider != null && _currentHoverHit.collider.gameObject == gameObject;
        
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _moveSpeed = 12f;
        [SerializeField] private float _dragRotationSpeed = 12f;
        [SerializeField] [Range(0f, 30f)] private float _maxDragRotationAngle = 30f;
        
        private Rigidbody _rigidbody;
        private Camera _mainCamera;
        private Vector2 _currentMousePosition;
        private RaycastHit _currentHoverHit;
        private bool _isDragging;
        private Vector3 _clickOffsetFromCenter;
        private Vector3 _origin;
        private Quaternion _originRotation;
        private Vector3 _currentMoveTarget;
        private Quaternion _currentRotationTarget;
        
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
            if (_isDragging)
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
        public void SetOrigin(Vector3 newOrigin)
        {
            _origin = newOrigin;
        }

        /// <summary>
        /// Sets the origin rotation that the card will return to when released
        /// </summary>
        /// <param name="newOriginRotation"></param>
        public void SetOriginRotation(Quaternion newOriginRotation)
        {
            _originRotation = newOriginRotation;
        }

        private void BindInputCallbacks()
        {
            InputManager.Point += InputManagerOnPoint;
            InputManager.Click += InputManagerOnClick;
            InputManager.Cancel += _ => Debug.Log("Canceled");
        }

        private void UnbindInputCallbacks()
        {
            InputManager.Point -= InputManagerOnPoint;            
            InputManager.Click -= InputManagerOnClick;
        }

        private void InputManagerOnPoint(InputAction.CallbackContext context)
        {
            if (_mainCamera == null) { return; }
            _currentMousePosition = context.ReadValue<Vector2>();

            Ray ray = _mainCamera.ScreenPointToRay(_currentMousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                _currentHoverHit = hit;
            }
        }

        private void InputManagerOnClick(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _isDragging = false;
                return;
            }

            if (context.started)
            {
                if (!IsHovered) { return; }
                
                _clickOffsetFromCenter = _currentHoverHit.point - _currentHoverHit.collider.transform.position;
                _isDragging = true;
            }
        }

        private void HandleDrag()
        {
            if (!_isDragging)
            {
                _currentMoveTarget = Vector3.Lerp(_rigidbody.position, _origin, _moveSpeed * Time.fixedDeltaTime);
                return;
            }
            Vector3 moveTarget = _currentHoverHit.point - _clickOffsetFromCenter;
            moveTarget.z = DragPlaneDepth;
            _currentMoveTarget = Vector3.Lerp(_rigidbody.position, moveTarget, _moveSpeed * Time.fixedDeltaTime);
        }
        
        private void HandleDragRotation()
        {
            float direction = Vector3.Dot(Vector3.right, _rigidbody.linearVelocity.normalized) * _maxDragRotationAngle;
            Quaternion rot = Quaternion.Lerp(
                _rigidbody.rotation, 
                _isDragging ? Quaternion.Euler(0f, direction, 0f) : _originRotation, 
                Time.fixedDeltaTime * _dragRotationSpeed);
            _rigidbody.MoveRotation(rot);
        }
    }
}
