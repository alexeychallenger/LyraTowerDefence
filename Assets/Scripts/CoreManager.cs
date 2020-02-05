using UnityEngine;

namespace LTD
{
    public class CoreManager : MonoBehaviour
    {
        public static CoreManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            Instance = this;
        }
    }
}
