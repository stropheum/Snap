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
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _mainCamera = Camera.main;
            Debug.Assert(_mainCamera != null);
        }

        private void Start()
        {
            BindInputCallbacks();
        }

        private void FixedUpdate()
        {
            HandleDrag();
        }

        private void OnDestroy()
        {
            UnbindInputCallbacks();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.25f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + _clickOffsetFromCenter, 0.25f);
        }

        private void BindInputCallbacks()
        {
            InputManager.Point += InputManagerOnPoint;
            InputManager.Click += InputManagerOnClick;
            InputManager.Cancel += context => Debug.Log("Canceled");
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
                _clickOffsetFromCenter = hit.point - hit.collider.transform.position;
            }
        }

        private void InputManagerOnClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (IsHovered)
                {
                    Debug.Log("Started Dragging: " + _currentHoverHit.collider.gameObject.name);
                    _isDragging = true;
                }
            }
            else if (context.canceled)
            {
                Debug.Log("Stopped Dragging: " + _currentHoverHit.collider.gameObject.name);
                _isDragging = false;
            }
            else if (context.performed)
            {
                Debug.Log("Performed");
            }
        }

        private void HandleDrag()
        {
            if (!_isDragging) { return; }
            Debug.Log("Dragging: " + gameObject.name);
            Vector3 hitPoint = _currentHoverHit.point;
            // var moveTarget = new Vector3(hitPoint.x + _clickOffsetFromCenter.x, hitPoint.y + _clickOffsetFromCenter.y, _rigidbody.position.z);
            Vector3 moveTarget = _currentHoverHit.point;
            moveTarget.z = _rigidbody.position.z;
            Vector3 newPosition = Vector3.Lerp(_rigidbody.position, moveTarget, Time.fixedDeltaTime * _moveSpeed);
            _rigidbody.MovePosition(newPosition);
            // moveTarget.z = _rigidbody.position.z;
            // Debug.DrawLine(_rigidbody.position, moveTarget, Color.green);
            // _rigidbody.MovePosition(moveTarget);
        }
    }
}
