using UnityEngine;

namespace OCSFX
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [SerializeField] private bool _dontDestroyOnLoad;
    
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;

            if (_dontDestroyOnLoad) DontDestroyOnLoad(this);
        }
    }
}
