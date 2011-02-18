using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace esecui
{
    partial class Editor
    {
        private sealed class EditorViewState : IDisposable
        {
            [Flags]
            public enum LockSet
            {
                None = 0,
                StartExperiment,
                PauseExperiment,
                StopExperiment,
                Menus,
                Tabs,
                Panels,
                ConfigurationList,
                Expressions,
                RunExperiment,

                Everything = -1
            }


            public Editor Owner { get; private set; }
            public bool IsLocked { get; private set; }
            private LockSet Locked;
            public bool IsDimmed { get; private set; }
            public bool IsBusy { get; private set; }

            public EditorViewState(Editor owner, LockSet locked, bool dimmed, bool busy)
            {
                Owner = owner;
                IsLocked = (locked != LockSet.None);
                Locked = locked;
#if MONO
                IsDimmed = false;
#else
                IsDimmed = dimmed;
#endif
                IsBusy = busy;

                if (Owner.InvokeRequired) Owner.Invoke((Action)Begin);
                else Begin();
            }

            public bool IsDisposed { get; private set; }
            public void Dispose()
            {
                if (IsDisposed) return;
                IsDisposed = true;

                if (Owner.InvokeRequired) Owner.Invoke((Action)End);
                else End();
            }

            private List<Control> ToEnable, ToDisable;
            private List<CheckBox> ToUncheck;
            private void Begin()
            {
                if (IsDimmed)
                {
                    Owner.LookDisabled();
                }

                ToEnable = null;
                ToDisable = null;
                ToUncheck = null;
                if (IsLocked)
                {
                    ToEnable = new List<Control>();
                    if (Locked.HasFlag(LockSet.StartExperiment)) ToEnable.Add(Owner.btnStart);
                    if (Locked.HasFlag(LockSet.PauseExperiment)) ToEnable.Add(Owner.btnPause);
                    if (Locked.HasFlag(LockSet.StopExperiment)) ToEnable.Add(Owner.btnStop);
                    if (Locked.HasFlag(LockSet.Menus)) ToEnable.Add(Owner.menuStrip);
                    if (Locked.HasFlag(LockSet.Tabs))
                    {
                        ToEnable.Add(Owner.chkSystem);
                        ToEnable.Add(Owner.chkLandscape);
                        ToEnable.Add(Owner.chkResults);
                        ToEnable.Add(Owner.chkLog);
                    }
                    if (Locked.HasFlag(LockSet.Panels))
                    {
                        ToEnable.Add(Owner.panelSystem);
                        ToEnable.Add(Owner.panelLandscape);
                        ToEnable.Add(Owner.panelResults);
                        ToEnable.Add(Owner.panelLog);
                    }
                    if (Locked.HasFlag(LockSet.ConfigurationList)) ToEnable.Add(Owner.lstConfigurations);
                    if (Locked.HasFlag(LockSet.Expressions))
                    {
                        ToEnable.Add(Owner.txtPlotExpression);
                        ToEnable.Add(Owner.txtBestIndividualExpression);
                    }

                    if (Locked.HasFlag(LockSet.RunExperiment))
                    {
                        ToEnable.Add(Owner.btnStart);
                        ToEnable.Add(Owner.txtPlotExpression);
                        
                        ToDisable = new List<Control>();
                        ToDisable.Add(Owner.btnPause);
                        ToDisable.Add(Owner.btnStop);

                        ToUncheck = new List<CheckBox>();
                        ToUncheck.Add(Owner.btnPause);
                    }

                    ToEnable.RemoveAll(c => c.Enabled == false);
                    foreach (var c in ToEnable) c.Enabled = false;
                    ToDisable.RemoveAll(c => c.Enabled == true);
                    foreach (var c in ToDisable) c.Enabled = true;
                }

                if (IsBusy)
                {
                    Owner.UseWaitCursor = true;
                }

                Owner.Refresh();
            }

            private void End()
            {
                if (ToEnable != null && ToEnable.Any())
                {
                    foreach (var c in ToEnable) c.Enabled = true;
                }
                if (ToDisable != null && ToDisable.Any())
                {
                    foreach (var c in ToDisable) c.Enabled = false;
                }
                if (ToUncheck != null && ToUncheck.Any())
                {
                    foreach (var c in ToUncheck) c.Checked = false;
                }

                if (IsDimmed)
                {
                    Owner.LookEnabled();
                }

                if (IsBusy)
                {
                    Owner.UseWaitCursor = false;
                }

                Owner.Refresh();
            }

            public static EditorViewState Busy(Editor owner)
            {
                return new EditorViewState(owner, LockSet.None, false, true);
            }

            public static EditorViewState Experiment(Editor owner)
            {
                return new EditorViewState(owner, LockSet.RunExperiment, false, false);
            }

            public static EditorViewState Loading(Editor owner)
            {
                return new EditorViewState(owner, LockSet.Everything, true, true);
            }

            public static EditorViewState Lock(Editor owner)
            {
                return new EditorViewState(owner, LockSet.Everything, true, false);
            }
        }

        #region Dimming Functions

#if MONO
        private void LookDisabled()
        {
            picDimmer.SendToBack();
            picDimmer.Visible = true;
        }

        private void LookEnabled()
        {
            panelMenu.Enabled = true;
            panelSystem.Enabled = true;
            panelLandscape.Enabled = true;
            panelResults.Enabled = true;
            panelLog.Enabled = true;
            picDimmer.Visible = false;
        }

        private void Editor_ClientSizeChanged(object sender, EventArgs e)
        { }

        private void Editor_ResizeBegin(object sender, EventArgs e)
        { }

        private void Editor_ResizeEnd(object sender, EventArgs e)
        { }

#else
        // Exclude the entire function from Mono builds to get an error if we try and use it.
        private void DrawPanelToBitmap(Panel panel, Bitmap bitmap)
        {
            if (panel.Visible) panel.DrawToBitmap(bitmap, new Rectangle(panel.Location, panel.Size));
        }

        private void LookDisabled()
        {
            Image oldImage = null;
            if (picDimmer.Visible) oldImage = picDimmer.Image;

            bool success = false;
            var dimmed = new Bitmap(ClientSize.Width, ClientSize.Height);
            try
            {
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
                success = true;
            }
            finally
            {
                if (!success)
                {
                    picDimmer.Image = null;
                    dimmed.Dispose();
                }
            }
        }

        private void LookEnabled()
        {
            var dimmed = picDimmer.Image;
            picDimmer.Image = null;
            picDimmer.Visible = false;
            if (dimmed != null) dimmed.Dispose();
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

#endif

        #endregion
    }
}
