using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Diagnostics;
using Microsoft.Scripting.Hosting;
using System.Runtime.Remoting;

namespace esecui
{
    class DefaultStringScope : IDictionary<string, object>
    {
        private ScriptScope Globals;
        private Dictionary<string,object> InnerDict;
        
        public DefaultStringScope(ScriptScope globals)
        {
            Globals = globals;
            InnerDict = new Dictionary<string, object>();
        }

        public void Add(string key, object value)
        {
            InnerDict.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return true;
        }

        public ICollection<string> Keys
        {
            get { return InnerDict.Keys; }
        }

        public bool Remove(string key)
        {
            return InnerDict.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            if (key == "dict" || key == "list" || key == "tuple" || key == "int" || key == "float")
            {
                value = null;
                return false;
            }

            if (!InnerDict.TryGetValue(key, out value) &&
                !Globals.TryGetVariable(key, out value))
            {
                value = key;
            }
            return true;
        }

        public ICollection<object> Values
        {
            get { return InnerDict.Values; }
        }

        public object this[string key]
        {
            get
            {
                object value;
                if (!TryGetValue(key, out value)) throw new KeyNotFoundException();
                return value;
            }
            set
            {
                InnerDict[key] = value;
            }
        }

        public void Add(KeyValuePair<string, object> item)
        {
            InnerDict.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            InnerDict.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return InnerDict.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return InnerDict.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            if (!InnerDict.Contains(item)) return false;
            InnerDict.Remove(item.Key);
            return true;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return InnerDict.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)InnerDict).GetEnumerator();
        }
    }
}
