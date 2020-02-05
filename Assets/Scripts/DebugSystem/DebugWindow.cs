using LTD.Utilities;
using UnityEngine;

namespace LTD.DebugSystem
{
    public abstract class DebugWindow : MonoBehaviour, IOnGuiInvokable
    {
        [Header("GUI settings")]
        public bool drawWindow = false;
        public DebugKey drawKey = new DebugKey(KeyCode.Keypad1);
        public Vector2 windowPosition = new Vector2(0f, 0f);
        public Vector2 windowSize = new Vector2(400f, 200f);

        protected Rect windowRect;
        protected Rect defaultRect;
        protected int windowId;

        public abstract string WindowHeader { get; }

        public virtual bool DrawGui => isActiveAndEnabled && drawWindow;

        public string WindowInfoString => $"{WindowHeader} [{drawKey.PrintCombination} - to hide]";

        private DebugManager debugManager;

        protected virtual void Awake()
        {
            windowId = GetType().Name.GetHashCode();
            windowRect = defaultRect = new Rect(windowPosition, windowSize);

            GameManager.DebugManager.AddDebugWindow(this);
        }

        protected virtual void OnDestroy()
        {
            GameManager.DebugManager.RemoveDebugWindow(this);
        }

        protected virtual void Update()
        {
            if (drawKey.IsPressed)
            {
                drawWindow = !drawWindow;
                //SwitchCursorMode();
            }
        }

        public virtual void OnGui()
        {
            windowRect = GUI.Window(windowId, windowRect, DrawGuiWindow, WindowInfoString);
        }

        protected abstract void DrawGuiWindow(int id);
    }
}
