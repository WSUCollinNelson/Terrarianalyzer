using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    public class TileObject
    {
        public int Type { get; set; }

        public TileObject(int type)
        {
            Type = type;
        }
    }
}
