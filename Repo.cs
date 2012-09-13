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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace cartographer
{
    static class Repo
    {
        public static bool knowsRepo = false;
        static public string pathMaps = "";
        static public string pathMinimaps = "";
        static public Dictionary<string, string> regexMatches = new Dictionary<string, string>();

        internal static bool readPathXml(string path)
        {
            Logger.log("Path to repo: " + path);

            string fullPath = Path.Combine(path, "paths.xml");

            XElement xml;
            try
            {
                xml = XElement.Load(fullPath);
            }
            catch (Exception e)
            {
                Logger.error(e.Message);
                return false;
            }

            foreach (XElement child in xml.Elements())
            {
                if (child.Attribute("name").Value == "maps")
                    pathMaps = Path.Combine(path, child.Attribute("value").Value);
                else if (child.Attribute("name").Value == "minimaps")
                    pathMinimaps = Path.Combine(path, child.Attribute("value").Value);
            }

            if (pathMaps == "" || pathMinimaps == "")
            {
                Logger.error("No path for maps or minimaps in: " + fullPath);
                return false;
            }

            knowsRepo = true;
            return true;
        }

        internal static bool findRegexMatches(string regexString)
        {
            Regex regex;
            Logger.bw.ReportProgress(0, "Regex: " + regexString);
            try
            {
                regex = new Regex(regexString);
            }
            catch (Exception ex)
            {
                Logger.bw.ReportProgress(1, "Problem with regex: " + ex.Message);
                return false;
            }

            var mapDirInfo = new DirectoryInfo(pathMaps);
            var mapNames = mapDirInfo.EnumerateFiles("*.tmx");
            Logger.bw.ReportProgress(0, "Total number of maps: " + mapNames.Count());
            regexMatches.Clear();
            foreach (FileInfo f in mapNames)
            {
                string name = f.Name.Substring(0, f.Name.Length - 4);
                if (regex.IsMatch(name))
                    regexMatches.Add(name, f.FullName);
            }
            Logger.bw.ReportProgress(0, "Number of maps matched by regex: " + regexMatches.Count);
            return true;
        }
    }
}
