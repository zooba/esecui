namespace esecui
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator menuConfigurationSeparator1;
            System.Windows.Forms.ToolStripSeparator menuConfigurationSeparator2;
            System.Windows.Forms.ToolStripSeparator menuConfigurationSeparator3;
            System.Windows.Forms.ToolStripSeparator menuViewSeparator1;
            System.Windows.Forms.ToolStripSeparator menuViewSeparator2;
            System.Windows.Forms.ToolStripSeparator menuViewSeparator3;
            System.Windows.Forms.ToolStripSeparator menuViewSeparator4;
            System.Windows.Forms.ToolStripSeparator menuViewSeparator5;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Example assignment", "VSObject_Constant.bmp");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Example class", "VSObject_Class.bmp");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Example function", "VSObject_Method.bmp");
            this.splitSystemErrors = new esecui.CompatibleSplitContainer();
            this.tabSourceView = new System.Windows.Forms.TabControl();
            this.tabSourceESDL = new System.Windows.Forms.TabPage();
            this.splitSystemVariables = new esecui.CompatibleSplitContainer();
            this.txtSystemESDL = new ICSharpCode.TextEditor.TextEditorControl();
            this.txtSystemVariables = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabSourcePython = new System.Windows.Forms.TabPage();
            this.splitPythonDefinitions = new esecui.CompatibleSplitContainer();
            this.txtSystemPython = new ICSharpCode.TextEditor.TextEditorControl();
            this.lstPythonDefinitions = new System.Windows.Forms.ListView();
            this.imlPythonDefinitions = new System.Windows.Forms.ImageList(this.components);
            this.lstErrors = new System.Windows.Forms.ListView();
            this.colLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstConfigurations = new System.Windows.Forms.ComboBox();
            this.splitLandscape = new esecui.CompatibleSplitContainer();
            this.lstLandscapes = new System.Windows.Forms.TreeView();
            this.tableLandscape = new System.Windows.Forms.TableLayoutPanel();
            this.txtEvaluatorCode = new ICSharpCode.TextEditor.TextEditorControl();
            this.lblLandscapeInternalName = new System.Windows.Forms.Label();
            this.txtLandscapeInternalName = new System.Windows.Forms.TextBox();
            this.lblLandscapeDescription = new System.Windows.Forms.Label();
            this.txtLandscapeDescription = new System.Windows.Forms.TextBox();
            this.lblLandscapeParameters = new System.Windows.Forms.Label();
            this.txtLandscapeParameters = new ICSharpCode.TextEditor.TextEditorControl();
            this.lblEvaluatorCode = new System.Windows.Forms.Label();
            this.splitLimitsGraph = new esecui.CompatibleSplitContainer();
            this.tableResults = new System.Windows.Forms.TableLayoutPanel();
            this.tableControls = new System.Windows.Forms.TableLayoutPanel();
            this.chkIterations = new System.Windows.Forms.CheckBox();
            this.chkFitness = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.CheckBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.chkSeconds = new System.Windows.Forms.CheckBox();
            this.chkEvaluations = new System.Windows.Forms.CheckBox();
            this.txtFitness = new System.Windows.Forms.TextBox();
            this.txtEvaluations = new System.Windows.Forms.TextBox();
            this.txtSeconds = new System.Windows.Forms.TextBox();
            this.txtIterations = new System.Windows.Forms.TextBox();
            this.lblStopAfter = new System.Windows.Forms.Label();
            this.lblOr1 = new System.Windows.Forms.Label();
            this.lblOr3 = new System.Windows.Forms.Label();
            this.lblOr2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitGraphStats = new esecui.CompatibleSplitContainer();
            this.tabResultView = new System.Windows.Forms.TabControl();
            this.tabChart = new System.Windows.Forms.TabPage();
            this.chartResults = new VisualiserLib.Visualiser();
            this.tab2DPlot = new System.Windows.Forms.TabPage();
            this.table2DPlot = new System.Windows.Forms.TableLayoutPanel();
            this.visPopulation = new VisualiserLib.Visualiser();
            this.lblPlotExpression = new System.Windows.Forms.Label();
            this.txtPlotExpression = new System.Windows.Forms.TextBox();
            this.tabBestIndividual = new System.Windows.Forms.TabPage();
            this.tableBestIndividual = new System.Windows.Forms.TableLayoutPanel();
            this.lblBestIndividualExpression = new System.Windows.Forms.Label();
            this.txtBestIndividualExpression = new System.Windows.Forms.TextBox();
            this.txtBestIndividual = new System.Windows.Forms.TextBox();
            this.tableStats = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStatsIterations = new System.Windows.Forms.TextBox();
            this.txtStatsEvaluations = new System.Windows.Forms.TextBox();
            this.txtStatsBirths = new System.Windows.Forms.TextBox();
            this.txtStatsTime = new System.Windows.Forms.TextBox();
            this.txtStatsBestFitness = new System.Windows.Forms.TextBox();
            this.txtStatsCurrentBest = new System.Windows.Forms.TextBox();
            this.txtStatsCurrentMean = new System.Windows.Forms.TextBox();
            this.txtStatsCurrentWorst = new System.Windows.Forms.TextBox();
            this.chkChartCurrentWorst = new System.Windows.Forms.CheckBox();
            this.chkChartCurrentMean = new System.Windows.Forms.CheckBox();
            this.chkChartCurrentBest = new System.Windows.Forms.CheckBox();
            this.chkChartBestFitness = new System.Windows.Forms.CheckBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCheckSyntax = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewLandscape = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewResults = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewSubview1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewSubview2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewSubview3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewProjectorMode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuControl = new System.Windows.Forms.ToolStripMenuItem();
            this.menuControlStartPause = new System.Windows.Forms.ToolStripMenuItem();
            this.menuControlStop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuControlStep = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSystem = new System.Windows.Forms.RadioButton();
            this.chkLandscape = new System.Windows.Forms.RadioButton();
            this.chkResults = new System.Windows.Forms.RadioButton();
            this.chkLog = new System.Windows.Forms.RadioButton();
            this.panelSystem = new System.Windows.Forms.Panel();
            this.panelLandscape = new System.Windows.Forms.Panel();
            this.panelResults = new System.Windows.Forms.Panel();
            this.panelLog = new System.Windows.Forms.Panel();
            this.txtLog = new ICSharpCode.TextEditor.TextEditorControl();
            this.picDimmer = new System.Windows.Forms.PictureBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            menuConfigurationSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            menuConfigurationSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            menuConfigurationSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            menuViewSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            menuViewSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            menuViewSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            menuViewSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            menuViewSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitSystemErrors)).BeginInit();
            this.splitSystemErrors.Panel1.SuspendLayout();
            this.splitSystemErrors.Panel2.SuspendLayout();
            this.splitSystemErrors.SuspendLayout();
            this.tabSourceView.SuspendLayout();
            this.tabSourceESDL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSystemVariables)).BeginInit();
            this.splitSystemVariables.Panel1.SuspendLayout();
            this.splitSystemVariables.Panel2.SuspendLayout();
            this.splitSystemVariables.SuspendLayout();
            this.tabSourcePython.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPythonDefinitions)).BeginInit();
            this.splitPythonDefinitions.Panel1.SuspendLayout();
            this.splitPythonDefinitions.Panel2.SuspendLayout();
            this.splitPythonDefinitions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitLandscape)).BeginInit();
            this.splitLandscape.Panel1.SuspendLayout();
            this.splitLandscape.Panel2.SuspendLayout();
            this.splitLandscape.SuspendLayout();
            this.tableLandscape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitLimitsGraph)).BeginInit();
            this.splitLimitsGraph.Panel1.SuspendLayout();
            this.splitLimitsGraph.Panel2.SuspendLayout();
            this.splitLimitsGraph.SuspendLayout();
            this.tableResults.SuspendLayout();
            this.tableControls.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGraphStats)).BeginInit();
            this.splitGraphStats.Panel1.SuspendLayout();
            this.splitGraphStats.Panel2.SuspendLayout();
            this.splitGraphStats.SuspendLayout();
            this.tabResultView.SuspendLayout();
            this.tabChart.SuspendLayout();
            this.tab2DPlot.SuspendLayout();
            this.table2DPlot.SuspendLayout();
            this.tabBestIndividual.SuspendLayout();
            this.tableBestIndividual.SuspendLayout();
            this.tableStats.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelSystem.SuspendLayout();
            this.panelLandscape.SuspendLayout();
            this.panelResults.SuspendLayout();
            this.panelLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDimmer)).BeginInit();
            this.SuspendLayout();
            // 
            // menuConfigurationSeparator1
            // 
            menuConfigurationSeparator1.Name = "menuConfigurationSeparator1";
            menuConfigurationSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // menuConfigurationSeparator2
            // 
            menuConfigurationSeparator2.Name = "menuConfigurationSeparator2";
            menuConfigurationSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // menuConfigurationSeparator3
            // 
            menuConfigurationSeparator3.Name = "menuConfigurationSeparator3";
            menuConfigurationSeparator3.Size = new System.Drawing.Size(192, 6);
            // 
            // menuViewSeparator1
            // 
            menuViewSeparator1.Name = "menuViewSeparator1";
            menuViewSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // menuViewSeparator2
            // 
            menuViewSeparator2.Name = "menuViewSeparator2";
            menuViewSeparator2.Size = new System.Drawing.Size(178, 6);
            // 
            // menuViewSeparator3
            // 
            menuViewSeparator3.Name = "menuViewSeparator3";
            menuViewSeparator3.Size = new System.Drawing.Size(178, 6);
            // 
            // menuViewSeparator4
            // 
            menuViewSeparator4.Name = "menuViewSeparator4";
            menuViewSeparator4.Size = new System.Drawing.Size(178, 6);
            // 
            // menuViewSeparator5
            // 
            menuViewSeparator5.Name = "menuViewSeparator5";
            menuViewSeparator5.Size = new System.Drawing.Size(178, 6);
            // 
            // splitSystemErrors
            // 
            this.splitSystemErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSystemErrors.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitSystemErrors.Location = new System.Drawing.Point(0, 0);
            this.splitSystemErrors.Name = "splitSystemErrors";
            this.splitSystemErrors.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitSystemErrors.Panel1
            // 
            this.splitSystemErrors.Panel1.Controls.Add(this.tabSourceView);
            // 
            // splitSystemErrors.Panel2
            // 
            this.splitSystemErrors.Panel2.Controls.Add(this.lstErrors);
            this.splitSystemErrors.Size = new System.Drawing.Size(963, 661);
            this.splitSystemErrors.SplitterDistance = 532;
            this.splitSystemErrors.TabIndex = 0;
            // 
            // tabSourceView
            // 
            this.tabSourceView.Controls.Add(this.tabSourceESDL);
            this.tabSourceView.Controls.Add(this.tabSourcePython);
            this.tabSourceView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSourceView.Location = new System.Drawing.Point(0, 0);
            this.tabSourceView.Multiline = true;
            this.tabSourceView.Name = "tabSourceView";
            this.tabSourceView.SelectedIndex = 0;
            this.tabSourceView.Size = new System.Drawing.Size(963, 532);
            this.tabSourceView.TabIndex = 9;
            // 
            // tabSourceESDL
            // 
            this.tabSourceESDL.Controls.Add(this.splitSystemVariables);
            this.tabSourceESDL.Location = new System.Drawing.Point(4, 24);
            this.tabSourceESDL.Name = "tabSourceESDL";
            this.tabSourceESDL.Padding = new System.Windows.Forms.Padding(3);
            this.tabSourceESDL.Size = new System.Drawing.Size(955, 504);
            this.tabSourceESDL.TabIndex = 0;
            this.tabSourceESDL.Text = "ESDL (Alt+1)";
            this.tabSourceESDL.UseVisualStyleBackColor = true;
            // 
            // splitSystemVariables
            // 
            this.splitSystemVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSystemVariables.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitSystemVariables.Location = new System.Drawing.Point(3, 3);
            this.splitSystemVariables.Name = "splitSystemVariables";
            // 
            // splitSystemVariables.Panel1
            // 
            this.splitSystemVariables.Panel1.Controls.Add(this.txtSystemESDL);
            // 
            // splitSystemVariables.Panel2
            // 
            this.splitSystemVariables.Panel2.Controls.Add(this.txtSystemVariables);
            this.splitSystemVariables.Size = new System.Drawing.Size(949, 498);
            this.splitSystemVariables.SplitterDistance = 652;
            this.splitSystemVariables.TabIndex = 0;
            // 
            // txtSystemESDL
            // 
            this.txtSystemESDL.ConvertTabsToSpaces = true;
            this.txtSystemESDL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSystemESDL.EnableFolding = false;
            this.txtSystemESDL.IsReadOnly = false;
            this.txtSystemESDL.Location = new System.Drawing.Point(0, 0);
            this.txtSystemESDL.Name = "txtSystemESDL";
            this.txtSystemESDL.ShowVRuler = false;
            this.txtSystemESDL.Size = new System.Drawing.Size(652, 498);
            this.txtSystemESDL.TabIndex = 0;
            this.txtSystemESDL.Text = resources.GetString("txtSystemESDL.Text");
            // 
            // txtSystemVariables
            // 
            this.txtSystemVariables.ConvertTabsToSpaces = true;
            this.txtSystemVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSystemVariables.EnableFolding = false;
            this.txtSystemVariables.IndentStyle = ICSharpCode.TextEditor.Document.IndentStyle.None;
            this.txtSystemVariables.IsReadOnly = false;
            this.txtSystemVariables.Location = new System.Drawing.Point(0, 0);
            this.txtSystemVariables.Name = "txtSystemVariables";
            this.txtSystemVariables.ShowLineNumbers = false;
            this.txtSystemVariables.ShowVRuler = false;
            this.txtSystemVariables.Size = new System.Drawing.Size(293, 498);
            this.txtSystemVariables.TabIndex = 0;
            this.txtSystemVariables.Text = "size: 50";
            // 
            // tabSourcePython
            // 
            this.tabSourcePython.Controls.Add(this.splitPythonDefinitions);
            this.tabSourcePython.Location = new System.Drawing.Point(4, 22);
            this.tabSourcePython.Name = "tabSourcePython";
            this.tabSourcePython.Padding = new System.Windows.Forms.Padding(3);
            this.tabSourcePython.Size = new System.Drawing.Size(955, 506);
            this.tabSourcePython.TabIndex = 1;
            this.tabSourcePython.Text = "Python (Alt+2)";
            this.tabSourcePython.UseVisualStyleBackColor = true;
            // 
            // splitPythonDefinitions
            // 
            this.splitPythonDefinitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPythonDefinitions.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitPythonDefinitions.Location = new System.Drawing.Point(3, 3);
            this.splitPythonDefinitions.Name = "splitPythonDefinitions";
            // 
            // splitPythonDefinitions.Panel1
            // 
            this.splitPythonDefinitions.Panel1.Controls.Add(this.txtSystemPython);
            // 
            // splitPythonDefinitions.Panel2
            // 
            this.splitPythonDefinitions.Panel2.Controls.Add(this.lstPythonDefinitions);
            this.splitPythonDefinitions.Size = new System.Drawing.Size(949, 500);
            this.splitPythonDefinitions.SplitterDistance = 726;
            this.splitPythonDefinitions.TabIndex = 2;
            // 
            // txtSystemPython
            // 
            this.txtSystemPython.ConvertTabsToSpaces = true;
            this.txtSystemPython.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSystemPython.EnableFolding = false;
            this.txtSystemPython.IsReadOnly = false;
            this.txtSystemPython.Location = new System.Drawing.Point(0, 0);
            this.txtSystemPython.Name = "txtSystemPython";
            this.txtSystemPython.ShowVRuler = false;
            this.txtSystemPython.Size = new System.Drawing.Size(726, 500);
            this.txtSystemPython.TabIndex = 1;
            // 
            // lstPythonDefinitions
            // 
            this.lstPythonDefinitions.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstPythonDefinitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPythonDefinitions.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lstPythonDefinitions.LabelWrap = false;
            this.lstPythonDefinitions.Location = new System.Drawing.Point(0, 0);
            this.lstPythonDefinitions.MultiSelect = false;
            this.lstPythonDefinitions.Name = "lstPythonDefinitions";
            this.lstPythonDefinitions.Size = new System.Drawing.Size(219, 500);
            this.lstPythonDefinitions.SmallImageList = this.imlPythonDefinitions;
            this.lstPythonDefinitions.TabIndex = 0;
            this.lstPythonDefinitions.UseCompatibleStateImageBehavior = false;
            this.lstPythonDefinitions.View = System.Windows.Forms.View.List;
            this.lstPythonDefinitions.ItemActivate += new System.EventHandler(this.lstPythonDefinitions_ItemActivate);
            // 
            // imlPythonDefinitions
            // 
            this.imlPythonDefinitions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlPythonDefinitions.ImageStream")));
            this.imlPythonDefinitions.TransparentColor = System.Drawing.Color.Magenta;
            this.imlPythonDefinitions.Images.SetKeyName(0, "VSObject_Class.bmp");
            this.imlPythonDefinitions.Images.SetKeyName(1, "VSObject_Constant.bmp");
            this.imlPythonDefinitions.Images.SetKeyName(2, "VSObject_Method.bmp");
            // 
            // lstErrors
            // 
            this.lstErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLocation,
            this.colCode,
            this.colMessage});
            this.lstErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstErrors.FullRowSelect = true;
            this.lstErrors.Location = new System.Drawing.Point(0, 0);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.Size = new System.Drawing.Size(963, 125);
            this.lstErrors.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstErrors.TabIndex = 1;
            this.lstErrors.UseCompatibleStateImageBehavior = false;
            this.lstErrors.View = System.Windows.Forms.View.Details;
            this.lstErrors.ItemActivate += new System.EventHandler(this.lstErrors_ItemActivate);
            // 
            // colLocation
            // 
            this.colLocation.Text = "Loc";
            // 
            // colCode
            // 
            this.colCode.Text = "Code";
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 400;
            // 
            // lstConfigurations
            // 
            this.lstConfigurations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConfigurations.Enabled = false;
            this.lstConfigurations.FormattingEnabled = true;
            this.lstConfigurations.Location = new System.Drawing.Point(552, 5);
            this.lstConfigurations.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lstConfigurations.MinimumSize = new System.Drawing.Size(20, 0);
            this.lstConfigurations.Name = "lstConfigurations";
            this.lstConfigurations.Size = new System.Drawing.Size(376, 23);
            this.lstConfigurations.TabIndex = 1;
            this.lstConfigurations.SelectedIndexChanged += new System.EventHandler(this.lstConfigurations_SelectedIndexChanged);
            this.lstConfigurations.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.lstConfigurations_Format);
            // 
            // splitLandscape
            // 
            this.splitLandscape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLandscape.Location = new System.Drawing.Point(0, 0);
            this.splitLandscape.Name = "splitLandscape";
            // 
            // splitLandscape.Panel1
            // 
            this.splitLandscape.Panel1.Controls.Add(this.lstLandscapes);
            // 
            // splitLandscape.Panel2
            // 
            this.splitLandscape.Panel2.Controls.Add(this.tableLandscape);
            this.splitLandscape.Size = new System.Drawing.Size(963, 661);
            this.splitLandscape.SplitterDistance = 320;
            this.splitLandscape.TabIndex = 0;
            // 
            // lstLandscapes
            // 
            this.lstLandscapes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLandscapes.HideSelection = false;
            this.lstLandscapes.Location = new System.Drawing.Point(0, 0);
            this.lstLandscapes.Name = "lstLandscapes";
            this.lstLandscapes.Size = new System.Drawing.Size(320, 661);
            this.lstLandscapes.TabIndex = 0;
            this.lstLandscapes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.lstLandscapes_AfterSelect);
            // 
            // tableLandscape
            // 
            this.tableLandscape.ColumnCount = 2;
            this.tableLandscape.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLandscape.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLandscape.Controls.Add(this.txtEvaluatorCode, 1, 3);
            this.tableLandscape.Controls.Add(this.lblLandscapeInternalName, 0, 0);
            this.tableLandscape.Controls.Add(this.txtLandscapeInternalName, 1, 0);
            this.tableLandscape.Controls.Add(this.lblLandscapeDescription, 0, 1);
            this.tableLandscape.Controls.Add(this.txtLandscapeDescription, 1, 1);
            this.tableLandscape.Controls.Add(this.lblLandscapeParameters, 0, 2);
            this.tableLandscape.Controls.Add(this.txtLandscapeParameters, 1, 2);
            this.tableLandscape.Controls.Add(this.lblEvaluatorCode, 0, 3);
            this.tableLandscape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLandscape.Location = new System.Drawing.Point(0, 0);
            this.tableLandscape.Name = "tableLandscape";
            this.tableLandscape.RowCount = 4;
            this.tableLandscape.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLandscape.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLandscape.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLandscape.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLandscape.Size = new System.Drawing.Size(639, 661);
            this.tableLandscape.TabIndex = 0;
            // 
            // txtEvaluatorCode
            // 
            this.txtEvaluatorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEvaluatorCode.ConvertTabsToSpaces = true;
            this.txtEvaluatorCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEvaluatorCode.EnableFolding = false;
            this.txtEvaluatorCode.IsReadOnly = false;
            this.txtEvaluatorCode.Location = new System.Drawing.Point(92, 473);
            this.txtEvaluatorCode.Name = "txtEvaluatorCode";
            this.txtEvaluatorCode.ShowLineNumbers = false;
            this.txtEvaluatorCode.ShowVRuler = false;
            this.txtEvaluatorCode.Size = new System.Drawing.Size(544, 185);
            this.txtEvaluatorCode.TabIndex = 6;
            this.txtEvaluatorCode.Text = "fitness = 0.0\r\nfor x in indiv:\r\n    fitness += x**2\r\n\r\nreturn FitnessMinimise(fit" +
                "ness)";
            this.txtEvaluatorCode.Visible = false;
            // 
            // lblLandscapeInternalName
            // 
            this.lblLandscapeInternalName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLandscapeInternalName.AutoSize = true;
            this.lblLandscapeInternalName.Location = new System.Drawing.Point(3, 7);
            this.lblLandscapeInternalName.Name = "lblLandscapeInternalName";
            this.lblLandscapeInternalName.Size = new System.Drawing.Size(83, 15);
            this.lblLandscapeInternalName.TabIndex = 0;
            this.lblLandscapeInternalName.Text = "Internal name:";
            // 
            // txtLandscapeInternalName
            // 
            this.txtLandscapeInternalName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLandscapeInternalName.Location = new System.Drawing.Point(92, 3);
            this.txtLandscapeInternalName.Name = "txtLandscapeInternalName";
            this.txtLandscapeInternalName.ReadOnly = true;
            this.txtLandscapeInternalName.Size = new System.Drawing.Size(544, 23);
            this.txtLandscapeInternalName.TabIndex = 1;
            // 
            // lblLandscapeDescription
            // 
            this.lblLandscapeDescription.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLandscapeDescription.AutoSize = true;
            this.lblLandscapeDescription.Location = new System.Drawing.Point(16, 116);
            this.lblLandscapeDescription.Name = "lblLandscapeDescription";
            this.lblLandscapeDescription.Size = new System.Drawing.Size(70, 15);
            this.lblLandscapeDescription.TabIndex = 2;
            this.lblLandscapeDescription.Text = "Description:";
            // 
            // txtLandscapeDescription
            // 
            this.txtLandscapeDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLandscapeDescription.Location = new System.Drawing.Point(92, 32);
            this.txtLandscapeDescription.Multiline = true;
            this.txtLandscapeDescription.Name = "txtLandscapeDescription";
            this.txtLandscapeDescription.ReadOnly = true;
            this.txtLandscapeDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLandscapeDescription.Size = new System.Drawing.Size(544, 183);
            this.txtLandscapeDescription.TabIndex = 3;
            // 
            // lblLandscapeParameters
            // 
            this.lblLandscapeParameters.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLandscapeParameters.AutoSize = true;
            this.lblLandscapeParameters.Location = new System.Drawing.Point(17, 336);
            this.lblLandscapeParameters.Name = "lblLandscapeParameters";
            this.lblLandscapeParameters.Size = new System.Drawing.Size(69, 15);
            this.lblLandscapeParameters.TabIndex = 4;
            this.lblLandscapeParameters.Text = "Parameters:";
            // 
            // txtLandscapeParameters
            // 
            this.txtLandscapeParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLandscapeParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLandscapeParameters.EnableFolding = false;
            this.txtLandscapeParameters.IsReadOnly = false;
            this.txtLandscapeParameters.Location = new System.Drawing.Point(92, 221);
            this.txtLandscapeParameters.Name = "txtLandscapeParameters";
            this.txtLandscapeParameters.ShowLineNumbers = false;
            this.txtLandscapeParameters.ShowVRuler = false;
            this.txtLandscapeParameters.Size = new System.Drawing.Size(544, 246);
            this.txtLandscapeParameters.TabIndex = 5;
            // 
            // lblEvaluatorCode
            // 
            this.lblEvaluatorCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEvaluatorCode.AutoSize = true;
            this.lblEvaluatorCode.Location = new System.Drawing.Point(7, 558);
            this.lblEvaluatorCode.Name = "lblEvaluatorCode";
            this.lblEvaluatorCode.Size = new System.Drawing.Size(79, 15);
            this.lblEvaluatorCode.TabIndex = 4;
            this.lblEvaluatorCode.Text = "Python Code:";
            this.lblEvaluatorCode.Visible = false;
            // 
            // splitLimitsGraph
            // 
            this.splitLimitsGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLimitsGraph.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitLimitsGraph.Location = new System.Drawing.Point(0, 0);
            this.splitLimitsGraph.Name = "splitLimitsGraph";
            // 
            // splitLimitsGraph.Panel1
            // 
            this.splitLimitsGraph.Panel1.Controls.Add(this.tableResults);
            // 
            // splitLimitsGraph.Panel2
            // 
            this.splitLimitsGraph.Panel2.Controls.Add(this.splitGraphStats);
            this.splitLimitsGraph.Size = new System.Drawing.Size(963, 661);
            this.splitLimitsGraph.SplitterDistance = 320;
            this.splitLimitsGraph.TabIndex = 0;
            // 
            // tableResults
            // 
            this.tableResults.ColumnCount = 1;
            this.tableResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableResults.Controls.Add(this.tableControls, 0, 1);
            this.tableResults.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableResults.Location = new System.Drawing.Point(0, 0);
            this.tableResults.Name = "tableResults";
            this.tableResults.RowCount = 4;
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableResults.Size = new System.Drawing.Size(320, 661);
            this.tableResults.TabIndex = 0;
            // 
            // tableControls
            // 
            this.tableControls.AutoSize = true;
            this.tableControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableControls.ColumnCount = 3;
            this.tableControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableControls.Controls.Add(this.chkIterations, 2, 1);
            this.tableControls.Controls.Add(this.chkFitness, 2, 4);
            this.tableControls.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableControls.Controls.Add(this.chkSeconds, 2, 3);
            this.tableControls.Controls.Add(this.chkEvaluations, 2, 2);
            this.tableControls.Controls.Add(this.txtFitness, 1, 4);
            this.tableControls.Controls.Add(this.txtEvaluations, 1, 2);
            this.tableControls.Controls.Add(this.txtSeconds, 1, 3);
            this.tableControls.Controls.Add(this.txtIterations, 1, 1);
            this.tableControls.Controls.Add(this.lblStopAfter, 0, 1);
            this.tableControls.Controls.Add(this.lblOr1, 0, 2);
            this.tableControls.Controls.Add(this.lblOr3, 0, 4);
            this.tableControls.Controls.Add(this.lblOr2, 0, 3);
            this.tableControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableControls.Location = new System.Drawing.Point(0, 0);
            this.tableControls.Margin = new System.Windows.Forms.Padding(0);
            this.tableControls.Name = "tableControls";
            this.tableControls.RowCount = 5;
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.Size = new System.Drawing.Size(320, 166);
            this.tableControls.TabIndex = 0;
            // 
            // chkIterations
            // 
            this.chkIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIterations.AutoSize = true;
            this.chkIterations.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIterations.Checked = true;
            this.chkIterations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIterations.Location = new System.Drawing.Point(231, 55);
            this.chkIterations.Name = "chkIterations";
            this.chkIterations.Size = new System.Drawing.Size(86, 19);
            this.chkIterations.TabIndex = 2;
            this.chkIterations.Text = "iterations";
            this.chkIterations.UseVisualStyleBackColor = true;
            this.chkIterations.MouseEnter += new System.EventHandler(this.chkLimit_MouseEnter);
            this.chkIterations.MouseLeave += new System.EventHandler(this.chkLimit_MouseLeave);
            // 
            // chkFitness
            // 
            this.chkFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFitness.AutoSize = true;
            this.chkFitness.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFitness.Location = new System.Drawing.Point(231, 142);
            this.chkFitness.Name = "chkFitness";
            this.chkFitness.Size = new System.Drawing.Size(86, 19);
            this.chkFitness.TabIndex = 11;
            this.chkFitness.Text = "fitness";
            this.chkFitness.UseVisualStyleBackColor = true;
            this.chkFitness.MouseEnter += new System.EventHandler(this.chkLimit_MouseEnter);
            this.chkFitness.MouseLeave += new System.EventHandler(this.chkLimit_MouseLeave);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel1.AutoSize = true;
            this.tableControls.SetColumnSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Controls.Add(this.btnStart);
            this.flowLayoutPanel1.Controls.Add(this.btnPause);
            this.flowLayoutPanel1.Controls.Add(this.btnStop);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(65, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(190, 44);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // btnStart
            // 
            this.btnStart.AutoSize = true;
            this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStart.Image = global::esecui.Properties.Resources.start;
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(96, 38);
            this.btnStart.TabIndex = 13;
            this.btnStart.Text = "&Start (F5)";
            this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPause.AutoSize = true;
            this.btnPause.Enabled = false;
            this.btnPause.Image = global::esecui.Properties.Resources.pause;
            this.btnPause.Location = new System.Drawing.Point(105, 3);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(38, 38);
            this.btnPause.TabIndex = 14;
            this.toolTips.SetToolTip(this.btnPause, "Pause (F5)");
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.CheckedChanged += new System.EventHandler(this.btnPause_CheckedChanged);
            // 
            // btnStop
            // 
            this.btnStop.AutoSize = true;
            this.btnStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStop.Enabled = false;
            this.btnStop.Image = global::esecui.Properties.Resources.stop;
            this.btnStop.Location = new System.Drawing.Point(149, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(38, 38);
            this.btnStop.TabIndex = 13;
            this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTips.SetToolTip(this.btnStop, "Stop (Shift+F5)");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // chkSeconds
            // 
            this.chkSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSeconds.AutoSize = true;
            this.chkSeconds.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSeconds.Location = new System.Drawing.Point(231, 113);
            this.chkSeconds.Name = "chkSeconds";
            this.chkSeconds.Size = new System.Drawing.Size(86, 19);
            this.chkSeconds.TabIndex = 8;
            this.chkSeconds.Text = "seconds";
            this.chkSeconds.UseVisualStyleBackColor = true;
            this.chkSeconds.MouseEnter += new System.EventHandler(this.chkLimit_MouseEnter);
            this.chkSeconds.MouseLeave += new System.EventHandler(this.chkLimit_MouseLeave);
            // 
            // chkEvaluations
            // 
            this.chkEvaluations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEvaluations.AutoSize = true;
            this.chkEvaluations.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEvaluations.Location = new System.Drawing.Point(231, 84);
            this.chkEvaluations.Name = "chkEvaluations";
            this.chkEvaluations.Size = new System.Drawing.Size(86, 19);
            this.chkEvaluations.TabIndex = 5;
            this.chkEvaluations.Text = "evaluations";
            this.chkEvaluations.UseVisualStyleBackColor = true;
            this.chkEvaluations.MouseEnter += new System.EventHandler(this.chkLimit_MouseEnter);
            this.chkEvaluations.MouseLeave += new System.EventHandler(this.chkLimit_MouseLeave);
            // 
            // txtFitness
            // 
            this.txtFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFitness.Location = new System.Drawing.Point(67, 140);
            this.txtFitness.Name = "txtFitness";
            this.txtFitness.Size = new System.Drawing.Size(158, 23);
            this.txtFitness.TabIndex = 10;
            this.txtFitness.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFitness_KeyDown);
            this.txtFitness.Validated += new System.EventHandler(this.txtFitness_Validated);
            // 
            // txtEvaluations
            // 
            this.txtEvaluations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEvaluations.Location = new System.Drawing.Point(67, 82);
            this.txtEvaluations.Name = "txtEvaluations";
            this.txtEvaluations.Size = new System.Drawing.Size(158, 23);
            this.txtEvaluations.TabIndex = 4;
            this.txtEvaluations.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEvaluations_KeyDown);
            this.txtEvaluations.Validated += new System.EventHandler(this.txtEvaluations_Validated);
            // 
            // txtSeconds
            // 
            this.txtSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSeconds.Location = new System.Drawing.Point(67, 111);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(158, 23);
            this.txtSeconds.TabIndex = 7;
            this.txtSeconds.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeconds_KeyDown);
            this.txtSeconds.Validated += new System.EventHandler(this.txtSeconds_Validated);
            // 
            // txtIterations
            // 
            this.txtIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIterations.Location = new System.Drawing.Point(67, 53);
            this.txtIterations.Name = "txtIterations";
            this.txtIterations.Size = new System.Drawing.Size(158, 23);
            this.txtIterations.TabIndex = 1;
            this.txtIterations.Text = "10";
            this.txtIterations.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIterations_KeyDown);
            this.txtIterations.Validated += new System.EventHandler(this.txtIterations_Validated);
            // 
            // lblStopAfter
            // 
            this.lblStopAfter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStopAfter.AutoSize = true;
            this.lblStopAfter.Location = new System.Drawing.Point(3, 57);
            this.lblStopAfter.Name = "lblStopAfter";
            this.lblStopAfter.Size = new System.Drawing.Size(58, 15);
            this.lblStopAfter.TabIndex = 0;
            this.lblStopAfter.Text = "Stop after";
            // 
            // lblOr1
            // 
            this.lblOr1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOr1.AutoSize = true;
            this.lblOr1.Location = new System.Drawing.Point(43, 86);
            this.lblOr1.Name = "lblOr1";
            this.lblOr1.Size = new System.Drawing.Size(18, 15);
            this.lblOr1.TabIndex = 3;
            this.lblOr1.Text = "or";
            // 
            // lblOr3
            // 
            this.lblOr3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOr3.AutoSize = true;
            this.lblOr3.Location = new System.Drawing.Point(30, 144);
            this.lblOr3.Name = "lblOr3";
            this.lblOr3.Size = new System.Drawing.Size(31, 15);
            this.lblOr3.TabIndex = 9;
            this.lblOr3.Text = "or at";
            // 
            // lblOr2
            // 
            this.lblOr2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOr2.AutoSize = true;
            this.lblOr2.Location = new System.Drawing.Point(43, 115);
            this.lblOr2.Name = "lblOr2";
            this.lblOr2.Size = new System.Drawing.Size(18, 15);
            this.lblOr2.TabIndex = 6;
            this.lblOr2.Text = "or";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 1);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // splitGraphStats
            // 
            this.splitGraphStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGraphStats.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitGraphStats.Location = new System.Drawing.Point(0, 0);
            this.splitGraphStats.Name = "splitGraphStats";
            this.splitGraphStats.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitGraphStats.Panel1
            // 
            this.splitGraphStats.Panel1.Controls.Add(this.tabResultView);
            // 
            // splitGraphStats.Panel2
            // 
            this.splitGraphStats.Panel2.Controls.Add(this.tableStats);
            this.splitGraphStats.Size = new System.Drawing.Size(639, 661);
            this.splitGraphStats.SplitterDistance = 492;
            this.splitGraphStats.TabIndex = 0;
            // 
            // tabResultView
            // 
            this.tabResultView.Controls.Add(this.tabChart);
            this.tabResultView.Controls.Add(this.tab2DPlot);
            this.tabResultView.Controls.Add(this.tabBestIndividual);
            this.tabResultView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResultView.Location = new System.Drawing.Point(0, 0);
            this.tabResultView.Name = "tabResultView";
            this.tabResultView.SelectedIndex = 0;
            this.tabResultView.Size = new System.Drawing.Size(639, 492);
            this.tabResultView.TabIndex = 0;
            // 
            // tabChart
            // 
            this.tabChart.Controls.Add(this.chartResults);
            this.tabChart.Location = new System.Drawing.Point(4, 24);
            this.tabChart.Name = "tabChart";
            this.tabChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabChart.Size = new System.Drawing.Size(631, 464);
            this.tabChart.TabIndex = 0;
            this.tabChart.Text = "Chart (Alt+1)";
            this.tabChart.UseVisualStyleBackColor = true;
            // 
            // chartResults
            // 
            this.chartResults.AutoRange = true;
            this.chartResults.AutoRangeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chartResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartResults.FlipVertical = true;
            this.chartResults.GridColor = System.Drawing.Color.Gray;
            this.chartResults.HorizontalAxisColor = System.Drawing.Color.Black;
            this.chartResults.Location = new System.Drawing.Point(3, 3);
            this.chartResults.MustIncludeHorizontalZero = true;
            this.chartResults.MustIncludeVerticalZero = true;
            this.chartResults.Name = "chartResults";
            this.chartResults.ShowMouseCoordinates = true;
            this.chartResults.Size = new System.Drawing.Size(625, 458);
            this.chartResults.TabIndex = 0;
            this.chartResults.VerticalAxisColor = System.Drawing.Color.Black;
            // 
            // tab2DPlot
            // 
            this.tab2DPlot.Controls.Add(this.table2DPlot);
            this.tab2DPlot.Location = new System.Drawing.Point(4, 22);
            this.tab2DPlot.Name = "tab2DPlot";
            this.tab2DPlot.Padding = new System.Windows.Forms.Padding(3);
            this.tab2DPlot.Size = new System.Drawing.Size(631, 466);
            this.tab2DPlot.TabIndex = 1;
            this.tab2DPlot.Text = "2D Plot (Alt+2)";
            this.tab2DPlot.UseVisualStyleBackColor = true;
            // 
            // table2DPlot
            // 
            this.table2DPlot.ColumnCount = 2;
            this.table2DPlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table2DPlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table2DPlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table2DPlot.Controls.Add(this.visPopulation, 0, 1);
            this.table2DPlot.Controls.Add(this.lblPlotExpression, 0, 0);
            this.table2DPlot.Controls.Add(this.txtPlotExpression, 1, 0);
            this.table2DPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table2DPlot.Location = new System.Drawing.Point(3, 3);
            this.table2DPlot.Name = "table2DPlot";
            this.table2DPlot.RowCount = 2;
            this.table2DPlot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table2DPlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table2DPlot.Size = new System.Drawing.Size(625, 460);
            this.table2DPlot.TabIndex = 0;
            // 
            // visPopulation
            // 
            this.visPopulation.AutoRange = true;
            this.visPopulation.AutoRangeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.visPopulation.BackColor = System.Drawing.Color.White;
            this.table2DPlot.SetColumnSpan(this.visPopulation, 2);
            this.visPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visPopulation.GridColor = System.Drawing.Color.Gray;
            this.visPopulation.HorizontalAxisColor = System.Drawing.Color.Black;
            this.visPopulation.HorizontalOffset = -100000D;
            this.visPopulation.HorizontalRange = 1000000D;
            this.visPopulation.Location = new System.Drawing.Point(3, 32);
            this.visPopulation.Name = "visPopulation";
            this.visPopulation.ShowMouseCoordinates = true;
            this.visPopulation.Size = new System.Drawing.Size(619, 425);
            this.visPopulation.TabIndex = 2;
            this.visPopulation.VerticalAxisColor = System.Drawing.Color.Black;
            this.visPopulation.VerticalOffset = -7.2505224288324825D;
            this.visPopulation.VerticalRange = 1000000D;
            // 
            // lblPlotExpression
            // 
            this.lblPlotExpression.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPlotExpression.AutoSize = true;
            this.lblPlotExpression.Location = new System.Drawing.Point(3, 7);
            this.lblPlotExpression.Name = "lblPlotExpression";
            this.lblPlotExpression.Size = new System.Drawing.Size(65, 15);
            this.lblPlotExpression.TabIndex = 3;
            this.lblPlotExpression.Text = "Expression:";
            // 
            // txtPlotExpression
            // 
            this.txtPlotExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlotExpression.Location = new System.Drawing.Point(74, 3);
            this.txtPlotExpression.Name = "txtPlotExpression";
            this.txtPlotExpression.Size = new System.Drawing.Size(548, 23);
            this.txtPlotExpression.TabIndex = 4;
            this.txtPlotExpression.Text = "(indiv[0], indiv[1])";
            this.txtPlotExpression.Enter += new System.EventHandler(this.txtExpression_Enter);
            this.txtPlotExpression.Leave += new System.EventHandler(this.txtExpression_Leave);
            // 
            // tabBestIndividual
            // 
            this.tabBestIndividual.Controls.Add(this.tableBestIndividual);
            this.tabBestIndividual.Location = new System.Drawing.Point(4, 22);
            this.tabBestIndividual.Name = "tabBestIndividual";
            this.tabBestIndividual.Padding = new System.Windows.Forms.Padding(3);
            this.tabBestIndividual.Size = new System.Drawing.Size(631, 466);
            this.tabBestIndividual.TabIndex = 2;
            this.tabBestIndividual.Text = "Best (Alt+3)";
            this.tabBestIndividual.UseVisualStyleBackColor = true;
            // 
            // tableBestIndividual
            // 
            this.tableBestIndividual.ColumnCount = 2;
            this.tableBestIndividual.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableBestIndividual.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBestIndividual.Controls.Add(this.lblBestIndividualExpression, 0, 0);
            this.tableBestIndividual.Controls.Add(this.txtBestIndividualExpression, 1, 0);
            this.tableBestIndividual.Controls.Add(this.txtBestIndividual, 0, 1);
            this.tableBestIndividual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBestIndividual.Location = new System.Drawing.Point(3, 3);
            this.tableBestIndividual.Name = "tableBestIndividual";
            this.tableBestIndividual.RowCount = 2;
            this.tableBestIndividual.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableBestIndividual.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableBestIndividual.Size = new System.Drawing.Size(625, 460);
            this.tableBestIndividual.TabIndex = 0;
            // 
            // lblBestIndividualExpression
            // 
            this.lblBestIndividualExpression.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBestIndividualExpression.AutoSize = true;
            this.lblBestIndividualExpression.Location = new System.Drawing.Point(3, 7);
            this.lblBestIndividualExpression.Name = "lblBestIndividualExpression";
            this.lblBestIndividualExpression.Size = new System.Drawing.Size(65, 15);
            this.lblBestIndividualExpression.TabIndex = 0;
            this.lblBestIndividualExpression.Text = "Expression:";
            // 
            // txtBestIndividualExpression
            // 
            this.txtBestIndividualExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBestIndividualExpression.Location = new System.Drawing.Point(74, 3);
            this.txtBestIndividualExpression.Name = "txtBestIndividualExpression";
            this.txtBestIndividualExpression.Size = new System.Drawing.Size(548, 23);
            this.txtBestIndividualExpression.TabIndex = 1;
            this.txtBestIndividualExpression.Text = "indiv.phenome_string";
            this.txtBestIndividualExpression.TextChanged += new System.EventHandler(this.txtBestIndividualExpression_TextChanged);
            this.txtBestIndividualExpression.Enter += new System.EventHandler(this.txtExpression_Enter);
            this.txtBestIndividualExpression.Leave += new System.EventHandler(this.txtExpression_Leave);
            // 
            // txtBestIndividual
            // 
            this.tableBestIndividual.SetColumnSpan(this.txtBestIndividual, 2);
            this.txtBestIndividual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBestIndividual.Location = new System.Drawing.Point(3, 32);
            this.txtBestIndividual.Multiline = true;
            this.txtBestIndividual.Name = "txtBestIndividual";
            this.txtBestIndividual.ReadOnly = true;
            this.txtBestIndividual.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBestIndividual.Size = new System.Drawing.Size(619, 425);
            this.txtBestIndividual.TabIndex = 2;
            this.txtBestIndividual.VisibleChanged += new System.EventHandler(this.txtBestIndividual_VisibleChanged);
            // 
            // tableStats
            // 
            this.tableStats.ColumnCount = 5;
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableStats.Controls.Add(this.label1, 0, 0);
            this.tableStats.Controls.Add(this.label2, 0, 1);
            this.tableStats.Controls.Add(this.label4, 0, 2);
            this.tableStats.Controls.Add(this.label3, 0, 3);
            this.tableStats.Controls.Add(this.label5, 2, 0);
            this.tableStats.Controls.Add(this.label6, 2, 1);
            this.tableStats.Controls.Add(this.label7, 2, 2);
            this.tableStats.Controls.Add(this.label8, 2, 3);
            this.tableStats.Controls.Add(this.txtStatsIterations, 1, 0);
            this.tableStats.Controls.Add(this.txtStatsEvaluations, 1, 1);
            this.tableStats.Controls.Add(this.txtStatsBirths, 1, 2);
            this.tableStats.Controls.Add(this.txtStatsTime, 1, 3);
            this.tableStats.Controls.Add(this.txtStatsBestFitness, 4, 0);
            this.tableStats.Controls.Add(this.txtStatsCurrentBest, 4, 1);
            this.tableStats.Controls.Add(this.txtStatsCurrentMean, 4, 2);
            this.tableStats.Controls.Add(this.txtStatsCurrentWorst, 4, 3);
            this.tableStats.Controls.Add(this.chkChartCurrentWorst, 3, 3);
            this.tableStats.Controls.Add(this.chkChartCurrentMean, 3, 2);
            this.tableStats.Controls.Add(this.chkChartCurrentBest, 3, 1);
            this.tableStats.Controls.Add(this.chkChartBestFitness, 3, 0);
            this.tableStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableStats.Location = new System.Drawing.Point(0, 0);
            this.tableStats.Name = "tableStats";
            this.tableStats.RowCount = 4;
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStats.Size = new System.Drawing.Size(639, 165);
            this.tableStats.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Iterations:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Evaluations:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Births:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Time:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(291, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Best Fitness:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(291, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Current Best:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(291, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Current Mean:";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(291, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Current Worst:";
            // 
            // txtStatsIterations
            // 
            this.txtStatsIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsIterations.Location = new System.Drawing.Point(85, 9);
            this.txtStatsIterations.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsIterations.Name = "txtStatsIterations";
            this.txtStatsIterations.ReadOnly = true;
            this.txtStatsIterations.Size = new System.Drawing.Size(194, 23);
            this.txtStatsIterations.TabIndex = 1;
            this.txtStatsIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsEvaluations
            // 
            this.txtStatsEvaluations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsEvaluations.Location = new System.Drawing.Point(85, 50);
            this.txtStatsEvaluations.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsEvaluations.Name = "txtStatsEvaluations";
            this.txtStatsEvaluations.ReadOnly = true;
            this.txtStatsEvaluations.Size = new System.Drawing.Size(194, 23);
            this.txtStatsEvaluations.TabIndex = 3;
            this.txtStatsEvaluations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsBirths
            // 
            this.txtStatsBirths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsBirths.Location = new System.Drawing.Point(85, 91);
            this.txtStatsBirths.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsBirths.Name = "txtStatsBirths";
            this.txtStatsBirths.ReadOnly = true;
            this.txtStatsBirths.Size = new System.Drawing.Size(194, 23);
            this.txtStatsBirths.TabIndex = 5;
            this.txtStatsBirths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsTime
            // 
            this.txtStatsTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsTime.Location = new System.Drawing.Point(85, 132);
            this.txtStatsTime.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsTime.Name = "txtStatsTime";
            this.txtStatsTime.ReadOnly = true;
            this.txtStatsTime.Size = new System.Drawing.Size(194, 23);
            this.txtStatsTime.TabIndex = 7;
            this.txtStatsTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsBestFitness
            // 
            this.txtStatsBestFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsBestFitness.Location = new System.Drawing.Point(434, 9);
            this.txtStatsBestFitness.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsBestFitness.Name = "txtStatsBestFitness";
            this.txtStatsBestFitness.ReadOnly = true;
            this.txtStatsBestFitness.Size = new System.Drawing.Size(196, 23);
            this.txtStatsBestFitness.TabIndex = 9;
            this.txtStatsBestFitness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsCurrentBest
            // 
            this.txtStatsCurrentBest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsCurrentBest.Location = new System.Drawing.Point(434, 50);
            this.txtStatsCurrentBest.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsCurrentBest.Name = "txtStatsCurrentBest";
            this.txtStatsCurrentBest.ReadOnly = true;
            this.txtStatsCurrentBest.Size = new System.Drawing.Size(196, 23);
            this.txtStatsCurrentBest.TabIndex = 11;
            this.txtStatsCurrentBest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsCurrentMean
            // 
            this.txtStatsCurrentMean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsCurrentMean.Location = new System.Drawing.Point(434, 91);
            this.txtStatsCurrentMean.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsCurrentMean.Name = "txtStatsCurrentMean";
            this.txtStatsCurrentMean.ReadOnly = true;
            this.txtStatsCurrentMean.Size = new System.Drawing.Size(196, 23);
            this.txtStatsCurrentMean.TabIndex = 13;
            this.txtStatsCurrentMean.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsCurrentWorst
            // 
            this.txtStatsCurrentWorst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsCurrentWorst.Location = new System.Drawing.Point(434, 132);
            this.txtStatsCurrentWorst.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsCurrentWorst.Name = "txtStatsCurrentWorst";
            this.txtStatsCurrentWorst.ReadOnly = true;
            this.txtStatsCurrentWorst.Size = new System.Drawing.Size(196, 23);
            this.txtStatsCurrentWorst.TabIndex = 15;
            this.txtStatsCurrentWorst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkChartCurrentWorst
            // 
            this.chkChartCurrentWorst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkChartCurrentWorst.AutoSize = true;
            this.chkChartCurrentWorst.BackColor = System.Drawing.Color.Blue;
            this.chkChartCurrentWorst.Location = new System.Drawing.Point(381, 137);
            this.chkChartCurrentWorst.Name = "chkChartCurrentWorst";
            this.chkChartCurrentWorst.Size = new System.Drawing.Size(41, 14);
            this.chkChartCurrentWorst.TabIndex = 17;
            this.chkChartCurrentWorst.Tag = "CurrentWorst";
            this.chkChartCurrentWorst.UseVisualStyleBackColor = false;
            this.chkChartCurrentWorst.CheckedChanged += new System.EventHandler(this.chkChartSeries_CheckedChanged);
            // 
            // chkChartCurrentMean
            // 
            this.chkChartCurrentMean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkChartCurrentMean.AutoSize = true;
            this.chkChartCurrentMean.BackColor = System.Drawing.Color.Blue;
            this.chkChartCurrentMean.Location = new System.Drawing.Point(381, 95);
            this.chkChartCurrentMean.Name = "chkChartCurrentMean";
            this.chkChartCurrentMean.Size = new System.Drawing.Size(41, 14);
            this.chkChartCurrentMean.TabIndex = 17;
            this.chkChartCurrentMean.Tag = "CurrentMean";
            this.chkChartCurrentMean.UseVisualStyleBackColor = false;
            this.chkChartCurrentMean.CheckedChanged += new System.EventHandler(this.chkChartSeries_CheckedChanged);
            // 
            // chkChartCurrentBest
            // 
            this.chkChartCurrentBest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkChartCurrentBest.AutoSize = true;
            this.chkChartCurrentBest.BackColor = System.Drawing.Color.Blue;
            this.chkChartCurrentBest.Checked = true;
            this.chkChartCurrentBest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChartCurrentBest.Location = new System.Drawing.Point(381, 54);
            this.chkChartCurrentBest.Name = "chkChartCurrentBest";
            this.chkChartCurrentBest.Size = new System.Drawing.Size(41, 14);
            this.chkChartCurrentBest.TabIndex = 17;
            this.chkChartCurrentBest.Tag = "CurrentBest";
            this.chkChartCurrentBest.UseVisualStyleBackColor = false;
            this.chkChartCurrentBest.CheckedChanged += new System.EventHandler(this.chkChartSeries_CheckedChanged);
            // 
            // chkChartBestFitness
            // 
            this.chkChartBestFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkChartBestFitness.AutoSize = true;
            this.chkChartBestFitness.BackColor = System.Drawing.Color.Blue;
            this.chkChartBestFitness.Checked = true;
            this.chkChartBestFitness.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChartBestFitness.Location = new System.Drawing.Point(381, 13);
            this.chkChartBestFitness.Name = "chkChartBestFitness";
            this.chkChartBestFitness.Size = new System.Drawing.Size(41, 14);
            this.chkChartBestFitness.TabIndex = 17;
            this.chkChartBestFitness.Tag = "BestFitness";
            this.chkChartBestFitness.UseVisualStyleBackColor = false;
            this.chkChartBestFitness.CheckedChanged += new System.EventHandler(this.chkChartSeries_CheckedChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConfiguration,
            this.menuHelp,
            this.menuView,
            this.menuControl});
            this.menuStrip.Location = new System.Drawing.Point(0, 3);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.menuStrip.Size = new System.Drawing.Size(151, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "Main Menu";
            // 
            // menuConfiguration
            // 
            this.menuConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this.menuSave,
            this.menuSaveAs,
            menuConfigurationSeparator1,
            this.menuExport,
            menuConfigurationSeparator2,
            this.menuCheckSyntax,
            menuConfigurationSeparator3,
            this.menuExit});
            this.menuConfiguration.Name = "menuConfiguration";
            this.menuConfiguration.Size = new System.Drawing.Size(93, 20);
            this.menuConfiguration.Text = "&Configuration";
            // 
            // menuNew
            // 
            this.menuNew.Name = "menuNew";
            this.menuNew.Size = new System.Drawing.Size(195, 22);
            this.menuNew.Text = "&New";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpen.Size = new System.Drawing.Size(195, 22);
            this.menuOpen.Text = "&Open...";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSave.Size = new System.Drawing.Size(195, 22);
            this.menuSave.Text = "&Save";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.menuSaveAs.Size = new System.Drawing.Size(195, 22);
            this.menuSaveAs.Text = "Save &As...";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuExport
            // 
            this.menuExport.Enabled = false;
            this.menuExport.Name = "menuExport";
            this.menuExport.Size = new System.Drawing.Size(195, 22);
            this.menuExport.Text = "&Export...";
            // 
            // menuCheckSyntax
            // 
            this.menuCheckSyntax.Name = "menuCheckSyntax";
            this.menuCheckSyntax.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.menuCheckSyntax.Size = new System.Drawing.Size(195, 22);
            this.menuCheckSyntax.Text = "&Check Syntax";
            this.menuCheckSyntax.Click += new System.EventHandler(this.menuCheckSyntax_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(195, 22);
            this.menuExit.Text = "E&xit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "&Help";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(107, 22);
            this.menuAbout.Text = "&About";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewSystem,
            menuViewSeparator1,
            this.menuViewLandscape,
            menuViewSeparator2,
            this.menuViewResults,
            menuViewSeparator3,
            this.menuViewLog,
            menuViewSeparator4,
            this.menuViewSubview1,
            this.menuViewSubview2,
            this.menuViewSubview3,
            menuViewSeparator5,
            this.menuViewProjectorMode});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(44, 20);
            this.menuView.Text = "&View";
            this.menuView.Visible = false;
            // 
            // menuViewSystem
            // 
            this.menuViewSystem.Name = "menuViewSystem";
            this.menuViewSystem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.menuViewSystem.Size = new System.Drawing.Size(181, 22);
            this.menuViewSystem.Text = "&System";
            this.menuViewSystem.Click += new System.EventHandler(this.menuViewSystem_Click);
            // 
            // menuViewLandscape
            // 
            this.menuViewLandscape.Name = "menuViewLandscape";
            this.menuViewLandscape.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.menuViewLandscape.Size = new System.Drawing.Size(181, 22);
            this.menuViewLandscape.Text = "&Landscape";
            this.menuViewLandscape.Click += new System.EventHandler(this.menuViewLandscape_Click);
            // 
            // menuViewResults
            // 
            this.menuViewResults.Name = "menuViewResults";
            this.menuViewResults.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.menuViewResults.Size = new System.Drawing.Size(181, 22);
            this.menuViewResults.Text = "&Results";
            this.menuViewResults.Click += new System.EventHandler(this.menuViewResults_Click);
            // 
            // menuViewLog
            // 
            this.menuViewLog.Name = "menuViewLog";
            this.menuViewLog.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.menuViewLog.Size = new System.Drawing.Size(181, 22);
            this.menuViewLog.Text = "Lo&g";
            this.menuViewLog.Click += new System.EventHandler(this.menuViewLog_Click);
            // 
            // menuViewSubview1
            // 
            this.menuViewSubview1.Name = "menuViewSubview1";
            this.menuViewSubview1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
            this.menuViewSubview1.Size = new System.Drawing.Size(181, 22);
            this.menuViewSubview1.Text = "Subview &1";
            this.menuViewSubview1.Click += new System.EventHandler(this.menuViewSubview1_Click);
            // 
            // menuViewSubview2
            // 
            this.menuViewSubview2.Name = "menuViewSubview2";
            this.menuViewSubview2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
            this.menuViewSubview2.Size = new System.Drawing.Size(181, 22);
            this.menuViewSubview2.Text = "Subview &2";
            this.menuViewSubview2.Click += new System.EventHandler(this.menuViewSubview2_Click);
            // 
            // menuViewSubview3
            // 
            this.menuViewSubview3.Name = "menuViewSubview3";
            this.menuViewSubview3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D3)));
            this.menuViewSubview3.Size = new System.Drawing.Size(181, 22);
            this.menuViewSubview3.Text = "Subview &3";
            this.menuViewSubview3.Click += new System.EventHandler(this.menuViewSubview3_Click);
            // 
            // menuViewProjectorMode
            // 
            this.menuViewProjectorMode.Name = "menuViewProjectorMode";
            this.menuViewProjectorMode.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.menuViewProjectorMode.Size = new System.Drawing.Size(181, 22);
            this.menuViewProjectorMode.Text = "&Projector Mode";
            this.menuViewProjectorMode.Click += new System.EventHandler(this.menuViewProjectorMode_Click);
            // 
            // menuControl
            // 
            this.menuControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuControlStartPause,
            this.menuControlStop,
            this.menuControlStep});
            this.menuControl.Name = "menuControl";
            this.menuControl.Size = new System.Drawing.Size(59, 20);
            this.menuControl.Text = "&Control";
            this.menuControl.Visible = false;
            // 
            // menuControlStartPause
            // 
            this.menuControlStartPause.Name = "menuControlStartPause";
            this.menuControlStartPause.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuControlStartPause.Size = new System.Drawing.Size(153, 22);
            this.menuControlStartPause.Text = "Start/Pause";
            this.menuControlStartPause.Click += new System.EventHandler(this.menuControlStartPause_Click);
            // 
            // menuControlStop
            // 
            this.menuControlStop.Name = "menuControlStop";
            this.menuControlStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.menuControlStop.Size = new System.Drawing.Size(153, 22);
            this.menuControlStop.Text = "Stop";
            this.menuControlStop.Click += new System.EventHandler(this.menuControlStop_Click);
            // 
            // menuControlStep
            // 
            this.menuControlStep.Name = "menuControlStep";
            this.menuControlStep.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.menuControlStep.Size = new System.Drawing.Size(153, 22);
            this.menuControlStep.Text = "Step";
            this.menuControlStep.Click += new System.EventHandler(this.menuControlStep_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.AutoSize = true;
            this.panelMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMenu.Controls.Add(this.menuStrip);
            this.panelMenu.Controls.Add(this.chkSystem);
            this.panelMenu.Controls.Add(this.chkLandscape);
            this.panelMenu.Controls.Add(this.chkResults);
            this.panelMenu.Controls.Add(this.chkLog);
            this.panelMenu.Controls.Add(this.lstConfigurations);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(963, 31);
            this.panelMenu.TabIndex = 3;
            this.panelMenu.WrapContents = false;
            this.panelMenu.SizeChanged += new System.EventHandler(this.panelMenu_SizeChanged);
            this.panelMenu.Layout += new System.Windows.Forms.LayoutEventHandler(this.panelMenu_Layout);
            // 
            // chkSystem
            // 
            this.chkSystem.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSystem.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSystem.AutoSize = true;
            this.chkSystem.Checked = true;
            this.chkSystem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkSystem.Location = new System.Drawing.Point(154, 3);
            this.chkSystem.Name = "chkSystem";
            this.chkSystem.Size = new System.Drawing.Size(78, 25);
            this.chkSystem.TabIndex = 2;
            this.chkSystem.TabStop = true;
            this.chkSystem.Text = "System (F1)";
            this.chkSystem.UseVisualStyleBackColor = true;
            this.chkSystem.CheckedChanged += new System.EventHandler(this.chkTabs_CheckedChanged);
            // 
            // chkLandscape
            // 
            this.chkLandscape.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkLandscape.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkLandscape.AutoSize = true;
            this.chkLandscape.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkLandscape.Location = new System.Drawing.Point(238, 3);
            this.chkLandscape.Name = "chkLandscape";
            this.chkLandscape.Size = new System.Drawing.Size(96, 25);
            this.chkLandscape.TabIndex = 2;
            this.chkLandscape.Text = "Landscape (F2)";
            this.chkLandscape.UseVisualStyleBackColor = true;
            this.chkLandscape.CheckedChanged += new System.EventHandler(this.chkTabs_CheckedChanged);
            // 
            // chkResults
            // 
            this.chkResults.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkResults.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkResults.AutoSize = true;
            this.chkResults.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkResults.Location = new System.Drawing.Point(340, 3);
            this.chkResults.Name = "chkResults";
            this.chkResults.Size = new System.Drawing.Size(77, 25);
            this.chkResults.TabIndex = 2;
            this.chkResults.Text = "Results (F3)";
            this.chkResults.UseVisualStyleBackColor = true;
            this.chkResults.CheckedChanged += new System.EventHandler(this.chkTabs_CheckedChanged);
            // 
            // chkLog
            // 
            this.chkLog.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkLog.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkLog.AutoSize = true;
            this.chkLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkLog.Location = new System.Drawing.Point(423, 3);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(120, 25);
            this.chkLog.TabIndex = 2;
            this.chkLog.Text = "Log Messages (F12)";
            this.chkLog.UseVisualStyleBackColor = true;
            this.chkLog.CheckedChanged += new System.EventHandler(this.chkTabs_CheckedChanged);
            // 
            // panelSystem
            // 
            this.panelSystem.Controls.Add(this.splitSystemErrors);
            this.panelSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSystem.Location = new System.Drawing.Point(0, 31);
            this.panelSystem.Name = "panelSystem";
            this.panelSystem.Size = new System.Drawing.Size(963, 661);
            this.panelSystem.TabIndex = 4;
            // 
            // panelLandscape
            // 
            this.panelLandscape.Controls.Add(this.splitLandscape);
            this.panelLandscape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLandscape.Location = new System.Drawing.Point(0, 31);
            this.panelLandscape.Name = "panelLandscape";
            this.panelLandscape.Size = new System.Drawing.Size(963, 661);
            this.panelLandscape.TabIndex = 5;
            this.panelLandscape.Visible = false;
            // 
            // panelResults
            // 
            this.panelResults.Controls.Add(this.splitLimitsGraph);
            this.panelResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResults.Location = new System.Drawing.Point(0, 31);
            this.panelResults.Name = "panelResults";
            this.panelResults.Size = new System.Drawing.Size(963, 661);
            this.panelResults.TabIndex = 6;
            this.panelResults.Visible = false;
            // 
            // panelLog
            // 
            this.panelLog.Controls.Add(this.txtLog);
            this.panelLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLog.Location = new System.Drawing.Point(0, 31);
            this.panelLog.Name = "panelLog";
            this.panelLog.Padding = new System.Windows.Forms.Padding(3);
            this.panelLog.Size = new System.Drawing.Size(963, 661);
            this.panelLog.TabIndex = 7;
            this.panelLog.Visible = false;
            // 
            // txtLog
            // 
            this.txtLog.ConvertTabsToSpaces = true;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.EnableFolding = false;
            this.txtLog.IsReadOnly = false;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Name = "txtLog";
            this.txtLog.ShowLineNumbers = false;
            this.txtLog.ShowVRuler = false;
            this.txtLog.Size = new System.Drawing.Size(957, 655);
            this.txtLog.TabIndex = 0;
            // 
            // picDimmer
            // 
            this.picDimmer.Location = new System.Drawing.Point(0, 0);
            this.picDimmer.Name = "picDimmer";
            this.picDimmer.Size = new System.Drawing.Size(100, 54);
            this.picDimmer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDimmer.TabIndex = 8;
            this.picDimmer.TabStop = false;
            this.picDimmer.Visible = false;
            // 
            // Editor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(963, 692);
            this.Controls.Add(this.panelResults);
            this.Controls.Add(this.panelSystem);
            this.Controls.Add(this.panelLog);
            this.Controls.Add(this.panelLandscape);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.picDimmer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Editor";
            this.Text = "esecui Prototyping Tool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Editor_FormClosed);
            this.Load += new System.EventHandler(this.Editor_Load);
            this.ResizeBegin += new System.EventHandler(this.Editor_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Editor_ResizeEnd);
            this.ClientSizeChanged += new System.EventHandler(this.Editor_ClientSizeChanged);
            this.splitSystemErrors.Panel1.ResumeLayout(false);
            this.splitSystemErrors.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSystemErrors)).EndInit();
            this.splitSystemErrors.ResumeLayout(false);
            this.tabSourceView.ResumeLayout(false);
            this.tabSourceESDL.ResumeLayout(false);
            this.splitSystemVariables.Panel1.ResumeLayout(false);
            this.splitSystemVariables.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSystemVariables)).EndInit();
            this.splitSystemVariables.ResumeLayout(false);
            this.tabSourcePython.ResumeLayout(false);
            this.splitPythonDefinitions.Panel1.ResumeLayout(false);
            this.splitPythonDefinitions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPythonDefinitions)).EndInit();
            this.splitPythonDefinitions.ResumeLayout(false);
            this.splitLandscape.Panel1.ResumeLayout(false);
            this.splitLandscape.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLandscape)).EndInit();
            this.splitLandscape.ResumeLayout(false);
            this.tableLandscape.ResumeLayout(false);
            this.tableLandscape.PerformLayout();
            this.splitLimitsGraph.Panel1.ResumeLayout(false);
            this.splitLimitsGraph.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLimitsGraph)).EndInit();
            this.splitLimitsGraph.ResumeLayout(false);
            this.tableResults.ResumeLayout(false);
            this.tableResults.PerformLayout();
            this.tableControls.ResumeLayout(false);
            this.tableControls.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitGraphStats.Panel1.ResumeLayout(false);
            this.splitGraphStats.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGraphStats)).EndInit();
            this.splitGraphStats.ResumeLayout(false);
            this.tabResultView.ResumeLayout(false);
            this.tabChart.ResumeLayout(false);
            this.tab2DPlot.ResumeLayout(false);
            this.table2DPlot.ResumeLayout(false);
            this.table2DPlot.PerformLayout();
            this.tabBestIndividual.ResumeLayout(false);
            this.tableBestIndividual.ResumeLayout(false);
            this.tableBestIndividual.PerformLayout();
            this.tableStats.ResumeLayout(false);
            this.tableStats.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.panelSystem.ResumeLayout(false);
            this.panelLandscape.ResumeLayout(false);
            this.panelResults.ResumeLayout(false);
            this.panelLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDimmer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private esecui.CompatibleSplitContainer splitSystemErrors;
        private esecui.CompatibleSplitContainer splitSystemVariables;
        private System.Windows.Forms.ListView lstErrors;
        private System.Windows.Forms.ColumnHeader colLocation;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colMessage;
        private esecui.CompatibleSplitContainer splitLandscape;
        private System.Windows.Forms.TreeView lstLandscapes;
        private System.Windows.Forms.TableLayoutPanel tableLandscape;
        private System.Windows.Forms.Label lblLandscapeInternalName;
        private System.Windows.Forms.TextBox txtLandscapeInternalName;
        private System.Windows.Forms.Label lblLandscapeDescription;
        private System.Windows.Forms.TextBox txtLandscapeDescription;
        private System.Windows.Forms.Label lblLandscapeParameters;
        private esecui.CompatibleSplitContainer splitLimitsGraph;
        private System.Windows.Forms.TableLayoutPanel tableResults;
        private esecui.CompatibleSplitContainer splitGraphStats;
        private System.Windows.Forms.TableLayoutPanel tableStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtStatsIterations;
        private System.Windows.Forms.TextBox txtStatsEvaluations;
        private System.Windows.Forms.TextBox txtStatsBirths;
        private System.Windows.Forms.TextBox txtStatsTime;
        private System.Windows.Forms.TextBox txtStatsBestFitness;
        private System.Windows.Forms.TextBox txtStatsCurrentBest;
        private System.Windows.Forms.TextBox txtStatsCurrentMean;
        private System.Windows.Forms.TextBox txtStatsCurrentWorst;
        private ICSharpCode.TextEditor.TextEditorControl txtSystemESDL;
        private ICSharpCode.TextEditor.TextEditorControl txtSystemVariables;
        private ICSharpCode.TextEditor.TextEditorControl txtLog;
        private ICSharpCode.TextEditor.TextEditorControl txtLandscapeParameters;
        private System.Windows.Forms.ComboBox lstConfigurations;
        private System.Windows.Forms.TableLayoutPanel tableControls;
        private System.Windows.Forms.CheckBox chkIterations;
        private System.Windows.Forms.CheckBox chkFitness;
        private System.Windows.Forms.CheckBox chkSeconds;
        private System.Windows.Forms.CheckBox chkEvaluations;
        private System.Windows.Forms.TextBox txtFitness;
        private System.Windows.Forms.TextBox txtEvaluations;
        private System.Windows.Forms.TextBox txtSeconds;
        private System.Windows.Forms.TextBox txtIterations;
        private System.Windows.Forms.Label lblStopAfter;
        private System.Windows.Forms.Label lblOr1;
        private System.Windows.Forms.Label lblOr3;
        private System.Windows.Forms.Label lblOr2;
        private System.Windows.Forms.TabControl tabResultView;
        private System.Windows.Forms.TabPage tabChart;
        private System.Windows.Forms.TabPage tab2DPlot;
        private ICSharpCode.TextEditor.TextEditorControl txtEvaluatorCode;
        private System.Windows.Forms.Label lblEvaluatorCode;
        private VisualiserLib.Visualiser chartResults;
        private System.Windows.Forms.CheckBox chkChartCurrentWorst;
        private System.Windows.Forms.CheckBox chkChartCurrentMean;
        private System.Windows.Forms.CheckBox chkChartCurrentBest;
        private System.Windows.Forms.CheckBox chkChartBestFitness;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuConfiguration;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuExport;
        private System.Windows.Forms.ToolStripMenuItem menuCheckSyntax;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.FlowLayoutPanel panelMenu;
        private System.Windows.Forms.RadioButton chkSystem;
        private System.Windows.Forms.RadioButton chkLandscape;
        private System.Windows.Forms.RadioButton chkResults;
        private System.Windows.Forms.RadioButton chkLog;
        private System.Windows.Forms.Panel panelResults;
        private System.Windows.Forms.Panel panelLandscape;
        private System.Windows.Forms.Panel panelSystem;
        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuViewSystem;
        private System.Windows.Forms.ToolStripMenuItem menuViewLandscape;
        private System.Windows.Forms.ToolStripMenuItem menuViewResults;
        private System.Windows.Forms.ToolStripMenuItem menuViewSubview1;
        private System.Windows.Forms.ToolStripMenuItem menuViewSubview2;
        private System.Windows.Forms.ToolStripMenuItem menuViewLog;
        private System.Windows.Forms.ToolStripMenuItem menuControl;
        private System.Windows.Forms.ToolStripMenuItem menuControlStartPause;
        private System.Windows.Forms.PictureBox picDimmer;
        private System.Windows.Forms.TabControl tabSourceView;
        private System.Windows.Forms.TabPage tabSourceESDL;
        private System.Windows.Forms.TabPage tabSourcePython;
        private ICSharpCode.TextEditor.TextEditorControl txtSystemPython;
        private esecui.CompatibleSplitContainer splitPythonDefinitions;
        private System.Windows.Forms.ListView lstPythonDefinitions;
        private System.Windows.Forms.ImageList imlPythonDefinitions;
        private System.Windows.Forms.ToolStripMenuItem menuViewProjectorMode;
        private System.Windows.Forms.TabPage tabBestIndividual;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableBestIndividual;
        private System.Windows.Forms.Label lblBestIndividualExpression;
        private System.Windows.Forms.TextBox txtBestIndividualExpression;
        private System.Windows.Forms.TextBox txtBestIndividual;
        private System.Windows.Forms.ToolStripMenuItem menuViewSubview3;
        private System.Windows.Forms.TableLayoutPanel table2DPlot;
        private VisualiserLib.Visualiser visPopulation;
        private System.Windows.Forms.Label lblPlotExpression;
        private System.Windows.Forms.TextBox txtPlotExpression;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox btnPause;
        private System.Windows.Forms.ToolStripMenuItem menuControlStop;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.ToolStripMenuItem menuControlStep;
    }
}

