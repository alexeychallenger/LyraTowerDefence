using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace LTD.ActorSystem
{
    [RequireComponent(typeof(Actor))]
    public abstract class ActorBehaviour : MonoBehaviour
    {
        private Actor actor;

        public Actor Actor
        {
            get
            {
                if (actor == null)
                {
                    actor = GetComponent<Actor>();
                }
                return actor;
            }
        }

        protected virtual void Awake()
        {
            Assert.IsNotNull(Actor, $"GameObject {name} miss Actor component!");
        }

        public virtual bool ContainActorComponent<T>() where T : ActorBehaviour
        {
            return Actor.ContainActorComponent<T>();
        }

        public virtual bool ContainActorComponent(Type actorComponentType)
        {
            return Actor.ContainActorComponent(actorComponentType);
        }

        public virtual bool TryGetActorComponent(Type type, out ActorBehaviour component)
        {
            return Actor.TryGetActorComponent(out component);
        }

        public virtual bool TryGetActorComponent<T>(out T component) where T : ActorBehaviour
        {
            return Actor.TryGetActorComponent(out component);
        }

        public virtual T GetActorComponent<T>() where T : ActorBehaviour
        {
            return Actor.GetActorComponent<T>();
        }

        public virtual ActorBehaviour GetActorComponent(Type type)
        {
            return Actor.GetActorComponent(type);
        }
    }
}
