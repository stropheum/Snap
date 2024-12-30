using System;
using System.Collections.Generic;
using Snap.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snap
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ShowOnHover : MonoBehaviour
    {
        public event Action<bool> HoverStateChanged;
        
        [SerializeField] private LayerMask _layerMask;
        private SpriteRenderer _spriteRenderer;
        private Camera _mainCamera;
        private HashSet<CardDragHandler> _activeCardDragHandlers = new();
        private bool _isHovered;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mainCamera = Camera.main;
        }

        private void Start()
        {
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
            if (_mainCamera == null || _activeCardDragHandlers.Count == 0) { return; }
            var mousePosition = context.ReadValue<Vector2>();

            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            bool isHovered = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask)
                && hit.collider.gameObject == gameObject;
            
            if (_isHovered == isHovered) { return; }
            
            //TODO: Temp code. Instead of instantly changing alpha, trigger coroutine to lerp alpha
            _isHovered = isHovered;
            _spriteRenderer.color = _isHovered ? Color.green : Color.white;
            HoverStateChanged?.Invoke(_isHovered);
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
