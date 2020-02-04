using System;
using UnityEngine;

namespace LTD.DebugSystem
{
    [Serializable]
    public struct DebugKey
    {
        public KeyPair mainKey;
        public KeyPair secondKey;

        public DebugKey(KeyCode mainKey) : this()
        {
            this.mainKey = new KeyPair(mainKey);
        }

        public DebugKey(KeyCode mainKey, KeyTrigger mainKeyTrigger) : this()
        {
            this.mainKey = new KeyPair(mainKey, mainKeyTrigger);
        }

        public DebugKey(KeyCode mainKey, KeyCode secondKey) : this()
        {
            this.mainKey = new KeyPair(mainKey, KeyTrigger.OnHold);
            this.secondKey = new KeyPair(secondKey);
        }

        public DebugKey(KeyCode mainKey, KeyTrigger mainKeyTrigger, KeyCode secondKey) : this()
        {
            this.mainKey = new KeyPair(mainKey, mainKeyTrigger);
            this.secondKey = new KeyPair(secondKey);
        }

        public DebugKey(KeyCode mainKey, KeyTrigger mainKeyTrigger, KeyCode secondKey, KeyTrigger secondKeyTrigger) : this()
        {
            this.mainKey = new KeyPair(mainKey, mainKeyTrigger);
            this.secondKey = new KeyPair(secondKey, secondKeyTrigger);
        }

        public bool IsPressed
        {
            get
            {
                if (GameManager.DebugManager.ignoreKeypadDebugKeys)
                {
                    return false;
                }
                if (secondKey.key == KeyCode.None)
                {
                    return mainKey.IsPressed;
                }
                return mainKey.IsPressed && secondKey.IsPressed;
            }
        }

        public string PrintCombination => secondKey.key == KeyCode.None
            ? $"[{mainKey.key}]"
            : $"[{mainKey.key} + {secondKey.key}]";
    }
}