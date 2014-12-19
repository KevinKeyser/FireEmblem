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

        private UnitType[] unitType;

        public UnitType[] UnitType
        {
            get { return unitType; }
            set { unitType = value; }
        }
        

        public CharacterClass(string name, WeaponType[] usableweapons, UnitType[] unitType, CharacterStatistics stats)
        {
            Name = name;
            UsableWeapons = usableweapons;
            Stats = stats;
            this.unitType = unitType;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
