using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class Item
    {
        public ItemType ItemType;
        public string Name;
        public int UsesMax;
        public int UsesCurrent;
        public int PriceValue;
        public Item(string name, int usesMax, int priceValue, ItemType itemType)
        {
            ItemType = itemType;
            Name = name;
            UsesMax = usesMax;
            UsesCurrent = UsesMax;
            PriceValue = priceValue;
        }

        public virtual object Clone()
        {
            return new Item(Name, UsesMax, PriceValue, ItemType) { UsesCurrent = this.UsesCurrent };
        }

        public override string ToString()
        {
            return string.Format("{0} {1}/{2}", Name, UsesCurrent, UsesMax);
        }
    }
}
