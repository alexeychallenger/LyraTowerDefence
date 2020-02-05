using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace LTD.Utilities
{
    public class OnGuiInvoker : MonoBehaviour
    {
        private IOnGuiInvokable[] invokableComponents = new IOnGuiInvokable[0];

        [SerializeField] private bool useGuiLayoutState = default;

        private void Awake()
        {
            useGuiLayoutState = useGUILayout;
            invokableComponents = GetComponents<IOnGuiInvokable>();
        }

        private void OnGUI()
        {
            bool isGuiCalled = false;
            foreach (var invokable in invokableComponents)
            {
                if (!invokable.DrawGui) continue;

                SwitchGuiLayout(true);
                isGuiCalled = true;

                invokable.OnGui();
            }

            if (!isGuiCalled)
            {
                SwitchGuiLayout(false);
            }
        }

        private void SwitchGuiLayout(bool state)
        {
            if (useGUILayout == state) return;

            useGuiLayoutState = state;
            useGUILayout = state;
        }
    }
}
