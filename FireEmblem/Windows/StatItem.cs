using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class StatItem : Item
    {
        int minRange;
        int maxRange;
        CharacterStatistics stats;
        bool isPerma;
        int decayAmount;
        public StatItem(string name, int minRange, int maxRange, CharacterStatistics stats, int usesMax, int priceValue, bool isPerma, int decayAmount)
            : base(name, usesMax, priceValue, ItemType.StatModifier)
        {
            this.minRange = minRange;
            this.maxRange = maxRange;
            this.stats = stats;
            this.isPerma = isPerma;
            this.decayAmount = decayAmount;
        }
    }
}
