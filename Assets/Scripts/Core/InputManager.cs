using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Snap.Core
{
    using CallbackContext = InputAction.CallbackContext;
    
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        public static event Action<CallbackContext> Navigate;
        public static event Action<CallbackContext> Submit;
        public static event Action<CallbackContext> Cancel;
        public static event Action<CallbackContext> Point;
        public static event Action<CallbackContext> Click;
        public static event Action<CallbackContext> RightClick;
        public static event Action<CallbackContext> MiddleClick;
        public static event Action<CallbackContext> ScrollWheel;
        public static event Action<CallbackContext> TrackedDevicePosition;
        public static event Action<CallbackContext> TrackedDeviceOrientation;
        
        private static InputManager _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("More than one InputManager in scene. Destroying duplicate instance");
                Destroy(this);
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnNavigate(CallbackContext context)
        {
            Navigate?.Invoke(context);
        }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnSubmit(CallbackContext context)
        {
            Submit?.Invoke(context);
        }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnCancel(CallbackContext context)
        {
            Cancel?.Invoke(context);
        }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnPoint(CallbackContext context)
        {
            Point?.Invoke(context);
        }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnClick(CallbackContext context)
        {
            Click?.Invoke(context);
        }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnRightClick(CallbackContext context)
        {
            RightClick?.Invoke(context);
        }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnMiddleClick(CallbackContext context)
        {
            MiddleClick?.Invoke(context);
        }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnScrollWheel(CallbackContext context)
        {
            ScrollWheel?.Invoke(context);
        }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnTrackedDevicePosition(CallbackContext context)
        {
            TrackedDevicePosition?.Invoke(context);
        }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public void OnTrackedDeviceOrientation(CallbackContext context)
        {
            TrackedDeviceOrientation?.Invoke(context);
        }
    }
}
