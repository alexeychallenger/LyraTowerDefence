using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IngameDebugConsole;
using LTD.SceneManagement;
using UnityEngine;

namespace LTD.DebugSystem
{
    public class DebugManager : DebugWindow
    {
        public bool ignoreKeypadDebugKeys;
        public DebugLogManager debugLogManagerPrefab;
        [HideInInspector] public DebugLogManager debugLogManager;
        
        private readonly List<DebugWindow> debugWindows = new List<DebugWindow>();
        private GUILayoutOption[] commonOptions;
        private Vector2 scrollPosition;

        public override string WindowHeader => "Debug Manager";
        public bool AnyDebugWindowsOpen => debugWindows.Any(x => x.drawWindow);

        public void AddDebugWindow(DebugWindow debugWindow)
        {
            debugWindows.Add(debugWindow);
        }

        public bool RemoveDebugWindow(DebugWindow debugWindow)
        {
            return debugWindows.Remove(debugWindow);
        }


        protected override void Awake()
        {
            base.Awake();
            debugLogManager = Instantiate(debugLogManagerPrefab);
            debugLogManager.gameObject.SetActive(true);

            commonOptions = new[]
            {
                GUILayout.Width(windowRect.width * 0.25f),
                GUILayout.Height(windowRect.width * 0.1f)
            };
        }

        protected override void Update()
        {
            base.Update();
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Pause))
            {
                Debug.Break();
            }
#endif
        }

        protected override void DrawGuiWindow(int id)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            foreach (var debugWindow in debugWindows)
            {
                DrawDebugWindowSettings(debugWindow);
            }
            GUILayout.EndScrollView();

            GUI.DragWindow();
        }

        private void DrawDebugWindowSettings(DebugWindow debugWindow)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(debugWindow.WindowInfoString, GUILayout.Width(windowRect.width * 0.65f));
            if (GUILayout.Button(debugWindow.drawWindow ? "Disable" : "Enable", commonOptions))
            {
                debugWindow.drawWindow = !debugWindow.drawWindow;
            }

            GUILayout.EndHorizontal();
        }
    }
}
