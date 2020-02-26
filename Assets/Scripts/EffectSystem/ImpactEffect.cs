using System;
using System.Collections.Generic;
using LTD.HealthSystem;
using LTD.ImpactSystem;

namespace LTD.EffectSystem
{
    public class ImpactEffect : Effect
    {
        public float value;

        private Health health;

        public override HashSet<Type> NecessaryComponents => new HashSet<Type>
        {
            typeof(Health)
        };

        protected override void InitInternalComponents()
        {
            health = EffectReceiver.GetActorComponent<Health>();
        }

        public override void Activate()
        {
            health.ReceiveImpact(new Impact(value, ImpactSource));
        }
    }
}
