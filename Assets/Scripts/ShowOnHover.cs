using System.Collections.Generic;
using Snap.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snap
{
    public class ShowOnHover : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        private Camera _mainCamera;
        private HashSet<CardDragHandler> _activeCardDragHandlers = new();
        private bool _isHovered;

        private void Awake()
        {
            _mainCamera = Camera.main;
            InputManager.Point += InputManager_OnPoint; 
            CardDragHandler.DragStateChanged += CardDragHandler_OnDragStateChanged;
        }

        private void OnDestroy()
        {
            InputManager.Point -= InputManager_OnPoint;
            CardDragHandler.DragStateChanged -= CardDragHandler_OnDragStateChanged;
        }
        
        /// <summary>
        /// Callback for the input system's mouse movement event
        /// </summary>
        /// <param name="context">Callback context</param>
        private void InputManager_OnPoint(InputAction.CallbackContext context)
        {
            if (_mainCamera == null) { return; }
            var mousePosition = context.ReadValue<Vector2>();

            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            _isHovered = Physics.Raycast(ray, out RaycastHit _, Mathf.Infinity, _layerMask);
        }

        /// <summary>
        /// Callback for When card drag handler starts or stops dragging
        /// </summary>
        /// <param name="cardDragHandler"></param>
        private void CardDragHandler_OnDragStateChanged(CardDragHandler cardDragHandler)
        {
            if (cardDragHandler.IsDragging)
            {
                _activeCardDragHandlers.Add(cardDragHandler);                
            }
            else
            {
                _activeCardDragHandlers.Remove(cardDragHandler);
            }
        }
    }
}
