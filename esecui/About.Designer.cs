namespace esecui
{
    partial class About
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
            this.panelText = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNameVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkEsecui = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkEsec = new System.Windows.Forms.LinkLabel();
            this.linkIronPython = new System.Windows.Forms.LinkLabel();
            this.linkSharpDevelop = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelText.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelText
            // 
            this.panelText.AutoSize = true;
            this.panelText.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelText.Controls.Add(this.lblNameVersion);
            this.panelText.Controls.Add(this.label1);
            this.panelText.Controls.Add(this.linkEsecui);
            this.panelText.Controls.Add(this.label2);
            this.panelText.Controls.Add(this.label3);
            this.panelText.Controls.Add(this.linkEsec);
            this.panelText.Controls.Add(this.linkIronPython);
            this.panelText.Controls.Add(this.linkSharpDevelop);
            this.panelText.Controls.Add(this.btnClose);
            this.panelText.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelText.Location = new System.Drawing.Point(0, 0);
            this.panelText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelText.Name = "panelText";
            this.panelText.Size = new System.Drawing.Size(427, 206);
            this.panelText.TabIndex = 0;
            // 
            // lblNameVersion
            // 
            this.lblNameVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNameVersion.AutoSize = true;
            this.lblNameVersion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameVersion.Location = new System.Drawing.Point(158, 0);
            this.lblNameVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNameVersion.Name = "lblNameVersion";
            this.lblNameVersion.Size = new System.Drawing.Size(111, 21);
            this.lblNameVersion.TabIndex = 1;
            this.lblNameVersion.Text = "esecui a.b.c.d";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Evolutionary Computation prototyping tool.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // linkEsecui
            // 
            this.linkEsecui.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkEsecui.AutoSize = true;
            this.linkEsecui.Location = new System.Drawing.Point(123, 41);
            this.linkEsecui.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkEsecui.Name = "linkEsecui";
            this.linkEsecui.Size = new System.Drawing.Size(181, 20);
            this.linkEsecui.TabIndex = 3;
            this.linkEsecui.TabStop = true;
            this.linkEsecui.Tag = "http://code.google.com/p/esecui";
            this.linkEsecui.Text = "Source Code and Updates";
            this.linkEsecui.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = " ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(421, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "esecui contains code from the following open-source projects.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // linkEsec
            // 
            this.linkEsec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkEsec.AutoSize = true;
            this.linkEsec.Location = new System.Drawing.Point(194, 101);
            this.linkEsec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkEsec.Name = "linkEsec";
            this.linkEsec.Size = new System.Drawing.Size(38, 20);
            this.linkEsec.TabIndex = 6;
            this.linkEsec.TabStop = true;
            this.linkEsec.Tag = "http://code.google.com/p/esec";
            this.linkEsec.Text = "esec";
            this.linkEsec.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
            // 
            // linkIronPython
            // 
            this.linkIronPython.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkIronPython.AutoSize = true;
            this.linkIronPython.Location = new System.Drawing.Point(173, 121);
            this.linkIronPython.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkIronPython.Name = "linkIronPython";
            this.linkIronPython.Size = new System.Drawing.Size(80, 20);
            this.linkIronPython.TabIndex = 7;
            this.linkIronPython.TabStop = true;
            this.linkIronPython.Tag = "http://www.ironpython.net/";
            this.linkIronPython.Text = "IronPython";
            this.linkIronPython.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
            // 
            // linkSharpDevelop
            // 
            this.linkSharpDevelop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkSharpDevelop.AutoSize = true;
            this.linkSharpDevelop.Location = new System.Drawing.Point(162, 141);
            this.linkSharpDevelop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkSharpDevelop.Name = "linkSharpDevelop";
            this.linkSharpDevelop.Size = new System.Drawing.Size(103, 20);
            this.linkSharpDevelop.TabIndex = 8;
            this.linkSharpDevelop.TabStop = true;
            this.linkSharpDevelop.Tag = "http://www.sharpdevelop.net/OpenSource/SD/";
            this.linkSharpDevelop.Text = "SharpDevelop";
            this.linkSharpDevelop.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(163, 166);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // About
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(507, 247);
            this.Controls.Add(this.panelText);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "About";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About esecui";
            this.panelText.ResumeLayout(false);
            this.panelText.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelText;
        private System.Windows.Forms.Label lblNameVersion;
        private System.Windows.Forms.LinkLabel linkSharpDevelop;
        private System.Windows.Forms.LinkLabel linkIronPython;
        private System.Windows.Forms.LinkLabel linkEsec;
        private System.Windows.Forms.LinkLabel linkEsecui;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}