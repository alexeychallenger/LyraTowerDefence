using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MenuUI;

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
