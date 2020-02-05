using System;
using System.Collections;
using LTD.UI.WindowSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LTD.SceneManagement
{
    public class EnterPoint : MonoBehaviour
    {
        void Awake()
        {
            StartCoroutine(LoadingCoroutine());
        }

        IEnumerator Call(Action act)
        {
            act.Invoke();
            yield return new WaitForEndOfFrame();
        }

        IEnumerator LoadingCoroutine()
        {
            Debug.Log("<color=blue>Begin!</color>");

            //yield return Call(Loader.Opening);

            yield return Call(MenuController.Instance.StartMenuMenu);

            SceneManager.LoadScene("Menu");

            //yield return Call(Loader.HideOpening);

            Debug.Log("<color=blue>Loaded!</color>");
        }
    }
}
