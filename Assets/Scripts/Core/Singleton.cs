using UnityEngine;

namespace Snap.Core
{
    /// <summary>
    /// Globally accessible instance. Must be placed in required scene. Will be destroyed when scene unloads
    /// </summary>
    /// <typeparam name="T">Singleton instance</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one InputManager in scene. Destroying duplicate instance");
                Destroy(this);
            }

            Instance = this as T;
        }
    }
}