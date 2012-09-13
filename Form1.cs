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

using System.Windows.Forms;

namespace cartographer
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            Logger.init(textBoxLog);

            if (args.Length == 1)
                Repo.readPathXml(args[0]);
            else if (args.Length > 1)
                Logger.error("Too many command line arguments.");
        }

        private void runButton_Click(object sender, System.EventArgs e)
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
    }
}
