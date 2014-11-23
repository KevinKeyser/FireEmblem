using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class Weapon
    {
        public string Name;
        public char Rank;
        public int UsesLeft;
        public int UsesMax;
        public int Might;
        public int Hit;
        public int Critical;
        public int Value;
        public int MinRange;
        public int MaxRange;
        public CharacterStatistics Special;
        public WeaponType WeaponType;
        public Weapon(string name, char rank, int usesmax, int might, int hit, int critical, int value, int minrange, int maxrange, CharacterStatistics special, WeaponType weaponType)
        {
            Name = name;
            Rank = rank.ToString().ToUpper()[0];
            UsesMax = usesmax;
            UsesLeft = UsesMax;
            Might = might;
            Hit = hit;
            Critical = critical;
            Value = value;
            MinRange = minrange;
            MaxRange = maxrange;
            Special = special;
            WeaponType = weaponType;
        }
    }
}
