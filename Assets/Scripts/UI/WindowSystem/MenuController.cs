using System;
using System.Collections.Generic;
using LTD.UI.WindowSystem.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;
using LTD.Map.LevelDesing;

namespace LTD.UI.WindowSystem
{
    public enum WindowMenu
    {
        Empty,
        StartMenu,
        MissionsMenu,
        SettingsMenu,
        CreditMenu,
        GameMenu,
    }

    public class MenuController : MonoBehaviour
    {
        private static MenuController _instance;
        public static MenuController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MenuController>();
                }
                return _instance;
            }
        }


        public Dictionary<WindowMenu, Window> _windowList;

        public void Init()
        {
            DontDestroyOnLoad(this);

            SceneManager.sceneLoaded += OnSceneLoaded;

            _windowList = new Dictionary<WindowMenu, Window>();
            var windows = GetComponentsInChildren<Window>();
            foreach (var window in windows)
            {
                window.Close(instant: true);
                var windowType = (WindowMenu)Enum.Parse(typeof(WindowMenu), window.name);
                _windowList.Add(windowType, window);
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OpenWindow(WindowMenu window, bool instant = false)
        {
            _windowList[window].Open(instant);
        }

        public void CloseWindow(WindowMenu window, bool instant = false)
        {
            _windowList[window].Close(instant);
        }

        public void CloseOtherWindowExcept(WindowMenu window)
        {
            foreach (var w in _windowList)
            {
                if (w.Key == window) continue;

                _windowList[window].Close();
            }
        }

        public void StartMenuMenu()
        {
            OpenWindow(WindowMenu.StartMenu);
        }

        public void LoadLevel(string level)
        {
            SceneManager.LoadScene(level);
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var level = FindObjectOfType<Level>();
            if (level != null)
            {
                level.Init();
            }
        }
    }
}