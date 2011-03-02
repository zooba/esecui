using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VisualiserLib;

namespace esecui
{
    public partial class Editor
    {
        #region Projector Mode

        private static readonly string[] Styles = new[] { "BestFitness", "CurrentBest", "CurrentMean", "CurrentWorst" };

        private List<Control> CodeFontControls;

        private Font UIFont;
        private Font ProjectorUIFont;
        private Font CodeFont;
        private Font ProjectorCodeFont;
        private double AxisThickness;
        private double ProjectorAxisThickness;
        private Color GridColor;
        private Color ProjectorGridColor;
        private double GridThickness;
        private double ProjectorGridThickness;

        private FormWindowState NormalWindowState;
        private FormBorderStyle NormalBorderStyle;

        private void InitialiseDisplay(bool projectorMode)
        {
            var settings = Properties.Settings.Default;

            // Set the "normal" values
            NormalWindowState = WindowState;
            NormalBorderStyle = FormBorderStyle;

            // Initialise the UI font
            UIFont = new Font("Segoe UI", settings.UIFontSize);
            if (UIFont.Name != UIFont.OriginalFontName)
            {
                UIFont = new Font("Tahoma", settings.UIFontSize);
            }
            if (UIFont.Name != UIFont.OriginalFontName)
            {
                UIFont = new Font(FontFamily.GenericSansSerif, settings.UIFontSize);
            }
            ProjectorUIFont = new Font(UIFont.Name, settings.UIFontSizeProjector);

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
            CodeFont = new Font("Consolas", settings.CodeFontSize);
            if (CodeFont.Name != CodeFont.OriginalFontName)
            {
                CodeFont = new Font(FontFamily.GenericMonospace, settings.CodeFontSize);
            }
            ProjectorCodeFont = new Font(CodeFont.Name, settings.CodeFontSizeProjector);

            // Specify which controls use the bigger/smaller font
            CodeFontControls = new List<Control>()
            {
                txtSystemESDL,
                txtSystemPython,
                txtSystemVariables,
                txtLandscapeParameters,
                txtEvaluatorCode,
                txtLog,
                txtBestIndividual
            };

            // Force some controls to always use the smaller font
            txtPlotExpression.Font = CodeFont;
            txtBestIndividualExpression.Font = CodeFont;

            // Initialise the chart styles
            AxisThickness = settings.AxisThickness;
            ProjectorAxisThickness = settings.AxisThicknessProjector;

            GridColor = settings.GridColor;
            ProjectorGridColor = settings.GridColorProjector;
            GridThickness = settings.GridThickness;
            ProjectorGridThickness = settings.GridThicknessProjector;

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

        private void DisposeDisplay()
        {
            if (UIFont != null) UIFont.Dispose();
            UIFont = null;
            if (ProjectorUIFont != null) ProjectorUIFont.Dispose();
            ProjectorUIFont = null;
            if (CodeFont != null) CodeFont.Dispose();
            CodeFont = null;
            if (ProjectorCodeFont != null) ProjectorCodeFont.Dispose();
            ProjectorCodeFont = null;
        }

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
                    if (!_ProjectorMode)
                    {
                        Font = UIFont;
                        foreach (var c in CodeFontControls) c.Font = CodeFont;

                        lblStopAfter.Visible = true;
                        lblOr1.Visible = true;
                        lblOr2.Visible = true;
                        lblOr3.Visible = true;

                        ActiveChartStyles = NormalChartStyles;
                        ActiveVisualiserStyle = NormalVisualiserStyle;
                        chartResults.HorizontalAxisThickness = AxisThickness;
                        chartResults.VerticalAxisThickness = AxisThickness;
                        chartResults.GridColor = GridColor;
                        chartResults.GridThickness = GridThickness;
                        visPopulation.HorizontalAxisThickness = AxisThickness;
                        visPopulation.VerticalAxisThickness = AxisThickness;
                        visPopulation.GridColor = GridColor;
                        visPopulation.GridThickness = GridThickness;

                        WindowState = NormalWindowState;
                        FormBorderStyle = NormalBorderStyle;
                    }
                    else
                    {
                        NormalWindowState = WindowState;
                        NormalBorderStyle = FormBorderStyle;

                        Font = ProjectorUIFont;
                        foreach (var c in CodeFontControls) c.Font = ProjectorCodeFont;

                        lblStopAfter.Visible = false;
                        lblOr1.Visible = false;
                        lblOr2.Visible = false;
                        lblOr3.Visible = false;

                        ActiveChartStyles = ProjectorChartStyles;
                        ActiveVisualiserStyle = ProjectorVisualiserStyle;
                        chartResults.HorizontalAxisThickness = ProjectorAxisThickness;
                        chartResults.VerticalAxisThickness = ProjectorAxisThickness;
                        chartResults.GridColor = ProjectorGridColor;
                        chartResults.GridThickness = ProjectorGridThickness;
                        visPopulation.HorizontalAxisThickness = ProjectorAxisThickness;
                        visPopulation.VerticalAxisThickness = ProjectorAxisThickness;
                        visPopulation.GridColor = ProjectorGridColor;
                        visPopulation.GridThickness = ProjectorGridThickness;

                        WindowState = FormWindowState.Maximized;
                        FormBorderStyle = FormBorderStyle.None;
                    }
                }
                finally
                {
                    visPopulation.EndUpdate();
                    chartResults.EndUpdate();
                    ResumeLayout(true);
                    OnClientSizeChanged(EventArgs.Empty);
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

        #region Projector Mode Dynamic Controls

        private void txtExpression_Enter(object sender, EventArgs e)
        {
            if (ProjectorMode)
            {
                ((TextBox)sender).Font = ProjectorCodeFont;
            }
        }

        private void txtExpression_Leave(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb.Font != CodeFont)
            {
                tb.Font = CodeFont;
            }
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

    }
}
