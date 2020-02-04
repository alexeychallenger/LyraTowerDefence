namespace LTD.UI.WindowSystem.Windows
{
    public class SettingsMenuWindow : Window
    {
        public void Button_OpenStartMenu()
        {
            mc.CloseWindow(MenuController.SETTINGS_MENU_WINDOW);
            mc.OpenWindow(MenuController.START_MENU_WINDOW);
        }

        public void Button_OpenMissions()
        {
            mc.CloseWindow(MenuController.SETTINGS_MENU_WINDOW);
            mc.OpenWindow(MenuController.MISSIONS_MENU_WINDOW);
        }
    }
}
