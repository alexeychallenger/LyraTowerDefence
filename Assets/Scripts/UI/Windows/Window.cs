using System.Collections;
using UnityEngine;

namespace MenuUI
{
    public abstract class Window : MonoBehaviour
    {
        protected MenuController mc;

        public string Name => gameObject.name;
        public bool IsOpened { get; protected set; }

        void Start()
        {
            mc = MenuController.Instance;
            Init();
        }

        public virtual void Init() { }

        public void Open(bool instant = false)
        {
            if (instant)
            {
                OnStart();
                OnActivate();
            }
            else
            {
                Coroutines.Start(OpenWindowCoroutine());
            }
        }

        public void Close(bool instant = false)
        {
            if (instant)
            {
                OnDeactivate();
                OnEnd();
            }
            else
            {
                Coroutines.Start(CloseWindowCoroutine());
            }
        }

        public virtual void OnStart()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnActivate()
        {
        }

        public virtual void OnDeactivate()
        {
        }

        public virtual void OnEnd()
        {
            gameObject.SetActive(false);
        }


        protected virtual IEnumerator BeforeStartCoroutine()
        {
            yield return new WaitForEndOfFrame();
        }

        protected virtual IEnumerator BeforeActivateCoroutine()
        {
            yield return new WaitForEndOfFrame();
        }

        protected virtual IEnumerator BeforeDeactivateCoroutine()
        {
            yield return new WaitForEndOfFrame();
        }

        protected virtual IEnumerator BeforeEndCoroutine()
        {
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator OpenWindowCoroutine()
        {
            yield return BeforeStartCoroutine();
            OnStart();
            yield return BeforeActivateCoroutine();
            OnActivate();
        }

        private IEnumerator CloseWindowCoroutine()
        {
            yield return BeforeDeactivateCoroutine();
            OnDeactivate();
            yield return BeforeEndCoroutine();
            OnEnd();
        }
    }
}
