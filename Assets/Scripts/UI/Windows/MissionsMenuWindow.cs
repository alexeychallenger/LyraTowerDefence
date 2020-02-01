using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuUI
{
    public class MissionsMenuWindow : Window
    {
        public void Button_OpenStartMenu()
        {
            mc.CloseWindow(MenuController.MISSIONS_MENU_WINDOW);
            mc.OpenWindow(MenuController.START_MENU_WINDOW);
        }

        public void Button_OpenSettings()
        {
            mc.CloseWindow(MenuController.MISSIONS_MENU_WINDOW);
            mc.OpenWindow(MenuController.SETTINGS_MENU_WINDOW);
        }
    }
}
