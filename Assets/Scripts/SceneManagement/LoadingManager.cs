using System;
using System.Collections;
using System.Collections.Generic;
using LTD.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LTD.SceneManagement
{
    public class LoadingManager : MonoBehaviour
    {
        public event Action<Scenes> OnSceneLoaded;

        private float progress;
        private Scenes currentScene;

        private readonly Color debugLogColor = Color.yellow;
        private Coroutine loadingCoroutine;

        public static readonly HashSet<Scenes> systemScenes = new HashSet<Scenes>
        {
            Scenes.EnterPoint,
            Scenes.Loading,
            Scenes.MainMenu,
        };

        public float LoadingProgress => progress;
        public bool IsSceneLoading => loadingCoroutine != null;
        public Scenes CurrentLoadingScene { get; private set; }

        public Scenes CurrentScene
        {
            get => currentScene;
            private set
            {
                if (currentScene == value) return;
                currentScene = value;
#if DEBUG
                Debug.Log($"[LoadingManager] CurrentScene changed to [{currentScene}]".AddColorTag(debugLogColor));
#endif
            }
        }

        private void Awake()
        {
            CurrentScene = (Scenes)Enum.Parse(typeof(Scenes), SceneManager.GetActiveScene().name);
        }

        public void LoadScene(Scenes scene)
        {
            if (IsSceneLoading)
            {
                if (scene == CurrentLoadingScene)
                {
#if DEBUG
                    Debug.LogWarning($"[LoadingManager] Scene [{scene}] is already loading. Skipping loading request".AddColorTag(debugLogColor));
#endif
                    return;
                }

#if DEBUG
                Debug.LogWarning($"[LoadingManager] Can't instant start loading scene [{scene}] cause other scene loading process is running!".AddColorTag(debugLogColor));
#endif
                StartCoroutine(EnqueueLoadSceneAsync(scene));
                return;
            }

            loadingCoroutine = StartCoroutine(LoadSceneAsync(scene));
        }

        private IEnumerator EnqueueLoadSceneAsync(Scenes sceneToLoad)
        {
#if DEBUG
            Debug.Log($"[LoadingManager] Enqueue loading scene [{sceneToLoad}]. Waiting while other loading process ends.".AddColorTag(debugLogColor));
#endif
            yield return new WaitUntil(() => loadingCoroutine == null);
            loadingCoroutine = StartCoroutine(LoadSceneAsync(sceneToLoad));
        }

        public IEnumerator LoadSceneAsync(Scenes sceneToLoad)
        {
            CurrentLoadingScene = sceneToLoad;
#if DEBUG
            Debug.Log($"[LoadingManager] Start loading scene [{CurrentLoadingScene}]".AddColorTag(debugLogColor));
            Debug.Log($"[LoadingManager] Start async loading scene [{Scenes.Loading}]".AddColorTag(debugLogColor));
#endif
            yield return SceneManager.LoadSceneAsync(Scenes.Loading.ToString());
            CurrentScene = Scenes.Loading;
#if DEBUG
            Debug.Log($"[LoadingManager] Start async loading operation for scene [{CurrentLoadingScene}]".AddColorTag(debugLogColor));
#endif
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(CurrentLoadingScene.ToString());
            loadingOperation.allowSceneActivation = false;

            while (!loadingOperation.isDone)
            {
                progress = Mathf.Clamp01(loadingOperation.progress / 0.9f) * 100;

                if (loadingOperation.progress >= 0.9f)
                {
                    CurrentScene = CurrentLoadingScene;
                    loadingOperation.allowSceneActivation = true;
                    break;
                }

                yield return null;
            }

            //again assign value in case when [if (loadingOperation.progress >= 0.9f)] block is not executed
            CurrentScene = CurrentLoadingScene;
            loadingOperation.allowSceneActivation = true;
#if DEBUG
            Debug.Log($"[LoadingManager] Ending async loading operation for scene [{CurrentLoadingScene}]".AddColorTag(Color.yellow));
#endif
            loadingCoroutine = null;
            OnSceneLoaded?.Invoke(CurrentScene);
        }
    }
}
