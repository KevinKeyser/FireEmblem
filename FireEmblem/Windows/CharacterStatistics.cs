using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class CharacterStatistics
    {
        public int MovementSpeed;
        public int AttackDamage;
        public int MagicDamage;
        public int AttackDefense;
        public int MagicDefense;
        public int Evasion;
        
        public CharacterStatistics(int movementSpeed, int attackDamage, int magicDamage, int attackDefense, int magicDefense, int evasion)
        {
            MovementSpeed = movementSpeed;
            AttackDamage = attackDamage;
            MagicDamage = magicDamage;
            AttackDefense = attackDefense;
            Evasion = evasion;
        }

        public override string ToString()
        {
            return string.Format("MovementSpeed: {0}\nAttackDamage: {1}\nMagicDamage: {2}\nAttackDefense: {3}\nMagicDefense: {4}\nEvasion: {5}", MovementSpeed, AttackDamage, MagicDamage, AttackDefense, MagicDefense, Evasion);
        }
    }
}
