namespace LTD.UI.WindowSystem.Windows
{
    public class StartMenuWindow : Window
    {
        public override WindowMenu Name => WindowMenu.StartMenu;

        public void Button_OpenMissions()
        {
            mc.CloseWindow(WindowMenu.StartMenu);
            mc.OpenWindow(WindowMenu.MissionsMenu);
        }

        public void Button_OpenSettings()
        {
            mc.CloseWindow(WindowMenu.StartMenu);
            mc.OpenWindow(WindowMenu.SettingsMenu);
        }
    }
}
