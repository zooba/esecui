using System;
using System.Collections.Generic;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace esecui
{
    public class PythonHost
    {
        public ScriptEngine Engine { get; private set; }
        public ScriptScope Scope { get; private set; }


        private ScriptSource ReprExpression;
        private ScriptSource DictExpression;
        private ScriptSource ImportSystemExpression;


        public PythonHost(string[] paths, bool debug = false)
        {
            var srs = Python.CreateRuntimeSetup(new Dictionary<string, object> { { "Debug", debug } });
            var runtime = new ScriptRuntime(srs);
            Engine = Python.GetEngine(runtime);
            
            if (paths != null)
            {
                var searchPaths = Engine.GetSearchPaths();
                foreach (var p in paths) searchPaths.Add(p);
                Engine.SetSearchPaths(searchPaths);
            }

            Scope = Engine.CreateScope();

            ReprExpression = Engine.CreateScriptSourceFromString("repr(__object)", SourceCodeKind.Expression);
            DictExpression = Engine.CreateScriptSourceFromString("dict(__contents)", SourceCodeKind.Expression);
            ImportSystemExpression = Engine.CreateScriptSourceFromString("from System import *");
        }

        public dynamic Import(string package)
        {
            return Engine.ImportModule(package);
        }

        public IDictionary<object, object> Dict()
        {
            return new IronPython.Runtime.PythonDictionary();
        }

        public IDictionary<object, object> Dict(string contents)
        {
            return (IDictionary<object, object>)Eval("dict(" + contents + ")");
        }

        public IDictionary<object, object> Dict(object contents)
        {
            dynamic scope = CreateScope();
            scope.__contents = contents;
            return DictExpression.Execute<IDictionary<object, object>>(scope);
        }

        public IDictionary<object, object> Dict(IDictionary<object, object> contents)
        {
            var result = Dict();
            foreach (var item in contents)
            {
                var asDict = item.Value as IDictionary<object, object>;
                if (asDict != null) result[item.Key] = Dict(asDict);
                else result[item.Key] = item.Value;
            }
            return result;
        }

        public IDictionary<object, object> Dict(object contents, ScriptScope scope)
        {
            scope.SetVariable("__contents", (object)contents);
            return DictExpression.Execute<IDictionary<object, object>>(scope);
        }

        public dynamic this[string name]
        {
            get { return Scope.GetVariable(name); }
            set { Scope.SetVariable(name, (object)value); }
        }

        public ScriptScope CreateScope()
        {
            var scope = Engine.CreateScope();
            ImportSystemExpression.Execute(scope);
            return scope;
        }

        public ScriptScope CreateScope(IDictionary<string, object> dictionary)
        {
            var scope = Engine.CreateScope(dictionary);
            ImportSystemExpression.Execute(scope);
            return scope;
        }

        public void Exec(string code)
        {
            Exec(code, Scope);
        }

        public void Exec(string code, ScriptScope scope)
        {
            Engine.Execute(code, scope);
        }

        public string Repr(object value)
        {
            var scope = CreateScope();
            scope.SetVariable("__object", (object)value);
            return ReprExpression.Execute<string>(scope);
        }

        public ScriptSource CompileExpression(string expression)
        {
            return Engine.CreateScriptSourceFromString(expression, Microsoft.Scripting.SourceCodeKind.Expression);
        }

        public dynamic Eval(string expression, ScriptScope scope = null)
        {
            return Engine.Execute(expression, scope ?? Scope);
        }

        public dynamic Eval(ScriptSource expression, ScriptScope scope = null)
        {
            return expression.Execute(scope ?? Scope);
        }
    }
}
