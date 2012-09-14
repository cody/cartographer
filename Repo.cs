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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
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
        static public Dictionary<string, Map> maps = new Dictionary<string, Map>();
        static Dictionary<string, Tileset> tilesets = new Dictionary<string, Tileset>();

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

        internal static Map readTmx(string tmx)
        {
            XElement xml;
            int width;
            int height;
            int tilewidth;
            int tileheight;
            string source;

            try
            {
                xml = XElement.Load(tmx);
            }
            catch (Exception e)
            {
                Logger.bw.ReportProgress(1, e.Message);
                return null;
            }

            try
            {
                width = Convert.ToInt32(xml.Attribute("width").Value);
                height = Convert.ToInt32(xml.Attribute("height").Value);
            }
            catch (Exception e)
            {
                Logger.bw.ReportProgress(1, "Map has no \"width\" or \"height\". " + e.Message);
                return null;
            }

            try
            {
                tilewidth = Convert.ToInt32(xml.Attribute("tilewidth").Value);
                tileheight = Convert.ToInt32(xml.Attribute("tileheight").Value);
            }
            catch (Exception e)
            {
                Logger.bw.ReportProgress(1, "Can't read tilewidth or tileheight. " + e.Message);
                return null;
            }
            if (tilewidth != 32 || tileheight != 32)
            {
                Logger.bw.ReportProgress(1, "Tilewidth and tileheight have to be 32x32.");
                return null;
            }

            Map map = new Map(width, height);

            foreach (XElement child in xml.Elements())
            {
                if (child.Name == "tileset")
                {
                    uint firstgid;
                    try
                    {
                        firstgid = Convert.ToUInt32(child.Attribute("firstgid").Value);
                    }
                    catch (Exception e)
                    {
                        Logger.bw.ReportProgress(1, "Tileset has no \"firstgid\" attribute. " + e.Message);
                        return null;
                    }

                    try
                    {
                        source = child.Attribute("source").Value;
                    }
                    catch (Exception e)
                    {
                        Logger.bw.ReportProgress(1, "Tileset has no \"source\" attribute. " + e.Message);
                        return null;
                    }

                    Tileset tileset;
                    if (!tilesets.TryGetValue(source, out tileset))
                    {
                        tileset = readTsx(Path.Combine(pathMaps, source));
                        if (tileset == null)
                            return null;
                        tilesets.Add(source, tileset);
                    }
                    map.tilesetList.Add(firstgid, tileset);
                }
                else if (child.Name == "layer")
                {
                    string name;
                    int widthLayer;
                    int heightLayer;
                    XElement data;
                    string encoding = "";
                    string compression = "";

                    try
                    {
                        name = child.Attribute("name").Value;
                    }
                    catch (Exception e)
                    {
                        Logger.bw.ReportProgress(1, "Layer has no \"name\" attribute. " + e.Message);
                        return null;
                    }
                    if (name.ToLowerInvariant() == "collision")
                        continue;

                    try
                    {
                        widthLayer = Convert.ToInt32(child.Attribute("width").Value);
                        heightLayer = Convert.ToInt32(child.Attribute("height").Value);
                    }
                    catch (Exception e)
                    {
                        Logger.bw.ReportProgress(1, "Layer \"" + name + "\" has no \"width\" or \"height\". " + e.Message);
                        return null;
                    }

                    try
                    {
                        data = child.Element("data");
                    }
                    catch (Exception e)
                    {
                        Logger.bw.ReportProgress(1, "Layer \"" + name + "\" has no \"data\" attribute. " + e.Message);
                        return null;
                    }
                    foreach (var a in data.Attributes())
                    {
                        if (a.Name == "encoding")
                            encoding = a.Value;
                        else if (a.Name == "compression")
                            compression = a.Value;
                    }

                    string dataString = data.Value;

                    if (encoding == "base64" && compression == "gzip")
                    {
                        byte[] compressed = System.Convert.FromBase64String(dataString);
                        var compressedStream = new MemoryStream(compressed);
                        var decompressedStream = new GZipStream(compressedStream, CompressionMode.Decompress);

                        for (int y = 0; y < heightLayer; y++)
                        {
                            for (int x = 0; x < widthLayer; x++)
                            {
                                byte[] buffer = new byte[4];
                                decompressedStream.Read(buffer, 0, 4);
                                uint number = BitConverter.ToUInt32(buffer, 0);

                                if (number == 0 || number > 0x20000000)
                                    continue;
                                var order = map.tilesetList.Last(n => n.Key <= number);
                                Tile tile = order.Value.tiles[number - order.Key];
                                tile.addToLayer(map.layers, x, y);
                            }
                        }
                    }
                    else
                    {
                        Logger.bw.ReportProgress(1, "Layer \"" + name + "\" has unsupported encoding or compression.");
                        return null;
                    }
                }
            }
            return map;
        }

        private static Tileset readTsx(string tsx)
        {
            XElement xml;
            int width;
            int height;
            int tilewidth;
            int tileheight;
            string source;

            try
            {
                xml = XElement.Load(tsx);
            }
            catch (Exception e)
            {
                Logger.bw.ReportProgress(1, "Problem loading tileset " + tsx + ": " + e.Message);
                return null;
            }

            try
            {
                tilewidth = Convert.ToInt32(xml.Attribute("tilewidth").Value);
                tileheight = Convert.ToInt32(xml.Attribute("tileheight").Value);
            }
            catch (Exception e)
            {
                Logger.bw.ReportProgress(1, "External tileset " + tsx + " has no \"tilewidth\" or \"tileheight\" attribute. " + e.Message);
                return null;
            }

            foreach (XElement child in xml.Elements())
            {
                if (child.Name != "image")
                    continue;

                try
                {
                    source = child.Attribute("source").Value;
                }
                catch (Exception e)
                {
                    Logger.bw.ReportProgress(1, "External tileset " + tsx + " has no \"source\" attribute. " + e.Message);
                    return null;
                }

                Bitmap fullBitmap;
                try
                {
                    fullBitmap = new Bitmap(Path.Combine(Path.GetDirectoryName(tsx), source));
                }
                catch (Exception e)
                {
                    Logger.bw.ReportProgress(1, "Can't create Bitmap from external tileset " + tsx + ": " + e.Message);
                    return null;
                }

                try
                {
                    width = Convert.ToInt32(child.Attribute("width").Value);
                    height = Convert.ToInt32(child.Attribute("height").Value);
                    if (width > fullBitmap.Width)
                        width = fullBitmap.Width;
                    if (height > fullBitmap.Height)
                        height = fullBitmap.Height;
                }
                catch (Exception e)
                {
                    Logger.bw.ReportProgress(1, "External tileset " + tsx + " has no \"width\" or \"height\" attribute. " + e.Message);
                    return null;
                }

                Tileset tileset = new Tileset(width: width, height: height, tileWidth: tilewidth,
                                              tileHeight: tileheight, fullBitmap: fullBitmap);

                fullBitmap.Dispose();
                fullBitmap = null;
                return tileset;
            }

            Logger.bw.ReportProgress(1, "Tileset has no \"image\": " + tsx);
            return null;
        }
    }
}
