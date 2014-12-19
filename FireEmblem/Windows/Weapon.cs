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
        public bool isPhysical;
        public ClassName[] ClassLimits;
        public int SelfHeal;

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
            switch (weaponType)
            {
                case WeaponType.Sword:
                case WeaponType.Lance:
                case WeaponType.Axe:
                case WeaponType.Bow:
                    isPhysical = true;
                    break;
                case WeaponType.Tome:
                case WeaponType.Staff:
                    isPhysical = false;
                    break;
                case WeaponType.Stone:
                    break;
                default:
                    break;
            }
            List<ClassName> limits = new List<ClassName>();
            for(int i = 0; i < Enum.GetValues(typeof(ClassName)).Length; i++)
            {
                limits.Add((ClassName)i);
            }
            ClassLimits = limits.ToArray();
            SelfHeal = 0;
        }

        public Weapon(string name, char rank, int might, int hit, int critical, int minRange, int maxRange, UnitType[] effects, int usesMax, int priceValue, int consecutiveAttacks, CharacterStatistics special, WeaponType weaponType, ClassName[] limits)
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
            switch (weaponType)
            {
                case WeaponType.Sword:
                case WeaponType.Lance:
                case WeaponType.Axe:
                case WeaponType.Bow:
                    isPhysical = true;
                    break;
                case WeaponType.Tome:
                case WeaponType.Staff:
                    isPhysical = false;
                    break;
                case WeaponType.Stone:
                    break;
                default:
                    break;
            }
            ClassLimits = limits;
            SelfHeal = 0;
        }

        public override object Clone()
        {
            return new Weapon(Name, Rank, Might, Hit, Critical, MinRange, MaxRange, Effects, UsesMax, PriceValue, ConsecutiveAttacks, Special, WeaponType) { UsesCurrent = this.UsesCurrent };
        }
    }
}
