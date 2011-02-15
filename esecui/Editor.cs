using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using VisualiserLib;

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

            txtSystemESDL.SetHighlighting("ESDL");
            txtSystemPython.SetHighlighting("Python");
            txtSystemVariables.SetHighlighting("ESDLVariables");
            txtLandscapeParameters.SetHighlighting("ESDLVariables");
            txtEvaluatorCode.SetHighlighting("Python");

            txtSystemPython.Document.DocumentChanged +=
                new ICSharpCode.TextEditor.Document.DocumentEventHandler(UpdatePythonDefinitions);

            InitialiseDisplay(false);
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            LookDisabled();
            using (var about = new About()) about.ShowDialog(this);
            LookEnabled();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Projector Mode

        private static readonly string[] Styles = new[] { "BestFitness", "CurrentBest", "CurrentMean", "CurrentWorst" };

        private Font UIFont;
        private Font ProjectorUIFont;
        private Font CodeFont;
        private Font ProjectorCodeFont;
        private double AxisThickness;
        private double ProjectorAxisThickness;

        private void InitialiseDisplay(bool projectorMode)
        {
            var settings = Properties.Settings.Default;

            // Initialise the UI font
            UIFont = new Font("Segoe UI", 9.0f);
            if (UIFont.Name != UIFont.OriginalFontName)
            {
                UIFont = new Font("Tahoma", 9.0f);
            }
            if (UIFont.Name != UIFont.OriginalFontName)
            {
                UIFont = new Font(FontFamily.GenericSansSerif, 9.0f);
            }
            ProjectorUIFont = new Font(UIFont.Name, 18.0f);

            // Force some controls to always use the smaller font
            panelMenu.Font = UIFont;
            tabSourceView.Font = UIFont;
            tabResultView.Font = UIFont;
            lblPlotExpression.Font = UIFont;
            lblBestIndividualExpression.Font = UIFont;
            btnStart.Font = UIFont;
            btnPause.Font = UIFont;
            btnStop.Font = UIFont;

            // Initialise the code font
            CodeFont = new Font("Consolas", 10.0f);
            if (CodeFont.Name != CodeFont.OriginalFontName)
            {
                CodeFont = new Font(FontFamily.GenericMonospace, 10.0f);
            }
            ProjectorCodeFont = new Font(CodeFont.Name, 20.0f);

            // Initialise the chart styles
            AxisThickness = settings.AxisThickness;
            ProjectorAxisThickness = settings.AxisThicknessProjector;

            SeriesNames = new Dictionary<string, int>();
            NormalChartStyles = new Dictionary<string, VisualiserPointStyle>();
            ProjectorChartStyles = new Dictionary<string, VisualiserPointStyle>();
            int seriesNumber = 0;

            foreach (var style in Styles)
            {
                SeriesNames[style] = seriesNumber++;

                NormalChartStyles[style] = new VisualiserPointStyle
                {
                    LineColor = (Color)settings[style + "LineColor"],
                    LineThickness = (double)settings[style + "LineThickness"]
                };

                ProjectorChartStyles[style] = new VisualiserPointStyle
                {
                    LineColor = (Color)settings[style + "LineColorProjector"],
                    LineThickness = (double)settings[style + "LineThicknessProjector"]
                };
            }

            // Initialise the visualiser styles
            NormalVisualiserStyle = new VisualiserPointStyle
            {
                BorderColor = settings.VisualiserBorderColor,
                BorderThickness = settings.VisualiserBorderThickness,
                FillColor = settings.VisualiserFillColor,
                Size = settings.VisualiserSize,
                ScaleMode = VisualiserPointScaleMode.Pixels,
                Shape = settings.VisualiserShape
            };

            ProjectorVisualiserStyle = new VisualiserPointStyle
            {
                BorderColor = settings.VisualiserBorderColorProjector,
                BorderThickness = settings.VisualiserBorderThicknessProjector,
                FillColor = settings.VisualiserFillColorProjector,
                Size = settings.VisualiserSizeProjector,
                ScaleMode = VisualiserPointScaleMode.Pixels,
                Shape = settings.VisualiserShapeProjector
            };

            // Set the mode to the specified parameter
            _ProjectorMode = !projectorMode;
            ProjectorMode = projectorMode;
        }

        private FormWindowState NormalWindowState = FormWindowState.Normal;
        private bool _ProjectorMode;
        public bool ProjectorMode
        {
            get { return _ProjectorMode; }
            set
            {
                if (_ProjectorMode == value) return;

                _ProjectorMode = value;

                SuspendLayout();
                visPopulation.BeginUpdate();
                chartResults.BeginUpdate();
                try
                {
                    if (!value)
                    {
                        Font = UIFont;

                        txtSystemESDL.Font = CodeFont;
                        txtSystemPython.Font = CodeFont;
                        txtSystemVariables.Font = CodeFont;
                        txtLandscapeParameters.Font = CodeFont;
                        txtEvaluatorCode.Font = CodeFont;
                        txtLog.Font = CodeFont;
                        txtBestIndividual.Font = CodeFont;

                        txtPlotExpression.Font = CodeFont;
                        txtBestIndividualExpression.Font = CodeFont;

                        lblStopAfter.Visible = true;
                        lblOr1.Visible = true;
                        lblOr2.Visible = true;
                        lblOr3.Visible = true;

                        ActiveChartStyles = NormalChartStyles;
                        ActiveVisualiserStyle = NormalVisualiserStyle;
                        chartResults.HorizontalAxisThickness = AxisThickness;
                        chartResults.VerticalAxisThickness = AxisThickness;
                        visPopulation.HorizontalAxisThickness = AxisThickness;
                        visPopulation.VerticalAxisThickness = AxisThickness;

                        WindowState = NormalWindowState;
                    }
                    else
                    {
                        NormalWindowState = WindowState;

                        Font = ProjectorUIFont;

                        txtSystemESDL.Font = ProjectorCodeFont;
                        txtSystemPython.Font = ProjectorCodeFont;
                        txtSystemVariables.Font = ProjectorCodeFont;
                        txtLandscapeParameters.Font = ProjectorCodeFont;
                        txtEvaluatorCode.Font = ProjectorCodeFont;
                        txtLog.Font = ProjectorCodeFont;
                        txtBestIndividual.Font = ProjectorCodeFont;

                        txtPlotExpression.Font = CodeFont;
                        txtBestIndividualExpression.Font = CodeFont;

                        lblStopAfter.Visible = false;
                        lblOr1.Visible = false;
                        lblOr2.Visible = false;
                        lblOr3.Visible = false;

                        ActiveChartStyles = ProjectorChartStyles;
                        ActiveVisualiserStyle = ProjectorVisualiserStyle;
                        chartResults.HorizontalAxisThickness = ProjectorAxisThickness;
                        chartResults.VerticalAxisThickness = ProjectorAxisThickness;
                        visPopulation.HorizontalAxisThickness = ProjectorAxisThickness;
                        visPopulation.VerticalAxisThickness = ProjectorAxisThickness;

                        WindowState = FormWindowState.Maximized;
                    }
                }
                finally
                {
                    visPopulation.EndUpdate();
                    chartResults.EndUpdate();
                    ResumeLayout(true);
                    Editor_ClientSizeChanged(this, EventArgs.Empty);
                    Refresh();
                }
            }
        }

        private void menuViewProjectorMode_Click(object sender, EventArgs e)
        {
            ProjectorMode = !ProjectorMode;
            menuViewProjectorMode.Checked = ProjectorMode;
        }

        #endregion

        #region Chart Styles

        private VisualiserPointStyle ActiveVisualiserStyle
        {
            set { visPopulation.SetSeriesStyle(0, value); }
        }
        private VisualiserPointStyle NormalVisualiserStyle;
        private VisualiserPointStyle ProjectorVisualiserStyle;

        private IDictionary<string, VisualiserPointStyle> ActiveChartStyles
        {
            set
            {
                foreach (var kv in value)
                {
                    chartResults.Series[SeriesNames[kv.Key]].Style = kv.Value;
                    foreach (var cb in new[] { chkChartBestFitness, chkChartCurrentBest, chkChartCurrentMean, chkChartCurrentWorst })
                    {
                        if ((string)cb.Tag == kv.Key) cb.BackColor = kv.Value.LineColor;
                    }
                }
                chartResults.Refresh();
            }
        }

        private Dictionary<string, VisualiserPointStyle> NormalChartStyles;
        private Dictionary<string, VisualiserPointStyle> ProjectorChartStyles;
        private Dictionary<string, int> SeriesNames;

        private void chkChartSeries_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            chartResults.ShowSeries(SeriesNames[(string)checkbox.Tag], checkbox.Checked);
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

            EsecOverrides.AddOverrides(esec);
            
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
            LookEnabled();
            menuStrip.Enabled = true;
            UseWaitCursor = false;

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

        const string DefaultCustomEvaluator = @"fitness = 0.0
for x in indiv:
    fitness += x**2

return FitnessMinimise(fitness)";

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

        private string GetCustomEvaluator()
        {
            if (lstLandscapes.SelectedNode.Name == "Custom")
            {
                var codeLines = txtEvaluatorCode.Text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => "        " + s)
                    .Aggregate((sum, s) => sum + "\r\n" + s);

                var code = EvaluatorTemplate + codeLines;

                return code;
            }
            else
            {
                return null;
            }
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

        private void menuCheckSyntax_Click(object sender, EventArgs e)
        {
            CheckSyntax();
        }

        private void CheckSyntax()
        {
            UseWaitCursor = true;

            // Reset everything
            lstErrors.Items.Clear();
            txtSystemESDL.BeginUpdate();
            txtSystemVariables.BeginUpdate();
            txtSystemESDL.Document.MarkerStrategy.RemoveAll(_ => true);
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
                dynamic scope = Python.CreateScope();
                scope.esec = esec;
                scope.esdlc = esdlc;
                Python.Exec((string)state[1], scope);

                dynamic ast = scope.esdlc.compileESDL((string)state[0], (List<string>)state[2]);
                return ast;
            }), new object[] { txtSystemESDL.Text, txtSystemPython.Text, externs })
            .ContinueWith((Action<Task<dynamic>>)(task =>
            {
                dynamic ast = task.Result;
                foreach (var error in ast._errors)
                {
                    lstErrors.Items.Add(ErrorItem.FromPython(txtSystemESDL, error));
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
                txtSystemESDL.EndUpdate();
                txtSystemESDL.Refresh();
                txtSystemVariables.EndUpdate();
                txtSystemVariables.Refresh();
            }), guiScheduler);
        }

        #endregion

        #region Monitor Callbacks

        private dynamic CurrentBestIndividual;

        public void UpdateStats(int iterations, int evaluations, int births, TimeSpan time,
            dynamic bestIndiv,
            dynamic bestFitness, dynamic currentBest, dynamic currentMean, dynamic currentWorst)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                Invoke((Action)(() => UpdateStats(iterations, evaluations, births, time,
                    bestIndiv, bestFitness, currentBest, currentMean, currentWorst)));
                return;
            }

            CurrentBestIndividual = bestIndiv;
            UpdateBestIndividual();

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
                chartResults.Add(new VisualiserPoint(iterations, value, 0.0), 0);
            }
            if (currentBest != null)
            {
                double value = (double)currentBest.simple;
                value = (value < min) ? min : (value > max) ? max : value;
                chartResults.Add(new VisualiserPoint(iterations, value, 0.0), 1);
            }
            if (currentMean != null)
            {
                double value = (double)currentMean.simple;
                value = (value < min) ? min : (value > max) ? max : value;
                chartResults.Add(new VisualiserPoint(iterations, value, 0.0), 2);
            }
            if (currentWorst != null)
            {
                double value = (double)currentWorst.simple;
                value = (value < min) ? min : (value > max) ? max : value;
                chartResults.Add(new VisualiserPoint(iterations, value, 0.0), 3);
            }
        }

        private bool DisableVisualisation;

        public void UpdateVisualisation(IEnumerable<dynamic> population, bool firstRun = false)
        {
            if (population == null) return;
            if (DisableVisualisation) return;

            var expr = Python.CompileExpression(txtPlotExpression.Text);
            var scope = Python.CreateScope();
            Python.Exec("from math import *", scope);

            List<VisualiserPoint> points;
            try
            {
                points = population
                    .OrderByDescending(indiv => indiv.fitness.simple)
                    .Select((indiv, i) =>
                        {
                            scope.SetVariable("indiv", (object)indiv);
                            scope.SetVariable("i", (object)i);
                            return Python.Eval(expr, scope);
                        })
                    .Select(pair =>
                        new VisualiserPoint(
                            (double)pair[0],
                            (double)pair[1],
                            (double)(pair.__len__() < 3 ? 0.0 : pair[2])))
                    .ToList();
            }
            catch
            {
                DisableVisualisation = true;
                return;
            }

            Action<List<VisualiserPoint>> continuation = pts =>
            {
                if (firstRun) visPopulation.AutoRangeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                else visPopulation.AutoRangeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;

                visPopulation.SetPoints(pts);
            };

            if (InvokeRequired) BeginInvoke(continuation, points);
            else continuation(points);

        }

        #endregion

        #region Best Individual Display

        private void txtBestIndividualExpression_TextChanged(object sender, EventArgs e)
        {
            UpdateBestIndividual();
        }

        private void txtBestIndividual_VisibleChanged(object sender, EventArgs e)
        {
            UpdateBestIndividual();
        }

        private void UpdateBestIndividual()
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                Invoke((Action)UpdateBestIndividual);
                return;
            }

            if (CurrentBestIndividual == null)
            {
                txtBestIndividual.ResetText();
                return;
            }

            if (txtBestIndividual.Visible == false) return;

            try
            {
                var scope = Python.CreateScope();
                Python.Exec("from math import *", scope);
                scope.SetVariable("indiv", (object)CurrentBestIndividual);

                var expr = string.IsNullOrWhiteSpace(txtBestIndividualExpression.Text)
                    ? "indiv.genome_string" : txtBestIndividualExpression.Text;
                expr = "str(" + expr + ")";

                txtBestIndividual.Text = Python.Eval(expr, scope).Replace("\n", "\r\n");
            }
            catch
            {
                txtBestIndividual.Text = "(Invalid expression: " + txtBestIndividualExpression.Text + ")";
            }
        }


        #endregion

        #region Start an Experiment

        private Task CurrentExperiment;
        private Monitor CurrentMonitor;

        private void menuControlStartPause_Click(object sender, EventArgs e)
        {
            if (picDimmer.Visible) return;

            if (IsExperimentRunning)
            {
                btnPause.Checked = !btnPause.Checked;
            }
            else
            {
                chkResults.Checked = true;
                StartExperiment();
            }
        }

        private void menuControlStep_Click(object sender, EventArgs e)
        {
            if (IsExperimentRunning)
            {
                CurrentMonitor.SingleStep();
                btnPause.Checked = true;
            }
            else
            {
                btnPause.Checked = true;
                StartExperiment(true);
            }
        }

        private void menuControlStop_Click(object sender, EventArgs e)
        {
            if (IsExperimentRunning)
            {
                StopExperiment();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartExperiment();
        }

        private void btnPause_CheckedChanged(object sender, EventArgs e)
        {
            if (IsExperimentRunning)
            {
                CurrentMonitor.IsPaused = btnPause.Checked;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (IsExperimentRunning)
            {
                StopExperiment();
            }
        }

        private bool IsExperimentRunning
        {
            get { return CurrentMonitor != null && CurrentExperiment != null && !CurrentExperiment.IsCompleted; }
        }

        private void StopExperiment()
        {
            CurrentMonitor.Cancel();
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnPause.Checked = false;
            btnStop.Enabled = false;
        }

        private void StartExperiment(bool singleStep = false)
        {
            UseWaitCursor = true;
            btnStart.Enabled = false;
            bool completed = false;

            try
            {
                if (txtSystemESDL.Text.Length == 0)
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

                CurrentMonitor = new Monitor(this);
                if (singleStep) CurrentMonitor.IsPaused = true;
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
                if (lstLandscapes.SelectedNode.Name != "Custom")
                {
                    landscape["class"] = lstLandscapes.SelectedNode.Tag;
                }

                var state = new Dictionary<string, object>();
                state["landscape"] = landscape;
                state["landscape.evaluator"] = GetCustomEvaluator();
                state["monitor"] = CurrentMonitor;
                state["system.description"] = txtSystemESDL.Text;
                state["system"] = variables;
                state["random_seed"] = 12345;       // TODO: Settable random seed
                state["preamble"] = txtSystemPython.Text;

                chartResults.ClearAll(true);
                chartResults.ShowSeries(0, chkChartBestFitness.Checked);
                chartResults.ShowSeries(1, chkChartCurrentBest.Checked);
                chartResults.ShowSeries(2, chkChartCurrentMean.Checked);
                chartResults.ShowSeries(3, chkChartCurrentWorst.Checked);

                txtPlotExpression.Enabled = false;
                DisableVisualisation = false;
                visPopulation.ClearAll(true);

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
                noErrTask.ContinueWith(Task_RunExperiment_Finally, TaskScheduler.FromCurrentSynchronizationContext());
                errTask.ContinueWith(Task_RunExperiment_Finally, TaskScheduler.FromCurrentSynchronizationContext());

                lstConfigurations.Enabled = false;
                btnPause.Enabled = true;
                btnStop.Enabled = true;

                chkResults.Checked = true;
                CurrentExperiment.Start();

                completed = true;
            }
            finally
            {
                if (!completed)
                {
                    UseWaitCursor = false;  // wait cursor is disabled after initialistion
                    btnStart.Enabled = true;
                }
            }

        }

        private void Task_RunExperiment(object state_obj)
        {
            var state = (Dictionary<string, object>)state_obj;
            var scopeObj = Python.CreateScope();
            dynamic scope = scopeObj;
            scope.esec = esec;
            scope.esdlc = esdlc;
            Python.Exec((string)state["preamble"], scope);

            var config = Python.Dict();

            config["monitor"] = state["monitor"];
            config["landscape"] = Python.Dict(state["landscape"]);
            var evaluator = state["landscape.evaluator"] as string;
            if (!string.IsNullOrEmpty(evaluator))
            {
                Python.Exec(evaluator, scope);
                ((dynamic)config["landscape"])["instance"] = scope.CustomEvaluator;
            }

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
                foreach (var name in scopeObj.GetVariableNames())
                {
                    if (!Python.Scope.ContainsVariable(name))
                    {
                        systemDict[name] = scopeObj.GetVariable(name);
                    }
                }
            }

            config["random_seed"] = state["random_seed"];

            dynamic exp;
            try
            {
                exp = scope.esec.Experiment(config);
            }
            catch (Exception ex)
            {
                // If the debugger breaks here saying the exception is unhandled, continue (press F5).
                // Alternatively, you can disable "Just My Code" in the debugging options.
                throw new Exception("Experiment.__init__", ex);
            }

            UseWaitCursor = false;
            exp.run();
        }

        private void Task_RunExperiment_Completed(Task task)
        { }

        private void Task_RunExperiment_NotCompleted(Task task)
        {
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
        }

        private void Task_RunExperiment_Finally(Task task)
        {
            txtPlotExpression.Enabled = true;
            lstConfigurations.Enabled = true;

            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            CurrentMonitor = null;
            if (CurrentExperiment != null) CurrentExperiment.Dispose();
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
            var chk = (CheckBox)sender;

            chk.ForeColor = SystemColors.Highlight;
        }

        private void chkLimit_MouseLeave(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;

            chk.ForeColor = SystemColors.WindowText;
        }

        private void panelMenu_Layout(object sender, LayoutEventArgs e)
        {
            panelMenu_SizeChanged(sender, EventArgs.Empty);
        }

        private void panelMenu_SizeChanged(object sender, EventArgs e)
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
            Image oldImage = null;
            if (picDimmer.Visible) oldImage = picDimmer.Image;

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

            if (oldImage != null) oldImage.Dispose();
        }

        private void LookEnabled()
        {
            var dimmed = picDimmer.Image;
            picDimmer.Image = null;
            picDimmer.Visible = false;
            dimmed.Dispose();
        }

        bool Resizing = false;
        private void Editor_ClientSizeChanged(object sender, EventArgs e)
        {
            picDimmer.SetBounds(0, 0, ClientSize.Width, ClientSize.Height);
            if (!Resizing && picDimmer.Visible) LookDisabled();
        }

        private void Editor_ResizeBegin(object sender, EventArgs e)
        {
            Resizing = true;
        }

        private void Editor_ResizeEnd(object sender, EventArgs e)
        {
            Resizing = false;
            Editor_ClientSizeChanged(sender, e);
            //PerformLayout();
        }

        private void txtExpression_Enter(object sender, EventArgs e)
        {
            if (ProjectorMode)
            {
                ((TextBox)sender).Font = ProjectorCodeFont;
            }
        }

        private void txtExpression_Leave(object sender, EventArgs e)
        {
            if (ProjectorMode)
            {
                ((TextBox)sender).Font = CodeFont;
            }
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
                    if (lstConfigurations.Items.Count == 10) break;
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

            chartResults.ClearAll(true);
            visPopulation.ClearAll(true);

            UpdateEditor(config);

            CurrentConfiguration = config;
        }

        private void UpdateConfig()
        {
            CurrentConfiguration.Definition = txtSystemESDL.Text;
            CurrentConfiguration.Support = txtSystemPython.Text;
            CurrentConfiguration.SystemParameters = txtSystemVariables.Text;
            CurrentConfiguration.Landscape = (lstLandscapes.SelectedNode ?? lstLandscapes.Nodes["Custom"]).Name;
            CurrentConfiguration.LandscapeParameters = txtLandscapeParameters.Text;
            CurrentConfiguration.CustomEvaluator = txtEvaluatorCode.Text;

            CurrentConfiguration.PlotExpression = txtPlotExpression.Text;
            CurrentConfiguration.BestIndividualExpression = txtBestIndividualExpression.Text;

            CurrentConfiguration.IterationLimit = chkIterations.Checked ? int.Parse(txtIterations.Text) : (int?)null;
            CurrentConfiguration.EvaluationLimit = chkEvaluations.Checked ? int.Parse(txtEvaluations.Text) : (int?)null;
            CurrentConfiguration.TimeLimit = chkSeconds.Checked ? TimeSpan.FromSeconds(double.Parse(txtSeconds.Text)) : (TimeSpan?)null;
            CurrentConfiguration.FitnessLimit = chkFitness.Checked ? double.Parse(txtFitness.Text) : (double?)null;
        }

        private void UpdateEditor(Configuration config)
        {
            txtSystemESDL.Text = config.Definition;
            txtSystemESDL.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystemESDL.Refresh();

            txtSystemPython.Text = config.Support;
            txtSystemPython.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystemPython.Refresh();

            txtSystemVariables.Text = config.SystemParameters;
            txtSystemVariables.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystemVariables.Refresh();

            lstLandscapes.SelectedNode = lstLandscapes.Nodes.Find(config.Landscape, true).FirstOrDefault();
            lstLandscapes.Refresh();

            txtLandscapeParameters.Text = config.LandscapeParameters;
            txtLandscapeParameters.Document.MarkerStrategy.RemoveAll(_ => true);
            txtLandscapeParameters.Refresh();

            txtEvaluatorCode.Text = config.CustomEvaluator;
            txtEvaluatorCode.Document.MarkerStrategy.RemoveAll(_ => true);
            txtEvaluatorCode.Refresh();

            txtPlotExpression.Text = config.PlotExpression;
            txtBestIndividualExpression.Text = config.BestIndividualExpression;

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

            txtSystemESDL.ResetText();
            txtSystemESDL.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystemESDL.Refresh();

            txtSystemPython.ResetText();
            txtSystemPython.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystemPython.Refresh();

            txtSystemVariables.ResetText();
            txtSystemVariables.Document.MarkerStrategy.RemoveAll(_ => true);
            txtSystemVariables.Refresh();

            lstLandscapes.SelectedNode = lstLandscapes.Nodes["Custom"];
            lstLandscapes.Refresh();

            txtLandscapeParameters.ResetText();
            txtLandscapeParameters.Document.MarkerStrategy.RemoveAll(_ => true);
            txtLandscapeParameters.Refresh();

            txtEvaluatorCode.Text = DefaultCustomEvaluator;
            txtEvaluatorCode.Document.MarkerStrategy.RemoveAll(_ => true);
            txtEvaluatorCode.Refresh();

            txtPlotExpression.Text = Configuration.DefaultPlotExpression;
            txtBestIndividualExpression.Text = Configuration.DefaultBestIndividualExpression;

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
            config.Source = path.FullName;

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

        private void menuViewSubview1_Click(object sender, EventArgs e)
        {
            if (chkSystem.Checked)
                tabSourceView.SelectTab(tabSourceESDL);
            else if (chkResults.Checked)
                tabResultView.SelectTab(tabChart);
        }

        private void menuViewSubview2_Click(object sender, EventArgs e)
        {
            if (chkSystem.Checked)
                tabSourceView.SelectTab(tabSourcePython);
            else if (chkResults.Checked)
                tabResultView.SelectTab(tab2DPlot);
        }

        private void menuViewSubview3_Click(object sender, EventArgs e)
        {
            if (chkResults.Checked)
                tabResultView.SelectTab(tabBestIndividual);
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
            if (picDimmer.Visible) LookDisabled();
        }


        #endregion

        #region Python Support Definitions

        void UpdatePythonDefinitions(object sender, ICSharpCode.TextEditor.Document.DocumentEventArgs e)
        {
            lstPythonDefinitions.BeginUpdate();
            try
            {
                lstPythonDefinitions.Items.Clear();

                var sr = new StringReader(e.Document.TextContent);
                int lineNo = 0;
                for (var line = sr.ReadLine(); line != null; line = sr.ReadLine(), lineNo += 1)
                {
                    if (line.Length == 0) continue;
                    if (char.IsWhiteSpace(line[0])) continue;

                    if (line.StartsWith("def ") || line.StartsWith("class "))
                    {
                        var text = line;
                        int i = text.IndexOf('(');
                        if (i > 0) text = text.Substring(0, i).Trim();
                        i = text.IndexOf(':');
                        if (i > 0) text = text.Substring(0, i).Trim();
                        i = text.IndexOf(' ');
                        if (i > 0) text = text.Substring(i + 1).Trim();

                        lstPythonDefinitions.Items.Add(new ListViewItem
                        {
                            Name = line,
                            Text = text,
                            ImageKey = line.StartsWith("def ") ? "VSObject_Method.bmp" : "VSObject_Class.bmp",
                            Tag = lineNo
                        });
                    }
                    else if (line.StartsWith("#"))
                    {
                        continue;
                    }
                    else
                    {
                        int i = line.IndexOf('=');
                        if (i < 0) continue;

                        line = line.Substring(0, i).Trim();

                        lstPythonDefinitions.Items.Add(new ListViewItem
                        {
                            Name = line,
                            Text = line,
                            ImageKey = "VSObject_Constant.bmp",
                            Tag = lineNo
                        });
                    }
                }
            }
            finally
            {
                lstPythonDefinitions.EndUpdate();
            }
        }

        private void lstPythonDefinitions_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                int line = (int)lstPythonDefinitions.SelectedItems[0].Tag;
                var lineLength = txtSystemPython.Document.GetLineSegment(line).Length;
                var pos = new ICSharpCode.TextEditor.TextLocation(0, line);
                txtSystemPython.ActiveTextAreaControl.Caret.Position = pos;
                txtSystemPython.ActiveTextAreaControl.SelectionManager.SetSelection(
                    pos, new ICSharpCode.TextEditor.TextLocation(lineLength, line));
                txtSystemPython.ActiveTextAreaControl.CenterViewOn(line, 5);
                txtSystemPython.Focus();
            }
            catch (Exception ex)
            {
                Log("Error selecting definition:\r\n{0}", ex.ToString());
            }
        }

        #endregion


    }
}
