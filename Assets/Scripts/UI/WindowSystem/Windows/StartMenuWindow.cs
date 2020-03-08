namespace LTD.UI.WindowSystem.Windows
{
    public class StartMenuWindow : Window
    {
        public override WindowMenu Name => WindowMenu.StartMenu;

        public void Button_OpenGameModes()
        {
            mc.CloseWindow(WindowMenu.StartMenu);
            mc.OpenWindow(WindowMenu.GameModesMenu);
        }

        public void Button_OpenSettings()
        {
            mc.CloseWindow(WindowMenu.StartMenu);
            mc.OpenWindow(WindowMenu.SettingsMenu);
        }
    }
}
