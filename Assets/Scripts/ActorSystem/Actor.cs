using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace LTD.ActorSystem
{
    public class Actor : MonoBehaviour
    {
        public event Action<Actor> Destroyed;

        private readonly Dictionary<Type, ActorBehaviour> actorComponents = new Dictionary<Type, ActorBehaviour>();
        
        public bool ContainActorComponent<T>() where T : ActorBehaviour
        {
            return GetActorComponent<T>();
        }

        public bool TryGetActorComponent<T>(out T component) where T : ActorBehaviour
        {
            if (!ContainActorComponent<T>())
            {
                component = default(T);
                return false;
            }

            component = GetActorComponent<T>();
            return true;
        }

        public bool TryGetActorComponent(Type type, out ActorBehaviour component)
        {
            if (!ContainActorComponent(type))
            {
                component = default(ActorBehaviour);
                return false;
            }
            component = GetActorComponent(type);
            return true;
        }

        public T GetActorComponent<T>() where T : ActorBehaviour
        {
            var type = typeof(T);

            if (!actorComponents.ContainsKey(type))
            {
                actorComponents.Add(type, GetComponent<T>());
            }

            return actorComponents[type] as T;
        }

        public bool ContainActorComponent(Type type)
        {
            return GetActorComponent(type);
        }

        public ActorBehaviour GetActorComponent(Type type)
        {
            Assert.IsTrue(type.IsSubclassOf(typeof(ActorBehaviour)), $"Try get ActorBehaviour of type [{type.Name}] which is not ActorBehaviour!");

            if (!actorComponents.ContainsKey(type))
            {
                actorComponents.Add(type, GetComponent(type) as ActorBehaviour);
            }

            return actorComponents[type];
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
