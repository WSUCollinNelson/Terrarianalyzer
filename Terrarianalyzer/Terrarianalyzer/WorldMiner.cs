using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    public static class WorldMiner
    {
        public static int CountTilesWithID(WorldObject world, int id)
        {
            return world.Tiles.Where(e => e.TypeID == id).Count();
        }

        public static int[] CountTilesByDepth(WorldObject world, int id)
        {
            int[] output = new int[world.WorldHeight];
            for(int i = 0; i < world.WorldHeight; i++)
            {
                for(int j = 0; j < world.WorldWidth; j++)
                {
                    if(world.Tiles[i + j * world.WorldHeight].TypeID == id)
                    {
                        output[i]++;
                    }
                }
            }
            return output;
        }
    }
}
