using Snap.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snap
{
    [RequireComponent(typeof(Rigidbody))]
    public class CardDragHandler : MonoBehaviour
    {
        private bool IsHovered => _currentHoverHit.collider != null && _currentHoverHit.collider.gameObject == gameObject;
        
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _moveSpeed = 12f;
        
        private Rigidbody _rigidbody;
        private Camera _mainCamera;
        private Vector2 _currentMousePosition;
        private RaycastHit _currentHoverHit;
        private bool _isDragging;
        private Vector3 _clickOffsetFromCenter;
        private Vector3 _origin;
        private Vector3 _currentMoveTarget;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
            Debug.Assert(_mainCamera != null);
            _rigidbody = GetComponent<Rigidbody>();
            SetOrigin(transform.position);
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
                
                Debug.Log("Started Dragging: " + _currentHoverHit.collider.gameObject.name);
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
            Debug.Log("Dragging: " + gameObject.name);
            Vector3 moveTarget = _currentHoverHit.point - _clickOffsetFromCenter;
            moveTarget.z = _rigidbody.position.z;
            _currentMoveTarget = Vector3.Lerp(_rigidbody.position, moveTarget, _moveSpeed * Time.fixedDeltaTime);
        }
    }
}
