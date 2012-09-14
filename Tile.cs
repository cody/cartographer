
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

using System.Collections.Generic;

namespace cartographer
{
    class Tile
    {
        public byte[,][] tiles;
        int xRange;
        int yRange;

        public Tile(byte[,][] t, int xR, int yR)
        {
            tiles = t;
            xRange = xR;
            yRange = yR;
        }

        public void addToLayer(List<byte[]>[,] layerBitmaps, int xStart, int yStart)
        {
            for (int y = 0; y < yRange; y++)
            {
                for (int x = 0; x < xRange; x++)
                {
                    if (xStart + x < layerBitmaps.GetLength(0) && yStart + y - yRange + 1 >= 0)
                    {
                        layerBitmaps[xStart + x, yStart + y - yRange + 1].Add(tiles[x, y]);
                    }
                }
            }
        }
    }
}
