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

namespace cartographer
{
    static class Repo
    {
        public static bool knowsRepo { get; private set; }

        internal static bool readPathXml(string path)
        {
            Logger.log("Path to repo: " + path);
            return false;
        }
    }
}
