﻿using System;
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

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            chkSystem.Tag = panelSystem;
            chkLandscape.Tag = panelLandscape;
            chkResults.Tag = panelResults;
            chkLog.Tag = panelLog;

            var codeFont = new Font("Consolas", 10.0f);
            if (codeFont.Name != codeFont.OriginalFontName)
            {
                codeFont = new Font(FontFamily.GenericMonospace, 10.0f);
            }

            chkChartBestFitness.BackColor = ChartStyles[0].LineColor;
            chkChartCurrentBest.BackColor = ChartStyles[1].LineColor;
            chkChartCurrentMean.BackColor = ChartStyles[2].LineColor;
            chkChartCurrentWorst.BackColor = ChartStyles[3].LineColor;

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

        private void menuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Chart Styles

        private static readonly VisualiserPointStyle VisualiserStyle = new VisualiserPointStyle
        {
            BorderColor = Color.RoyalBlue,
            BorderThickness = 1.0,
            FillColor = Color.FromArgb(64, Color.Blue),
            Size = 8.0,
            ScaleMode = VisualiserPointScaleMode.Pixels,
            Shape = VisualiserPointShape.Circle
        };

        private static readonly VisualiserPointStyle[] ChartStyles = new[]
        {
            new VisualiserPointStyle
            {
                LineColor = Color.Blue,
                LineThickness = 2.0,
            },
            new VisualiserPointStyle
            {
                LineColor = Color.Red,
                LineThickness = 1.0,
            },
            new VisualiserPointStyle
            {
                LineColor = Color.Green,
                LineThickness = 1.0,
            },
            new VisualiserPointStyle
            {
                LineColor = Color.Gold,
                LineThickness = 1.0,
            },
        };

        private void chkChartBestFitness_CheckedChanged(object sender, EventArgs e)
        {
            chartResults.ShowSeries(0, ((CheckBox)sender).Checked);
        }

        private void chkChartCurrentBest_CheckedChanged(object sender, EventArgs e)
        {
            chartResults.ShowSeries(1, ((CheckBox)sender).Checked);
        }

        private void chkChartCurrentMean_CheckedChanged(object sender, EventArgs e)
        {
            chartResults.ShowSeries(2, ((CheckBox)sender).Checked);
        }

        private void chkChartCurrentWorst_CheckedChanged(object sender, EventArgs e)
        {
            chartResults.ShowSeries(3, ((CheckBox)sender).Checked);
        }


        #endregion

        #region Startup and early termination

        private Task InitialisationTask;
        private CancellationTokenSource InitialisationTaskCTS;

        private void Editor_Load(object sender, EventArgs e)
        {
            menuStrip.Left = ClientSize.Width - menuStrip.Width;

            lstErrors.ListViewItemSorter = new ErrorItemSorter();
            lstLandscapes.TreeViewNodeSorter = new LandscapeSorter();

            Text = "esec Experiment Designer - Loading...";

            LookDisabled();
            UseWaitCursor = true;
            menuStrip.Enabled = false;

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
            FillLandscapeTree();
            InitialisationTask.Dispose();
            InitialisationTask = null;
            InitialisationTaskCTS.Dispose();
            InitialisationTaskCTS = null;

            LookEnabled();
            menuStrip.Enabled = true;
            UseWaitCursor = false;

            ConfigurationList_Refresh("FirstRunDefault");
            lstConfigurations.Enabled = true;
            menuSave.Enabled = true;
            menuSaveAs.Enabled = true;

            Text = "esec Experiment Designer";
        }

        private void Task_Init_NotCompleted(Task task)
        {
            menuStrip.Enabled = true;
            LookEnabled();

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
                chkLog.Checked = true;
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
                if (panelLandscape.Contains(error.Source)) chkLandscape.Checked = true;
                error.Source.Focus();
            }
        }

        #endregion

        #region System validation


        private void CheckSyntax()
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
            if (IsDisposed) return;
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

            double min = chartResults.MinimumVerticalOffset;
            double max = chartResults.MaximumVerticalOffset + chartResults.MaximumVerticalRange;

            if (bestFitness != null)
            {
                double value = (double)bestFitness.simple;
                value = (value < min) ? min : (value > max) ? max : value;
                chartResults.Add(new VisualiserPoint(iterations, value, ChartStyles[0]), 0);
            }
            if (currentBest != null)
            {
                double value = (double)currentBest.simple;
                value = (value < min) ? min : (value > max) ? max : value;
                chartResults.Add(new VisualiserPoint(iterations, value, ChartStyles[1]), 1);
            }
            if (currentMean != null)
            {
                double value = (double)currentMean.simple;
                value = (value < min) ? min : (value > max) ? max : value;
                chartResults.Add(new VisualiserPoint(iterations, value, ChartStyles[2]), 2);
            }
            if (currentWorst != null)
            {
                double value = (double)currentWorst.simple;
                value = (value < min) ? min : (value > max) ? max : value;
                chartResults.Add(new VisualiserPoint(iterations, value, ChartStyles[3]), 3);
            }
        }

        private bool DisableVisualisation;

        public void UpdateVisualisation(IEnumerable<dynamic> population, bool firstRun = false)
        {
            if (population == null) return;
            if (DisableVisualisation) return;

            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => UpdateVisualisation(population, firstRun)));
                return;
            }

            IList<VisualiserPoint> points;

            try
            {
                points = population.Select(indiv => new VisualiserPoint((double)indiv[0], (double)indiv[1], VisualiserStyle)).ToList();
            }
            catch
            {
                DisableVisualisation = true;
                return;
            }

            if (firstRun) visPopulation.AutoRangeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            else visPopulation.AutoRangeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;

            visPopulation.SetPoints(points);
        }

        #endregion

        #region Start an Experiment

        private Task CurrentExperiment;
        private Monitor CurrentMonitor;

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (IsExperimentRunning)
            {
                StopExperiment();
            }
            else
            {
                StartExperiment();
            }
        }

        private bool IsExperimentRunning
        {
            get { return CurrentMonitor != null && CurrentExperiment != null && !CurrentExperiment.IsCompleted; }
        }

        private void StopExperiment()
        {
            CurrentMonitor.Cancel();
            btnStartStop.Enabled = false;
        }

        private void StartExperiment()
        {
            if (txtSystem.Text.Length == 0)
            {
                chkSystem.Checked = true;
                return;
            }
            if (lstLandscapes.SelectedNode == null ||
                (lstLandscapes.SelectedNode.Tag == null && lstLandscapes.SelectedNode.Name != "Custom"))
            {
                chkLandscape.Checked = true;
                return;
            }

            chartResults.ClearAll();
            chartResults.ShowSeries(0, chkChartBestFitness.Checked);
            chartResults.ShowSeries(1, chkChartCurrentBest.Checked);
            chartResults.ShowSeries(2, chkChartCurrentMean.Checked);
            chartResults.ShowSeries(3, chkChartCurrentWorst.Checked);

            DisableVisualisation = false;

            CurrentMonitor = new Monitor(this);
            CurrentMonitor.IterationLimit = chkIterations.Checked ? int.Parse(txtIterations.Text) : (int?)null;
            CurrentMonitor.EvaluationLimit = chkEvaluations.Checked ? int.Parse(txtEvaluations.Text) : (int?)null;
            CurrentMonitor.TimeLimit = chkSeconds.Checked ? TimeSpan.FromSeconds(double.Parse(txtSeconds.Text)) : (TimeSpan?)null;
            CurrentMonitor.FitnessLimit = chkFitness.Checked ? double.Parse(txtFitness.Text) : (double?)null;

            IList<ErrorItem> errors = null;
            var variables = Python.ConfigDict(txtSystemVariables, out errors);
            if (errors.Any())
            {
                foreach (var err in errors) lstErrors.Items.Add(err);
                chkSystem.Checked = true;
                return;
            }

            var landscape = Python.ConfigDict(txtLandscapeParameters, out errors);
            if (errors.Any())
            {
                foreach (var err in errors) lstErrors.Items.Add(err);
                chkLandscape.Checked = true;
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

            CurrentExperiment = new Task(Task_RunExperiment, state,
                CancellationToken.None,
                TaskCreationOptions.LongRunning);
            var noErrTask = CurrentExperiment.ContinueWith(Task_RunExperiment_Completed,
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                TaskScheduler.FromCurrentSynchronizationContext());
            var errTask = CurrentExperiment.ContinueWith(Task_RunExperiment_NotCompleted,
                CancellationToken.None,
                TaskContinuationOptions.NotOnRanToCompletion,
                TaskScheduler.FromCurrentSynchronizationContext());

            btnStartStop.Text = "&Stop (F5)";

            chkResults.Checked = true;
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
                // If the debugger breaks here saying the exception is unhandled, continue (press F5).
                // Alternatively, you can disable "Just My Code" in the debugging options.
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
                    chkSystem.Checked = true;
                    CheckSyntax();
                }
            }
            else
            {
                Log("Unhandled exception.\n" + ex.ToString());
                if (ex.InnerException != null)
                {
                    Log("Inner exception:\n" + ex.InnerException.ToString() + "\n");
                }

                chkLog.Checked = true;
            }
            CurrentMonitor = null;
            CurrentExperiment.Dispose();
            CurrentExperiment = null;
        }
        #endregion

        #region Fancy UI Tricks

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

        private void panelMenu_Layout(object sender, LayoutEventArgs e)
        {
            lstConfigurations.Width = panelMenu.ClientSize.Width
                - lstConfigurations.Left
                - lstConfigurations.Margin.Right - panelMenu.Padding.Right;
            lstConfigurations.DropDownWidth = Math.Max(lstConfigurations.Width, 200);
        }

        private void DrawPanelToBitmap(Panel panel, Bitmap bitmap)
        {
            if (panel.Visible) panel.DrawToBitmap(bitmap, new Rectangle(panel.Location, panel.Size));
        }

        private void LookDisabled()
        {
            var dimmed = new Bitmap(ClientSize.Width, ClientSize.Height);
            DrawPanelToBitmap(panelMenu, dimmed);
            DrawPanelToBitmap(panelSystem, dimmed);
            DrawPanelToBitmap(panelLandscape, dimmed);
            DrawPanelToBitmap(panelResults, dimmed);
            DrawPanelToBitmap(panelLog, dimmed);
            using (var g = Graphics.FromImage(dimmed))
            using (var brush = new SolidBrush(Color.FromArgb(64, 128, 128, 128)))
            {
                g.FillRectangle(brush, ClientRectangle);
            }
            picDimmer.Image = dimmed;
            picDimmer.BringToFront();
            picDimmer.SetBounds(0, 0, ClientSize.Width, ClientSize.Height);
            picDimmer.Visible = true;

        }

        private void LookEnabled()
        {
            var dimmed = picDimmer.Image;
            picDimmer.Image = null;
            picDimmer.Visible = false;
            dimmed.Dispose();
        }

        #endregion

        #region Configuration List/Load/Save

        private Configuration CurrentConfiguration = null;
        private int lstConfigurations_SuppressLoad = 0;

        private void ConfigurationList_Refresh(string newSelection = null)
        {
            lstConfigurations.BeginUpdate();
            try
            {
                var selection = lstConfigurations.SelectedItem as FileInfo;
                lstConfigurations.Items.Clear();
                if (Properties.Settings.Default.MRU == null)
                {
                    Properties.Settings.Default.MRU = new System.Collections.Specialized.StringCollection();
                    Properties.Settings.Default.Save();
                }

                var set = new HashSet<string>();
                foreach (var path in Properties.Settings.Default.MRU)
                {
                    if (set.Contains(path.ToUpperInvariant())) continue;
                    set.Add(path.ToUpperInvariant());
                    lstConfigurations.Items.Insert(0, new FileInfo(path));
                }

                // Add built-in configurations
                int firstRunDefault = 
                    lstConfigurations.Items.Add(new Configuration(Properties.Resources.GeneticAlgorithm));
                lstConfigurations.Items.Add(new Configuration(Properties.Resources.EvolutionaryProgramming));
                lstConfigurations.Items.Add(new Configuration(Properties.Resources.GeneticProgramming));
                lstConfigurations.Items.Add(new Configuration(Properties.Resources.SteadyStateGA));

                if (newSelection == "FirstRunDefault")
                {
                    lstConfigurations.SelectedIndex = firstRunDefault;
                }
                else if (newSelection != null && set.Contains(newSelection.ToUpperInvariant()))
                {
                    lstConfigurations_SuppressLoad += 1;
                    lstConfigurations.SelectedItem = lstConfigurations.Items
                        .OfType<FileInfo>()
                        .FirstOrDefault(i => i.FullName.Equals(newSelection, StringComparison.InvariantCultureIgnoreCase));
                }
                else if (selection != null && set.Contains(selection.FullName.ToUpperInvariant()))
                {
                    lstConfigurations_SuppressLoad += 1;
                    lstConfigurations.SelectedItem = lstConfigurations.Items
                        .OfType<FileInfo>()
                        .FirstOrDefault(i => i.FullName.Equals(selection.FullName, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            finally
            {
                lstConfigurations.EndUpdate();
            }
        }

        #region GetRelativePath

        [System.Runtime.InteropServices.DllImport("shlwapi.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi,
            CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern bool PathRelativePathTo(string pszPath,
            string pszFrom, uint dwAttrFrom, string pszTo, uint dwAttrTo);

        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;
        private const int MAX_PATH=260;

        private static string GetRelativePath(string fromPath, string toPath)
        {
            string dest = new string(' ', MAX_PATH);
            if (PathRelativePathTo(dest, fromPath, FILE_ATTRIBUTE_DIRECTORY, toPath, 0))
            {
                return dest.Trim(' ', '\0');
            }
            else
            {
                return toPath;
            }
        }

        #endregion

        private void lstConfigurations_Format(object sender, ListControlConvertEventArgs e)
        {
            var fi = e.ListItem as FileInfo;
            var config = e.ListItem as Configuration;
            if (fi != null)
            {
                e.Value = GetRelativePath(Environment.CurrentDirectory, fi.FullName);
            }
            else if (config != null)
            {
                e.Value = config.Name + " (built in)";
            }
            else
            {
                e.Value = "(error)";
            }


        }

        private void lstConfigurations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstConfigurations_SuppressLoad > 0)
            {
                lstConfigurations_SuppressLoad -= 1;
                return;
            }

            var lst = sender as ComboBox;
            if (lst == null) return;

            var config = lst.SelectedItem as Configuration;
            if (config == null)
            {
                var path = lst.SelectedItem as FileInfo;
                if (path == null || !path.Exists) return;

                config = new Configuration();

                if (path.Extension.Equals(".py", StringComparison.InvariantCultureIgnoreCase))
                {
                    using (var stream = path.Open(FileMode.Open, FileAccess.Read))
                    {
                        config.ReadPython(stream, Python);
                    }
                }
                else
                {
                    config.Read(path.FullName);
                }
            }

            UpdateEditor(config);

            CurrentConfiguration = config;
        }

        private void UpdateConfig()
        {
            CurrentConfiguration.Definition = txtSystem.Text;
            CurrentConfiguration.SystemParameters = txtSystemVariables.Text;
            CurrentConfiguration.Landscape = (lstLandscapes.SelectedNode ?? lstLandscapes.Nodes["Custom"]).Name;
            CurrentConfiguration.LandscapeParameters = txtLandscapeParameters.Text;

            CurrentConfiguration.IterationLimit = chkIterations.Checked ? int.Parse(txtIterations.Text) : (int?)null;
            CurrentConfiguration.EvaluationLimit = chkEvaluations.Checked ? int.Parse(txtEvaluations.Text) : (int?)null;
            CurrentConfiguration.TimeLimit = chkSeconds.Checked ? TimeSpan.FromSeconds(double.Parse(txtSeconds.Text)) : (TimeSpan?)null;
            CurrentConfiguration.FitnessLimit = chkFitness.Checked ? double.Parse(txtFitness.Text) : (double?)null;
        }

        private void UpdateEditor(Configuration config)
        {
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

        private void menuNew_Click(object sender, EventArgs e)
        {
            CurrentConfiguration = null;
            lstConfigurations.SelectedIndex = -1;

            txtSystem.ResetText();
            txtSystem.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystem.Refresh();

            txtSystemVariables.ResetText();
            txtSystemVariables.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystemVariables.Refresh();

            lstLandscapes.SelectedNode = null;
            lstLandscapes.Refresh();

            txtLandscapeParameters.ResetText();
            txtLandscapeParameters.Document.MarkerStrategy.RemoveAll(_ => true);
            txtLandscapeParameters.Refresh();

            txtIterations.Text = "10";
            txtEvaluations.ResetText();
            txtSeconds.ResetText();
            txtFitness.ResetText();
            this.ValidateChildren(ValidationConstraints.Enabled);
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            FileInfo path;

            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "esec File (*.esec;*.xml)|*.esec;*.xml|Python File (*.py)|*.py";
                ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ofd.RestoreDirectory = false;
                ofd.SupportMultiDottedExtensions = true;
                ofd.AutoUpgradeEnabled = true;
                ofd.AddExtension = true;
                if (ofd.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
                path = new FileInfo(ofd.FileName);
            }

            Configuration config = new Configuration();

            if (path.Extension.Equals(".py", StringComparison.InvariantCultureIgnoreCase))
            {
                using (var stream = path.Open(FileMode.Open, FileAccess.Read))
                {
                    config.ReadPython(stream, Python);
                }
            }
            else
            {
                config.Read(path.FullName);
            }

            UpdateEditor(config);

            CurrentConfiguration = config;

            Properties.Settings.Default.MRU.Add(path.FullName);
            Properties.Settings.Default.Save();
            ConfigurationList_Refresh(path.FullName);
        }


        private void menuSave_Click(object sender, EventArgs e)
        {
            if (CurrentConfiguration == null || CurrentConfiguration.Source == null ||
                CurrentConfiguration.Source.EndsWith("py", StringComparison.InvariantCultureIgnoreCase))
            {
                menuSaveAs.PerformClick();
                return;
            }

            UpdateConfig();
            CurrentConfiguration.Write(Python);

            Properties.Settings.Default.MRU.Add(CurrentConfiguration.Source);
            Properties.Settings.Default.Save();
            ConfigurationList_Refresh();
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            if (CurrentConfiguration == null)
            {
                CurrentConfiguration = new Configuration();
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "esec File (*.esec)|*.esec";
                sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                sfd.RestoreDirectory = false;
                sfd.SupportMultiDottedExtensions = true;
                sfd.AutoUpgradeEnabled = true;
                sfd.AddExtension = true;
                sfd.DefaultExt = ".esec";
                if (sfd.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
                CurrentConfiguration.Name = Path.GetFileNameWithoutExtension(sfd.FileName);
                CurrentConfiguration.Source = sfd.FileName;
            }

            UpdateConfig();
            CurrentConfiguration.Write(Python);

            Properties.Settings.Default.MRU.Add(CurrentConfiguration.Source);
            Properties.Settings.Default.Save();
            ConfigurationList_Refresh(CurrentConfiguration.Source);
        }

        #endregion

        #region View Selection

        private void menuViewSystem_Click(object sender, EventArgs e)
        {
            chkSystem.Checked = true;
        }

        private void menuViewLandscape_Click(object sender, EventArgs e)
        {
            chkLandscape.Checked = true;
        }

        private void menuViewResults_Click(object sender, EventArgs e)
        {
            chkResults.Checked = true;
        }

        private void menuViewResultsChart_Click(object sender, EventArgs e)
        {
            tabResultView.SelectTab(tabChart);
        }

        private void menuViewResultsPlot_Click(object sender, EventArgs e)
        {
            tabResultView.SelectTab(tab2DPlot);
        }

        private void menuViewLog_Click(object sender, EventArgs e)
        {
            chkLog.Checked = true;
        }

        private void chkTabs_CheckedChanged(object sender, EventArgs e)
        {
            var chk = sender as RadioButton;
            Debug.Assert(chk != null);

            var panel = chk.Tag as Panel;
            Debug.Assert(panel != null);

            panel.Visible = chk.Checked;

            menuViewResultsChart.Enabled = (panel == panelResults);
            menuViewResultsPlot.Enabled = (panel == panelResults);
        }


        #endregion

        private void menuAbout_Click(object sender, EventArgs e)
        {
            LookDisabled();
            using (var about = new About())
            {
                about.ShowDialog(this);
            }
            LookEnabled();
        }


    }
}
