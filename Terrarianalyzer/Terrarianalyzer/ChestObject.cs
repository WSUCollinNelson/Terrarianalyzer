using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    public class ChestObject
    {
        List<ItemObject> ChestItems { get; } = new List<ItemObject>();

        public ChestObject(List<ItemObject> chestItems)
        {
            ChestItems = chestItems;
        }
    }
}
