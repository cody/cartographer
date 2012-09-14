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
using System.Drawing.Imaging;
using System.IO;

namespace cartographer
{
    class Node
    {
        public byte[] sureface = null;
        public Color color;
        public bool hasColor = false;
        public Dictionary<byte[], Node> edges = new Dictionary<byte[], Node>();
    }

    class Map
    {
        public string name { get; set; }
        public int width { get; private set; }
        public int height { get; private set; }
        public List<byte[]>[,] layers;
        public SortedList<uint, Tileset> tilesetList = new SortedList<uint, Tileset>();
        public Bitmap minimap { get; private set; }
        public Bitmap oldMinimap { get; private set; }
        public Bitmap diffMinimap { get; private set; }
        public string diffText { get; set; }
        public bool isDifferent = false;
        Node root = new Node();

        public Map(int w, int h)
        {
            width = w;
            height = h;

            layers = new List<byte[]>[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    layers[x, y] = new List<byte[]>();
                }
            }
        }

        internal void render()
        {
            minimap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            string oldMinimapName = Path.Combine(Repo.pathMinimaps, name) + ".png";
            var f = new FileInfo(oldMinimapName);
            if (f.Exists)
            {
                oldMinimap = new Bitmap(oldMinimapName);
                if (oldMinimap.Width == width && oldMinimap.Height == height)
                {
                    diffMinimap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                }
                else
                {
                    diffText = "Old map has size " + oldMinimap.Width + "x" + oldMinimap.Height +
                               " and new map has size " + width + "x" + height + ".";
                    isDifferent = true;
                }
            }
            else
            {
                diffText = "No minimap in repo.";
                isDifferent = true;
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Node current = root;
                    foreach (byte[] tile in layers[x, y])
                    {
                        Node next;
                        if (!current.edges.TryGetValue(tile, out next))
                        {
                            next = new Node();
                            current.edges.Add(tile, next);
                            if (current == root)
                                next.sureface = tile;
                            else
                            {
                                next.sureface = new byte[4096];
                                for (int pos = 0; pos < 4096; pos++)
                                {
                                    int a = tile[pos + 3]; // alpha
                                    for (int i = 0; i < 3; i++) // b g r
                                    {
                                        int color = (tile[pos] * a + current.sureface[pos] * (255 - a)) / 255;
                                        next.sureface[pos++] = (byte)color;
                                    }
                                }
                            }
                        }
                        current = next;
                    }

                    int r = 0, g = 0, b = 0;
                    if (!current.hasColor)
                    {
                        for (int pos = 0; pos < 4096; pos++)
                        {
                            b += current.sureface[pos++];
                            g += current.sureface[pos++];
                            r += current.sureface[pos++];
                        }
                        r /= 1024;
                        g /= 1024;
                        b /= 1024;
                        current.color = Color.FromArgb(r, g, b);
                        current.hasColor = true;
                    }
                    minimap.SetPixel(x, y, current.color);

                    if (diffMinimap != null)
                    {
                        Color oldColor = oldMinimap.GetPixel(x, y);
                        int diffR = Math.Abs(current.color.R - oldColor.R);
                        int diffG = Math.Abs(current.color.G - oldColor.G);
                        int diffB = Math.Abs(current.color.B - oldColor.B);
                        if (diffR != 0 || diffG != 0 || diffB != 0)
                        {
                            diffMinimap.SetPixel(x, y, Color.FromArgb(diffR, diffG, diffB));
                            isDifferent = true;
                        }
                    }
                }
                if (!isDifferent && diffMinimap != null)
                {
                    diffText = "No changes.";
                    diffMinimap.Dispose();
                    diffMinimap = null;
                }
            }
        }
    }
}
