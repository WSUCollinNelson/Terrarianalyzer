using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    public class TileObject
    {
        public int TypeID { get; set; }
        public string TypeName { get { return XMLUtilities.GetTileName(TypeID); } }

        public TileObject(int type)
        {
            TypeID = type;
        }
    }
}
