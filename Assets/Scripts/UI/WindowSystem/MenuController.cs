using System;
using System.Collections.Generic;
using LTD.UI.WindowSystem.Windows;
using UnityEngine;

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
        public static event Action OnLevelChanged;

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

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _windowList = new Dictionary<WindowMenu, Window>();
            var windows = GetComponentsInChildren<Window>();
            foreach (var window in windows)
            {
                window.Close(instant: true);
                var windowType = (WindowMenu)Enum.Parse(typeof(WindowMenu), window.name);
                _windowList.Add(windowType, window);
            }
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
    }
}