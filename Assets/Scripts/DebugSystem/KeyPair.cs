using System;
using UnityEngine;

namespace LTD.DebugSystem
{
    [Serializable]
    public struct KeyPair
    {
        public KeyCode key;
        public KeyTrigger keyTrigger;

        public KeyPair(KeyCode key) : this()
        {
            this.key = key;
        }

        public KeyPair(KeyCode key, KeyTrigger keyTrigger)
        {
            this.key = key;
            this.keyTrigger = keyTrigger;
        }

        public bool IsPressed
        {
            get
            {
                switch (keyTrigger)
                {
                    case KeyTrigger.OnDown:
                        return Input.GetKeyDown(key);
                    case KeyTrigger.OnHold:
                        return Input.GetKey(key);
                    case KeyTrigger.OnUp:
                        return Input.GetKeyUp(key);
                }
                return false;
            }
        }
    }
}