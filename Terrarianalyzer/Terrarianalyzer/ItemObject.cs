using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    public class ItemObject
    {
        public int ItemID { get; }
        public int ItemPrefixID { get; }

        public string ItemName { get { return XMLUtilities.GetItemName(ItemID); } }
        public string ItemPrefix { get { return XMLUtilities.GetItemPrefix(ItemPrefixID); } }

        public ItemObject(int itemID, int itemPrefix)
        {
            ItemID = itemID;
            ItemPrefixID = itemPrefix;
        }
    }
}
