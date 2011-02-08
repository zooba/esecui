using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Scripting.Hosting;
using IronPython.Runtime;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace esecui
{
    public partial class Editor : Form
    {
        public PythonHost Python;
        private dynamic esec;
        private dynamic esdlc;

        public Editor()
        {
            InitializeComponent();

            chartResults.ChartAreas.Add("Results");

            var codeFont = new Font("Consolas", 10.0f);
            if (codeFont.Name != codeFont.OriginalFontName)
            {
                codeFont = new Font(FontFamily.GenericMonospace, 10.0f);
            }

            txtSystem.Font = codeFont;
            txtSystemVariables.Font = codeFont;
            txtLandscapeParameters.Font = codeFont;
            txtEvaluatorCode.Font = codeFont;
            txtLog.Font = codeFont;

            txtSystem.SetHighlighting("ESDL");
            txtSystemVariables.SetHighlighting("ESDLVariables");
            txtLandscapeParameters.SetHighlighting("ESDLVariables");
            txtEvaluatorCode.SetHighlighting("Python");
        }

        #region Startup and early termination

        private Task InitialisationTask;
        private CancellationTokenSource InitialisationTaskCTS;

        private void Editor_Load(object sender, EventArgs e)
        {
            tabTabs.SelectedIndex = 0;

            lstErrors.ListViewItemSorter = new ErrorItemSorter();
            lstLandscapes.TreeViewNodeSorter = new LandscapeSorter();

            Text = "esec Experiment Designer - Loading...";

            UseWaitCursor = true;
            tabTabs.Enabled = false;
            
            var guiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            InitialisationTaskCTS = new CancellationTokenSource();

            InitialisationTask = new Task(Task_Init, InitialisationTaskCTS.Token);
            InitialisationTask.ContinueWith(Task_Init_Completed,
                CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, guiScheduler);
            InitialisationTask.ContinueWith(Task_Init_NotCompleted,
                CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion, guiScheduler);

            InitialisationTask.Start();
        }

        private void Task_Init()
        {
            var libraryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            Python = new PythonHost(libraryPath);

            esec = Python.Import("esec");
            esdlc = Python.Import("esdlc");

            PrepareLandscapeTree();
        }

        private void Task_Init_Completed(Task task)
        {
            btnCheckSyntax.Enabled = true;
            btnStartStop.Enabled = true;

            FillLandscapeTree();
            InitialisationTask.Dispose();
            InitialisationTask = null;
            InitialisationTaskCTS.Dispose();
            InitialisationTaskCTS = null;

            tabTabs.Enabled = true;
            UseWaitCursor = false;

            ConfigurationList_Refresh();
            lstConfigurations.Enabled = true;
            btnSaveConfiguration.Enabled = true;
            btnSaveAsConfiguration.Enabled = true;

            // Select the GA template by default
            var config = lstConfigurations.Items
                .OfType<Configuration>()
                .FirstOrDefault(c => c.Name == "Template: Genetic Algorithm");
            if (config != null) lstConfigurations.SelectedItem = config;

            Text = "esec Experiment Designer";
        }

        private void Task_Init_NotCompleted(Task task)
        {
            if (!task.IsCanceled)
            {
                Log("An error occurred while initialising IronPython.\n");
                var ae = task.Exception as AggregateException;
                if (ae == null)
                {
                    Log(task.Exception.ToString());
                }
                else
                {
                    foreach (var ex in ae.InnerExceptions) Log(ex.ToString());
                }
                tabTabs.SelectedTab = tabLog;
            }
            InitialisationTask.Dispose();
            InitialisationTask = null;
            InitialisationTaskCTS = null;
        }


        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            var initTask = InitialisationTask;
            var initTaskCTS = InitialisationTaskCTS;
            if (initTask != null && initTaskCTS != null)
            {
                initTaskCTS.Cancel(false);
            }
        }

        #endregion

        #region Landscape list handling

        private Dictionary<string, TreeNode> LandscapeNodes;
        private bool LoadLandscapes = true;

        private class LandscapeSorter : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                TreeNode tn1 = (TreeNode)x, tn2 = (TreeNode)y;
                if (tn1.Name == tn2.Name) return 0;
                if (tn1.Name == "Custom") return 1;
                if (tn2.Name == "Custom") return -1;
                return tn1.Name.CompareTo(tn2.Name);
            }
        }

        private void PrepareLandscapeTree()
        {
            var landscapes = esec.esec.landscape.LANDSCAPES;

            LandscapeNodes = new Dictionary<string, TreeNode>();

            if (!LoadLandscapes) return;

            foreach (var landscape in landscapes)
            {
                string type = landscape.ltype;
                if (LandscapeNodes.ContainsKey(type) == false)
                {
                    LandscapeNodes[type] = new TreeNode(landscape.ltype_name);
                    LandscapeNodes[type].Name = type;
                }

                var node = new TreeNode
                {
                    Name = landscape.__module__.ToString() + "." + landscape.__name__,
                    Text = landscape.lname,
                    Tag = landscape
                };
                LandscapeNodes[type].Nodes.Add(node);
            }
        }

        private void FillLandscapeTree()
        {
            if (LandscapeNodes == null || LandscapeNodes.Count == 0) PrepareLandscapeTree();

            lstLandscapes.BeginUpdate();
            foreach (var node in LandscapeNodes)
            {
                lstLandscapes.Nodes.Add(node.Value);
            }
            lstLandscapes.Nodes.Add(new TreeNode
            {
                Name = "Custom",
                Text = "Custom"
            });
            lstLandscapes.EndUpdate();
        }

        const string CustomEvaluatorName = "Custom Evaluator";
        const string CustomEvaluatorDescription = @"Specify a fitness evaluation function as Python code.

The evaluator always receives the individual as ""indiv"" and must return
an integer or floating-point number, or an instance of FitnessMaximise or
FitnessMinimise, each of which takes a list of values in its initialiser.
";

        private void lstLandscapes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "Custom")
            {
                lblEvaluatorCode.Visible = true;
                txtEvaluatorCode.Visible = true;
                txtLandscapeInternalName.Text = CustomEvaluatorName;
                txtLandscapeDescription.Text = CustomEvaluatorDescription;
                txtLandscapeParameters.ResetText();
                lblLandscapeParameters.Enabled = false;
                txtLandscapeParameters.Enabled = false;
                return;
            }
            else if (txtEvaluatorCode.Visible)
            {
                lblEvaluatorCode.Visible = false;
                txtEvaluatorCode.Visible = false;
                lblLandscapeParameters.Enabled = true;
                txtLandscapeParameters.Enabled = true;
            }

            if (e.Node.Tag != null)
            {
                dynamic landscape = e.Node.Tag;
                txtLandscapeInternalName.Text = e.Node.Name;
                txtLandscapeDescription.Text = ((string)landscape.__doc__).Replace("\n", "\r\n");
                if (e.Action != TreeViewAction.Unknown)
                {
                    txtLandscapeParameters.ResetText();
                    txtLandscapeParameters.Text = Python.FromSyntaxDefaults((object)landscape).ToText(Python);
                }
            }
            else
            {
                txtLandscapeInternalName.ResetText();
                txtLandscapeDescription.ResetText();
                txtLandscapeParameters.ResetText();
            }
        }

        private const string EvaluatorTemplate = @"import esec.landscape
from esec.fitness import FitnessMaximise, FitnessMinimise

# Don't change the class name from CustomEvaluator
class CustomEvaluator(esec.landscape.Landscape):
    def eval(self, indiv):
        
";

        private dynamic GetCustomEvaluator()
        {
            var scope = Python.CreateScope();

            var codeLines = txtEvaluatorCode.Text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(s => "        " + s)
                .Aggregate((sum, s) => sum + "\r\n" + s);

            var code = EvaluatorTemplate + codeLines;

            Python.Exec(code, scope);
            return scope.GetVariable("CustomEvaluator");
        }

        #endregion

        #region Log Functions

        public void Log(string format, params object[] values)
        {
            Log(string.Format(format, values));
        }

        public void Log(string message)
        {
            message = message.Replace("\n", "\r\n").Replace("\r\r\n", "\r\n") + "\r\n";
            if (InvokeRequired)
            {
                Invoke((Action<string>)(msg =>
                {
                    txtLog.Document.Insert(txtLog.Document.TextLength, msg);
                }), message);
            }
            else
            {
                txtLog.Document.Insert(txtLog.Document.TextLength, message);
            }
        }

        public void Log()
        {
            if (InvokeRequired)
            {
                Invoke((Action)Log);
            }
            else
            {
                txtLog.Document.Insert(txtLog.Document.TextLength, "\r\n");
            }
        }

        #endregion

        #region Error list handling

        private class ErrorItemSorter : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                var x1 = ((ListViewItem)x).Tag as ErrorItem;
                var y1 = ((ListViewItem)y).Tag as ErrorItem;
                return x1.CompareTo(y1);
            }
        }

        private void lstErrors_ItemActivate(object sender, EventArgs e)
        {
            var error = ((ListView)sender).SelectedItems[0].Tag as ErrorItem;
            if (error != null)
            {
                error.Source.ActiveTextAreaControl.Caret.Position = error.EndPosition;
                error.Source.ActiveTextAreaControl.SelectionManager.SetSelection(error);
                if (tabLandscape.Contains(error.Source)) tabTabs.SelectedTab = tabLandscape;
                error.Source.Focus();
            }
        }

        #endregion

        #region System validation


        private void btnCheckSyntax_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;

            // Reset everything
            lstErrors.Items.Clear();
            txtSystem.BeginUpdate();
            txtSystemVariables.BeginUpdate();
            txtSystem.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystemVariables.Document.MarkerStrategy.RemoveAll(_ => true);

            var variables = Python.ConfigDict(txtSystemVariables);

            var externs = variables.Keys.OfType<string>().ToList();
            externs.Add("config");
            externs.Add("notify");
            externs.Add("rand");

            var guiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew((Func<object, dynamic>)(state_obj =>
            {
                var state = (object[])state_obj;

                dynamic compile = esdlc.compileESDL;
                dynamic ast = compile((string)state[0], (List<string>)state[1]);
                return ast;
            }), new object[] { txtSystem.Text, externs })
            .ContinueWith((Action<Task<dynamic>>)(task =>
            {
                dynamic ast = task.Result;
                foreach (var error in ast._errors)
                {
                    lstErrors.Items.Add(ErrorItem.FromPython(txtSystem, error));
                }

                foreach (var uninit in ast.warnings)
                {
                    string code = uninit.code;
                    if (code != "W2002" && code != "W2003" && code != "W2004") continue;

                    string variable = uninit.text;

                    if (!variables.ContainsKey(variable))
                    {
                        if (txtSystemVariables.Document.TextLength > 0 &&
                            txtSystemVariables.Document.GetText(txtSystemVariables.Document.TextLength - 1, 1) != "\n")
                        {
                            txtSystemVariables.Document.Insert(txtSystemVariables.Document.TextLength, "\r\n");
                        }
                        txtSystemVariables.Document.Insert(txtSystemVariables.Document.TextLength,
                            string.Format("{0}: None\r\n", variable));
                    }
                }

                UseWaitCursor = false;
                txtSystem.EndUpdate();
                txtSystem.Refresh();
                txtSystemVariables.EndUpdate();
                txtSystemVariables.Refresh();
            }), guiScheduler);
        }

        #endregion

        #region Monitor Callbacks

        public void UpdateStats(int iterations, int evaluations, int births, TimeSpan time,
            dynamic bestFitness, dynamic currentBest, dynamic currentMean, dynamic currentWorst)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => UpdateStats(iterations, evaluations, births, time,
                    bestFitness, currentBest, currentMean, currentWorst)));
                return;
            }

            txtStatsIterations.Text = iterations.ToString();
            txtStatsEvaluations.Text = evaluations.ToString();
            txtStatsBirths.Text = births.ToString();
            txtStatsTime.Text = time.ToString();

            txtStatsBestFitness.Text = bestFitness;
            txtStatsCurrentBest.Text = currentBest;
            txtStatsCurrentMean.Text = currentMean;
            txtStatsCurrentWorst.Text = currentWorst;

            bool noPoints = (!chartResults.Series[0].Points.Any());
            if (bestFitness != null)
            {
                double value = (double)bestFitness.simple;
                if (noPoints || value > chartResults.ChartAreas[0].AxisY.Minimum * 2.0)
                    chartResults.Series[0].Points.Add(new DataPoint(iterations, value));
            }
            if (currentBest != null)
            {
                double value = (double)currentBest.simple;
                if (noPoints || value > chartResults.ChartAreas[0].AxisY.Minimum * 2.0)
                    chartResults.Series[1].Points.Add(new DataPoint(iterations, value));
            }
            if (currentMean != null)
            {
                double value = (double)currentMean.simple;
                if (noPoints || value > chartResults.ChartAreas[0].AxisY.Minimum * 2.0)
                    chartResults.Series[2].Points.Add(new DataPoint(iterations, value));
            }

            if (chartResults.Series[0].Points.Count > chartResults.ChartAreas[0].AxisX.Maximum)
            {
                chartResults.ChartAreas[0].AxisX.Maximum += chartResults.Series[0].Points.Count / 2;
            }
        }

        public void UpdateVisualisation(IEnumerable<dynamic> population)
        {
            if (population == null) return;

            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => UpdateVisualisation(population)));
                return;
            }

            IList<VisualiserPoint> points;

            try
            {
                points = population.Select(indiv => new VisualiserPoint((double)indiv[0], (double)indiv[1])).ToList();
            }
            catch
            {
                return;
            }

            visPopulation.SetPoints(points);
        }

        #endregion

        #region Start an Experiment

        private Task CurrentExperiment;
        private Monitor CurrentMonitor;

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (CurrentMonitor != null && CurrentExperiment != null && !CurrentExperiment.IsCompleted)
            {
                CurrentMonitor.Cancel();
                btnStartStop.Enabled = false;
                return;
            }

            if (txtSystem.Text.Length == 0)
            {
                tabTabs.SelectedTab = tabSystem;
                return;
            }
            if (lstLandscapes.SelectedNode == null ||
                (lstLandscapes.SelectedNode.Tag == null && lstLandscapes.SelectedNode.Name != "Custom"))
            {
                tabTabs.SelectedTab = tabLandscape;
                return;
            }

            chartResults.Series.Clear();
            chartResults.Series.Add("Best Fitness").ChartType = SeriesChartType.FastLine;
            chartResults.Series.Add("Current Best").ChartType = SeriesChartType.FastLine;
            chartResults.Series.Add("Current Mean").ChartType = SeriesChartType.FastLine;

            CurrentMonitor = new Monitor(this);
            CurrentMonitor.IterationLimit = chkIterations.Checked ? int.Parse(txtIterations.Text) : (int?)null;
            CurrentMonitor.EvaluationLimit = chkEvaluations.Checked ? int.Parse(txtEvaluations.Text) : (int?)null;
            CurrentMonitor.TimeLimit = chkSeconds.Checked ? TimeSpan.FromSeconds(double.Parse(txtSeconds.Text)) : (TimeSpan?)null;
            CurrentMonitor.FitnessLimit = chkFitness.Checked ? double.Parse(txtFitness.Text) : (double?)null;

            chartResults.ChartAreas[0].AxisX.Minimum = 0;
            chartResults.ChartAreas[0].AxisX.Maximum = CurrentMonitor.IterationLimit ?? 10;

            IList<ErrorItem> errors = null;
            var variables = Python.ConfigDict(txtSystemVariables, out errors);
            if (errors.Any())
            {
                foreach (var err in errors) lstErrors.Items.Add(err);
                tabTabs.SelectedTab = tabSystem;
                return;
            }

            var landscape = Python.ConfigDict(txtLandscapeParameters, out errors);
            if (errors.Any())
            {
                foreach (var err in errors) lstErrors.Items.Add(err);
                tabTabs.SelectedTab = tabLandscape;
                return;
            }
            if (lstLandscapes.SelectedNode.Name == "Custom")
            {
                landscape["instance"] = GetCustomEvaluator();
            }
            else
            {
                landscape["class"] = lstLandscapes.SelectedNode.Tag;
            }

            var state = new Dictionary<string, object>();
            state["landscape"] = landscape;
            state["monitor"] = CurrentMonitor;
            state["system.description"] = txtSystem.Text;
            state["system"] = variables;
            state["random_seed"] = 12345;       // TODO: Settable random seed

            CurrentExperiment = new Task(Task_RunExperiment, state);

            var noErrTask = CurrentExperiment.ContinueWith(Task_RunExperiment_Completed,
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                TaskScheduler.FromCurrentSynchronizationContext());
            var errTask = CurrentExperiment.ContinueWith(Task_RunExperiment_NotCompleted,
                CancellationToken.None,
                TaskContinuationOptions.NotOnRanToCompletion,
                TaskScheduler.FromCurrentSynchronizationContext());

            btnStartStop.Text = "&Stop (F5)";

            CurrentExperiment.Start();
        }

        private void Task_RunExperiment(object state_obj)
        {
            var state = (Dictionary<string, object>)state_obj;

            var config = Python.Dict();

            config["monitor"] = state["monitor"];
            config["landscape"] = Python.Dict(state["landscape"]);

            var systemDict = Python.Dict();
            config["system"] = systemDict;
            systemDict["definition"] = state["system.description"];

            var variables = state["system"] as IDictionary<object, object>;
            if (variables != null)
            {
                foreach (var item in variables)
                {
                    systemDict[item.Key] = item.Value;
                }
            }

            config["random_seed"] = state["random_seed"];

            Python["Experiment"] = esec.Experiment;
            Python["config"] = config;

            dynamic exp;

            try
            {
                exp = Python.Eval("Experiment(config)");
            }
            catch (Exception ex)
            {
                throw new Exception("Experiment.__init__", ex);
            }
            exp.run();
        }

        private void Task_RunExperiment_Completed(Task task)
        {
            btnStartStop.Text = "&Start (F5)";
            btnStartStop.Enabled = true;
            CurrentMonitor = null;
            CurrentExperiment.Dispose();
            CurrentExperiment = null;

            chartResults.ChartAreas[0].AxisX.Maximum = chartResults.Series[0].Points.Count;
        }

        private void Task_RunExperiment_NotCompleted(Task task)
        {
            btnStartStop.Text = "&Start (F5)";
            btnStartStop.Enabled = true;

            Exception ex = task.Exception;
            if (ex is AggregateException) ex = ((AggregateException)ex).InnerExceptions[0];

            if (ex.Message == "Experiment.__init__")
            {
                ex = ex.InnerException;

                Log("Error creating experiment.\n" + ex.ToString());

                if (ex.Message == "ExceptionGroup")
                {
                    tabTabs.SelectedTab = tabSystem;
                    btnCheckSyntax.PerformClick();
                }
            }
            else
            {
                Log("Unhandled exception.\n" + ex.ToString());
                if (ex.InnerException != null)
                {
                    Log("Inner exception:\n" + ex.InnerException.ToString() + "\n");
                }

                tabTabs.SelectedTab = tabLog;
            }
            CurrentMonitor = null;
            CurrentExperiment.Dispose();
            CurrentExperiment = null;
        }
        #endregion

        #region Fancy UI Tricks

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == 0)
            {
                if (e.KeyCode == Keys.F1) tabTabs.SelectedTab = tabSystem;
                else if (e.KeyCode == Keys.F2) tabTabs.SelectedTab = tabLandscape;
                else if (e.KeyCode == Keys.F3) tabTabs.SelectedTab = tabResults;
                else if (e.KeyCode == Keys.F4) { tabTabs.SelectedTab = tabSystem; btnCheckSyntax.PerformClick(); }
                else if (e.KeyCode == Keys.F5) { tabTabs.SelectedTab = tabResults; btnStartStop.PerformClick(); }
                else if (e.KeyCode == Keys.F12) tabTabs.SelectedTab = tabLog;
            }
            else if (e.Modifiers == Keys.Alt)
            {
                if (e.KeyCode == Keys.D1) tabResultView.SelectedTab = tabChart;
                if (e.KeyCode == Keys.D2) tabResultView.SelectedTab = tab2DPlot;
            }
        }

        private void txtIterations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { txtEvaluations.Focus(); txtEvaluations.SelectAll(); e.Handled = true; }
        }

        private void txtEvaluations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { txtSeconds.Focus(); txtSeconds.SelectAll(); e.Handled = true; }
            if (e.KeyCode == Keys.Up) { txtIterations.Focus(); txtIterations.SelectAll(); e.Handled = true; }
        }

        private void txtSeconds_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { txtFitness.Focus(); txtFitness.SelectAll(); e.Handled = true; }
            if (e.KeyCode == Keys.Up) { txtEvaluations.Focus(); txtEvaluations.SelectAll(); e.Handled = true; }
        }

        private void txtFitness_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { txtSeconds.Focus(); txtSeconds.SelectAll(); e.Handled = true; }
        }

        private void txtIterations_Validated(object sender, EventArgs e)
        {
            int temp;
            if (!int.TryParse(txtIterations.Text, out temp)) txtIterations.Text = string.Empty;
            if (ActiveControl != chkIterations)
                chkIterations.Checked = !string.IsNullOrWhiteSpace(txtIterations.Text);
        }

        private void txtEvaluations_Validated(object sender, EventArgs e)
        {
            int temp;
            if (!int.TryParse(txtEvaluations.Text, out temp)) txtEvaluations.Text = string.Empty;
            if (ActiveControl != chkEvaluations)
                chkEvaluations.Checked = !string.IsNullOrWhiteSpace(txtEvaluations.Text);
        }

        private void txtSeconds_Validated(object sender, EventArgs e)
        {
            double temp;
            if (!double.TryParse(txtSeconds.Text, out temp)) txtSeconds.Text = string.Empty;
            if (ActiveControl != chkSeconds)
                chkSeconds.Checked = !string.IsNullOrWhiteSpace(txtSeconds.Text);
        }

        private void txtFitness_Validated(object sender, EventArgs e)
        {
            double temp;
            if (!double.TryParse(txtFitness.Text, out temp)) txtFitness.Text = string.Empty;
            if (ActiveControl != chkFitness)
                chkFitness.Checked = !string.IsNullOrWhiteSpace(txtFitness.Text);
        }

        private void chkLimit_MouseEnter(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;
            if (chk == null) return;

            chk.ForeColor = SystemColors.Highlight;
        }

        private void chkLimit_MouseLeave(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;
            if (chk == null) return;

            chk.ForeColor = SystemColors.WindowText;
        }

        #endregion

        #region Configuration List/Load/Save

        private void watcherConfigurationDirectory_Changed(object sender, FileSystemEventArgs e)
        {
            ConfigurationList_Refresh();
        }

        private void watcherConfigurationDirectory_Created(object sender, FileSystemEventArgs e)
        {
            ConfigurationList_Refresh();
        }

        private void watcherConfigurationDirectory_Deleted(object sender, FileSystemEventArgs e)
        {
            ConfigurationList_Refresh();
        }

        private void watcherConfigurationDirectory_Renamed(object sender, RenamedEventArgs e)
        {
            ConfigurationList_Refresh();
        }

        private void menuConfigurationDirectory_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (Directory.Exists(Properties.Settings.Default.ConfigurationDirectory))
                {
                    fbd.SelectedPath = Properties.Settings.Default.ConfigurationDirectory;
                }
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog(this) != DialogResult.OK) return;

                Properties.Settings.Default.ConfigurationDirectory = fbd.SelectedPath;
                Properties.Settings.Default.Save();
                watcherConfigurationDirectory.Path = fbd.SelectedPath;
                watcherConfigurationDirectory.EnableRaisingEvents = true;
                ConfigurationList_Refresh();
            }
        }

        private void ConfigurationList_Refresh(string selectSource = null)
        {
            if (!Directory.Exists(Properties.Settings.Default.ConfigurationDirectory))
            {
                var defaultDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cfgs");
                if (Directory.Exists(defaultDirectory))
                {
                    Properties.Settings.Default.ConfigurationDirectory = defaultDirectory;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    return;
                }
            }

            if (watcherConfigurationDirectory.EnableRaisingEvents == false)
            {
                watcherConfigurationDirectory.Path = Properties.Settings.Default.ConfigurationDirectory;
                watcherConfigurationDirectory.EnableRaisingEvents = true;
            }

            lstConfigurations.BeginUpdate();
            try
            {
                foreach (var file in Directory.EnumerateFiles(Properties.Settings.Default.ConfigurationDirectory, "*.xml"))
                {
                    try
                    {
                        var config = new Configuration();
                        config.Source = file;
                        config.Read(file);
                        if (string.IsNullOrWhiteSpace(config.Name)) config.Name = Path.GetFileNameWithoutExtension(file);

                        if (lstConfigurations.Items.Contains(config)) lstConfigurations.Items.Remove(config);
                        lstConfigurations.Items.Add(config);
                    }
                    catch { }
                }


                foreach (var file in Directory.EnumerateFiles(Properties.Settings.Default.ConfigurationDirectory, "*.py"))
                {
                    try
                    {
                        var config = new Configuration();
                        config.Source = file;
                        using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                            config.ReadPython(stream, Python);
                        }
                        if (string.IsNullOrWhiteSpace(config.Name)) config.Name = Path.GetFileNameWithoutExtension(file);

                        if (lstConfigurations.Items.Contains(config)) lstConfigurations.Items.Remove(config);
                        lstConfigurations.Items.Add(config);
                    }
                    catch { }
                }

                if (selectSource != null)
                {
                    var config = lstConfigurations.Items
                        .OfType<Configuration>()
                        .FirstOrDefault(c => c.Source == selectSource);
                    if (config != null) lstConfigurations.SelectedItem = config;
                }
            }
            finally
            {
                lstConfigurations.EndUpdate();
            }
        }

        private void lstConfigurations_Format(object sender, ListControlConvertEventArgs e)
        {
            var config = e.ListItem as Configuration;
            if (config == null)
                e.Value = "(null)";
            else
                e.Value = config.Name + " (" + Path.GetFileName(config.Source) + ")";
        }

        private void lstConfigurations_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lst = sender as ComboBox;
            if (lst == null) return;

            var config = lst.SelectedItem as Configuration;
            if (config == null) return;

            txtSystem.Text = config.Definition;
            txtSystem.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystem.Refresh();

            txtSystemVariables.Text = config.SystemParameters;
            txtSystemVariables.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystemVariables.Refresh();

            lstLandscapes.SelectedNode = lstLandscapes.Nodes.Find(config.Landscape, true).FirstOrDefault();
            lstLandscapes.Refresh();

            txtLandscapeParameters.Text = config.LandscapeParameters;
            txtLandscapeParameters.Document.MarkerStrategy.RemoveAll(_ => true);
            txtLandscapeParameters.Refresh();

            txtIterations.Text = config.IterationLimit.HasValue ? config.IterationLimit.Value.ToString() : "";
            txtEvaluations.Text = config.EvaluationLimit.HasValue ? config.EvaluationLimit.Value.ToString() : "";
            txtSeconds.Text = config.TimeLimit.HasValue ? config.TimeLimit.Value.TotalSeconds.ToString() : "";
            txtFitness.Text = config.FitnessLimit.HasValue ? config.FitnessLimit.Value.ToString() : "";
            this.ValidateChildren(ValidationConstraints.Enabled);
        }

        private void UpdateConfig(Configuration config)
        {
            config.Definition = txtSystem.Text;
            config.SystemParameters = txtSystemVariables.Text;
            config.Landscape = (lstLandscapes.SelectedNode ?? lstLandscapes.Nodes["Custom"]).Name;
            config.LandscapeParameters = txtLandscapeParameters.Text;

            config.IterationLimit = chkIterations.Checked ? int.Parse(txtIterations.Text) : (int?)null;
            config.EvaluationLimit = chkEvaluations.Checked ? int.Parse(txtEvaluations.Text) : (int?)null;
            config.TimeLimit = chkSeconds.Checked ? TimeSpan.FromSeconds(double.Parse(txtSeconds.Text)) : (TimeSpan?)null;
            config.FitnessLimit = chkFitness.Checked ? double.Parse(txtFitness.Text) : (double?)null;
        }

        private void btnSaveConfiguration_Click(object sender, EventArgs e)
        {
            var config = lstConfigurations.SelectedItem as Configuration;
            if (config == null) return;

            if (config.Source == null || config.Source.EndsWith("py", StringComparison.InvariantCultureIgnoreCase))
            {
                btnSaveAsConfiguration.PerformClick();
                return;
            }

            watcherConfigurationDirectory.EnableRaisingEvents = false;

            UpdateConfig(config);
            config.Write(Python);

            ConfigurationList_Refresh();
        }

        private void btnSaveAsConfiguration_Click(object sender, EventArgs e)
        {
            var config = lstConfigurations.SelectedItem as Configuration;
            if (config == null)
            {
                config = new Configuration();
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "XML File (*.xml)|*.xml";
                sfd.InitialDirectory = Properties.Settings.Default.ConfigurationDirectory;
                sfd.RestoreDirectory = true;
                sfd.SupportMultiDottedExtensions = true;
                sfd.AutoUpgradeEnabled = true;
                sfd.AddExtension = true;
                sfd.DefaultExt = ".xml";
                if (sfd.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
                config.Name = Path.GetFileNameWithoutExtension(sfd.FileName);
                config.Source = sfd.FileName;
            }

            watcherConfigurationDirectory.EnableRaisingEvents = false;

            UpdateConfig(config);
            config.Write(Python);

            ConfigurationList_Refresh(config.Source);
        }

        #endregion


    }
}
