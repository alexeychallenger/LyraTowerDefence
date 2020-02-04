using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuUI
{
    public class StartMenuWindow : Window
    {
        public void Button_OpenMissions()
        {
            mc.CloseWindow(MenuController.START_MENU_WINDOW);
            mc.OpenWindow(MenuController.MISSIONS_MENU_WINDOW);
        }

        public void Button_OpenSettings()
        {
            mc.CloseWindow(MenuController.START_MENU_WINDOW);
            mc.OpenWindow(MenuController.SETTINGS_MENU_WINDOW);
        }
    }
}
