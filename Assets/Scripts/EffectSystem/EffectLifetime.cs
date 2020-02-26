using System.Collections;
using UnityEngine;

namespace LTD.EffectSystem
{
    [RequireComponent(typeof(Effect))]
    public class EffectLifetime : MonoBehaviour
    {
        public float lifetime = 0;

        private Coroutine effectLifetimeCoroutine;

        public float LifetimeLeft { get; protected set; }

        [SerializeField][HideInInspector] protected Effect effect;

        private void OnValidate()
        {
            effect = GetComponent<Effect>();
        }

        private void Awake()
        {
            effect.onActivate.WaitEvent(OnEffectActivate);
            effect.onDestroy.WaitEvent(OnEffectDestroy);
        }

        private void OnDestroy()
        {
            effect.onActivate.StopWaitEvent(OnEffectActivate);
            effect.onDestroy.StopWaitEvent(OnEffectDestroy);
            StopLifetimeCoroutine();
        }

        private void OnEffectActivate(Effect effect)
        {
            if (lifetime <= 0)
            {
                this.effect.DestroyEffect();
            }
            else
            {
                effectLifetimeCoroutine = StartCoroutine(EffectLifetimeCoroutine());
            }
        }

        private void OnEffectDestroy(Effect effect)
        {
            effect.onActivate.StopWaitEvent(OnEffectActivate);
            effect.onDestroy.StopWaitEvent(OnEffectDestroy);
            StopLifetimeCoroutine();
        }

        private void StopLifetimeCoroutine()
        {
            if (effectLifetimeCoroutine != null)
            {
                StopCoroutine(effectLifetimeCoroutine);
                effectLifetimeCoroutine = null;
            }
        }

        private IEnumerator EffectLifetimeCoroutine()
        {
            LifetimeLeft = lifetime;
            while (LifetimeLeft > 0)
            {
                LifetimeLeft -= Time.deltaTime;
                yield return null;
            }
            effect.DestroyEffect();
            effectLifetimeCoroutine = null;
        }
    }
}
