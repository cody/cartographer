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
using System.Windows.Forms;

namespace cartographer
{
    public static class Logger
    {
        private static TextBox textBox;
        private static int errorCounter;

        public static void init(TextBox tb)
        {
            textBox = tb;
        }

        public static void error(string s)
        {
            errorCounter++;
            log("Error: " + s);
        }

        public static void log(string s)
        {
            textBox.AppendText(s + Environment.NewLine);
        }

        public static void clear()
        {
            textBox.Clear();
            textBox.Update();
            errorCounter = 0;
        }

        public static void reportErrors()
        {
            log("Number of errors: " + errorCounter);
        }
    }
}
