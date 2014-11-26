﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class Weapon : Item
    {
        public char Rank;
        public int ConsecutiveAttacks;
        public int Might;
        public int Hit;
        public int Critical;
        public int MinRange;
        public int MaxRange;
        public CharacterStatistics Special;
        public WeaponType WeaponType;
        public Weapon(string name, char rank, int might, int hit, int critical, int minRange, int maxRange, int usesMax, int priceValue, CharacterStatistics special, WeaponType weaponType)
            : base(name, usesMax, priceValue, ItemType.Weapon)
        {
            Name = name;
            Rank = rank.ToString().ToUpper()[0];
            Might = might;
            Hit = hit;
            Critical = critical;
            MinRange = minRange;
            MaxRange = maxRange;
            Special = special;
            WeaponType = weaponType;
        }
    }
}
