using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public static class Info
    {
        public static Dictionary<WeaponName, Weapon> Weapons = new Dictionary<WeaponName, Weapon>()
        {
            { WeaponName.name, new Weapon("Test Sword", 'A', 25, 20, 15, 30, 10000, 1, 2, new CharacterStatistics(), WeaponType.Sword) }
        };

        public static Dictionary<ClassName, CharacterClass> Classes = new Dictionary<ClassName, CharacterClass>()
        {
            { ClassName.Lord, new CharacterClass("Lord", new WeaponType[] { WeaponType.Sword }, new CharacterStatistics(18, 6, 0, 5, 7, 7, 0, 5)) },
            { ClassName.GreatLord, new CharacterClass("Great Lord", new WeaponType[] {WeaponType.Sword, WeaponType.Lance}, new CharacterStatistics(23, 10, 0, 7, 9, 10, 3, 6)) },
            { ClassName.Tactician, new CharacterClass("Tactician", new WeaponType[] {WeaponType.Sword, WeaponType.Tome}, new CharacterStatistics(16, 4, 3, 5, 5, 5, 3, 5)) },
            { ClassName.Grandmaster, new CharacterClass("Grandmaster", new WeaponType[] {WeaponType.Sword, WeaponType.Tome}, new CharacterStatistics(20, 7, 6, 7, 7, 7, 5, 6))},
            { ClassName.Cavalier, new CharacterClass("Cavalier", new WeaponType[] {WeaponType.Sword, WeaponType.Lance}, new CharacterStatistics(18, 6,0, 5, 6, 7, 0, 7))},
            { ClassName.Paladin, new CharacterClass("Paladin", new WeaponType[] {WeaponType.Sword, WeaponType.Lance}, new CharacterStatistics(25, 9, 1, 7, 8, 10, 6, 8))},
            {ClassName.GreatKnight, new CharacterClass("Great Knight", new WeaponType[] {WeaponType.Sword, WeaponType.Lance, WeaponType.Axe}, new CharacterStatistics(26, 11, 0, 6, 5, 14, 1, 7))},
            {ClassName.Knight, new CharacterClass("Knight", new WeaponType[] {WeaponType.Lance}, new CharacterStatistics(18,8,0,4,2,11,0,4))},
            {ClassName.General, new CharacterClass("General", new WeaponType[] {WeaponType.Lance, WeaponType.Axe}, new CharacterStatistics(28, 12,0,7,4,15,3,5))},
            {ClassName.Myrmidon, new CharacterClass("Myrmidon", new WeaponType[] {WeaponType.Sword}, new CharacterStatistics(16, 4, 1,9, 10, 4, 1, 5))},
            {ClassName.Swordmaster, new CharacterClass("Swordmaster", new WeaponType[] {WeaponType.Sword}, new CharacterStatistics(20,7,2,11,13,6,4,6))},
            {ClassName.Mercenary, new CharacterClass("Mercenary", new WeaponType[] {WeaponType.Sword}, new CharacterStatistics(18, 5,0, 8, 7, 5,0,5))},
            {ClassName.Hero, new CharacterClass("Hero", new WeaponType[] {WeaponType.Sword, WeaponType.Axe}, new CharacterStatistics(22, 8, 1, 11, 10, 8,3, 6))},
            {ClassName.Fighter, new CharacterClass("Fighter", new WeaponType[]{WeaponType.Axe}, new CharacterStatistics(20, 8, 0, 5, 5, 4, 0, 5))},
            {ClassName.Warrior, new CharacterClass("Warrior", new WeaponType[] {WeaponType.Axe, WeaponType.Bow}, new CharacterStatistics(28, 12, 0, 8, 7, 7, 3, 6))},
            {ClassName.Barbarian, new CharacterClass("Barbarian", new WeaponType[] {WeaponType.Axe}, new CharacterStatistics(22, 8, 0, 3, 8, 3,0,5))},
            {ClassName.Berserker, new CharacterClass("Berserker", new WeaponType[] {WeaponType.Axe}, new CharacterStatistics(30, 13, 0, 5, 11, 5, 1, 6))},
            {ClassName.Archer, new CharacterClass("Archer", new WeaponType[] {WeaponType.Bow}, new CharacterStatistics(16, 5, 0, 8, 6, 5, 0, 5))},
            {ClassName.Sniper, new CharacterClass("Sniper", new WeaponType[] {WeaponType.Bow}, new CharacterStatistics(20, 7, 1, 12, 9, 10, 3, 6))},
            {ClassName.BowKnight, new CharacterClass("Bow Knight", new WeaponType[] {WeaponType.Sword, WeaponType.Bow}, new CharacterStatistics(24, 8, 0, 10, 10,6, 2, 8))},
            {ClassName.Thief, new CharacterClass("Thief", new WeaponType[]{WeaponType.Sword}, new CharacterStatistics(16, 3, 0, 6, 8, 2, 0, 5))},
            {ClassName.Assassin, new CharacterClass("Assassin", new WeaponType[] {WeaponType.Sword, WeaponType.Bow}, new CharacterStatistics(21, 8, 0, 13, 12, 5, 1, 6))},
            {ClassName.Trickster, new CharacterClass("Trickster", new WeaponType[] {WeaponType.Sword, WeaponType.Staff}, new CharacterStatistics(19, 4, 4, 10, 11, 3, 5,6))},
            {ClassName.PegasusKnight, new CharacterClass("Pegasus Knight", new WeaponType[] { WeaponType.Lance}, new CharacterStatistics(16, 4, 2, 7, 8 ,4, 6, 7))},
            {ClassName.FalconKnight, new CharacterClass("Falcon Knight", new WeaponType[] {WeaponType.Lance, WeaponType.Staff}, new CharacterStatistics(20, 6, 3, 10, 11, 6, 9, 8))},
            {ClassName.DarkFlier, new CharacterClass("Dark Flier", new WeaponType[] {WeaponType.Lance, WeaponType.Tome}, new CharacterStatistics(19, 5, 6, 8, 10, 5, 9, 8))},
            {ClassName.WyvernRider, new CharacterClass("Wyvern Rider", new WeaponType[] {WeaponType.Axe}, new CharacterStatistics(19, 7, 0 ,6, 5, 8, 0, 7))},
            {ClassName.WyvernLord, new CharacterClass("Wyvern Lord", new WeaponType[]{WeaponType.Lance, WeaponType.Axe}, new CharacterStatistics(24, 11, 0, 8, 7, 11, 3, 8))},
            {ClassName.GriffonRider, new CharacterClass("Griffon Rider", new WeaponType[] {WeaponType.Axe}, new CharacterStatistics(22, 9,0,10,9,8,3,8))},
            {ClassName.Mage, new CharacterClass("Mage", new WeaponType[] {WeaponType.Tome}, new CharacterStatistics(16, 0, 4, 3, 4, 2,3, 5))},
            {ClassName.Sage, new CharacterClass("Sage", new WeaponType[]{WeaponType.Tome, WeaponType.Staff}, new CharacterStatistics(20, 1, 7, 5, 7, 4, 5, 6))},
            {ClassName.DarkMage, new CharacterClass("Dark Mage", new WeaponType[]{WeaponType.Tome}, new CharacterStatistics(18,1,3,2,3,4,4,5)},
            {ClassName.Sorcerer, new CharacterClass("Sorcerer", new WeaponType[]{WeaponType.Tome}, new CharacterStatistics(23, 2, 6, 4, 4, 7, 7, 6))},
            {ClassName.DarkKnight, new CharacterClass("Dark Knight", new WeaponType[]{WeaponType.Sword, WeaponType.Tome}, new CharacterStatistics(25, 2, 6, 4, 4, 7, 7, 6))},
            {ClassName.Cleric, new CharacterClass("Cleric", new WeaponType[]{WeaponType.Staff}, new CharacterStatistics(16, 0, 3, 2, 4, 1,6, 5))},
            {ClassName.Priest, new CharacterClass("Priest", new WeaponType[]{WeaponType.Staff}, new CharacterStatistics(16, 0, 3, 2, 4, 1,6, 5))},
            {ClassName.WarMonk, new CharacterClass("War Monk", new WeaponType[] {WeaponType.Staff, WeaponType.Axe}, new CharacterStatistics(24, 5, 5, 4, 8, 3, 8,8))},
            {ClassName.Troubadour, new CharacterClass("Troubadour", new WeaponType[] { WeaponType.Staff}, new CharacterStatistics(16, 0, 3, 2, 5, 1, 5, 7))},
            {ClassName.Valkyrie, new CharacterClass("Valkyrie", new WeaponType[] {WeaponType.Tome, WeaponType.Staff}, new CharacterStatistics(19, 0, 5, 4, 8, 3, 8, 8))},
            {ClassName.Villager, new CharacterClass("Villager", new WeaponType[] {WeaponType.Lance}, new CharacterStatistics(16, 1, 0, 1, 1, 1, 0, 5))},
            {ClassName.Dancer, new CharacterClass("Dancer", new WeaponType[] {WeaponType.Sword}, new CharacterStatistics(16, 1, 1, 5, 8, 3, 1, 5))},
            {ClassName.Lodestar, new CharacterClass("Lodestar", new WeaponType[] {WeaponType.Sword}, new CharacterStatistics(21, 9, 1, 10, 10,8, 4, 6))},
            {ClassName.DreadFighter, new CharacterClass("Dread Fighter", new WeaponType[] {WeaponType.Sword, WeaponType.Axe, WeaponType.Tome}, new CharacterStatistics(22, 8, 4, 7, 9, 7 ,10, 6))},
            {ClassName.Soldier, new CharacterClass("Soldier", new WeaponType[] {WeaponType.Sword, WeaponType.Lance}, new CharacterStatistics(18, 3, 0, 3, 3, 3, 0, 5))},
            {ClassName.Conqueror, new CharacterClass("Conqueror", new WeaponType[] {WeaponType.Sword, WeaponType.Lance, WeaponType.Axe}, new CharacterStatistics(24, 10, 3, 9, 8, 12, 5, 8))}
        };
    }
}
