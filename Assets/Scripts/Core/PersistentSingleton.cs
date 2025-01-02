namespace Snap.Core
{
    /// <summary>
    /// Globally accessible instance. Must be placed in required scene. Will be destroyed when scene unloads
    /// </summary>
    /// <typeparam name="T">Singleton instance</typeparam>
    public class PersistentSingleton<T> : Singleton<PersistentSingleton<T>> where T : PersistentSingleton<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
