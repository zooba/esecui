using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace esecui
{
    public class Configuration : IComparable
    {
        public string Name { get; set; }
        public string Source { get; set; }

        public string Definition { get; set; }
        public IDictionary<object, object> SystemParameters { get; set; }

        public string Landscape { get; set; }
        public IDictionary<object, object> LandscapeParameters { get; set; }

        public int? IterationLimit { get; set; }
        public int? EvaluationLimit { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public double? FitnessLimit { get; set; }


        private static string ToLines(IDictionary<object, object> source, string indent)
        {
            var sb = new StringBuilder();
            foreach (var kv in source)
            {
                sb.Append(indent);
                sb.Append("'");
                sb.Append(kv.Key.ToString());
                sb.Append("': ");
                var asStr = kv.Value as string;
                if (asStr != null)
                {
                    sb.Append("'");
                    sb.Append(asStr);
                    sb.Append("'");
                }
                else
                {
                    sb.Append(kv.Value);
                }
                sb.AppendLine(",");
            }

            return sb.ToString();
        }


        public void WritePython(Stream destination)
        {
            using (var s = new StreamWriter(destination))
            {
                int i = Landscape.LastIndexOf('.');
                if (i > 0) { s.WriteLine("import " + Landscape.Substring(0, i)); s.WriteLine(); }

                s.Write(@"name = '" + Name + @"'

config = {
    'landscape': {
        'class': " + Landscape + @",
" + ToLines(LandscapeParameters, "        ") + @"
    },
    'system': {
        'definition': r'''" + Definition + @",
        '''
" + ToLines(SystemParameters, "        ") + @"
    },
    'monitor': {
        'limits': {
            " + (IterationLimit.HasValue ? "" : "# ") + "'iterations': " + IterationLimit.Value.ToString() + @",
            " + (EvaluationLimit.HasValue ? "" : "# ") + "'evaluations': " + EvaluationLimit.Value.ToString() + @",
            " + (TimeLimit.HasValue ? "" : "# ") + "'seconds': " + TimeLimit.Value.TotalSeconds.ToString() + @",
            " + (FitnessLimit.HasValue ? "" : "# ") + "'fitness': " + FitnessLimit.Value.ToString() + @",
        },
    },
}
");

            }
        }

        public void ReadPython(Stream source, PythonHost python)
        {
            string code = null;

            using (var r = new StreamReader(source))
            {
                code = r.ReadToEnd();
            }

            var scope = python.CreateScope();
            python.Exec(code, scope);
            string name;
            if (scope.TryGetVariable<string>("name", out name)) Name = name;
            else Name = null;

            dynamic config = scope.GetVariable("config");

            var landscape = (IDictionary<object, object>)config["landscape"];
            LandscapeParameters = landscape.ToDictionary(kv => (object)kv.Key.ToString(), kv => kv.Value);
            dynamic landscape_class = LandscapeParameters["class"];
            Landscape = landscape_class.__module__ + "." + landscape_class.__name__;
            LandscapeParameters.Remove("class");

            var system = (IDictionary<object, object>)config["system"];
            SystemParameters = system.ToDictionary(kv => (object)kv.Key.ToString(), kv => kv.Value);
            Definition = SystemParameters["definition"] as string;
            if (Definition != null) Definition = Definition.Trim().Trim('\r', '\n');
            SystemParameters.Remove("definition");

            var limits = (IDictionary<object, object>)config["monitor"]["limits"];
            object temp;
            IterationLimit = limits.TryGetValue("iterations", out temp) ? (int?)temp : null;
            EvaluationLimit = limits.TryGetValue("evaluations", out temp) ? (int?)temp : null;
            TimeLimit = limits.TryGetValue("seconds", out temp) ? (TimeSpan?)TimeSpan.FromSeconds((double)temp) : null;
            if (limits.TryGetValue("fitness", out temp)) FitnessLimit = temp as double?;
        }

        public void Write(PythonHost python)
        {
            using (var writer = XmlWriter.Create(Source))
            {
                Write(writer, python);
            }
        }

        public void Write(XmlWriter destination, PythonHost python)
        {
            var xml = new XElement("configuration",
                new XAttribute("name", Name),
                new XElement("system",
                    new XElement("definition", Definition),
                    SystemParameters.Select(i => new XElement(i.Key.ToString(), python.Repr(i.Value)))),
                new XElement("landscape",
                    new XElement("class", Landscape),
                    LandscapeParameters.Select(i => new XElement(i.Key.ToString(), python.Repr(i.Value)))),
                new XElement("monitor",
                    new XElement("limits",
                        IterationLimit.HasValue ? new XAttribute("iterations", IterationLimit.Value) : null,
                        EvaluationLimit.HasValue ? new XAttribute("evaluations", EvaluationLimit.Value) : null,
                        TimeLimit.HasValue ? new XAttribute("seconds", TimeLimit.Value.TotalSeconds) : null,
                        FitnessLimit.HasValue ? new XAttribute("fitness", FitnessLimit.Value) : null)));
            xml.WriteTo(destination);
        }

        public void Read(string source, PythonHost python)
        {
            var xml = XElement.Load(source) as XElement;
            if (xml == null) return;

            Name = (string)xml.Attribute("name");

            SystemParameters = xml.Element("system").Elements()
                .Where(e => e.Name.LocalName != "definition")
                .ToDictionary(e => (object)e.Name.LocalName, e => (object)python.Eval(e.Value));
            Definition = (string)xml.Element("system").Element("definition");
            if (Definition != null) Definition = Definition.Trim().Trim('\n', '\r');

            LandscapeParameters = xml.Element("landscape").Elements()
                .Where(e => e.Name.LocalName != "class")
                .ToDictionary(e => (object)e.Name.LocalName, e => (object)python.Eval(e.Value));
            Landscape = (string)xml.Element("landscape").Element("class");

            var limits = xml.Element("monitor").Element("limits");
            IterationLimit = (int?)limits.Attribute("iterations");
            EvaluationLimit = (int?)limits.Attribute("evaluations");
            double? temp = (double?)limits.Attribute("seconds");
            TimeLimit = temp.HasValue ? (TimeSpan?)TimeSpan.FromSeconds(temp.Value) : null;
            FitnessLimit = (double?)limits.Attribute("fitness");
        }

        public int CompareTo(object obj)
        {
            var other = obj as Configuration;
            if (other == null) return 1;
            return Source.CompareTo(other.Source);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Configuration;
            if (other == null) return base.Equals(obj);
            return Source.Equals(other.Source, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode();
        }
    }
}
