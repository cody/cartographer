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
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace cartographer
{
    class MyGroupBox : GroupBox
    {
        public Map map;
        public Label picbox = new Label();
        public RadioButton radioDiff = new RadioButton();
        public RadioButton radioOld = new RadioButton();
        public RadioButton radioNew = new RadioButton();
        public Button saveButton = new Button();

        public MyGroupBox(Map m)
        {
            map = m;
            AutoSize = true;
            MinimumSize = new System.Drawing.Size(10, m.height);
            Text = m.name;

            radioDiff.Location = new System.Drawing.Point(8, 15);
            radioDiff.Text = "Difference";
            radioDiff.Checked = true;
            radioDiff.CheckedChanged += radioButton_CheckedChanged;
            Controls.Add(radioDiff);
            radioOld.Location = new System.Drawing.Point(8, 33);
            radioOld.Text = "Old";
            radioOld.CheckedChanged += radioButton_CheckedChanged;
            Controls.Add(radioOld);
            radioNew.Location = new System.Drawing.Point(8, 51);
            radioNew.Text = "New";
            radioNew.CheckedChanged += radioButton_CheckedChanged;
            Controls.Add(radioNew);

            if (map.isDifferent)
            {
                saveButton.Text = "Save";
                saveButton.Location = new System.Drawing.Point(8, 78);
                saveButton.Click += buttonSave_Click;
                Controls.Add(saveButton);
            }
            int width = map.oldMinimap == null ? map.minimap.Width
                                               : Math.Max(map.minimap.Width, map.oldMinimap.Width);
            int height = map.oldMinimap == null ? map.minimap.Height
                                                : Math.Max(map.minimap.Height, map.oldMinimap.Height);
            picbox.MinimumSize = new Size(width, height);
            picbox.AutoSize = true;
            picbox.ImageAlign = ContentAlignment.TopLeft;
            if (map.diffMinimap == null)
                picbox.Text = map.diffText;
            else
                picbox.Image = map.diffMinimap;
        }

        void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (!button.Checked)
                return;

            if (button == radioDiff)
            {
                picbox.Text = map.diffText;
                picbox.Image = map.diffMinimap;
            }
            else if (button == radioOld)
            {
                picbox.Text = "";
                picbox.Image = map.oldMinimap;
            }
            else if (button == radioNew)
            {
                picbox.Text = "";
                picbox.Image = map.minimap;
            }
        }

        public void buttonSave_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!button.Enabled)
                return;

            button.Enabled = false;
            button.Visible = false;
            map.isDifferent = false;
            map.diffText = "Saved.";
            if (radioDiff.Checked || radioOld.Checked)
            {
                picbox.Image = null;
                picbox.Text = map.diffText;
            }

            if (map.diffMinimap != null)
            {
                map.diffMinimap.Dispose();
                map.diffMinimap = null;
            }

            if (map.oldMinimap != null)
            {
                map.oldMinimap.Dispose();
                map.oldMinimap = null;
            }

            try
            {
                map.minimap.Save(Path.Combine(Repo.pathMinimaps, map.name) + ".png");
            }
            catch (Exception ex)
            {
                map.isDifferent = true;
                Logger.error("Can't save map. " + ex.Message);
                map.diffText = "Error saving map.";
                picbox.Text = map.diffText;
                picbox.Image = null;
            }
        }
    }
}
