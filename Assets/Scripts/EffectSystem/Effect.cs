using System;
using System.Collections.Generic;
using LTD.ImpactSystem;
using LTD.Utilities;
using UnityEngine;

namespace LTD.EffectSystem
{
    public abstract class Effect : MonoBehaviour, IImpactDataProvider
    {
        public readonly GameEvent<Effect> onActivate = new GameEvent<Effect>();
        public readonly GameEvent<Effect> onDestroy = new GameEvent<Effect>();

        public virtual HashSet<Type> NecessaryComponents => new HashSet<Type>();
        
        public ImpactOwner ImpactOwner => ImpactSource.impactOwner;
        public ImpactSource ImpactSource { get; private set; }
        public EffectReceiver EffectReceiver { get; private set; }

        public virtual void Init(ref ImpactSource impactSource, EffectReceiver effectReceiver)
        {
            ImpactSource = impactSource;
            EffectReceiver = effectReceiver;

            InitInternalComponents();

            Activate();
            onActivate.InvokeEvent(this);
        }

        protected virtual void InitInternalComponents()
        {

        }

        public virtual void Activate()
        {
            //method for overriding
        }

        public virtual void DestroyEffect()
        {
            onDestroy.InvokeEvent(this);
            Destroy(gameObject);
        }
    }
}
