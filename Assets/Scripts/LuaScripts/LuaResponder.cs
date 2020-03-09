using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using LTD.Utilities;

namespace LTD.Lua
{
    public partial class LuaResponder
    {
        protected LuaScript lua;
        protected string ScriptName { get; private set; }

        protected static Dictionary<string, object> SavedVariables = new Dictionary<string, object>();

        public void LoadScript(string name)
        {
            ScriptName = name;
            string url = string.Concat("lua/", "level", "/", name);
            Debug.Log($"Load lua script: {url}");

            lua = LuaScript.CreateFunction(name, FilesUtil.LoadScript(url));

            PrepareFunctions(lua);
        }


        protected void CallFunction(string name)
        {
            if (lua == null) return;
            lua.CallFunction(name);
        }

        protected T CallFunction<T>(string name)
        {
            return lua.CallFunction<T>(name);
        }

        protected double CallFunctionNumber(string name, params object[] args)
        {
            if (lua == null) return 0;
            return lua.CallFunctionNumber(name, args);
        }

        protected string CallFunctionString(string name, params object[] args)
        {
            if (lua == null) return string.Empty;
            return lua.CallFunctionString(name, args);
        }

        public void CallCoroutine(string name, params object[] args)
        {
            if (lua == null) return;
            Coroutines.Start(LuaCoroutine(lua.CallCoroutine(name, args)));
        }

        private IEnumerator LuaCoroutine(DynValue coroutine)
        {
            do
            {
                DynValue x = coroutine.Coroutine.Resume();
                if (x.IsNil() || x.IsVoid()) yield break;
                yield return YieldCustom(x);
            }
            while (true);
        }

        private IEnumerator YieldCustom(DynValue dynvalue)
        {
            var obj = dynvalue.ToObject();
            var tuple = dynvalue.Tuple;

            if (tuple != null)
            {
                return YieldTupleCustom(tuple);
            }
            else if (obj is double num)
            {
                return WaitingFrames((int)num);
            }
            else if (obj is string keyname)
            {
                return YieldStringCustom(keyname);
            }
            return null;
        }
        private IEnumerator YieldStringCustom(string key)
        {
            switch (key)
            {
                case "dialog":
                    {
                        return new WaitUntil(() => LTD.UI.DialogWindow.AnswerRecieved);
                    }
            }
            return null;
        }
        private IEnumerator YieldTupleCustom(DynValue[] tuple)
        {
            var eventType = tuple[0].String;
            switch (eventType)
            {
                case "monster":
                    {
                        break;
                    }
            }
            return null;
        }

        private IEnumerator WaitingFrames(int frames)
        {
            int counter = 0;
            do
            {
                if (frames <= counter)
                {
                    yield break;
                }
                yield return null;
                counter++;
            } while (true);
        }

        private void PrepareFunctions(LuaScript lua)
        {
            try
            {
                //lua.Script.Globals["iEquip"] = (Func<ItemType, int>)ItemEquip;
                //lua.Script.Globals["iCreateShooter"] = (Func<string, int>)ItemCreateShooter;
                //lua.Script.Globals["iMoveShooter"] = (Func<string, float, float, float, int>)ItemMoveShooter;
                //lua.Script.Globals["iRotateShooter"] = (Func<string, float, float, int>)ItemRotateShooter;
                //lua.Script.Globals["iReplaceAbility"] = (Func<string, int>)ItemReplaceAbility;

                //lua.Script.Globals["lCreateObject"] = (Func<string, int>)LevelCreateObject;
                //lua.Script.Globals["lDestroyObject"] = (Func<string, int>)LevelDestroyObject;
                //lua.Script.Globals["lMoveElement"] = (Func<string, string, float, int>)LevelMoveElement;
                //lua.Script.Globals["lRotateElement"] = (Func<string, string, float, int>)LevelRotateElement;
                //lua.Script.Globals["lMoveMap"] = (Func<float, int>)LevelMoveMap;
                //lua.Script.Globals["lShowElement"] = (Func<string, float, int>)LevelShowElement;
                //lua.Script.Globals["lHideElement"] = (Func<string, float, int>)LevelHideElement;
                //lua.Script.Globals["lShakePositionElement"] = (Func<string, float, float, int, int>)LevelShakePositionElement;
                //lua.Script.Globals["lShakeRotationElement"] = (Func<string, float, float, int, int>)LevelShakeRotationElement;
                //lua.Script.Globals["lSetText"] = (Func<string, string, int>)LevelSetText;

                lua.Script.Globals["ShowDialog2"] = (Func<string, string, string, string, int>)ShowDialog2;
                lua.Script.Globals["ShowDialog1"] = (Func<string, string, string, int>)ShowDialog1;
                lua.Script.Globals["ShowDialog"] = (Func<string, string, int>)ShowDialog;
                lua.Script.Globals["HideDialog"] = (Func)HideDialog;

                lua.Script.Globals["SetStringVariable"] = (Func<string, string, int>)SetStringVariable;
                lua.Script.Globals["SetNumberVariable"] = (Func<string, double, int>)SetNumberVariable;
                lua.Script.Globals["GetStringVariable"] = (Func<string, string>)GetStringVariable;
                lua.Script.Globals["GetNumberVariable"] = (Func<string, double>)GetNumberVariable;
                lua.Script.Globals["LoadScriptFunc"] = (Func<string, string, int>)LoadScriptFuncLua;
                lua.Script.Globals["LoadScriptCor"] = (Func<string, string, int>)LoadScriptCorLua;

                lua.Script.Globals["GetRandom"] = (Func<float, float, float>)GetRandom;

            }
            catch (Exception ex)
            {
                Debug.LogError("can't prepare functions in " + lua.Name + "" + ex.Message);
            }
        }

        protected void LogWarning(string message)
        {
            Debug.LogFormat("{0} [<color=yellow>LuaWarning</color>]:{1} {2}", Time.frameCount, ScriptName, message);
        }

        protected void LogError(string message)
        {
            Debug.LogFormat("{0} [<color=red>LuaError</color>]:{1} {2}", Time.frameCount, ScriptName, message);
        }

        public static void Initialize()
        {
            //UserData.RegisterAssembly();
            //UserData.RegisterType<ItemType>();
        }

        public static void SaveVariable(string key, object value)
        {
            if (SavedVariables.ContainsKey(key))
            {
                SavedVariables.Remove(key);
            }
            SavedVariables.Add("answer", "left");
        }
        public static object LoadVariable(string key)
        {
            if (SavedVariables.ContainsKey(key))
            {
                return SavedVariables[key];
            }
            Debug.LogError($"cant find variable named {key}");
            return 0;
        }
    }
}
