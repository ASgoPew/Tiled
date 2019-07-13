using OTAPI.Tile;
using System;
using Terraria;

namespace Tiled.OneDimension
{
    public class OneDimensionTileProvider : ITileCollection, IDisposable
    {
        public StructTile[] data;

        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public int Size => this.data.Length;

        public OneDimensionTileProvider()
        {
            data = new StructTile[TiledPlugin.realMaxTilesX * TiledPlugin.realMaxTilesY];

            //this._width = TiledPlugin.realMaxTilesX;
            //this._height = TiledPlugin.realMaxTilesY;
            Console.WriteLine($"1dTileProvider: {Width}, {Height}");
        }

        public ITile this[int x, int y]
        {
            get
            {
                int relativeX = x - TiledPlugin.offsetX, relativeY = y - TiledPlugin.offsetY;
                return relativeX >= 0 && relativeX < TiledPlugin.realMaxTilesX && relativeY >= 0 && relativeY < TiledPlugin.realMaxTilesY
                    ? new OneDimensionTileReference(data, relativeX, relativeY)
                    : new OneDimensionTileReference(TiledPlugin.BorderTiles, 0, 0);
                // Cyclic world:
                //return new OneDimensionTileReference(data, x % TiledPlugin.realMaxTilesX, y % TiledPlugin.realMaxTilesY);
            }

            set
            {
                int relativeX = x - TiledPlugin.offsetX, relativeY = y - TiledPlugin.offsetY;
                if (relativeX >= 0 && relativeX < TiledPlugin.realMaxTilesX && relativeY >= 0 && relativeY < TiledPlugin.realMaxTilesY)
                    (new OneDimensionTileReference(data, relativeX, relativeY)).CopyFrom(value);

                // Cyclic world:
                //(new OneDimensionTileReference(data, x % TiledPlugin.realMaxTilesX, y % TiledPlugin.realMaxTilesY)).CopyFrom(value);
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
