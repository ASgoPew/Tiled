using OTAPI.Tile;
using System;
using Terraria;

namespace Tiled.OneDimension
{
    public class OneDimensionTileProvider : ITileCollection, IDisposable
    {
        private StructTile[] data;
        private int _width;
        private int _height;

        public int Width => this._width;
        public int Height => this._height;
        public int Size => this.data.Length;

        public OneDimensionTileProvider()
        {
            data = new StructTile[(TiledPlugin.maxTilesX + 1) * (TiledPlugin.maxTilesY + 1)];

            this._width = TiledPlugin.maxTilesX + 1;
            this._height = TiledPlugin.maxTilesY + 1;
            Console.WriteLine($"1dTileProvider: {Width}, {Height}");
        }

        public ITile this[int x, int y]
        {
            get
            {
                return new OneDimensionTileReference(data, x - TiledPlugin.offsetX, y - TiledPlugin.offsetY);
            }

            set
            {
                (new OneDimensionTileReference(data, x - TiledPlugin.offsetX, y - TiledPlugin.offsetY)).CopyFrom(value);
            }
        }

        public void Dispose()
        {
            if (data != null)
            {
                for (var x = 0; x < data.Length; x++)
                {
                    data[x].bTileHeader = 0;
                    data[x].bTileHeader2 = 0;
                    data[x].bTileHeader3 = 0;
                    data[x].frameX = 0;
                    data[x].frameY = 0;
                    data[x].liquid = 0;
                    data[x].type = 0;
                    data[x].wall = 0;
                }
                data = null;
            }
        }
    }
}
