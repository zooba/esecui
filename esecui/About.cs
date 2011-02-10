using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace esecui
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            lblNameVersion.Text = "esecui " + Application.ProductVersion;
        }

        private void Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;
            var url = control.Tag as string;
            if (url == null) return;

            var psi = new ProcessStartInfo();
            psi.FileName = url;
            psi.UseShellExecute = true;
            Process.Start(psi);
        }
    }
}
