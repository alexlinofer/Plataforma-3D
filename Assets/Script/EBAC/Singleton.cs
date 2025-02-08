using UnityEngine;

namespace JogoPlataforma3D.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = FindObjectOfType<T>();

                            if (_instance == null)
                            {
                                Debug.LogError($"An instance of {typeof(T)} is needed in the scene, but there is none.");
                            }
                        }
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
                Debug.Log("Singleton Instance Created: " + typeof(T).ToString());
            }
            else if (_instance != this)
            {
                Debug.LogWarning("Instance already exists, not destroying: " + gameObject.name);
                // Ao invés de destruir, apenas ignora o novo e mantém o existente
                return;
            }
        }
    }
}
