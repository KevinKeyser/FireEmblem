using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class CharacterClass
    {
        public string Name;
        public WeaponType[] UsableWeapons;
        public CharacterStatistics Stats;

        public CharacterClass(string name, WeaponType[] usableweapons, CharacterStatistics stats)
        {
            Name = name;
            UsableWeapons = usableweapons;
            Stats = stats;
        }
    }
}
