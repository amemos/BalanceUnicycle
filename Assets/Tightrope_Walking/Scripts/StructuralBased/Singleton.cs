using UnityEngine;

namespace amemo.balanceUnicycle.structurals.Singleton
{
    /// <summary>
    ///  Generic Singleton template.
    ///  
    ///  created by: Ahmet Þentürk
    /// </summary>
    /// 
    public class Singleton<T> : BaseMonoBehaviour where T : BaseMonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                    return null;

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                            return _instance;

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "Singleton " + typeof(T).ToString();
                            DontDestroyOnLoad(singleton);
                        }
                        else
                            DontDestroyOnLoad(_instance.gameObject);
                    }
                    return _instance;
                }
            }
        }

        private static bool applicationIsQuitting = false;

        //public override void Initialize() => base.Initialize();

        protected override void OnDestroy()
        {
            base.OnDestroy();
            applicationIsQuitting = true;
        }
    }
}