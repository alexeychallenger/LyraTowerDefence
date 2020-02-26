using System;
using LTD.ActorSystem;
using LTD.ImpactSystem;
using LTD.Utilities;
using UnityEngine;

namespace LTD.HealthSystem
{
    public class Health : ActorBehaviour
    {
        public readonly GameEvent<Health, ImpactSource> onDied = new GameEvent<Health, ImpactSource>();
        public readonly GameEvent<Health, ImpactSource> onResurrected = new GameEvent<Health, ImpactSource>();
        
        public event ImpactModifier ImpactModifiers;
        public event Action<float> CurrentHpChanged;
        
        public float maxHp = 100f;
        private float currentHp;

        public bool IsAlive => !onDied.IsEventCaused;
        public bool IsDead => onDied.IsEventCaused;

        public float CurrentHp
        {
            get => currentHp;
            private set
            {
                value = Mathf.Clamp(value, 0f, maxHp);
                if (Mathf.Approximately(currentHp, value)) return;

                currentHp = value;
                CurrentHpChanged?.Invoke(currentHp);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            currentHp = maxHp;

            //debug logs;
            CurrentHpChanged += (hpValue) => { Debug.Log($"[Health][{name}] change current hp to: {hpValue}".AddColorTag(new Color(1f, 0.5f, 0f))); };
            onDied.WaitEvent((target, impactSource) => { Debug.Log($"[Health][{name}] died from [{impactSource.impactOwner.actor.name}]".AddColorTag(new Color(1f, 0.5f, 0f))); });
            onResurrected.WaitEvent((target, impactSource) => { Debug.Log($"[Health][{name}] resurrected from [{impactSource.impactOwner.actor.name}]".AddColorTag(new Color(1f, 0.5f, 0f))); });
            ////////////////////////
        }

        public void ReceiveImpact(Impact impact)
        {
            if (IsDead) return;

            ImpactModifiers?.Invoke(ref impact);

            CurrentHp += impact.ModifiedValue;
            if (CurrentHp <= 0)
            {
                Die(impact.ImpactSource);
            }
        }

        public void Die(ImpactSource impactSource)
        {
            if (IsDead) return;

            onResurrected.Reset();
            onDied.InvokeEvent(this, impactSource);
        }

        public void Resurrect(ImpactSource impactSource, float hpRestore01)
        {
            if (IsAlive) return;

            CurrentHp = maxHp * Mathf.Clamp01(hpRestore01);

            onDied.Reset();
            onResurrected.InvokeEvent(this, impactSource);
        }
    }
}
