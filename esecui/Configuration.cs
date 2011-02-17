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
        public Configuration() { }

        public Configuration(byte[] sourceData)
            : this(new MemoryStream(sourceData))
        { }

        public Configuration(Stream source)
        {
            Read(source);
        }

        public string Name { get; set; }
        public string Source { get; set; }

        public string Definition { get; set; }
        public string Support { get; set; }
        public string SystemParameters { get; set; }

        public string Landscape { get; set; }
        public string LandscapeParameters { get; set; }
        public string CustomEvaluator { get; set; }

        public string PlotExpression { get; set; }
        public string BestIndividualExpression { get; set; }
        public const string DefaultPlotExpression = "(indiv[0], indiv[1])";
        public const string DefaultBestIndividualExpression = "indiv.phenome_string";


        public int? IterationLimit { get; set; }
        public int? EvaluationLimit { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public double? FitnessLimit { get; set; }

        private static string ToPythonCode(Dictionary<string, object> source, string indent)
        {
            var sb = new StringBuilder();
            foreach (var kv in source)
            {
                sb.Append(indent);
                string asStr;
                Dictionary<string,object> asDict;

                sb.Append("'");
                sb.Append(kv.Key);
                sb.Append("': ");

                if ((asStr = kv.Value as string) != null)
                {
                    sb.Append(asStr);
                }
                else if ((asDict = kv.Value as Dictionary<string, object>) != null)
                {
                    sb.AppendLine(" {");
                    sb.Append(ToPythonCode(asDict, indent + "    "));
                    sb.Append(indent);
                    sb.Append("}");
                }
                sb.AppendLine(",");
            }
            return sb.ToString();
        }

        private static string ToPythonCode(string source, string indent)
        {
            var dict = new Dictionary<string, object>();
            var reader = new StringReader(source);
            for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                int i1 = line.IndexOf('='), i2 = line.IndexOf(':');
                if (i2 >= 0 && i2 < i1) i1 = i2;
                if (i1 < 0)
                {
                    dict[line] = "True";
                }
                else
                {
                    var name = line.Substring(0, i1);
                    var value = line.Substring(i1 + 1);
                    var subdict = dict;
                    var parts = name.Split('.');
                    if (parts.Length > 1)
                    {
                        foreach (var part in parts.Take(parts.Length - 1))
                        {
                            if (!subdict.ContainsKey(part)) subdict[part] = new Dictionary<string, string>();
                        }
                    }
                    subdict[parts[parts.Length - 1]] = value;
                }
            }


            return ToPythonCode(dict, indent);
        }


        public void WritePython(Stream destination)
        {
            using (var s = new StreamWriter(destination))
            {
                int i = Landscape.LastIndexOf('.');
                if (i > 0) { s.WriteLine("import " + Landscape.Substring(0, i)); s.WriteLine(); }

                s.WriteLine(Support);
                s.Write(@"name = '" + Name + @"'

config = {
    'landscape': {
        'class': " + Landscape + @",
" + ToPythonCode(LandscapeParameters, "        ") + @"
    },
    'system': {
        'definition': r'''" + Definition + @",
        '''
" + ToPythonCode(SystemParameters, "        ") + @"
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
            var landscape_parameters = landscape.ToDictionary(kv => (object)kv.Key.ToString(), kv => kv.Value);
            dynamic landscape_class = landscape_parameters["class"];
            Landscape = landscape_class.__module__ + "." + landscape_class.__name__;
            landscape_parameters.Remove("class");
            LandscapeParameters = landscape_parameters.ToText(python);

            var system = (IDictionary<object, object>)config["system"];
            var system_parameters = system.ToDictionary(kv => (object)kv.Key.ToString(), kv => kv.Value);
            Definition = system_parameters["definition"] as string;
            if (Definition != null) Definition = Definition.Trim().Trim('\r', '\n');
            system_parameters.Remove("definition");
            SystemParameters = system_parameters.ToText(python);

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
                    new XElement("support", Support),
                    new XElement("parameters", SystemParameters)),
                new XElement("landscape",
                    new XElement("class", Landscape),
                    new XElement("parameters", LandscapeParameters),
                    new XElement("evaluator", CustomEvaluator)),
                new XElement("monitor",
                    new XElement("limits",
                        IterationLimit.HasValue ? new XAttribute("iterations", IterationLimit.Value) : null,
                        EvaluationLimit.HasValue ? new XAttribute("evaluations", EvaluationLimit.Value) : null,
                        TimeLimit.HasValue ? new XAttribute("seconds", TimeLimit.Value.TotalSeconds) : null,
                        FitnessLimit.HasValue ? new XAttribute("fitness", FitnessLimit.Value) : null)),
                new XElement("display",
                    new XElement("bestindiv", BestIndividualExpression))
                );
            xml.WriteTo(destination);
        }

        public void Read(Stream source)
        {
            var xml = XElement.Load(source) as XElement;
            if (xml == null) return;

            Name = (string)xml.Attribute("name");

            SystemParameters = xml.Element("system").Element("parameters").Value;
            Definition = (string)xml.Element("system").Element("definition");
            if (Definition != null) Definition = Definition.Trim().Trim('\n', '\r');
            Support = (string)xml.Element("system").Element("support");
            if (Support != null) Support = Support.Trim().Trim('\n', '\r');

            LandscapeParameters = xml.Element("landscape").Element("parameters").Value;
            Landscape = (string)xml.Element("landscape").Element("class");
            CustomEvaluator = (string)xml.Element("landscape").Element("evaluator") ?? "";


            var displayElement = xml.Element("display");
            if (displayElement != null)
            {
                PlotExpression = (string)displayElement.Element("plot") ?? DefaultPlotExpression;
                BestIndividualExpression = (string)displayElement.Element("bestindiv") ?? DefaultBestIndividualExpression;
            }
            else
            {
                PlotExpression = DefaultPlotExpression;
                BestIndividualExpression = DefaultBestIndividualExpression;
            }

            var limits = xml.Element("monitor").Element("limits");
            IterationLimit = (int?)limits.Attribute("iterations");
            EvaluationLimit = (int?)limits.Attribute("evaluations");
            double? temp = (double?)limits.Attribute("seconds");
            TimeLimit = temp.HasValue ? (TimeSpan?)TimeSpan.FromSeconds(temp.Value) : null;
            FitnessLimit = (double?)limits.Attribute("fitness");
        }

        public void Read(string source)
        {
            using (var file = File.Open(source, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Read(file);
            }
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
            if (Source == null) return other.Source == null;
            return Source.Equals(other.Source, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return (Source ?? Name ?? "").GetHashCode();
        }
    }
}
