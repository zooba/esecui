using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace esecui
{
    public partial class Export : Form
    {
        Configuration Configuration;

        #region Initialisation and Display

        public static void Show(IWin32Window owner,
            Configuration config)
        {
            using (var export = new Export(config))
            {
                export.ShowDialog(owner);
            }
        }

        public Export(Configuration config)
            : this()
        {
            Configuration = config;

            FillParameterList();
        }

        public Export()
        {
            Configuration = null;
            InitializeComponent();

            panelEdit.Dock = DockStyle.Fill;
            panelEdit.Visible = true;

            panelPreview.Dock = DockStyle.Fill;
            panelPreview.Visible = false;

            var settings = Properties.Settings.Default;

            // Initialise the UI font
            var uiFont = new Font("Segoe UI", settings.UIFontSize);
            if (uiFont.Name != uiFont.OriginalFontName)
            {
                uiFont = new Font("Tahoma", settings.UIFontSize);
            }
            if (uiFont.Name != uiFont.OriginalFontName)
            {
                uiFont = new Font(FontFamily.GenericSansSerif, settings.UIFontSize);
            }
            Font = uiFont;

            // Initialise the code font
            var codeFont = new Font("Consolas", settings.CodeFontSize);
            if (codeFont.Name != codeFont.OriginalFontName)
            {
                codeFont = new Font(FontFamily.GenericMonospace, settings.CodeFontSize);
            }
            txtPreview.Font = codeFont;

            txtPreview.SetHighlighting("Python");
            txtPreview.Document.ReadOnly = true;

            txtCountExact.Tag = 0;
        }

        #endregion

        #region Parameter List

        private void FillParameterList()
        {
            lstParameters.Items.Clear();
            while (lstParameters.Columns.Count > 1) lstParameters.Columns.RemoveAt(1);

            var lvi = new ListViewItem();
            lvi.Name = "1";
            lvi.Text = "1";

            foreach (var kv in ReadVariables(Configuration.LandscapeParameters))
            {
                lstParameters.Columns.Add("landscape." + kv.Key, kv.Key + " (Landscape)");
                lvi.SubItems.Add(kv.Value);
            }
            foreach (var kv in ReadVariables(Configuration.SystemParameters))
            {
                lstParameters.Columns.Add("system." + kv.Key, kv.Key + " (System)");
                lvi.SubItems.Add(kv.Value);
            }

            lstParameters.Items.Add(lvi);
            lstParameters.Items.Add("*");
        }

        private Dictionary<string, List<string>> lstParameters_GetParameters()
        {
            var parameterCount = lstParameters.Columns.Count - 1;
            var parameterNames = new string[parameterCount];
            var parameters = new List<string>[parameterCount];

            for (int i = 0; i < parameterCount; ++i)
            {
                parameterNames[i] = lstParameters.Columns[i + 1].Name;
                parameters[i] = new List<string>();
            }
            foreach (var item in lstParameters.Items.OfType<ListViewItem>())
            {
                if (item.Text == "*") continue;

                for (int i = 0; i < parameterCount; ++i)
                {
                    parameters[i].Add(item.SubItems[i + 1].Text);
                }
            }

            var result = new Dictionary<string, List<string>>();
            for (int i = 0; i < parameterCount; ++i)
            {
                result[parameterNames[i]] = parameters[i];
            }
            return result;
        }

        #endregion

        #region Save/Close

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Python File (*.py)|*.py|All Files (*.*)|*.*";
                sfd.AutoUpgradeEnabled = true;
                sfd.AddExtension = true;
                sfd.DefaultExt = ".py";
                sfd.CreatePrompt = false;
                sfd.RestoreDirectory = false;
                sfd.OverwritePrompt = true;
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                UpdatePreview();
                using (var dest = new System.IO.StreamWriter(sfd.OpenFile(), Encoding.ASCII))
                {
                    dest.Write(txtPreview.Text);
                }
            }
        }

        private void Export_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnClose.PerformClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Template Parts

        const string TemplateHeader = @"#
# {0}
#
# Generated by {1} {2} at {3}
#

";

        const string TemplateImports = @"import itertools
import esec

";

        const string TemplateDefinition = @"DEFINITION = r'''{0}'''

";

        const string TemplateCustomLandscape = @"@esec.esdl_eval
def {0}(indiv):
";

        const string TemplateConfiguration = @"config = {{
    'landscape': {{
        'class': {0},
{1}    }},
    'system': {{
        'definition': DEFINITION,
{2}    }},
    'monitor': {{
        'report': 'iter+evals+|+best_fit+|+local_max+local_ave+local_min+|+time+time_delta',
        'summary': 'status+|+best+best_genome+best_phenome',
        'exception_summary': 'status+iter+evals+time',
        'limits': {{
            'iterations': {3},
            'evaluations': {4},
            'fitness': {5},
        }},
    }},
}}

";

        const string TemplateSimpleBatch = @"settings = 'csv={0};quiet={1};'

def batch():
    for i in {2}({3}):
        yield {{
            'config': config,
            'tags': [],
            'names': None,
            'settings': {4},
            'format': None,
        }}
";

        #endregion

        #region Text Helpers

        private string IndentBlock(string source, string indent)
        {
            var sb = new StringBuilder();
            IndentBlock(source, indent, sb);
            return sb.ToString();
        }

        private void IndentBlock(string source, string indent, StringBuilder destination)
        {
            using (var reader = new StringReader(source))
            {
                for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    destination.AppendLine(indent + line);
                }
            }
        }

        private IEnumerable<KeyValuePair<string, string>> ReadVariables(string source)
        {
            using (var reader = new StringReader(source))
            {
                for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    string key, value;
                    int iComment = line.IndexOf('#');
                    if (iComment >= 0) line = line.Substring(0, iComment);
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    int i1 = line.IndexOf(':');
                    int i2 = line.IndexOf('=');
                    if (i1 == -1) i1 = int.MaxValue;
                    if (i2 == -1) i2 = int.MaxValue;
                    if (i1 < i2)
                    {
                        key = line.Substring(0, i1).Trim();
                        value = line.Substring(i1 + 1).Trim();
                    }
                    else if (i2 < i1)
                    {
                        key = line.Substring(0, i2).Trim();
                        value = line.Substring(i2 + 1).Trim();
                    }
                    else
                    {
                        key = value = line.Trim();
                    }

                    if (key.Length > 0 && !"#/;".Contains(key[0]) && value.Length > 0)
                    {
                        yield return new KeyValuePair<string, string>(key, value);
                    }
                }
            }
        }

        #endregion

        #region Code Preview

        private void UpdatePreview()
        {
            var sb = new StringBuilder();
            sb.AppendFormat(TemplateHeader,
                Configuration.Name,
                Application.ProductName,
                Application.ProductVersion,
                DateTime.Now.ToString());

            sb.Append(TemplateImports);
            sb.AppendFormat(TemplateDefinition, Configuration.Definition);


            var landscapeName = Configuration.Landscape;
            if (landscapeName == "Custom")
            {
                landscapeName = "CustomLandscape";
                sb.AppendFormat(TemplateCustomLandscape, landscapeName);
                IndentBlock(Configuration.CustomEvaluator, "    ", sb);
            }

            var parameters = lstParameters_GetParameters();

            string landscapeVariables, systemVariables;
            {
                var sbLandscape = new StringBuilder();
                var sbSystem = new StringBuilder();

                foreach (var kv in parameters)
                {
                    if (kv.Key.StartsWith("landscape."))
                    {
                        sbLandscape.AppendFormat("        '{0}': {1},\n",
                            kv.Key.Substring(10),
                            kv.Value.FirstOrDefault() ?? "None");
                    }
                    else if (kv.Key.StartsWith("system."))
                    {
                        sbSystem.AppendFormat("        '{0}': {1},\n",
                            kv.Key.Substring(7),
                            kv.Value.FirstOrDefault() ?? "None");
                    }
                    else
                    {
                        sbSystem.AppendFormat("        '{0}': {1},\n",
                            kv.Key,
                            kv.Value.FirstOrDefault() ?? "None");
                    }
                }

                landscapeVariables = sbLandscape.ToString();
                systemVariables = sbSystem.ToString();
            }



            sb.AppendFormat(TemplateConfiguration,
                landscapeName,
                landscapeVariables,
                systemVariables,
                Configuration.IterationLimit.HasValue ? Configuration.IterationLimit.Value.ToString() : "None",
                Configuration.EvaluationLimit.HasValue ? Configuration.EvaluationLimit.Value.ToString() : "None",
                Configuration.FitnessLimit.HasValue ? Configuration.FitnessLimit.Value.ToString() : "None"
                );


            var batchFunc = "";
            var batchArgs = "";
            var batchSettings = "None";
            var outputCSV = btnOutputCSV.Checked ? "True" : "False";
            var outputConsole = chkOutputConsole.Checked ? "False" : "True";


            if (btnCountInfinite.Checked || btnCountExact.Checked)
            {
                batchFunc = "xrange";
                batchArgs = ((int)txtCountExact.Value).ToString();
            }
            else
            {
                batchFunc = btnCountParameterCombinations.Checked ? "itertools.product" : "itertools.izip";

                var sbArgs = new StringBuilder();
                var sbSettings = new StringBuilder();

                sbSettings.Append("'");
                foreach (var kv in parameters)
                {
                    sbArgs.Append("[");
                    foreach (var value in kv.Value) sbArgs.AppendFormat("{0},", value);
                    sbArgs.Append("],");
                    
                    sbSettings.AppendFormat("{0}=%s;", kv.Key);
                }
                sbSettings.Append("' % i");
                
                batchArgs = sbArgs.ToString();
                batchSettings = sbSettings.ToString();
            }

            sb.AppendFormat(TemplateSimpleBatch,
                outputCSV,
                outputConsole,
                batchFunc,
                batchArgs,
                batchSettings
                );


            txtPreview.Text = sb.ToString();
        }

        private void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPreview.Checked) UpdatePreview();
            panelEdit.Visible = !(panelPreview.Visible = chkPreview.Checked);   // yes, "=" is deliberate
        }

        #endregion

        #region Count Handling

        private void btnCountExact_CheckedChanged(object sender, EventArgs e)
        {
            if (btnCountExact.Checked)
            {
                txtCountExact.Value = (int)txtCountExact.Tag;
                txtCountExact.Enabled = true;
            }
            else
            {
                txtCountExact.Tag = (int)txtCountExact.Value;
                txtCountExact.Enabled = false;
            }
        }

        private void btnCountOther_CheckChanged(object sender, EventArgs e)
        {
            // Do nothing if we're being unchecked
            if (!((RadioButton)sender).Checked) return;

            txtCountExact_Update();
        }

        private void txtCountExact_Update()
        {
            if (btnCountInfinite.Checked)
            {
                txtCountExact.Value = int.MaxValue;
            }
            else if (btnCountParameterList.Checked)
            {
                txtCountExact.Value = lstParameters.Items.OfType<ListViewItem>().Count(lvi => lvi.Text != "*");
            }
            else if (btnCountParameterCombinations.Checked)
            {
                var parameters = lstParameters_GetParameters();

                txtCountExact.Value = parameters.Select(kv => kv.Value.Count).Aggregate(1, (a, n) => a * n);
            }
        }

        #endregion
    }
}
