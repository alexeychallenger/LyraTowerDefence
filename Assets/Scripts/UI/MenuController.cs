using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuUI
{

    public class MenuController : MonoBehaviour
    {
        public const string START_MENU_WINDOW = "StartMenuWindow";
        public const string MISSIONS_MENU_WINDOW = "MissionsMenuWindow";
        public const string SETTINGS_MENU_WINDOW = "SettingsMenuWindow";
        public const string GAME_MENU_WINDOW = "GameMenuWindow";

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

        public static event OnLevelChanged onLevelChanged;
        public delegate void OnLevelChanged(Level levelContainer);

        public Dictionary<string, Window> _windowList;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _windowList = new Dictionary<string, Window>();
            var windows = GetComponentsInChildren<Window>();
            foreach (var window in windows)
            {
                window.Close(instant: true);
                _windowList.Add(window.name, window);
            }
        }

        public void OpenWindow(string windowName, bool instant = false)
        {
            _windowList[windowName].Open(instant);
        }

        public void CloseWindow(string windowName, bool instant = false)
        {
            _windowList[windowName].Close(instant);
        }

        public void CloseOtherWindowExcept(string windowName)
        {
            foreach (var window in _windowList)
            {
                if (window.Key == windowName) continue;

                _windowList[windowName].Close();
            }
        }

        public void StartMenuMenu()
        {
            OpenWindow(START_MENU_WINDOW);
        }
    }
}