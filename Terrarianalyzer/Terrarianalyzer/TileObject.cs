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
        public int LiquidAmount { get; set; }
        public LiquidType LiquidType { get; set; }

        public TileObject(int type, int liquidAmount, LiquidType liquidType)
        {
            TypeID = type;
            LiquidType = liquidType;
            LiquidAmount = liquidAmount;
        }
    }
}
