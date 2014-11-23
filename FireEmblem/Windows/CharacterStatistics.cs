using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class CharacterStatistics
    {
        public int HealthPoints;
        public int Strength;
        public int Magic;
        public int Skill;
        public int Speed;
        public int Luck;
        public int Defense;
        public int Resistance;
        public int Movement;
        
        public CharacterStatistics()
            : this(0,0,0,0,0,0,0,0,0) {}

        public CharacterStatistics(int healthPoints, int strength, int magic, int skill, int speed, int defense, int resistance, int movement)
            : this(healthPoints, strength, magic, skill, speed, 0, defense, resistance, movement) {}

        public CharacterStatistics(int healthPoints, int strength, int magic, int skill, int speed, int luck, int defense, int resistance, int movement)
        {
            HealthPoints = healthPoints;
            Strength = strength;
            Magic = magic;
            Skill = skill;
            Speed = speed;
            Luck = luck;
            Defense = defense;
            Resistance = resistance;
            Movement = movement;
        }

        public override string ToString()
        {
            return string.Format("HP: {0}\nStrength: {1}\nMagic: {2}\nSkill: {3}\nSpeed: {4}\nLuck: {5}\nDefense: {6}\nResistance: {7}", HealthPoints, Strength, Magic, Skill, Speed, Luck, Defense, Resistance);
        }

        public static CharacterStatistics operator +(CharacterStatistics stats, CharacterStatistics other)
        {
            return new CharacterStatistics(stats.HealthPoints + other.HealthPoints, stats.Strength + other.Strength, stats.Magic + other.Magic, stats.Skill + other.Skill, stats.Speed + other.Speed, stats.Luck + other.Luck, stats.Defense + other.Defense, stats.Resistance + other.Resistance, stats.Movement + other.Movement);
        }

        public static CharacterStatistics operator -(CharacterStatistics stats, CharacterStatistics other)
        {
            return new CharacterStatistics(stats.HealthPoints - other.HealthPoints, stats.Strength - other.Strength, stats.Magic - other.Magic, stats.Skill - other.Skill, stats.Speed - other.Speed, stats.Luck - other.Luck, stats.Defense - other.Defense, stats.Resistance - other.Resistance, stats.Movement - other.Movement);
        }
    }
}
