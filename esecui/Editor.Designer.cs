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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.tabTabs = new System.Windows.Forms.TabControl();
            this.tabSystem = new System.Windows.Forms.TabPage();
            this.splitSystemErrors = new System.Windows.Forms.SplitContainer();
            this.splitSystemVariables = new System.Windows.Forms.SplitContainer();
            this.txtSystem = new ICSharpCode.TextEditor.TextEditorControl();
            this.txtSystemVariables = new ICSharpCode.TextEditor.TextEditorControl();
            this.lstErrors = new System.Windows.Forms.ListView();
            this.colLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCheckSyntax = new System.Windows.Forms.Button();
            this.btnSaveConfiguration = new System.Windows.Forms.Button();
            this.btnSaveAsConfiguration = new System.Windows.Forms.Button();
            this.lstConfigurations = new System.Windows.Forms.ComboBox();
            this.menuConfigurationSource = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuConfigurationDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.tabLandscape = new System.Windows.Forms.TabPage();
            this.splitLandscape = new System.Windows.Forms.SplitContainer();
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
            this.tabResults = new System.Windows.Forms.TabPage();
            this.splitLimitsGraph = new System.Windows.Forms.SplitContainer();
            this.tableResults = new System.Windows.Forms.TableLayoutPanel();
            this.tableControls = new System.Windows.Forms.TableLayoutPanel();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.chkIterations = new System.Windows.Forms.CheckBox();
            this.chkFitness = new System.Windows.Forms.CheckBox();
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
            this.splitGraphStats = new System.Windows.Forms.SplitContainer();
            this.tabResultView = new System.Windows.Forms.TabControl();
            this.tabChart = new System.Windows.Forms.TabPage();
            this.chartResults = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tab2DPlot = new System.Windows.Forms.TabPage();
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
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtLog = new ICSharpCode.TextEditor.TextEditorControl();
            this.watcherConfigurationDirectory = new System.IO.FileSystemWatcher();
            this.visPopulation = new esecui.Visualiser();
            this.tabTabs.SuspendLayout();
            this.tabSystem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSystemErrors)).BeginInit();
            this.splitSystemErrors.Panel1.SuspendLayout();
            this.splitSystemErrors.Panel2.SuspendLayout();
            this.splitSystemErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSystemVariables)).BeginInit();
            this.splitSystemVariables.Panel1.SuspendLayout();
            this.splitSystemVariables.Panel2.SuspendLayout();
            this.splitSystemVariables.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuConfigurationSource.SuspendLayout();
            this.tabLandscape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitLandscape)).BeginInit();
            this.splitLandscape.Panel1.SuspendLayout();
            this.splitLandscape.Panel2.SuspendLayout();
            this.splitLandscape.SuspendLayout();
            this.tableLandscape.SuspendLayout();
            this.tabResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitLimitsGraph)).BeginInit();
            this.splitLimitsGraph.Panel1.SuspendLayout();
            this.splitLimitsGraph.Panel2.SuspendLayout();
            this.splitLimitsGraph.SuspendLayout();
            this.tableResults.SuspendLayout();
            this.tableControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGraphStats)).BeginInit();
            this.splitGraphStats.Panel1.SuspendLayout();
            this.splitGraphStats.Panel2.SuspendLayout();
            this.splitGraphStats.SuspendLayout();
            this.tabResultView.SuspendLayout();
            this.tabChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).BeginInit();
            this.tab2DPlot.SuspendLayout();
            this.tableStats.SuspendLayout();
            this.tabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.watcherConfigurationDirectory)).BeginInit();
            this.SuspendLayout();
            // 
            // tabTabs
            // 
            this.tabTabs.Controls.Add(this.tabSystem);
            this.tabTabs.Controls.Add(this.tabLandscape);
            this.tabTabs.Controls.Add(this.tabResults);
            this.tabTabs.Controls.Add(this.tabLog);
            this.tabTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTabs.Location = new System.Drawing.Point(0, 0);
            this.tabTabs.Name = "tabTabs";
            this.tabTabs.SelectedIndex = 0;
            this.tabTabs.Size = new System.Drawing.Size(963, 646);
            this.tabTabs.TabIndex = 0;
            // 
            // tabSystem
            // 
            this.tabSystem.Controls.Add(this.splitSystemErrors);
            this.tabSystem.Location = new System.Drawing.Point(4, 23);
            this.tabSystem.Name = "tabSystem";
            this.tabSystem.Padding = new System.Windows.Forms.Padding(3);
            this.tabSystem.Size = new System.Drawing.Size(955, 619);
            this.tabSystem.TabIndex = 0;
            this.tabSystem.Text = "System (F1)";
            this.tabSystem.UseVisualStyleBackColor = true;
            // 
            // splitSystemErrors
            // 
            this.splitSystemErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSystemErrors.Location = new System.Drawing.Point(3, 3);
            this.splitSystemErrors.Name = "splitSystemErrors";
            this.splitSystemErrors.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitSystemErrors.Panel1
            // 
            this.splitSystemErrors.Panel1.Controls.Add(this.splitSystemVariables);
            // 
            // splitSystemErrors.Panel2
            // 
            this.splitSystemErrors.Panel2.Controls.Add(this.lstErrors);
            this.splitSystemErrors.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitSystemErrors.Size = new System.Drawing.Size(949, 613);
            this.splitSystemErrors.SplitterDistance = 437;
            this.splitSystemErrors.TabIndex = 0;
            // 
            // splitSystemVariables
            // 
            this.splitSystemVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSystemVariables.Location = new System.Drawing.Point(0, 0);
            this.splitSystemVariables.Name = "splitSystemVariables";
            // 
            // splitSystemVariables.Panel1
            // 
            this.splitSystemVariables.Panel1.Controls.Add(this.txtSystem);
            // 
            // splitSystemVariables.Panel2
            // 
            this.splitSystemVariables.Panel2.Controls.Add(this.txtSystemVariables);
            this.splitSystemVariables.Size = new System.Drawing.Size(949, 437);
            this.splitSystemVariables.SplitterDistance = 653;
            this.splitSystemVariables.TabIndex = 0;
            // 
            // txtSystem
            // 
            this.txtSystem.ConvertTabsToSpaces = true;
            this.txtSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSystem.EnableFolding = false;
            this.txtSystem.IsReadOnly = false;
            this.txtSystem.Location = new System.Drawing.Point(0, 0);
            this.txtSystem.Name = "txtSystem";
            this.txtSystem.ShowVRuler = false;
            this.txtSystem.Size = new System.Drawing.Size(653, 437);
            this.txtSystem.TabIndex = 0;
            this.txtSystem.Text = resources.GetString("txtSystem.Text");
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
            this.txtSystemVariables.Size = new System.Drawing.Size(292, 437);
            this.txtSystemVariables.TabIndex = 0;
            this.txtSystemVariables.Text = "size: 50";
            // 
            // lstErrors
            // 
            this.lstErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLocation,
            this.colCode,
            this.colMessage});
            this.lstErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstErrors.FullRowSelect = true;
            this.lstErrors.Location = new System.Drawing.Point(0, 36);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.Size = new System.Drawing.Size(949, 136);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnCheckSyntax, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveConfiguration, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveAsConfiguration, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstConfigurations, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(949, 36);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnCheckSyntax
            // 
            this.btnCheckSyntax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCheckSyntax.AutoSize = true;
            this.btnCheckSyntax.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCheckSyntax.Enabled = false;
            this.btnCheckSyntax.Location = new System.Drawing.Point(136, 3);
            this.btnCheckSyntax.Name = "btnCheckSyntax";
            this.btnCheckSyntax.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnCheckSyntax.Size = new System.Drawing.Size(130, 30);
            this.btnCheckSyntax.TabIndex = 0;
            this.btnCheckSyntax.Text = "&Check Syntax (F4)";
            this.btnCheckSyntax.UseVisualStyleBackColor = true;
            this.btnCheckSyntax.Click += new System.EventHandler(this.btnCheckSyntax_Click);
            // 
            // btnSaveConfiguration
            // 
            this.btnSaveConfiguration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveConfiguration.AutoSize = true;
            this.btnSaveConfiguration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveConfiguration.Enabled = false;
            this.btnSaveConfiguration.Location = new System.Drawing.Point(807, 4);
            this.btnSaveConfiguration.Name = "btnSaveConfiguration";
            this.btnSaveConfiguration.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveConfiguration.Size = new System.Drawing.Size(49, 28);
            this.btnSaveConfiguration.TabIndex = 0;
            this.btnSaveConfiguration.Text = "&Save";
            this.btnSaveConfiguration.UseVisualStyleBackColor = true;
            this.btnSaveConfiguration.Click += new System.EventHandler(this.btnSaveConfiguration_Click);
            // 
            // btnSaveAsConfiguration
            // 
            this.btnSaveAsConfiguration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSaveAsConfiguration.AutoSize = true;
            this.btnSaveAsConfiguration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveAsConfiguration.Enabled = false;
            this.btnSaveAsConfiguration.Location = new System.Drawing.Point(862, 3);
            this.btnSaveAsConfiguration.Name = "btnSaveAsConfiguration";
            this.btnSaveAsConfiguration.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnSaveAsConfiguration.Size = new System.Drawing.Size(84, 30);
            this.btnSaveAsConfiguration.TabIndex = 0;
            this.btnSaveAsConfiguration.Text = "Save &As...";
            this.btnSaveAsConfiguration.UseVisualStyleBackColor = true;
            this.btnSaveAsConfiguration.Click += new System.EventHandler(this.btnSaveAsConfiguration_Click);
            // 
            // lstConfigurations
            // 
            this.lstConfigurations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConfigurations.ContextMenuStrip = this.menuConfigurationSource;
            this.lstConfigurations.Enabled = false;
            this.lstConfigurations.FormattingEnabled = true;
            this.lstConfigurations.Location = new System.Drawing.Point(408, 7);
            this.lstConfigurations.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lstConfigurations.Name = "lstConfigurations";
            this.lstConfigurations.Size = new System.Drawing.Size(390, 22);
            this.lstConfigurations.Sorted = true;
            this.lstConfigurations.TabIndex = 1;
            this.lstConfigurations.SelectedIndexChanged += new System.EventHandler(this.lstConfigurations_SelectedIndexChanged);
            this.lstConfigurations.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.lstConfigurations_Format);
            // 
            // menuConfigurationSource
            // 
            this.menuConfigurationSource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConfigurationDirectory});
            this.menuConfigurationSource.Name = "menuConfigurationSource";
            this.menuConfigurationSource.Size = new System.Drawing.Size(151, 26);
            // 
            // menuConfigurationDirectory
            // 
            this.menuConfigurationDirectory.Name = "menuConfigurationDirectory";
            this.menuConfigurationDirectory.Size = new System.Drawing.Size(150, 22);
            this.menuConfigurationDirectory.Text = "&Set Directory...";
            this.menuConfigurationDirectory.Click += new System.EventHandler(this.menuConfigurationDirectory_Click);
            // 
            // tabLandscape
            // 
            this.tabLandscape.Controls.Add(this.splitLandscape);
            this.tabLandscape.Location = new System.Drawing.Point(4, 23);
            this.tabLandscape.Name = "tabLandscape";
            this.tabLandscape.Padding = new System.Windows.Forms.Padding(3);
            this.tabLandscape.Size = new System.Drawing.Size(955, 619);
            this.tabLandscape.TabIndex = 1;
            this.tabLandscape.Text = "Landscape (F2)";
            this.tabLandscape.UseVisualStyleBackColor = true;
            // 
            // splitLandscape
            // 
            this.splitLandscape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLandscape.Location = new System.Drawing.Point(3, 3);
            this.splitLandscape.Name = "splitLandscape";
            // 
            // splitLandscape.Panel1
            // 
            this.splitLandscape.Panel1.Controls.Add(this.lstLandscapes);
            // 
            // splitLandscape.Panel2
            // 
            this.splitLandscape.Panel2.Controls.Add(this.tableLandscape);
            this.splitLandscape.Size = new System.Drawing.Size(949, 614);
            this.splitLandscape.SplitterDistance = 316;
            this.splitLandscape.TabIndex = 0;
            // 
            // lstLandscapes
            // 
            this.lstLandscapes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLandscapes.Location = new System.Drawing.Point(0, 0);
            this.lstLandscapes.Name = "lstLandscapes";
            this.lstLandscapes.Size = new System.Drawing.Size(316, 614);
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
            this.tableLandscape.Size = new System.Drawing.Size(629, 614);
            this.tableLandscape.TabIndex = 0;
            // 
            // txtEvaluatorCode
            // 
            this.txtEvaluatorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEvaluatorCode.ConvertTabsToSpaces = true;
            this.txtEvaluatorCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEvaluatorCode.EnableFolding = false;
            this.txtEvaluatorCode.IsReadOnly = false;
            this.txtEvaluatorCode.Location = new System.Drawing.Point(96, 440);
            this.txtEvaluatorCode.Name = "txtEvaluatorCode";
            this.txtEvaluatorCode.ShowLineNumbers = false;
            this.txtEvaluatorCode.ShowVRuler = false;
            this.txtEvaluatorCode.Size = new System.Drawing.Size(530, 171);
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
            this.lblLandscapeInternalName.Size = new System.Drawing.Size(87, 14);
            this.lblLandscapeInternalName.TabIndex = 0;
            this.lblLandscapeInternalName.Text = "Internal name:";
            // 
            // txtLandscapeInternalName
            // 
            this.txtLandscapeInternalName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLandscapeInternalName.Location = new System.Drawing.Point(96, 3);
            this.txtLandscapeInternalName.Name = "txtLandscapeInternalName";
            this.txtLandscapeInternalName.ReadOnly = true;
            this.txtLandscapeInternalName.Size = new System.Drawing.Size(530, 22);
            this.txtLandscapeInternalName.TabIndex = 1;
            // 
            // lblLandscapeDescription
            // 
            this.lblLandscapeDescription.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLandscapeDescription.AutoSize = true;
            this.lblLandscapeDescription.Location = new System.Drawing.Point(19, 108);
            this.lblLandscapeDescription.Name = "lblLandscapeDescription";
            this.lblLandscapeDescription.Size = new System.Drawing.Size(71, 14);
            this.lblLandscapeDescription.TabIndex = 2;
            this.lblLandscapeDescription.Text = "Description:";
            // 
            // txtLandscapeDescription
            // 
            this.txtLandscapeDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLandscapeDescription.Location = new System.Drawing.Point(96, 31);
            this.txtLandscapeDescription.Multiline = true;
            this.txtLandscapeDescription.Name = "txtLandscapeDescription";
            this.txtLandscapeDescription.ReadOnly = true;
            this.txtLandscapeDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLandscapeDescription.Size = new System.Drawing.Size(530, 169);
            this.txtLandscapeDescription.TabIndex = 3;
            // 
            // lblLandscapeParameters
            // 
            this.lblLandscapeParameters.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLandscapeParameters.AutoSize = true;
            this.lblLandscapeParameters.Location = new System.Drawing.Point(18, 313);
            this.lblLandscapeParameters.Name = "lblLandscapeParameters";
            this.lblLandscapeParameters.Size = new System.Drawing.Size(72, 14);
            this.lblLandscapeParameters.TabIndex = 4;
            this.lblLandscapeParameters.Text = "Parameters:";
            // 
            // txtLandscapeParameters
            // 
            this.txtLandscapeParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLandscapeParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLandscapeParameters.EnableFolding = false;
            this.txtLandscapeParameters.IsReadOnly = false;
            this.txtLandscapeParameters.Location = new System.Drawing.Point(96, 206);
            this.txtLandscapeParameters.Name = "txtLandscapeParameters";
            this.txtLandscapeParameters.ShowLineNumbers = false;
            this.txtLandscapeParameters.ShowVRuler = false;
            this.txtLandscapeParameters.Size = new System.Drawing.Size(530, 228);
            this.txtLandscapeParameters.TabIndex = 5;
            // 
            // lblEvaluatorCode
            // 
            this.lblEvaluatorCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEvaluatorCode.AutoSize = true;
            this.lblEvaluatorCode.Location = new System.Drawing.Point(8, 518);
            this.lblEvaluatorCode.Name = "lblEvaluatorCode";
            this.lblEvaluatorCode.Size = new System.Drawing.Size(82, 14);
            this.lblEvaluatorCode.TabIndex = 4;
            this.lblEvaluatorCode.Text = "Python Code:";
            this.lblEvaluatorCode.Visible = false;
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.splitLimitsGraph);
            this.tabResults.Location = new System.Drawing.Point(4, 23);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults.Size = new System.Drawing.Size(955, 619);
            this.tabResults.TabIndex = 2;
            this.tabResults.Text = "Results (F3)";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // splitLimitsGraph
            // 
            this.splitLimitsGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLimitsGraph.Location = new System.Drawing.Point(3, 3);
            this.splitLimitsGraph.Name = "splitLimitsGraph";
            // 
            // splitLimitsGraph.Panel1
            // 
            this.splitLimitsGraph.Panel1.Controls.Add(this.tableResults);
            // 
            // splitLimitsGraph.Panel2
            // 
            this.splitLimitsGraph.Panel2.Controls.Add(this.splitGraphStats);
            this.splitLimitsGraph.Size = new System.Drawing.Size(949, 614);
            this.splitLimitsGraph.SplitterDistance = 316;
            this.splitLimitsGraph.TabIndex = 0;
            // 
            // tableResults
            // 
            this.tableResults.ColumnCount = 1;
            this.tableResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableResults.Controls.Add(this.tableControls, 0, 0);
            this.tableResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableResults.Location = new System.Drawing.Point(0, 0);
            this.tableResults.Name = "tableResults";
            this.tableResults.RowCount = 6;
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableResults.Size = new System.Drawing.Size(316, 614);
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
            this.tableControls.Controls.Add(this.btnStartStop, 0, 0);
            this.tableControls.Controls.Add(this.chkIterations, 2, 1);
            this.tableControls.Controls.Add(this.chkFitness, 2, 4);
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
            this.tableControls.Size = new System.Drawing.Size(316, 163);
            this.tableControls.TabIndex = 0;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStartStop.AutoSize = true;
            this.btnStartStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableControls.SetColumnSpan(this.btnStartStop, 3);
            this.btnStartStop.Enabled = false;
            this.btnStartStop.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnStartStop.Location = new System.Drawing.Point(101, 3);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Padding = new System.Windows.Forms.Padding(8);
            this.btnStartStop.Size = new System.Drawing.Size(114, 45);
            this.btnStartStop.TabIndex = 12;
            this.btnStartStop.Text = "&Start (F5)";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // chkIterations
            // 
            this.chkIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIterations.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIterations.Checked = true;
            this.chkIterations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIterations.Location = new System.Drawing.Point(227, 56);
            this.chkIterations.Name = "chkIterations";
            this.chkIterations.Size = new System.Drawing.Size(86, 18);
            this.chkIterations.TabIndex = 2;
            this.chkIterations.Text = "iterations";
            this.chkIterations.UseVisualStyleBackColor = true;
            this.chkIterations.MouseEnter += new System.EventHandler(this.chkLimit_MouseEnter);
            this.chkIterations.MouseLeave += new System.EventHandler(this.chkLimit_MouseLeave);
            // 
            // chkFitness
            // 
            this.chkFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFitness.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFitness.Location = new System.Drawing.Point(227, 140);
            this.chkFitness.Name = "chkFitness";
            this.chkFitness.Size = new System.Drawing.Size(86, 18);
            this.chkFitness.TabIndex = 11;
            this.chkFitness.Text = "fitness";
            this.chkFitness.UseVisualStyleBackColor = true;
            this.chkFitness.MouseEnter += new System.EventHandler(this.chkLimit_MouseEnter);
            this.chkFitness.MouseLeave += new System.EventHandler(this.chkLimit_MouseLeave);
            // 
            // chkSeconds
            // 
            this.chkSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSeconds.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSeconds.Location = new System.Drawing.Point(227, 112);
            this.chkSeconds.Name = "chkSeconds";
            this.chkSeconds.Size = new System.Drawing.Size(86, 18);
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
            this.chkEvaluations.Location = new System.Drawing.Point(227, 84);
            this.chkEvaluations.Name = "chkEvaluations";
            this.chkEvaluations.Size = new System.Drawing.Size(86, 18);
            this.chkEvaluations.TabIndex = 5;
            this.chkEvaluations.Text = "evaluations";
            this.chkEvaluations.UseVisualStyleBackColor = true;
            this.chkEvaluations.MouseEnter += new System.EventHandler(this.chkLimit_MouseEnter);
            this.chkEvaluations.MouseLeave += new System.EventHandler(this.chkLimit_MouseLeave);
            // 
            // txtFitness
            // 
            this.txtFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFitness.Location = new System.Drawing.Point(72, 138);
            this.txtFitness.Name = "txtFitness";
            this.txtFitness.Size = new System.Drawing.Size(149, 22);
            this.txtFitness.TabIndex = 10;
            this.txtFitness.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFitness_KeyDown);
            this.txtFitness.Validated += new System.EventHandler(this.txtFitness_Validated);
            // 
            // txtEvaluations
            // 
            this.txtEvaluations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEvaluations.Location = new System.Drawing.Point(72, 82);
            this.txtEvaluations.Name = "txtEvaluations";
            this.txtEvaluations.Size = new System.Drawing.Size(149, 22);
            this.txtEvaluations.TabIndex = 4;
            this.txtEvaluations.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEvaluations_KeyDown);
            this.txtEvaluations.Validated += new System.EventHandler(this.txtEvaluations_Validated);
            // 
            // txtSeconds
            // 
            this.txtSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSeconds.Location = new System.Drawing.Point(72, 110);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(149, 22);
            this.txtSeconds.TabIndex = 7;
            this.txtSeconds.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeconds_KeyDown);
            this.txtSeconds.Validated += new System.EventHandler(this.txtSeconds_Validated);
            // 
            // txtIterations
            // 
            this.txtIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIterations.Location = new System.Drawing.Point(72, 54);
            this.txtIterations.Name = "txtIterations";
            this.txtIterations.Size = new System.Drawing.Size(149, 22);
            this.txtIterations.TabIndex = 1;
            this.txtIterations.Text = "10";
            this.txtIterations.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIterations_KeyDown);
            this.txtIterations.Validated += new System.EventHandler(this.txtIterations_Validated);
            // 
            // lblStopAfter
            // 
            this.lblStopAfter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStopAfter.AutoSize = true;
            this.lblStopAfter.Location = new System.Drawing.Point(3, 58);
            this.lblStopAfter.Name = "lblStopAfter";
            this.lblStopAfter.Size = new System.Drawing.Size(63, 14);
            this.lblStopAfter.TabIndex = 0;
            this.lblStopAfter.Text = "Stop after";
            // 
            // lblOr1
            // 
            this.lblOr1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOr1.AutoSize = true;
            this.lblOr1.Location = new System.Drawing.Point(48, 86);
            this.lblOr1.Name = "lblOr1";
            this.lblOr1.Size = new System.Drawing.Size(18, 14);
            this.lblOr1.TabIndex = 3;
            this.lblOr1.Text = "or";
            // 
            // lblOr3
            // 
            this.lblOr3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOr3.AutoSize = true;
            this.lblOr3.Location = new System.Drawing.Point(33, 142);
            this.lblOr3.Name = "lblOr3";
            this.lblOr3.Size = new System.Drawing.Size(33, 14);
            this.lblOr3.TabIndex = 9;
            this.lblOr3.Text = "or at";
            // 
            // lblOr2
            // 
            this.lblOr2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOr2.AutoSize = true;
            this.lblOr2.Location = new System.Drawing.Point(48, 114);
            this.lblOr2.Name = "lblOr2";
            this.lblOr2.Size = new System.Drawing.Size(18, 14);
            this.lblOr2.TabIndex = 6;
            this.lblOr2.Text = "or";
            // 
            // splitGraphStats
            // 
            this.splitGraphStats.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitGraphStats.Size = new System.Drawing.Size(629, 614);
            this.splitGraphStats.SplitterDistance = 459;
            this.splitGraphStats.TabIndex = 0;
            // 
            // tabResultView
            // 
            this.tabResultView.Controls.Add(this.tabChart);
            this.tabResultView.Controls.Add(this.tab2DPlot);
            this.tabResultView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResultView.Location = new System.Drawing.Point(0, 0);
            this.tabResultView.Name = "tabResultView";
            this.tabResultView.SelectedIndex = 0;
            this.tabResultView.Size = new System.Drawing.Size(629, 459);
            this.tabResultView.TabIndex = 0;
            // 
            // tabChart
            // 
            this.tabChart.Controls.Add(this.chartResults);
            this.tabChart.Location = new System.Drawing.Point(4, 23);
            this.tabChart.Name = "tabChart";
            this.tabChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabChart.Size = new System.Drawing.Size(621, 432);
            this.tabChart.TabIndex = 0;
            this.tabChart.Text = "Chart (Alt+1)";
            this.tabChart.UseVisualStyleBackColor = true;
            // 
            // chartResults
            // 
            this.chartResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartResults.Location = new System.Drawing.Point(3, 3);
            this.chartResults.Name = "chartResults";
            this.chartResults.Size = new System.Drawing.Size(615, 426);
            this.chartResults.TabIndex = 0;
            // 
            // tab2DPlot
            // 
            this.tab2DPlot.Controls.Add(this.visPopulation);
            this.tab2DPlot.Location = new System.Drawing.Point(4, 23);
            this.tab2DPlot.Name = "tab2DPlot";
            this.tab2DPlot.Padding = new System.Windows.Forms.Padding(3);
            this.tab2DPlot.Size = new System.Drawing.Size(621, 432);
            this.tab2DPlot.TabIndex = 1;
            this.tab2DPlot.Text = "2D Plot (Alt+2)";
            this.tab2DPlot.UseVisualStyleBackColor = true;
            // 
            // tableStats
            // 
            this.tableStats.ColumnCount = 4;
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
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
            this.tableStats.Controls.Add(this.txtStatsBestFitness, 3, 0);
            this.tableStats.Controls.Add(this.txtStatsCurrentBest, 3, 1);
            this.tableStats.Controls.Add(this.txtStatsCurrentMean, 3, 2);
            this.tableStats.Controls.Add(this.txtStatsCurrentWorst, 3, 3);
            this.tableStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableStats.Location = new System.Drawing.Point(0, 0);
            this.tableStats.Name = "tableStats";
            this.tableStats.RowCount = 4;
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableStats.Size = new System.Drawing.Size(629, 151);
            this.tableStats.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Iterations:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Evaluations:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "Births:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Time:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(308, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Best Fitness:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(308, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "Current Best:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(308, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 14);
            this.label7.TabIndex = 12;
            this.label7.Text = "Current Mean:";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(308, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 14);
            this.label8.TabIndex = 14;
            this.label8.Text = "Current Worst:";
            // 
            // txtStatsIterations
            // 
            this.txtStatsIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsIterations.Location = new System.Drawing.Point(86, 7);
            this.txtStatsIterations.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsIterations.Name = "txtStatsIterations";
            this.txtStatsIterations.ReadOnly = true;
            this.txtStatsIterations.Size = new System.Drawing.Size(210, 22);
            this.txtStatsIterations.TabIndex = 1;
            this.txtStatsIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsEvaluations
            // 
            this.txtStatsEvaluations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsEvaluations.Location = new System.Drawing.Point(86, 44);
            this.txtStatsEvaluations.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsEvaluations.Name = "txtStatsEvaluations";
            this.txtStatsEvaluations.ReadOnly = true;
            this.txtStatsEvaluations.Size = new System.Drawing.Size(210, 22);
            this.txtStatsEvaluations.TabIndex = 3;
            this.txtStatsEvaluations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsBirths
            // 
            this.txtStatsBirths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsBirths.Location = new System.Drawing.Point(86, 81);
            this.txtStatsBirths.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsBirths.Name = "txtStatsBirths";
            this.txtStatsBirths.ReadOnly = true;
            this.txtStatsBirths.Size = new System.Drawing.Size(210, 22);
            this.txtStatsBirths.TabIndex = 5;
            this.txtStatsBirths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsTime
            // 
            this.txtStatsTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsTime.Location = new System.Drawing.Point(86, 120);
            this.txtStatsTime.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsTime.Name = "txtStatsTime";
            this.txtStatsTime.ReadOnly = true;
            this.txtStatsTime.Size = new System.Drawing.Size(210, 22);
            this.txtStatsTime.TabIndex = 7;
            this.txtStatsTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsBestFitness
            // 
            this.txtStatsBestFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsBestFitness.Location = new System.Drawing.Point(409, 7);
            this.txtStatsBestFitness.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsBestFitness.Name = "txtStatsBestFitness";
            this.txtStatsBestFitness.ReadOnly = true;
            this.txtStatsBestFitness.Size = new System.Drawing.Size(211, 22);
            this.txtStatsBestFitness.TabIndex = 9;
            this.txtStatsBestFitness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsCurrentBest
            // 
            this.txtStatsCurrentBest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsCurrentBest.Location = new System.Drawing.Point(409, 44);
            this.txtStatsCurrentBest.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsCurrentBest.Name = "txtStatsCurrentBest";
            this.txtStatsCurrentBest.ReadOnly = true;
            this.txtStatsCurrentBest.Size = new System.Drawing.Size(211, 22);
            this.txtStatsCurrentBest.TabIndex = 11;
            this.txtStatsCurrentBest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsCurrentMean
            // 
            this.txtStatsCurrentMean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsCurrentMean.Location = new System.Drawing.Point(409, 81);
            this.txtStatsCurrentMean.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsCurrentMean.Name = "txtStatsCurrentMean";
            this.txtStatsCurrentMean.ReadOnly = true;
            this.txtStatsCurrentMean.Size = new System.Drawing.Size(211, 22);
            this.txtStatsCurrentMean.TabIndex = 13;
            this.txtStatsCurrentMean.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStatsCurrentWorst
            // 
            this.txtStatsCurrentWorst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatsCurrentWorst.Location = new System.Drawing.Point(409, 120);
            this.txtStatsCurrentWorst.Margin = new System.Windows.Forms.Padding(9, 3, 9, 3);
            this.txtStatsCurrentWorst.Name = "txtStatsCurrentWorst";
            this.txtStatsCurrentWorst.ReadOnly = true;
            this.txtStatsCurrentWorst.Size = new System.Drawing.Size(211, 22);
            this.txtStatsCurrentWorst.TabIndex = 15;
            this.txtStatsCurrentWorst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 23);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(955, 619);
            this.tabLog.TabIndex = 3;
            this.tabLog.Text = "Log Messages (F12)";
            this.tabLog.UseVisualStyleBackColor = true;
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
            this.txtLog.Size = new System.Drawing.Size(949, 614);
            this.txtLog.TabIndex = 0;
            // 
            // watcherConfigurationDirectory
            // 
            this.watcherConfigurationDirectory.EnableRaisingEvents = true;
            this.watcherConfigurationDirectory.Filter = "*.py;*.xml";
            this.watcherConfigurationDirectory.SynchronizingObject = this;
            this.watcherConfigurationDirectory.Changed += new System.IO.FileSystemEventHandler(this.watcherConfigurationDirectory_Changed);
            this.watcherConfigurationDirectory.Created += new System.IO.FileSystemEventHandler(this.watcherConfigurationDirectory_Created);
            this.watcherConfigurationDirectory.Deleted += new System.IO.FileSystemEventHandler(this.watcherConfigurationDirectory_Deleted);
            this.watcherConfigurationDirectory.Renamed += new System.IO.RenamedEventHandler(this.watcherConfigurationDirectory_Renamed);
            // 
            // visPopulation
            // 
            this.visPopulation.AutoRange = true;
            this.visPopulation.BackColor = System.Drawing.Color.White;
            this.visPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visPopulation.Location = new System.Drawing.Point(3, 3);
            this.visPopulation.Name = "visPopulation";
            this.visPopulation.Size = new System.Drawing.Size(615, 427);
            this.visPopulation.TabIndex = 1;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 646);
            this.Controls.Add(this.tabTabs);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.KeyPreview = true;
            this.Name = "Editor";
            this.Text = "esec Experiment Designer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Editor_FormClosed);
            this.Load += new System.EventHandler(this.Editor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            this.tabTabs.ResumeLayout(false);
            this.tabSystem.ResumeLayout(false);
            this.splitSystemErrors.Panel1.ResumeLayout(false);
            this.splitSystemErrors.Panel2.ResumeLayout(false);
            this.splitSystemErrors.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSystemErrors)).EndInit();
            this.splitSystemErrors.ResumeLayout(false);
            this.splitSystemVariables.Panel1.ResumeLayout(false);
            this.splitSystemVariables.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSystemVariables)).EndInit();
            this.splitSystemVariables.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuConfigurationSource.ResumeLayout(false);
            this.tabLandscape.ResumeLayout(false);
            this.splitLandscape.Panel1.ResumeLayout(false);
            this.splitLandscape.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLandscape)).EndInit();
            this.splitLandscape.ResumeLayout(false);
            this.tableLandscape.ResumeLayout(false);
            this.tableLandscape.PerformLayout();
            this.tabResults.ResumeLayout(false);
            this.splitLimitsGraph.Panel1.ResumeLayout(false);
            this.splitLimitsGraph.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLimitsGraph)).EndInit();
            this.splitLimitsGraph.ResumeLayout(false);
            this.tableResults.ResumeLayout(false);
            this.tableResults.PerformLayout();
            this.tableControls.ResumeLayout(false);
            this.tableControls.PerformLayout();
            this.splitGraphStats.Panel1.ResumeLayout(false);
            this.splitGraphStats.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGraphStats)).EndInit();
            this.splitGraphStats.ResumeLayout(false);
            this.tabResultView.ResumeLayout(false);
            this.tabChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).EndInit();
            this.tab2DPlot.ResumeLayout(false);
            this.tableStats.ResumeLayout(false);
            this.tableStats.PerformLayout();
            this.tabLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.watcherConfigurationDirectory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabTabs;
        private System.Windows.Forms.TabPage tabSystem;
        private System.Windows.Forms.TabPage tabLandscape;
        private System.Windows.Forms.TabPage tabResults;
        private System.Windows.Forms.SplitContainer splitSystemErrors;
        private System.Windows.Forms.SplitContainer splitSystemVariables;
        private System.Windows.Forms.ListView lstErrors;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCheckSyntax;
        private System.Windows.Forms.ColumnHeader colLocation;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.SplitContainer splitLandscape;
        private System.Windows.Forms.TreeView lstLandscapes;
        private System.Windows.Forms.TableLayoutPanel tableLandscape;
        private System.Windows.Forms.Label lblLandscapeInternalName;
        private System.Windows.Forms.TextBox txtLandscapeInternalName;
        private System.Windows.Forms.Label lblLandscapeDescription;
        private System.Windows.Forms.TextBox txtLandscapeDescription;
        private System.Windows.Forms.Label lblLandscapeParameters;
        private System.Windows.Forms.SplitContainer splitLimitsGraph;
        private System.Windows.Forms.TableLayoutPanel tableResults;
        private System.Windows.Forms.SplitContainer splitGraphStats;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResults;
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
        private ICSharpCode.TextEditor.TextEditorControl txtSystem;
        private ICSharpCode.TextEditor.TextEditorControl txtSystemVariables;
        private ICSharpCode.TextEditor.TextEditorControl txtLog;
        private ICSharpCode.TextEditor.TextEditorControl txtLandscapeParameters;
        private System.Windows.Forms.Button btnSaveConfiguration;
        private System.Windows.Forms.Button btnSaveAsConfiguration;
        private System.Windows.Forms.ComboBox lstConfigurations;
        private System.Windows.Forms.ContextMenuStrip menuConfigurationSource;
        private System.IO.FileSystemWatcher watcherConfigurationDirectory;
        private System.Windows.Forms.ToolStripMenuItem menuConfigurationDirectory;
        private System.Windows.Forms.TableLayoutPanel tableControls;
        private System.Windows.Forms.Button btnStartStop;
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
        private Visualiser visPopulation;
        private ICSharpCode.TextEditor.TextEditorControl txtEvaluatorCode;
        private System.Windows.Forms.Label lblEvaluatorCode;
    }
}

