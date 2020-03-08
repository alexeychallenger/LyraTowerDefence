namespace LTD.UI.WindowSystem.Windows
{
    public class SettingsMenuWindow : Window
    {
        public override WindowMenu Name => WindowMenu.SettingsMenu;

        public void Button_BackToStartMenu()
        {
            mc.CloseWindow(WindowMenu.SettingsMenu);
            mc.OpenWindow(WindowMenu.StartMenu);
        }
    }
}
