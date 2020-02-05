namespace LTD.UI.WindowSystem.Windows
{
    public class MissionsMenuWindow : Window
    {
        public override WindowMenu Name => WindowMenu.MissionsMenu;

        public void Button_OpenStartMenu()
        {
            mc.CloseWindow(WindowMenu.MissionsMenu);
            mc.OpenWindow(WindowMenu.StartMenu);
        }

        public void Button_OpenSettings()
        {
            mc.CloseWindow(WindowMenu.MissionsMenu);
            mc.OpenWindow(WindowMenu.SettingsMenu);
        }
    }
}
