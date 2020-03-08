using UnityEngine;
using UnityEngine.UI;

namespace LTD.UI.WindowSystem.Windows
{
    public class GameMenuWindow : Window
    {
        [SerializeField] GameObject MainPageTrap;
        [SerializeField] GameObject MainPageCastle;
        [SerializeField] GameObject MainPageTower;



        public override WindowMenu Name => WindowMenu.GameMenu;

        public override void Init()
        {
            base.Init();
        }


        public void Button_PressTrapPage()
        {
            if (MainPageTrap.activeInHierarchy)
            {
                MainPageTrap.SetActive(false);
                MainPageCastle.SetActive(false);
                MainPageTower.SetActive(false);
            }
            else
            {
                MainPageTrap.SetActive(true);
                MainPageCastle.SetActive(false);
                MainPageTower.SetActive(false);
            }
        }

        public void Button_PressCastlePage()
        {
            if (MainPageCastle.activeInHierarchy)
            {
                MainPageTrap.SetActive(false);
                MainPageCastle.SetActive(false);
                MainPageTower.SetActive(false);
            }
            else
            {
                MainPageTrap.SetActive(false);
                MainPageCastle.SetActive(true);
                MainPageTower.SetActive(false);
            }
        }

        public void Button_PressTowerPage()
        {
            if (MainPageTower.activeInHierarchy)
            {
                MainPageTrap.SetActive(false);
                MainPageCastle.SetActive(false);
                MainPageTower.SetActive(false);
            }
            else
            {
                MainPageTrap.SetActive(false);
                MainPageCastle.SetActive(false);
                MainPageTower.SetActive(true);
            }
        }

        public void Button_BuildTower(string key)
        {
            PlayerControls.PlayerControl.Instance.StartBuilding(key);
        }
        public void Button_BuyUpgrade(string key)
        {

        }
    }
}
