using System;
using System.Collections.Generic;
using IronPython.Runtime;

namespace esecui
{
    static class EsecOverrides
    {
        public static void AddOverrides(dynamic esec)
        {
            esec.species._pairs = new Func<object, IEnumerable<object>>(_Pairs);
        }
        
        public static IEnumerable<object> _Pairs(object source)
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

    }
}
