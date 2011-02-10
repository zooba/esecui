using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;

namespace esecui
{
    public class PythonHost
    {
        public ScriptEngine Engine { get; private set; }
        public ScriptScope Scope { get; private set; }


        public PythonHost(string path)
        {
            Engine = Python.CreateEngine();

            if (path != null)
            {
                var searchPaths = Engine.GetSearchPaths();
                searchPaths.Add(path);
                Engine.SetSearchPaths(searchPaths);
            }

            Scope = Engine.CreateScope();

            Engine.Execute("import clr", Scope);
            Engine.Execute("clr.AddReference('System')", Scope);
            Engine.Execute("from System import *", Scope);
        }

        public dynamic Import(string package)
        {
            return Engine.ImportModule(package);
        }

        public IDictionary<object, object> Dict()
        {
            return (IDictionary<object, object>)Eval("dict()");
        }

        public IDictionary<object, object> Dict(string contents)
        {
            return (IDictionary<object, object>)Eval("dict(" + contents + ")");
        }

        public IDictionary<object, object> Dict(object contents)
        {
            dynamic scope = CreateScope();
            scope.contents = contents;
            return (IDictionary<object, object>)Eval("dict(contents)", scope);
        }

        public IDictionary<object, object> Dict(IDictionary<object,object> contents)
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
            scope.SetVariable("__contents", contents);
            return (IDictionary<object, object>)Eval("dict(__contents)", scope);
        }

        public dynamic this[string name]
        {
            get { return Scope.GetVariable(name); }
            set { Scope.SetVariable(name, (object)value); }
        }

        public ScriptScope CreateScope()
        {
            var scope = Engine.CreateScope();
            Engine.Execute("import clr", scope);
            Engine.Execute("clr.AddReference('System')", scope);
            Engine.Execute("from System import *", scope);
            return scope;
        }

        public ScriptScope CreateScope(IDictionary<string, object> dictionary)
        {
            var scope = Engine.CreateScope(dictionary);
            Engine.Execute("import clr", scope);
            Engine.Execute("clr.AddReference('System')", scope);
            Engine.Execute("from System import *", scope);
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
            scope.SetVariable("__object", value);
            return Engine.Execute<string>("repr(__object)", scope);
        }

        public dynamic Eval(string expression)
        {
            return Eval(expression, Scope);
        }

        public dynamic Eval(string expression, ScriptScope scope)
        {
            return Engine.Execute(expression, scope);
        }
    }
}
