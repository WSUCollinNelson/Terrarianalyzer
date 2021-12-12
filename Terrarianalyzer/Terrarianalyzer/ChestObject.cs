using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrarianalyzer
{
    public class ChestObject
    {
        public List<ItemObject> ChestItems { get; } = new List<ItemObject>();
        public (int, int) Position { get; set; }

        public ChestObject(List<ItemObject> chestItems, (int, int) position)
        {
            ChestItems = chestItems;
            Position = position;
        }
    }
}
