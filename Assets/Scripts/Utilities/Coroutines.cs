using System.Collections;
using UnityEngine;

namespace LTD.Utilities
{
    public class Coroutines
    {
        private static CoroutineInstance _instance;
        public static CoroutineInstance instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("_coroutines").AddComponent<CoroutineInstance>();

                    DontDestroy();
                }

                return _instance;
            }
        }

        public static void Clear()
        {
            Debug.Log("Routiner Clear");

            if (_instance == null) return;
            Object.Destroy(_instance.gameObject);
            _instance = null;
        }

        private static void DontDestroy()
        {
            if (Application.isPlaying)
            {
                GameObject.DontDestroyOnLoad(_instance);
            }
        }

        public static Coroutine Start(IEnumerator routine)
        {
            return instance.StartCoroutine(routine);
        }

        public class CoroutineInstance : MonoBehaviour
        {

        }
    }
}
