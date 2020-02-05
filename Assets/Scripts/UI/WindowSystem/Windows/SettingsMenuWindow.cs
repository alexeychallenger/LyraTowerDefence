namespace LTD.UI.WindowSystem.Windows
{
    public class SettingsMenuWindow : Window
    {
        public override WindowMenu Name => WindowMenu.SettingsMenu;

        public void Button_OpenStartMenu()
        {
            mc.CloseWindow(WindowMenu.SettingsMenu);
            mc.OpenWindow(WindowMenu.StartMenu);
        }

        public void Button_OpenMissions()
        {
            mc.CloseWindow(WindowMenu.SettingsMenu);
            mc.OpenWindow(WindowMenu.MissionsMenu);
        }
    }
}
