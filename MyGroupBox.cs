﻿#region License
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
        private Map map;
        public Label picbox = new Label();
        RadioButton radioDiff = new RadioButton();
        RadioButton radioOld = new RadioButton();
        RadioButton radioNew = new RadioButton();

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
                Button save = new Button();
                save.Text = "Save";
                save.Location = new System.Drawing.Point(8, 78);
                save.Click += buttonSave_Click;
                Controls.Add(save);
            }

            picbox.MinimumSize = new Size(map.minimap.Width, map.minimap.Height);
            picbox.AutoSize = true;
            if (map.diffMinimap == null)
            {
                picbox.Text = map.diffText;
                picbox.Image = null;
            }
            else
            {
                picbox.Text = "";
                picbox.Image = map.diffMinimap;
            }
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

        void buttonSave_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!button.Enabled)
                return;

            button.Enabled = false;
            map.isDifferent = false;
            map.diffText = "Saved.";
            if (radioDiff.Checked)
            {
                picbox.Image = null;
                picbox.Text = map.diffText;
            }

            if (map.diffMinimap != null)
            {
                map.diffMinimap.Dispose();
                map.diffMinimap = null;
            }

            try
            {
                map.minimap.Save(Path.Combine(Repo.pathMinimaps, map.name) + ".png");
            }
            catch(Exception ex)
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
