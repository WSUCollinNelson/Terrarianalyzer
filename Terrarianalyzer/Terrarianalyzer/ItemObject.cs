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
        int ItemPrefixID { get; }

        string ItemName { get { return XMLUtilities.GetItemName(ItemID); } }
        string ItemPrefix { get { return XMLUtilities.GetItemPrefix(ItemPrefixID); } }

        public ItemObject(int itemID, int itemPrefix)
        {
            ItemID = itemID;
            ItemPrefixID = itemPrefix;
        }
    }
}
