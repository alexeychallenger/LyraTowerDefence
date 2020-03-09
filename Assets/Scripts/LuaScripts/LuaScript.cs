using UnityEngine;
using System.Collections;
using MoonSharp.Interpreter;

namespace LTD.Lua
{
    public class LuaScript
    {
        public static LuaScript CreateFunction(string name, string text)
        {
            LuaScript lua = new LuaScript();
            lua.Script.Options.DebugPrint = o => Debug.Log($"{UnityEngine.Time.frameCount} <color=blue>{name}</color> {o}");
            lua.Script.DoString(text);
            lua.Name = name;

            return lua;
        }

        private Script _script;
        public Script Script { get { return _script; } }

        private DynValue _coroutine;
        public DynValue Coroutine { get { return _coroutine; } }

        public string Name { get; private set; }

        public LuaScript()
        {
            _script = new Script();

        }

        private DynValue CallFunctionInternal(string function, params object[] args)
        {
            return Script.Call(Script.Globals[function], args);
        }

        public void CallFunction(string function, params object[] args)
        {
            CallFunctionInternal(function, args);
        }

        public T CallFunction<T>(string function, params object[] args)
        {
            return CallFunctionInternal(function, args).ToObject<T>();
        }

        public double CallFunctionNumber(string function, params object[] args)
        {
            return CallFunctionInternal(function, args).Number;
        }

        public string CallFunctionString(string function, params object[] args)
        {
            return CallFunctionInternal(function, args).String;
        }

        public DynValue CallCoroutine(string function, params object[] args)
        {
            _coroutine = Script.CreateCoroutine(Script.Globals.Get(function));
            return _coroutine;
        }

        public IEnumerator Waiting(double time)
        {
            yield return new WaitForSeconds((float)time);
        }
    }
}
