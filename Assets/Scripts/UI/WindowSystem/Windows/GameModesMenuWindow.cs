namespace LTD.UI.WindowSystem.Windows
{
    public class GameModesMenuWindow : Window
    {
        public override WindowMenu Name => WindowMenu.GameModesMenu;

        public void Button_OpenStoryMode()
        {
            mc.CloseWindow(WindowMenu.GameModesMenu);
            mc.OpenWindow(WindowMenu.StoryModeMenu);
        }

        public void Button_OpenEndlessMode()
        {
            mc.CloseWindow(WindowMenu.GameModesMenu);
            mc.OpenWindow(WindowMenu.EndlessModeMenu);
        }

        public void Button_BackToStartMenu()
        {
            mc.CloseWindow(WindowMenu.GameModesMenu);
            mc.OpenWindow(WindowMenu.StartMenu);
        }
    }
}
