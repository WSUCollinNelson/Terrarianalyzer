using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    public class ItemObject
    {
        int ItemID { get; }
        int ItemPrefix { get; }

        public ItemObject(int itemID, int itemPrefix)
        {
            ItemID = itemID;
            ItemPrefix = itemPrefix;
        }
    }
}
