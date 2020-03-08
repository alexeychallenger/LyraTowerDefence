namespace LTD.UI.WindowSystem.Windows
{
    public class StoryModeMenuWindow : Window
    {
        public override WindowMenu Name => WindowMenu.EndlessModeMenu;

        public void Button_OpenGame(int level)
        {
            mc.CloseWindow(WindowMenu.StoryModeMenu);
            mc.LoadLevel("Level1");
        }

        public void Button_BackToGameModes()
        {
            mc.CloseWindow(WindowMenu.StoryModeMenu);
            mc.OpenWindow(WindowMenu.GameModesMenu);
        }
    }
}
