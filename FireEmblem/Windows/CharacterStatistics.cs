using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class CharacterStatistics
    {
        public int Strength;
        public int Magic;
        public int Skill;
        public int Speed;
        public int Luck;
        public int Defense;
        public int Resistance;
        
        public CharacterStatistics( int strength, int magic, int skill, int speed, int luck, int defense, int resistance)
        {
            Strength = strength;
            Magic = magic;
            Skill = skill;
            Speed = speed;
            Luck = luck;
            Defense = defense;
            Resistance = resistance;
        }

        public override string ToString()
        {
            return string.Format("Strength: {0}\nMagic: {1}\nSkill: {2}\nSpeed: {3}\nLuck: {4}\nDefense: {5}\nResistance: {6}", Strength, Magic, Skill, Speed, Luck, Defense, Resistance);
        }
    }
}
