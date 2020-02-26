using System;
using System.Collections.Generic;
using System.Linq;
using LTD.ActorSystem;
using LTD.ImpactSystem;
using LTD.Utilities;
using UnityEngine;

namespace LTD.EffectSystem
{
    public class EffectReceiver : ActorBehaviour
    {
        public event Action<Effect> EffectAdded;
        public event Action<Effect> EffectRemoved;

        public Transform effectsParent;

        private readonly Color debugColor = new Color(0.3f, 0.1f, 0.4f);

        public readonly List<Effect> effects = new List<Effect>();

        private readonly HashSet<Type> availableComponents = new HashSet<Type>();

        protected override void Awake()
        {
            base.Awake();

            //debug events
            EffectAdded += (effect) => { Debug.Log($"[EffectReceiver] effect [{effect.name}] added".AddColorTag(debugColor)); };
            EffectRemoved += (effect) => { Debug.Log($"[EffectReceiver] effect [{effect.name}] removed".AddColorTag(debugColor)); };
        }

        public void ReceiveEffects(IEnumerable<Effect> effectPrefabs, ref ImpactSource impactSource)
        {
            for (int i = 0; i < effectPrefabs.Count(); i++)
            {
                ReceiveEffect(effectPrefabs.ElementAt(i), ref impactSource);
            }
        }

        public void ReceiveEffect(Effect effectPrefab, ref ImpactSource impactSource)
        {
            if (!HasNecessaryComponents(effectPrefab.NecessaryComponents))
            {
                Debug.LogWarning($"[EffectReceiver] has no NecessaryComponents for effect [{effectPrefab}]");
                return;
            }

            Effect effect = Instantiate(effectPrefab, effectsParent);
            effect.name = effectPrefab.name;
            effects.Add(effect);
            EffectAdded?.Invoke(effect);

            effect.Init(ref impactSource, this);
            effect.onDestroy.WaitEvent(OnEffectDestroy);
        }

        private bool HasNecessaryComponents(IEnumerable<Type> necessaryComponents)
        {
            for (int i = 0; i < necessaryComponents.Count(); i++)
            {
                if (!ContainActorComponent(necessaryComponents.ElementAt(i)))
                {
                    return false;
                }
            }

            return true;
        }

        private void OnEffectDestroy(Effect effect)
        {
            effect.onDestroy.StopWaitEvent(OnEffectDestroy);
            effects.Remove(effect);
            EffectRemoved?.Invoke(effect);
        }
    }
}
