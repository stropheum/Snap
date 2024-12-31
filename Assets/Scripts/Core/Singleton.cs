using UnityEngine;

namespace Snap.Core
{
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
