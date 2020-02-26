using LTD.ActorSystem;
using LTD.EffectSystem;
using LTD.ImpactSystem;
using UnityEngine;

namespace LTD.HealthSystem
{
    [RequireComponent(typeof(Health))]
    public class TestDamager : ActorBehaviour
    {
        public Effect[] effects;

        public float heal = 25f;
        public float damage = -25f;
        public float resurrectPercent = 0.3f;

        private Health health;
        private EffectReceiver effectReceiver;

        private ImpactSource impactSource;
        private Impact healImpact;
        private Impact damageImpact;

        private void OnValidate()
        {
            health = GetComponent<Health>();
            effectReceiver = GetComponent<EffectReceiver>();
        }

        private void Start()
        {
            impactSource = new ImpactSource(new ImpactOwner(Actor));
            healImpact = new Impact(heal, impactSource);
            damageImpact = new Impact(damage, impactSource);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                effectReceiver.ReceiveEffects(effects, ref impactSource);
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                health.ReceiveImpact(damageImpact);
            }

            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                health.ReceiveImpact(healImpact);
            }
            if (Input.GetKeyDown(KeyCode.KeypadPeriod))
            {
                health.ReceiveImpact(damageImpact);
            }
            if (Input.GetKeyDown(KeyCode.KeypadDivide))
            {
                health.Die(impactSource);
            }
            if (Input.GetKeyDown(KeyCode.KeypadMultiply))
            {
                health.Resurrect(impactSource, resurrectPercent);
            }
        }
    }
}
