using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    class HealthItem : Item
    {
        bool isPercentBased;
        public float HealthPercent;
        public int HealthAmount;
        public int MinRange;
        public int MaxRange;

        public HealthItem(string name, int minRange, int maxRange, int healthAmount, int usesMax, int priceValue)
            : base(name, usesMax, priceValue, ItemType.Health)
        {
            MaxRange = maxRange;
            MinRange = minRange;
            HealthAmount = healthAmount;
            isPercentBased = false;
        }

        public HealthItem(string name, int minRange, int maxRange, float healthPercent, int usesMax, int priceValue)
            : base(name, usesMax, priceValue, ItemType.Health)
        {
            MaxRange = maxRange;
            MinRange = minRange;
            HealthPercent = healthPercent;
            isPercentBased = true;
        }

        public int Heal(int MaxHealth)
        {
            return isPercentBased ? (int)(MaxHealth * HealthPercent) : HealthAmount;
        }

        public override object Clone()
        {
            return new HealthItem(Name, MinRange, MaxRange, HealthAmount, UsesMax, PriceValue) { isPercentBased = this.isPercentBased, HealthPercent = this.HealthPercent, HealthAmount = this.HealthAmount };
        }
    }
}
