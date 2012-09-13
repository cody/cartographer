#region License
/*
 * This file is part of Cartographer.
 *
 *  Copyright 2012, Stefan Dombrowski
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 2 as
 *  published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace cartographer
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            Logger.init(textBoxLog, backgroundWorker);

            if (args.Length == 1)
                Repo.readPathXml(args[0]);
            else if (args.Length > 1)
                Logger.error("Too many command line arguments.");
        }

        private void textBoxRegex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' )
                runButton_Click();
        }

        private void runButton_Click(object sender = null, System.EventArgs e = null)
        {
            if (!Repo.knowsRepo)
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Select a client-data repository folder.";
                    folderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
                    folderDialog.ShowNewFolderButton = false;

                    var result = folderDialog.ShowDialog();
                    if (result.ToString() != "OK")
                        return;

                    if (!Repo.readPathXml(folderDialog.SelectedPath))
                    {
                        Logger.log("Aborting run.");
                        return;
                    }
                }
            }

            runButton.Enabled = false;
            Logger.clear();
            tabControl.SelectedIndex = 0;
            textBoxRegex.Text = textBoxRegex.Text.Trim();
            tableLayoutPanel.Controls.Clear();
            backgroundWorker.RunWorkerAsync(textBoxRegex.Text);
        }

        private void quitMenu_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void optionMenu_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem choice = (ToolStripMenuItem)sender;

            if (choice.CheckState == CheckState.Checked)
                return;

            if (sender == showChangedMapsOnlyMenu)
            {
                showChangedMapsOnlyMenu.CheckState = CheckState.Checked;
                showAllMapsMenu.CheckState = CheckState.Unchecked;
            }
            else
            {
                showChangedMapsOnlyMenu.CheckState = CheckState.Unchecked;
                showAllMapsMenu.CheckState = CheckState.Checked;
            }
        }

        private void helpRegexMenu_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("http://msdn.microsoft.com/en-us/library/az24scfc.aspx");
        }

        private void aboutMenu_Click(object sender, System.EventArgs e)
        {
            AboutForm a = new AboutForm();
            a.ShowDialog();
            a.Dispose();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (!Repo.knowsRepo && e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop);
            Repo.readPathXml(data[0]);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime startTime = DateTime.Now;
            Logger.bw.ReportProgress(0, "Path to maps: " + Repo.pathMaps);
            Logger.bw.ReportProgress(0, "Path to minimaps: " + Repo.pathMinimaps);

            if (!Repo.findRegexMatches((string)e.Argument))
                return;

            TimeSpan timeSpan = DateTime.Now - startTime;
            string duration = "Time: ";
            if (timeSpan.TotalMinutes >= 1)
                duration += (int)timeSpan.TotalMinutes + " minutes ";
            duration += timeSpan.Seconds + " seconds";
            Logger.bw.ReportProgress(0, duration);
            Logger.bw.ReportProgress(2, "");
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch(e.ProgressPercentage)
            {
                case 0: Logger.log((string)e.UserState); break;
                case 1: Logger.error((string)e.UserState); break;
                case 2: Logger.reportErrors(); break;
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Logger.error("Unhandled error in backgroundworker. " + e.Error.Message);
                return;
            }
            else if (e.Cancelled)
            {
                Logger.error("Backgroundworker cancelled.");
                return;
            }

            runButton.Enabled = true;
        }
    }
}
