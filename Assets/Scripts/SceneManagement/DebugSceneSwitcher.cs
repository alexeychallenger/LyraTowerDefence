using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTD.DebugSystem;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace LTD.SceneManagement
{
    public class DebugSceneSwitcher : DebugWindow
    {
        private Vector2 scrollPos;

        public override string WindowHeader => "Scene Switcher";

        private string[] sceneNames;
        private Scenes selectedScene;

        protected override void Awake()
        {
            base.Awake();
            sceneNames = typeof(Scenes).GetEnumNames();
            GameManager.LoadingManager.OnSceneLoaded += OnSceneLoaded;
        }

        protected override void OnDestroy()
        {
            GameManager.LoadingManager.OnSceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scenes currentScene)
        {
            selectedScene = currentScene;
        }

        protected override void DrawGuiWindow(int id)
        {
            GUILayout.Label("---------------------------------------------------------");
            GUILayout.Label($"Current Scene: {GameManager.LoadingManager.CurrentScene}");
            GUILayout.Label("---------------------------------------------------------");

            scrollPos = GUILayout.BeginScrollView(scrollPos);
            selectedScene = (Scenes)GUILayout.SelectionGrid((int)selectedScene, sceneNames, 1);
            GUILayout.EndScrollView();

            GUILayout.Label("---------------------------------------------------------");
            if (GUILayout.Button($"Load [{selectedScene}] scene"))
            {
                GameManager.LoadingManager.LoadScene(selectedScene);
            }
            GUILayout.Label("---------------------------------------------------------");

            GUI.DragWindow();
        }
    }
}
