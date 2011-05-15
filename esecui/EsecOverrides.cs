using System;
using System.Collections.Generic;
using System.Linq;
using IronPython.Runtime;

namespace esecui
{
    static class EsecOverrides
    {
        public static void AddOverrides(dynamic esec)
        {
            esec.utils.pairs = new Func<object, IEnumerable<object>>(Pairs);
            esec.utils.overlapped_pairs = new Func<object, IEnumerable<object>>(OverlappedPairs);
            esec.system.ESDLCompilerError = new Func<dynamic, string, object>(ESDLCompilerError);
        }
        
        public static IEnumerable<object> Pairs(object source)
        {
            object[] objs = new object[2];
            System.Collections.IEnumerator e;
            if (source is System.Collections.IEnumerator) e = (System.Collections.IEnumerator)source;
            else e = ((System.Collections.IEnumerable)source).GetEnumerator();
            while (true)
            {
                if (!e.MoveNext()) break;
                objs[0] = e.Current;
                if (!e.MoveNext()) break;
                objs[1] = e.Current;
                yield return new PythonTuple(objs);
            }
        }

        public static IEnumerable<object> OverlappedPairs(object source)
        {
            object[] objs = new object[2];
            System.Collections.IEnumerator e;
            if (source is System.Collections.IEnumerator) e = (System.Collections.IEnumerator)source;
            else e = ((System.Collections.IEnumerable)source).GetEnumerator();
            if (!e.MoveNext()) yield break;
            objs[0] = e.Current;
            while (true)
            {
                if (!e.MoveNext()) break;
                objs[1] = e.Current;
                yield return new PythonTuple(objs);
                objs[0] = objs[1];
            }
        }

        public static ESDLCompilationException ESDLCompilerError(dynamic validationResult, string message)
        {
            return new ESDLCompilationException(message, validationResult);
        }

    }
}
