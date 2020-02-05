using LTD.DebugSystem;
using LTD.SceneManagement;
using LTD.Utilities;
using UnityEngine;
using UnityEngine.Assertions;

namespace LTD
{
    [RequireComponent(typeof(LoadingManager))]
    public class GameManager : MonoBehaviour
    {
        public static readonly GameEvent onLibrariesInitialization = new GameEvent();

        public static GameManager Instance { get; private set; }

        [SerializeField, HideInInspector] private LoadingManager loadingManager = default;
        [SerializeField] private DebugManager debugManager = default;

        public static LoadingManager LoadingManager => Instance.loadingManager;
        public static DebugManager DebugManager => Instance.debugManager;

#if DEBUG
        private readonly Color debugLogColor = new Color(0.3f, 0.5f, 0.1f);
#endif
        private void OnValidate()
        {
            loadingManager = GetComponent<LoadingManager>();
        }

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Init();
        }

        private void Init()
        {
#if DEBUG
            Debug.Log("[GameManager] Start initialization".AddColorTag(debugLogColor));
#endif
            InitLibraries();
            //other init methods
#if DEBUG
            Debug.Log("[GameManager] Initialization is done!".AddColorTag(debugLogColor));
#endif
        }

        private void InitLibraries()
        {
#if DEBUG
            Debug.Log("[GameManager] Libraries initialization".AddColorTag(debugLogColor));
#endif

            //TODO: load custom libraries here

            onLibrariesInitialization.InvokeEvent();

#if DEBUG
            Debug.Log("[GameManager] Libraries initialization is done!".AddColorTag(debugLogColor));
#endif
        }
    }
}
