using UnityEngine;
using UnityEngine.UI;
using LTD.PlayerControls;

namespace LTD.UI
{
    public class DialogWindow : MonoBehaviour
    {
        private static DialogWindow _instance = null;
        public static DialogWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DialogWindow>();
                }
                return _instance;
            }
        }

        public static bool AnswerRecieved
        {
            get
            {
                return !Instance.gameObject.activeInHierarchy;
            }
        }

        [SerializeField]
        private Image Avatar;
        [SerializeField]
        private Text MainText;
        [SerializeField]
        private Text LeftButtonText;
        [SerializeField]
        private Text RightButtonText;
        [SerializeField]
        private GameObject LeftButton;
        [SerializeField]
        private GameObject RightButton;

        private void Awake()
        {
            _instance = this;
            gameObject.SetActive(false);
        }

        public void ShowDialog(string avatar, string mainText, string rightButtonText = null, string leftButtonText = null)
        {
            gameObject.SetActive(true);
            PlayerControl.Instance.SetPause(true);
            try
            {
                Avatar.sprite = Resources.Load<Sprite>("Sprites/Dialog/" + avatar);
            }
            catch
            {
                Debug.LogError($"something wrong with [{avatar}] string");
                Avatar.sprite = Resources.Load<Sprite>("Sprites/Dialog/grumpy");
            }

            MainText.text = mainText;

            LeftButton.SetActive(leftButtonText != null);
            if (leftButtonText != null)
            {
                LeftButtonText.text = leftButtonText;
            }

            RightButton.SetActive(rightButtonText != null);
            if (rightButtonText != null)
            {
                RightButtonText.text = rightButtonText;
            }

        }

        public void HideDialog()
        {
            gameObject.SetActive(false);
            PlayerControl.Instance.SetPause(false);
        }

        public void Button_PressLeft()
        {
            Lua.LuaResponder.SaveVariable("answer", "left");
            HideDialog();
        }
        public void Button_PressRight()
        {
            Lua.LuaResponder.SaveVariable("answer", "right");
            HideDialog();
        }
    }
}
