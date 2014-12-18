using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class Weapon : Item
    {
        public char Rank;
        public int Might;
        public int Hit;
        public int Critical;
        public int MinRange;
        public int MaxRange;
        public int ConsecutiveAttacks;
        public CharacterStatistics Special;
        public WeaponType WeaponType;
        public UnitType[] Effects;

        public Weapon(string name, char rank, int might, int hit, int critical, int minRange, int maxRange, UnitType[] effects, int usesMax, int priceValue, int consecutiveAttacks, CharacterStatistics special, WeaponType weaponType)
            : base(name, usesMax, priceValue, ItemType.Weapon)
        {
            Name = name;
            Rank = rank.ToString().ToUpper()[0];
            Might = might;
            Hit = hit;
            Critical = critical;
            MinRange = minRange;
            MaxRange = maxRange;
            Effects = effects;
            ConsecutiveAttacks = consecutiveAttacks;
            Special = special;
            WeaponType = weaponType;
        }

        public override object Clone()
        {
            return new Weapon(Name, Rank, Might, Hit, Critical, MinRange, MaxRange, Effects, UsesMax, PriceValue, ConsecutiveAttacks, Special, WeaponType) { UsesCurrent = this.UsesCurrent };
        }
    }
}
