using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTD.Utilities;

namespace LTD.Lua
{
    public partial class LuaResponder
    {
        public delegate void Func();

        #region other
        protected virtual void HideDialog()
        {
            UI.DialogWindow.Instance.HideDialog();
        }
        protected virtual int ShowDialog2(string avatar, string text, string leftbutton, string rightbutton)
        {
            UI.DialogWindow.Instance.ShowDialog(avatar, text, leftbutton, rightbutton);
            return 1;
        }
        protected virtual int ShowDialog1(string avatar, string text, string continueButton)
        {
            UI.DialogWindow.Instance.ShowDialog(avatar, text, continueButton);
            return 1;
        }
        protected virtual int ShowDialog(string avatar, string text)
        {
            UI.DialogWindow.Instance.ShowDialog(avatar, text);
            return 1;
        }
        #endregion
        #region other
        protected virtual int SetStringVariable(string key, string value)
        {
            SavedVariables.Add(key, value);
            return 1;
        }
        protected virtual int SetNumberVariable(string key, double value)
        {
            SavedVariables.Add(key, value);
            return 1;
        }
        protected virtual string GetStringVariable(string key)
        {
            var value = GetVariable(key);
            if (!(value is string))
            {
                LogWarning($"variable named:[{key}] is not string");
            }
            return string.Empty;
        }
        protected virtual double GetNumberVariable(string key)
        {
            var value = GetVariable(key);
            if(!(value is double))
            {
                LogWarning($"variable named:[{key}] is not number");
            }
            return -1;
        }
        private object GetVariable(string key)
        {
            if (SavedVariables.ContainsKey(key))
            {
                return SavedVariables[key];
            }
            LogWarning($"cant find string variable named:[{key}]");
            return -1;
        }

        protected virtual float GetRandom(float min = 0, float max = 1)
        {
            return Random.Range(min, max);
        }

        protected virtual int LoadScriptFuncLua(string name, string function)
        {
            LoadScript(name);
            if (function != null && function.Length > 0)
            {
                CallFunction(function);
            }
            return 1;
        }
        protected virtual int LoadScriptCorLua(string name, string function)
        {
            LoadScript(name);
            if (function != null && function.Length > 0)
            {
                CallCoroutine(function);
            }
            return 1;
        }

        #endregion
    }
}