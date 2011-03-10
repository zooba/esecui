namespace esecui
{
    partial class Export
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
            this.tableButtons = new System.Windows.Forms.TableLayoutPanel();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.txtPreview = new ICSharpCode.TextEditor.TextEditorControl();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.tableEdit = new System.Windows.Forms.TableLayoutPanel();
            this.groupParameters = new System.Windows.Forms.GroupBox();
            this.txtParameterEdit = new System.Windows.Forms.TextBox();
            this.lstParameters = new System.Windows.Forms.ListView();
            this.colIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupCount = new System.Windows.Forms.GroupBox();
            this.tableCount = new System.Windows.Forms.TableLayoutPanel();
            this.btnCountInfinite = new System.Windows.Forms.RadioButton();
            this.btnCountExact = new System.Windows.Forms.RadioButton();
            this.btnCountParameterList = new System.Windows.Forms.RadioButton();
            this.btnCountParameterCombinations = new System.Windows.Forms.RadioButton();
            this.txtCountExact = new System.Windows.Forms.NumericUpDown();
            this.picWarningParameterList = new System.Windows.Forms.PictureBox();
            this.groupOutput = new System.Windows.Forms.GroupBox();
            this.flowOutput = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOutputPlainText = new System.Windows.Forms.RadioButton();
            this.btnOutputCSV = new System.Windows.Forms.RadioButton();
            this.chkOutputConsole = new System.Windows.Forms.CheckBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.tableButtons.SuspendLayout();
            this.panelPreview.SuspendLayout();
            this.panelEdit.SuspendLayout();
            this.tableEdit.SuspendLayout();
            this.groupParameters.SuspendLayout();
            this.groupCount.SuspendLayout();
            this.tableCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountExact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWarningParameterList)).BeginInit();
            this.groupOutput.SuspendLayout();
            this.flowOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableButtons
            // 
            this.tableButtons.AutoSize = true;
            this.tableButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableButtons.ColumnCount = 4;
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableButtons.Controls.Add(this.chkPreview, 3, 0);
            this.tableButtons.Controls.Add(this.btnSave, 0, 0);
            this.tableButtons.Controls.Add(this.btnClose, 1, 0);
            this.tableButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableButtons.Location = new System.Drawing.Point(0, 0);
            this.tableButtons.Name = "tableButtons";
            this.tableButtons.RowCount = 1;
            this.tableButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableButtons.Size = new System.Drawing.Size(724, 37);
            this.tableButtons.TabIndex = 0;
            // 
            // chkPreview
            // 
            this.chkPreview.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPreview.AutoSize = true;
            this.chkPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPreview.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkPreview.Location = new System.Drawing.Point(663, 3);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(58, 31);
            this.chkPreview.TabIndex = 2;
            this.chkPreview.Text = "&Preview";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.CheckedChanged += new System.EventHandler(this.chkPreview_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(3);
            this.btnSave.Size = new System.Drawing.Size(56, 31);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(65, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(3);
            this.btnClose.Size = new System.Drawing.Size(52, 31);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelPreview
            // 
            this.panelPreview.Controls.Add(this.txtPreview);
            this.panelPreview.Location = new System.Drawing.Point(126, 253);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(200, 100);
            this.panelPreview.TabIndex = 1;
            this.panelPreview.Visible = false;
            // 
            // txtPreview
            // 
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.EnableFolding = false;
            this.txtPreview.IsReadOnly = false;
            this.txtPreview.Location = new System.Drawing.Point(0, 0);
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ShowLineNumbers = false;
            this.txtPreview.Size = new System.Drawing.Size(200, 100);
            this.txtPreview.TabIndex = 0;
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.tableEdit);
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEdit.Location = new System.Drawing.Point(0, 37);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(724, 466);
            this.panelEdit.TabIndex = 0;
            // 
            // tableEdit
            // 
            this.tableEdit.ColumnCount = 2;
            this.tableEdit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableEdit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableEdit.Controls.Add(this.groupParameters, 0, 1);
            this.tableEdit.Controls.Add(this.groupCount, 0, 0);
            this.tableEdit.Controls.Add(this.groupOutput, 1, 0);
            this.tableEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableEdit.Location = new System.Drawing.Point(0, 0);
            this.tableEdit.Name = "tableEdit";
            this.tableEdit.RowCount = 2;
            this.tableEdit.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableEdit.Size = new System.Drawing.Size(724, 466);
            this.tableEdit.TabIndex = 0;
            // 
            // groupParameters
            // 
            this.tableEdit.SetColumnSpan(this.groupParameters, 2);
            this.groupParameters.Controls.Add(this.txtParameterEdit);
            this.groupParameters.Controls.Add(this.lstParameters);
            this.groupParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupParameters.Location = new System.Drawing.Point(3, 154);
            this.groupParameters.Name = "groupParameters";
            this.groupParameters.Size = new System.Drawing.Size(718, 309);
            this.groupParameters.TabIndex = 2;
            this.groupParameters.TabStop = false;
            this.groupParameters.Text = "Use these parameters";
            // 
            // txtParameterEdit
            // 
            this.txtParameterEdit.AcceptsReturn = true;
            this.txtParameterEdit.AcceptsTab = true;
            this.txtParameterEdit.Location = new System.Drawing.Point(50, 33);
            this.txtParameterEdit.Name = "txtParameterEdit";
            this.txtParameterEdit.Size = new System.Drawing.Size(100, 23);
            this.txtParameterEdit.TabIndex = 2;
            this.txtParameterEdit.Visible = false;
            this.txtParameterEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtParameterEdit_KeyDown);
            this.txtParameterEdit.Leave += new System.EventHandler(this.txtParameterEdit_Leave);
            // 
            // lstParameters
            // 
            this.lstParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex});
            this.lstParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstParameters.FullRowSelect = true;
            this.lstParameters.GridLines = true;
            this.lstParameters.Location = new System.Drawing.Point(3, 19);
            this.lstParameters.Name = "lstParameters";
            this.lstParameters.Size = new System.Drawing.Size(712, 287);
            this.lstParameters.TabIndex = 0;
            this.lstParameters.UseCompatibleStateImageBehavior = false;
            this.lstParameters.View = System.Windows.Forms.View.Details;
            this.lstParameters.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstParameters_MouseClick);
            // 
            // colIndex
            // 
            this.colIndex.Text = "";
            this.colIndex.Width = 30;
            // 
            // groupCount
            // 
            this.groupCount.AutoSize = true;
            this.groupCount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupCount.Controls.Add(this.tableCount);
            this.groupCount.Location = new System.Drawing.Point(3, 3);
            this.groupCount.Name = "groupCount";
            this.groupCount.Size = new System.Drawing.Size(215, 145);
            this.groupCount.TabIndex = 1;
            this.groupCount.TabStop = false;
            this.groupCount.Text = "How many experiments?";
            // 
            // tableCount
            // 
            this.tableCount.AutoSize = true;
            this.tableCount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableCount.ColumnCount = 3;
            this.tableCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableCount.Controls.Add(this.btnCountInfinite, 0, 0);
            this.tableCount.Controls.Add(this.btnCountExact, 0, 1);
            this.tableCount.Controls.Add(this.btnCountParameterList, 0, 2);
            this.tableCount.Controls.Add(this.btnCountParameterCombinations, 0, 3);
            this.tableCount.Controls.Add(this.txtCountExact, 1, 1);
            this.tableCount.Controls.Add(this.picWarningParameterList, 2, 2);
            this.tableCount.Location = new System.Drawing.Point(3, 19);
            this.tableCount.Name = "tableCount";
            this.tableCount.RowCount = 4;
            this.tableCount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableCount.Size = new System.Drawing.Size(206, 104);
            this.tableCount.TabIndex = 0;
            // 
            // btnCountInfinite
            // 
            this.btnCountInfinite.AutoSize = true;
            this.btnCountInfinite.Checked = true;
            this.tableCount.SetColumnSpan(this.btnCountInfinite, 3);
            this.btnCountInfinite.Location = new System.Drawing.Point(3, 3);
            this.btnCountInfinite.Name = "btnCountInfinite";
            this.btnCountInfinite.Size = new System.Drawing.Size(129, 19);
            this.btnCountInfinite.TabIndex = 0;
            this.btnCountInfinite.TabStop = true;
            this.btnCountInfinite.Text = "Infinite experiments";
            this.btnCountInfinite.UseVisualStyleBackColor = true;
            this.btnCountInfinite.CheckedChanged += new System.EventHandler(this.btnCountOther_CheckChanged);
            // 
            // btnCountExact
            // 
            this.btnCountExact.AutoSize = true;
            this.btnCountExact.Location = new System.Drawing.Point(3, 28);
            this.btnCountExact.Name = "btnCountExact";
            this.btnCountExact.Size = new System.Drawing.Size(61, 19);
            this.btnCountExact.TabIndex = 1;
            this.btnCountExact.Text = "Exactly";
            this.btnCountExact.UseVisualStyleBackColor = true;
            this.btnCountExact.CheckedChanged += new System.EventHandler(this.btnCountExact_CheckedChanged);
            // 
            // btnCountParameterList
            // 
            this.btnCountParameterList.AutoSize = true;
            this.tableCount.SetColumnSpan(this.btnCountParameterList, 2);
            this.btnCountParameterList.Location = new System.Drawing.Point(3, 57);
            this.btnCountParameterList.Name = "btnCountParameterList";
            this.btnCountParameterList.Size = new System.Drawing.Size(144, 19);
            this.btnCountParameterList.TabIndex = 3;
            this.btnCountParameterList.Text = "Each set of parameters";
            this.btnCountParameterList.UseVisualStyleBackColor = true;
            this.btnCountParameterList.CheckedChanged += new System.EventHandler(this.btnCountOther_CheckChanged);
            // 
            // btnCountParameterCombinations
            // 
            this.btnCountParameterCombinations.AutoSize = true;
            this.tableCount.SetColumnSpan(this.btnCountParameterCombinations, 3);
            this.btnCountParameterCombinations.Location = new System.Drawing.Point(3, 82);
            this.btnCountParameterCombinations.Name = "btnCountParameterCombinations";
            this.btnCountParameterCombinations.Size = new System.Drawing.Size(200, 19);
            this.btnCountParameterCombinations.TabIndex = 4;
            this.btnCountParameterCombinations.Text = "Every combination of parameters";
            this.btnCountParameterCombinations.UseVisualStyleBackColor = true;
            this.btnCountParameterCombinations.CheckedChanged += new System.EventHandler(this.btnCountOther_CheckChanged);
            // 
            // txtCountExact
            // 
            this.txtCountExact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableCount.SetColumnSpan(this.txtCountExact, 2);
            this.txtCountExact.Enabled = false;
            this.txtCountExact.Location = new System.Drawing.Point(70, 28);
            this.txtCountExact.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.txtCountExact.MinimumSize = new System.Drawing.Size(100, 0);
            this.txtCountExact.Name = "txtCountExact";
            this.txtCountExact.Size = new System.Drawing.Size(133, 23);
            this.txtCountExact.TabIndex = 2;
            this.txtCountExact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCountExact.ThousandsSeparator = true;
            this.txtCountExact.Value = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            // 
            // picWarningParameterList
            // 
            this.picWarningParameterList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picWarningParameterList.BackColor = System.Drawing.Color.Transparent;
            this.picWarningParameterList.Image = global::esecui.Properties.Resources.warning;
            this.picWarningParameterList.Location = new System.Drawing.Point(191, 59);
            this.picWarningParameterList.Margin = new System.Windows.Forms.Padding(0);
            this.picWarningParameterList.Name = "picWarningParameterList";
            this.picWarningParameterList.Size = new System.Drawing.Size(15, 15);
            this.picWarningParameterList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picWarningParameterList.TabIndex = 5;
            this.picWarningParameterList.TabStop = false;
            this.toolTips.SetToolTip(this.picWarningParameterList, "Avoid blank parameter values when using this setting.");
            this.picWarningParameterList.Visible = false;
            // 
            // groupOutput
            // 
            this.groupOutput.AutoSize = true;
            this.groupOutput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupOutput.Controls.Add(this.flowOutput);
            this.groupOutput.Location = new System.Drawing.Point(365, 3);
            this.groupOutput.Name = "groupOutput";
            this.groupOutput.Size = new System.Drawing.Size(139, 116);
            this.groupOutput.TabIndex = 0;
            this.groupOutput.TabStop = false;
            this.groupOutput.Text = "Write output to...";
            // 
            // flowOutput
            // 
            this.flowOutput.AutoSize = true;
            this.flowOutput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowOutput.Controls.Add(this.btnOutputPlainText);
            this.flowOutput.Controls.Add(this.btnOutputCSV);
            this.flowOutput.Controls.Add(this.chkOutputConsole);
            this.flowOutput.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowOutput.Location = new System.Drawing.Point(3, 19);
            this.flowOutput.Name = "flowOutput";
            this.flowOutput.Size = new System.Drawing.Size(130, 75);
            this.flowOutput.TabIndex = 0;
            // 
            // btnOutputPlainText
            // 
            this.btnOutputPlainText.AutoSize = true;
            this.btnOutputPlainText.Location = new System.Drawing.Point(3, 3);
            this.btnOutputPlainText.Name = "btnOutputPlainText";
            this.btnOutputPlainText.Size = new System.Drawing.Size(97, 19);
            this.btnOutputPlainText.TabIndex = 0;
            this.btnOutputPlainText.TabStop = true;
            this.btnOutputPlainText.Text = "Plain text files";
            this.btnOutputPlainText.UseVisualStyleBackColor = true;
            // 
            // btnOutputCSV
            // 
            this.btnOutputCSV.AutoSize = true;
            this.btnOutputCSV.Location = new System.Drawing.Point(3, 28);
            this.btnOutputCSV.Name = "btnOutputCSV";
            this.btnOutputCSV.Size = new System.Drawing.Size(70, 19);
            this.btnOutputCSV.TabIndex = 1;
            this.btnOutputCSV.TabStop = true;
            this.btnOutputCSV.Text = "CSV files";
            this.btnOutputCSV.UseVisualStyleBackColor = true;
            // 
            // chkOutputConsole
            // 
            this.chkOutputConsole.AutoSize = true;
            this.chkOutputConsole.Location = new System.Drawing.Point(3, 53);
            this.chkOutputConsole.Name = "chkOutputConsole";
            this.chkOutputConsole.Size = new System.Drawing.Size(124, 19);
            this.chkOutputConsole.TabIndex = 2;
            this.chkOutputConsole.Text = "and to the console";
            this.chkOutputConsole.UseVisualStyleBackColor = true;
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 503);
            this.Controls.Add(this.panelEdit);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.tableButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "Export";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export esec Batch File";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Export_KeyDown);
            this.tableButtons.ResumeLayout(false);
            this.tableButtons.PerformLayout();
            this.panelPreview.ResumeLayout(false);
            this.panelEdit.ResumeLayout(false);
            this.tableEdit.ResumeLayout(false);
            this.tableEdit.PerformLayout();
            this.groupParameters.ResumeLayout(false);
            this.groupParameters.PerformLayout();
            this.groupCount.ResumeLayout(false);
            this.groupCount.PerformLayout();
            this.tableCount.ResumeLayout(false);
            this.tableCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountExact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWarningParameterList)).EndInit();
            this.groupOutput.ResumeLayout(false);
            this.groupOutput.PerformLayout();
            this.flowOutput.ResumeLayout(false);
            this.flowOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableButtons;
        private System.Windows.Forms.CheckBox chkPreview;
        private System.Windows.Forms.Panel panelPreview;
        private ICSharpCode.TextEditor.TextEditorControl txtPreview;
        private System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tableEdit;
        private System.Windows.Forms.GroupBox groupOutput;
        private System.Windows.Forms.FlowLayoutPanel flowOutput;
        private System.Windows.Forms.RadioButton btnOutputPlainText;
        private System.Windows.Forms.RadioButton btnOutputCSV;
        private System.Windows.Forms.CheckBox chkOutputConsole;
        private System.Windows.Forms.GroupBox groupCount;
        private System.Windows.Forms.TableLayoutPanel tableCount;
        private System.Windows.Forms.RadioButton btnCountInfinite;
        private System.Windows.Forms.RadioButton btnCountExact;
        private System.Windows.Forms.RadioButton btnCountParameterList;
        private System.Windows.Forms.RadioButton btnCountParameterCombinations;
        private System.Windows.Forms.NumericUpDown txtCountExact;
        private System.Windows.Forms.GroupBox groupParameters;
        private System.Windows.Forms.ListView lstParameters;
        private System.Windows.Forms.ColumnHeader colIndex;
        private System.Windows.Forms.TextBox txtParameterEdit;
        private System.Windows.Forms.PictureBox picWarningParameterList;
        private System.Windows.Forms.ToolTip toolTips;
    }
}