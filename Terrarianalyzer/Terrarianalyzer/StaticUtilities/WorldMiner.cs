using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    /// <summary>
    /// Provides search algorithms for mining the WorldObject
    /// </summary>
    public static class WorldMiner
    {
        /// <summary>
        /// Gets a count of how many tiles exist in the WorldObject with the specified id
        /// </summary>
        /// <param name="world">The world to search</param>
        /// <param name="id">The id of the tile to reference</param>
        /// <returns>A count of how many of those tiles exist</returns>
        public static int CountTilesWithID(WorldObject world, int id)
        {
            return world.Tiles.Where(e => e.TypeID == id).Count();
        }

        /// <summary>
        /// Gets a count of how many tiles exist in each row of the world save
        /// </summary>
        /// <param name="world">The world to search</param>
        /// <param name="id">The id of the tile to reference</param>
        /// <returns>An array of hits for each depth index</returns>
        public static int[] CountTilesByDepth(WorldObject world, int id)
        {
            //Foreach row in the worldObject
            int[] output = new int[world.WorldHeight];
            for(int i = 0; i < world.WorldHeight; i++)
            {
                //Foreach tile in the column
                for(int j = 0; j < world.WorldWidth; j++)
                {
                    //Increment count
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
