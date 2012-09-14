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

using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace cartographer
{
    class Tileset
    {
        public Tile[] tiles { get; private set; }

        public Tileset(int width, int height, int tileWidth, int tileHeight, Bitmap fullBitmap)
        {
            tiles = new Tile[(width / tileWidth) * (height / tileHeight)];
            byte[] tilesetArray = new byte[width * height * 4];
            int xParts = tileWidth / 32;
            int yParts = tileHeight / 32;
            int i = 0;

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = fullBitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(bmpData.Scan0, tilesetArray, 0, width * height * 4);
            fullBitmap.UnlockBits(bmpData);

            for (int h = 0; h < height; h += tileHeight)
            {
                for (int w = 0; w < width * 4; w += tileWidth * 4)
                {
                    byte[,][] tileMatrix = new byte[xParts, yParts][];
                    for (int y = 0; y < yParts; y++)
                    {
                        for (int x = 0; x < xParts; x++)
                        {
                            tileMatrix[x, y] = new byte[4096];
                            int j = 0;
                            for (int b = h + y * 32; b < h + y * 32 + 32; b++)
                            {
                                for (int a = w + x * 128; a < w + x * 128 + 128; a++)
                                {
                                    tileMatrix[x, y][j++] = tilesetArray[b * width * 4 + a];
                                }
                            }
                        }
                    }
                    tiles[i++] = new Tile(tileMatrix, xParts, yParts);
                }
            }
        }
    }
}
