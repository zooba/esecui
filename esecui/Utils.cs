using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.TextEditor;
using Microsoft.Scripting.Hosting;


namespace esecui
{
    public static class Utils
    {
        public static IDictionary<object, object> FromSyntaxDefaults(this PythonHost python, object landscape)
        {
            dynamic esec = python.Import("esec");

            var syntax = esec.utils.merge_cls_dicts(landscape, "syntax");
            var defaults = esec.utils.merge_cls_dicts(landscape, "default");

            return python.FromSyntaxDefaults(syntax as IDictionary<object, object>, defaults as IDictionary<object, object>);
        }

        private static readonly IList<string> OmitKeys = new List<string> { "instance", "class", "lower_bounds", "upper_bounds" };

        private static IDictionary<object, object> FromSyntaxDefaults(this PythonHost python,
            IDictionary<object, object> syntax, IDictionary<object, object> defaults)
        {
            var settings = new Dictionary<object, object>();
            if (syntax == null || defaults == null) return settings;

            foreach (var item in (IDictionary<object, object>)syntax)
            {
                string key = (string)item.Key;
                if (key.EndsWith("?")) key = key.Substring(0, key.Length - 1);
                if (OmitKeys.Contains(key))
                { }
                else if (defaults.ContainsKey(item.Key))
                {
                    var value = defaults[key];
                    if (value is IDictionary<object, object>)
                    {
                        settings[key] = python.FromSyntaxDefaults((IDictionary<object, object>)item.Value, (IDictionary<object, object>)value);
                    }
                    else
                    {
                        settings[key] = value;
                    }
                }
                else
                {
                    settings[key] = "(default)";
                }
            }
            return settings;
        }


        private static IEnumerable<string> DictionaryToLines(IDictionary<object, object> dictionary, PythonHost python)
        {
            foreach (var item in dictionary.OrderBy(item => item.Key))
            {
                var asDict = item.Value as IDictionary<object, object>;
                var asList = item.Value as IList<object>;
                var asStr = item.Value as string;

                if (asDict != null)
                {
                    foreach (var line in DictionaryToLines(asDict, python))
                    {
                        yield return item.Key.ToString() + "." + line;
                    }
                }
                else if (asList != null)
                {
                    var sb = new StringBuilder();
                    sb.Append(item.Key.ToString());
                    sb.Append(": [");
                    foreach (var element in asList)
                    {
                        sb.Append(python.Repr(element));
                        sb.Append(", ");
                    }
                    sb.Length -= 2;
                    sb.Append(" ]");
                    yield return sb.ToString();
                }
                else if (asStr == "(default)")
                {
                    yield return "# " + item.Key.ToString() + ": (default)";
                }
                else if (asStr != null)
                {
                    yield return item.Key.ToString() + ": " + python.Repr(item.Value);
                }
                else if (item.Value == null)
                {
                    yield return item.Key.ToString() + ": None";
                }
                else
                {
                    yield return item.Key.ToString() + ": " + item.Value.ToString();
                }
            }
        }

        public static string ToText(this IDictionary<object, object> dictionary, PythonHost python)
        {
            var sb = new StringBuilder();

            foreach (var line in DictionaryToLines(dictionary, python))
            {
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        public static IDictionary<object, object> ConfigDict(this PythonHost python, TextEditorControl source)
        {
            IList<ErrorItem> errors = null;
            return ConfigDict(python, source, out errors);
        }

        public static void Set(this IDictionary<object, object> dict, string key, dynamic value, ScriptScope scope,
            Func<IDictionary<object,object>> newNested)
        {
            dynamic dest = dict;
            var parts = key.Split('.');
            if (!scope.ContainsVariable(parts[0]))
            {
                dest[parts[0]] = newNested();
                scope.SetVariable(parts[0], dest[parts[0]]);
            }

            foreach (var part in parts.Take(parts.Length - 1))
            {
                dynamic newDest = null;
                if (!dest.TryGetValue(part, out newDest))
                {
                    newDest = newNested();
                    dest[part] = newDest;
                }
                dest = newDest;
            }
            dest[parts[parts.Length - 1]] = value;
        }

        public static IDictionary<object, object> ConfigDict(this PythonHost python, TextEditorControl source, out IList<ErrorItem> errors)
        {
            var result = python.Dict();
            errors = new List<ErrorItem>();
            var scope = python.CreateScope();
            int lineNumber = 0;

            using (var sr = new StringReader(source.Text))
            {
                string fullExpr = "";
                for (var line = sr.ReadLine(); line != null; line = sr.ReadLine(), lineNumber += 1)
                {
                    int i = line.IndexOf('#');
                    if (i >= 0) line = line.Substring(0, i);
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    i = line.IndexOf(':');
                    if (i < 0) i = line.IndexOf('=');
                    if (i < 0) continue;

                    var name = line.Substring(0, i).Trim();
                    fullExpr += line.Substring(i + 1);
                    var expr = fullExpr.Trim();

                    object value = null;
                    bool succeeded = false;
                    try
                    {
                        value = python.Eval(expr, scope);
                        fullExpr = "";
                        succeeded = true;
                    }
                    catch (Microsoft.Scripting.SyntaxErrorException)
                    {
                        fullExpr += "\n";
                    }
                    catch (Exception ex)
                    {
                        i += (fullExpr.Length - expr.Length);
                        errors.Add(new ErrorItem(source, lineNumber, i + 1, lineNumber, line.Length,
                            ex.Message, ex.GetType().Name, false));
                    }


                    if (succeeded)
                    {
                        result.Set(name, value, scope, python.Dict);
                    }
                }
            }
            return result;
        }


    }
}
