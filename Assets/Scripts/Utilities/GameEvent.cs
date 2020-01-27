using System;

namespace LTD.Utilities
{
    public class GameEvent
    {
        private event Action OnEvent;

        public bool IsEventCaused { get; private set; }

        public void WaitEvent(Action callback)
        {
            if (IsEventCaused)
            {
                callback?.Invoke();
            }
            else
            {
                OnEvent += callback;
            }
        }

        public void StopWaitEvent(Action callback)
        {
            OnEvent -= callback;
        }

        public void InvokeEvent()
        {
            IsEventCaused = true;
            OnEvent?.Invoke();
        }

        public void Reset()
        {
            IsEventCaused = false;
        }
    }

    public class GameEvent<T>
    {
        private event Action<T> OnEvent;

        private T parameter;

        public bool IsEventCaused { get; private set; }

        public void WaitEvent(Action<T> callback)
        {
            if (IsEventCaused)
            {
                callback?.Invoke(parameter);
            }
            else
            {
                OnEvent += callback;
            }
        }

        public void StopWaitEvent(Action<T> callback)
        {
            OnEvent -= callback;
        }

        public void InvokeEvent(T eventParameter)
        {
            IsEventCaused = true;
            parameter = eventParameter;
            OnEvent?.Invoke(parameter);
        }

        public void Reset()
        {
            IsEventCaused = false;
            parameter = default;
        }
    }


    public class GameEvent<T1, T2>
    {
        private event Action<T1, T2> OnEvent;

        private T1 parameter1;
        private T2 parameter2;

        public bool IsEventCaused { get; private set; }

        public void WaitEvent(Action<T1, T2> callback)
        {
            if (IsEventCaused)
            {
                callback?.Invoke(parameter1, parameter2);
            }
            else
            {
                OnEvent += callback;
            }
        }

        public void StopWaitEvent(Action<T1, T2> callback)
        {
            OnEvent -= callback;
        }

        public void InvokeEvent(T1 eventParameter1, T2 eventParameter2)
        {
            parameter1 = eventParameter1;
            parameter2 = eventParameter2;

            IsEventCaused = true;
            OnEvent?.Invoke(parameter1, parameter2);
        }

        public void Reset()
        {
            IsEventCaused = false;
            parameter1 = default;
            parameter2 = default;
        }
    }
}
