using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public static class Info
    {
        //Ana (12 across)
        //start with 2 px offset on y
        //rect - 32, 32
        // higher models 32, 35 any flying mount
        //DarkFlier
        //WyvernRider/Lord
        //GriffonRider
        //

        public static Dictionary<CharacterName, Dictionary<ClassName, Texture2D>> Images = new Dictionary<CharacterName, Dictionary<ClassName, Texture2D>>();
        public static Dictionary<ClassName, Dictionary<Team, Dictionary<AnimationName, Animation>>> Animations = new Dictionary<ClassName, Dictionary<Team, Dictionary<AnimationName, Animation>>>();

        public static Dictionary<WeaponName, Weapon> Weapons = new Dictionary<WeaponName, Weapon>()
        {
            { WeaponName.BronzeSword,       new Weapon("Bronze Sword",      'E', 3, 100,0, 1, 1, new UnitType[] {},                                             50,350,  1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.IronSword,         new Weapon("Iron Sword",        'D', 5, 95, 0, 1, 1, new UnitType[] {},                                             40,520,  1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.SteelSword,        new Weapon("Steel Sword",       'C', 8, 90, 0, 1, 1, new UnitType[] {},                                             35,840,  1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.SilverSword,       new Weapon("Silver Sword",      'B', 11,85, 0, 1, 1, new UnitType[] {},                                             30,1410, 1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.BraveSword,        new Weapon("Brave Sword",       'A', 9, 80, 0, 1, 1, new UnitType[] {},                                             30,2100, 2,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.Armourslayer,      new Weapon("Armourslayer",      'D', 8, 80, 0, 1, 1, new UnitType[] { UnitType.Armored },                           25,1450, 1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.Wyrmslayer,        new Weapon("Wyrmslayer",        'D', 8, 80, 0, 1, 1, new UnitType[] { UnitType.Dragons },                           25,1500, 1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.KillingEdge,       new Weapon("Killing Edge",      'C', 9, 90, 30,1, 1, new UnitType[] {},                                             30,1470, 1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.LevinSword,        new Weapon("Levin Sword",       'C', 10,80, 0, 1, 2, new UnitType[] {},                                             25,1600, 1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.Rapier,            new Weapon("Rapier",            'E', 5, 90, 10,1, 1, new UnitType[] { UnitType.Beasts, UnitType.Armored },          35,1600, 1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.NobleRapier,       new Weapon("Noble Rapier",      'C', 8, 85, 10,1, 1, new UnitType[] { UnitType.Beasts, UnitType.Armored },          25,2100, 1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.Missiletainn,      new Weapon("Missiletainn",      'C', 8, 85, 10,1, 1, new UnitType[] {},                                             35,1050, 1,                     new CharacterStatistics(0,0,0,1,0,0,0,0), WeaponType.Sword) },
            { WeaponName.Sol,               new Weapon("Sol",               'B', 12,85, 5, 1, 1, new UnitType[] {},                                             30,0,    1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.Amatsu,            new Weapon("Amatsu",            'A', 12,60, 5, 1, 2, new UnitType[] {},                                             30,0,    1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.Falchion,          new Weapon("Falchion",          'E', 5, 80, 0, 1, 1, new UnitType[] { UnitType.Dragons },                           -1,-1,   1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.ExaltedFalchion,   new Weapon("Exalted Falchion",  'E', 15,80, 10,1, 1, new UnitType[] { UnitType.Dragons, UnitType.FellDragons },     -1,-1,   1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.ParallelFalchion,  new Weapon("Parallel Falchion", 'E', 12,80, 5, 1, 1, new UnitType[] { UnitType.Dragons, UnitType.FellDragons },     -1,-1,   1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.Mercurius,         new Weapon("Mercurius",         'A', 17,95, 5, 1, 1, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.Tyfing,            new Weapon("Tyrfing",           'A', 15,85, 10,1, 1, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(0,0,0,0,0,0,5,0), WeaponType.Sword) },
            { WeaponName.Balmung,           new Weapon("Balmung",           'A', 13,90, 10,1, 1, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(0,0,0,0,5,0,0,0), WeaponType.Sword) },
            { WeaponName.Mystletainn,       new Weapon("Mystletainn",       'A', 14,85, 15,1, 1, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(0,0,0,5,0,0,0,0), WeaponType.Sword) },
            { WeaponName.SolKatti,          new Weapon("Sol Katti",         'A', 8, 100,50,1, 1, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(0,0,0,0,0,0,5,0), WeaponType.Sword) },
            { WeaponName.Ragnell,           new Weapon("Ragnell",           'A', 15,70, 0, 1, 2, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(0,0,0,0,0,5,0,0), WeaponType.Sword) },
            { WeaponName.TreeBranch,        new Weapon("Tree Branch",       'E', 1, 100,0, 1, 1, new UnitType[] {},                                             20,100,  1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.GlassSword,        new Weapon("Glass Sword",       'E', 11,85, 0, 1, 1, new UnitType[] {},                                             3, 600,  1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.SoothingSword,     new Weapon("Soothing Sword",    'D', 8, 85, 0, 1, 1, new UnitType[] {},                                             10,920,  1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.SuperiorEdge,      new Weapon("Superior Edge",     'B', 11,80, 0, 1, 1, new UnitType[] {},                                             10,1950, 1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.LeifsBlade,        new Weapon("Leif's Blade",      'D', 4, 95, 30,1, 1, new UnitType[] {},                                             20,820,  1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.RoysBlade,         new Weapon("Roy's Blade",       'D', 8, 95, 5, 1, 1, new UnitType[] {},                                             25,900,  1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.EliwoodsBlade,     new Weapon("Eliwood's Blade",   'C', 10,85, 5, 1, 1, new UnitType[] {},                                             20,960,  1,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.EirikasBlade,      new Weapon("Eirika's Blade",    'C', 6, 95, 10,1, 1, new UnitType[] {},                                             20,1220, 2,                     new CharacterStatistics(), WeaponType.Sword) },
            { WeaponName.SeliphsBlade,      new Weapon("Seliph's Blade",    'B', 12,90, 15,1, 1, new UnitType[] {},                                             15,1530, 1,                     new CharacterStatistics(0,0,0,0,2,0,2,0), WeaponType.Sword) },
            { WeaponName.AlmsBlade,         new Weapon("Alm's Blade",       'B', 15,75, 10,1, 1, new UnitType[] {},                                             10,1630, 1,                     new CharacterStatistics(), WeaponType.Sword) },

            { WeaponName.BronzeLance,       new Weapon("Bronze Lance",      'E', 3, 90, 0, 1, 1, new UnitType[] {},                                             50,350,  1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.IronLance,         new Weapon("Iron Lance",        'D', 6, 85, 0, 1, 1, new UnitType[] {},                                             40,560,  1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.SteelLance,        new Weapon("Steel Lance",       'C', 9, 80, 0, 1, 1, new UnitType[] {},                                             35,910,  1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.SilverLance,       new Weapon("Silver Lance",      'B', 13,75, 0, 1, 1, new UnitType[] {},                                             30,1560, 1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.BraveLance,        new Weapon("Brave Lance",       'A', 10,70, 0, 1, 1, new UnitType[] {},                                             30,2220, 2,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.Javelin,           new Weapon("Javelin",           'D', 2, 80, 0, 1, 2, new UnitType[] {},                                             25,700,  1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.ShortSpear,        new Weapon("Short Spear",       'C', 5, 75, 0, 1, 2, new UnitType[] {},                                             25,1600, 1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.Spear,             new Weapon("Spear",             'B', 8, 70, 0, 1, 2, new UnitType[] {},                                             25,2400, 1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.BeastKiller,       new Weapon("Beast Killer",      'D', 9, 70, 0, 1, 1, new UnitType[] { UnitType.Beasts },                            25,1650, 1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.BlessedLance,      new Weapon("Blessed Lance",     'C', 11,70, 0, 1, 1, new UnitType[] { UnitType.Monsters },                          35,1540, 1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.KillerLance,       new Weapon("Killer Lance",      'C', 10,80, 30,1, 1, new UnitType[] {},                                             30,1680, 1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.Luna,              new Weapon("Luna",              'B', 14,80, 5, 1, 1, new UnitType[] {},                                             30,0,    1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.Gradivus,          new Weapon("Gradivus",          'A', 19,85, 5, 1, 2, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.GaeBolg,           new Weapon("Gáe Bolg",          'A', 15,75, 10,1, 1, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(0,0,0,0,0,5,0,0), WeaponType.Lance)  },
            { WeaponName.Gungnir,           new Weapon("Gungnir",           'A', 16,70, 10,1, 1, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(0,5,0,0,0,0,0,0), WeaponType.Lance)  },
            { WeaponName.Log,               new Weapon("Log",               'E', 1, 90, 0, 1, 1, new UnitType[] {},                                             20,100,  1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.GlassLance,        new Weapon("Glass Lance",       'E', 13,75, 0, 1, 1, new UnitType[] {},                                             3, 600,  1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.MiniatureLance,    new Weapon("Miniature Lance",   'D', 1, 55, 35,1, 2, new UnitType[] {},                                             10,650,  1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.Shockstick,        new Weapon("Shockstick",        'C', 11,85, 10,1, 1, new UnitType[] {},                                             20,1200, 1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.SuperiorLance,     new Weapon("Superior Lance",    'B', 13,70, 0, 1, 1, new UnitType[] {},                                             10,2100, 1,                     new CharacterStatistics(), WeaponType.Lance)  },
            { WeaponName.FinnsLance,        new Weapon("Finn's Lance",      'D', 8, 85, 10,1, 1, new UnitType[] {},                                             25,950,  1,                     new CharacterStatistics(0,0,0,0,0,2,2,0,0), WeaponType.Lance)  },
            { WeaponName.EphraimsLance,     new Weapon("Ephraim's Lance",   'C', 11,80, 10,1, 1, new UnitType[] {},                                             20,1220, 1,                     new CharacterStatistics(0,2,0,0,2,0,0,0), WeaponType.Lance)  },
            { WeaponName.SigurdsLance,      new Weapon("Sigurd's Lance",    'B', 14,85, 15,1, 1, new UnitType[] {},                                             15,1920, 1,                     new CharacterStatistics(), WeaponType.Lance)  },

            { WeaponName.BronzeAxe,         new Weapon("Bronze Axe",        'E', 4, 80, 0, 1, 1, new UnitType[] {},                                             50,400,  1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.IronAxe,           new Weapon("Iron Axe",          'D', 7, 75, 0, 1, 1, new UnitType[] {},                                             40,600,  1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.SteelAxe,          new Weapon("Steel Axe",         'C', 11,70, 0, 1, 1, new UnitType[] {},                                             35,980,  1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.SilverAxe,         new Weapon("Silver Axe",        'B', 15,65, 0, 1, 1, new UnitType[] {},                                             30,1740, 1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.BraveAxe,          new Weapon("Brave Axe",         'A', 12,60, 0, 1, 1, new UnitType[] {},                                             30,2400, 2,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.HandAxe,           new Weapon("Hand Axe",          'D', 3, 70, 0, 1, 2, new UnitType[] {},                                             25,750,  1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.ShortAxe,          new Weapon("Short Axe",         'C', 7, 65, 0, 1, 2, new UnitType[] {},                                             25,1750, 1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.Tomahawk,          new Weapon("Tomahawk",          'B', 10,60, 0, 1, 2, new UnitType[] {},                                             25,2550, 1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.Hammer,            new Weapon("Hammer",            'D', 10,60, 0, 1, 1, new UnitType[] { UnitType.Armored },                           25,1850, 1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.BoltAxe,           new Weapon("Bolt Axe",          'B', 14,70, 5, 1, 2, new UnitType[] {},                                             30,1920, 1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.KillerAxe,         new Weapon("Killer Axe",        'C', 12,70, 30,1, 1, new UnitType[] {},                                             30,1860, 1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.Vegeance,          new Weapon("Vegeance",          'B', 16,75, 5, 1, 1, new UnitType[] {},                                             30,0,    1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.WolfBerg,          new Weapon("Wolf Berg",         'A', 18,75, 5, 1, 2, new UnitType[] {},                                             35,0,    1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.Hauteclere,        new Weapon("Hauteclere",        'A', 21,70, 5, 1, 1, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.Helswath,          new Weapon("Helswath",          'A', 18,60, 10,1, 2, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(0,0,0,0,0,5,0,0), WeaponType.Axe) },
            { WeaponName.Armads,            new Weapon("Armads",            'A', 17,80, 10,1, 1, new UnitType[] {},                                             25,0,    1,                     new CharacterStatistics(0,0,0,0,0,5,0,0), WeaponType.Axe) },
            { WeaponName.Ladle,             new Weapon("Ladle",             'E', 1, 80, 0, 1, 1, new UnitType[] {},                                             20,100,  1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.GlassAxe,          new Weapon("Glass Axe",         'E', 15,65, 0, 1, 1, new UnitType[] {},                                             3, 600,  1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.ImposingAxe,       new Weapon("Imposing Axe",      'D', 14,35, 10,1, 1, new UnitType[] {},                                             10,830,  1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.VolantAxe,         new Weapon("Volant Axe",        'C', 8, 55, 0, 1, 2, new UnitType[] { UnitType.Flying },                            10,1510, 1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.SuperiorAxe,       new Weapon("Superior Axe",      'B', 15,60, 0, 1, 1, new UnitType[] {},                                             10,2150, 1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.OrsinsHatchet,     new Weapon("Orsin's Axe",       'D', 4, 85, 5, 1, 2, new UnitType[] {},                                             20,960,  1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.TitaniasAxe,       new Weapon("Titania's Axe",     'C', 12,80, 10,1, 1, new UnitType[] {},                                             20,1320, 1,                     new CharacterStatistics(), WeaponType.Axe) },
            { WeaponName.HectorsAxe,        new Weapon("Hector's Axe",      'B', 15,75, 15,1, 1, new UnitType[] {},                                             15,2010, 1,                     new CharacterStatistics(0,2,0,0,0,2,0,0), WeaponType.Axe) },

            { WeaponName.BronzeBow,         new Weapon("Bronze Bow",        'E', 3, 90, 0, 2, 2, new UnitType[] { UnitType.Flying }, 50, 350, 1,   new CharacterStatistics(), WeaponType.Bow) },
        };


        public static Dictionary<ClassName, CharacterClass> Classes = new Dictionary<ClassName, CharacterClass>()
        {
            { ClassName.Lord,           new CharacterClass("Lord",          new WeaponType[] { WeaponType.Sword },                                  new CharacterStatistics(18, 6, 0, 5, 7, 7, 0, 5)) },
            { ClassName.GreatLord,      new CharacterClass("Great Lord",    new WeaponType[] { WeaponType.Sword, WeaponType.Lance },                new CharacterStatistics(23, 10, 0, 7, 9, 10, 3, 6)) },
            { ClassName.Tactician,      new CharacterClass("Tactician",     new WeaponType[] { WeaponType.Sword, WeaponType.Tome },                 new CharacterStatistics(16, 4, 3, 5, 5, 5, 3, 5)) },
            { ClassName.Grandmaster,    new CharacterClass("Grandmaster",   new WeaponType[] { WeaponType.Sword, WeaponType.Tome },                 new CharacterStatistics(20, 7, 6, 7, 7, 7, 5, 6)) },
            { ClassName.Cavalier,       new CharacterClass("Cavalier",      new WeaponType[] { WeaponType.Sword, WeaponType.Lance },                new CharacterStatistics(18, 6, 0, 5, 6, 7, 0, 7)) },
            { ClassName.Paladin,        new CharacterClass("Paladin",       new WeaponType[] { WeaponType.Sword, WeaponType.Lance },                new CharacterStatistics(25, 9, 1, 7, 8, 10, 6, 8)) },
            { ClassName.GreatKnight,    new CharacterClass("Great Knight",  new WeaponType[] { WeaponType.Sword, WeaponType.Lance, WeaponType.Axe },new CharacterStatistics(26, 11, 0, 6, 5, 14, 1, 7)) },
            { ClassName.Knight,         new CharacterClass("Knight",        new WeaponType[] { WeaponType.Lance },                                  new CharacterStatistics(18, 8, 0, 4, 2, 11, 0, 4)) },
            { ClassName.General,        new CharacterClass("General",       new WeaponType[] { WeaponType.Lance, WeaponType.Axe },                  new CharacterStatistics(28, 12, 0, 7, 4, 15, 3, 5)) },
            { ClassName.Myrmidon,       new CharacterClass("Myrmidon",      new WeaponType[] { WeaponType.Sword },                                  new CharacterStatistics(16, 4, 1, 9, 10, 4, 1, 5)) },
            { ClassName.Swordmaster,    new CharacterClass("Swordmaster",   new WeaponType[] { WeaponType.Sword },                                  new CharacterStatistics(20, 7, 2, 11, 13, 6, 4, 6)) },
            { ClassName.Mercenary,      new CharacterClass("Mercenary",     new WeaponType[] { WeaponType.Sword },                                  new CharacterStatistics(18, 5, 0, 8, 7, 5, 0, 5)) },
            { ClassName.Hero,           new CharacterClass("Hero",          new WeaponType[] { WeaponType.Sword, WeaponType.Axe },                  new CharacterStatistics(22, 8, 1, 11, 10, 8, 3, 6)) },
            { ClassName.Fighter,        new CharacterClass("Fighter",       new WeaponType[] { WeaponType.Axe },                                    new CharacterStatistics(20, 8, 0, 5, 5, 4, 0, 5)) },
            { ClassName.Warrior,        new CharacterClass("Warrior",       new WeaponType[] { WeaponType.Axe, WeaponType.Bow },                    new CharacterStatistics(28, 12, 0, 8, 7, 7, 3, 6)) },
            { ClassName.Barbarian,      new CharacterClass("Barbarian",     new WeaponType[] { WeaponType.Axe },                                    new CharacterStatistics(22, 8, 0, 3, 8, 3, 0, 5)) },
            { ClassName.Berserker,      new CharacterClass("Berserker",     new WeaponType[] { WeaponType.Axe },                                    new CharacterStatistics(30, 13, 0, 5, 11, 5, 1, 6)) },
            { ClassName.Archer,         new CharacterClass("Archer",        new WeaponType[] { WeaponType.Bow },                                    new CharacterStatistics(16, 5, 0, 8, 6, 5, 0, 5)) },
            { ClassName.Sniper,         new CharacterClass("Sniper",        new WeaponType[] { WeaponType.Bow },                                    new CharacterStatistics(20, 7, 1, 12, 9, 10, 3, 6)) },
            { ClassName.BowKnight,      new CharacterClass("Bow Knight",    new WeaponType[] { WeaponType.Sword, WeaponType.Bow },                  new CharacterStatistics(24, 8, 0, 10, 10, 6, 2, 8)) },
            { ClassName.Thief,          new CharacterClass("Thief",         new WeaponType[] { WeaponType.Sword },                                  new CharacterStatistics(16, 3, 0, 6, 8, 2, 0, 5)) },
            { ClassName.Assassin,       new CharacterClass("Assassin",      new WeaponType[] { WeaponType.Sword, WeaponType.Bow },                  new CharacterStatistics(21, 8, 0, 13, 12, 5, 1, 6)) },
            { ClassName.Trickster,      new CharacterClass("Trickster",     new WeaponType[] { WeaponType.Sword, WeaponType.Staff },                new CharacterStatistics(19, 4, 4, 10, 11, 3, 5, 6)) },
            { ClassName.PegasusKnight,  new CharacterClass("Pegasus Knight",new WeaponType[] { WeaponType.Lance },                                  new CharacterStatistics(16, 4, 2, 7, 8 ,4, 6, 7)) },
            { ClassName.FalconKnight,   new CharacterClass("Falcon Knight", new WeaponType[] { WeaponType.Lance, WeaponType.Staff },                new CharacterStatistics(20, 6, 3, 10, 11, 6, 9, 8)) },
            { ClassName.DarkFlier,      new CharacterClass("Dark Flier",    new WeaponType[] { WeaponType.Lance, WeaponType.Tome },                 new CharacterStatistics(19, 5, 6, 8, 10, 5, 9, 8)) },
            { ClassName.WyvernRider,    new CharacterClass("Wyvern Rider",  new WeaponType[] { WeaponType.Axe },                                    new CharacterStatistics(19, 7, 0 ,6, 5, 8, 0, 7)) },
            { ClassName.WyvernLord,     new CharacterClass("Wyvern Lord",   new WeaponType[] { WeaponType.Lance, WeaponType.Axe },                  new CharacterStatistics(24, 11, 0, 8, 7, 11, 3, 8)) },
            { ClassName.GriffonRider,   new CharacterClass("Griffon Rider", new WeaponType[] { WeaponType.Axe },                                    new CharacterStatistics(22, 9, 0, 10, 9, 8, 3, 8)) },
            { ClassName.Mage,           new CharacterClass("Mage",          new WeaponType[] { WeaponType.Tome },                                   new CharacterStatistics(16, 0, 4, 3, 4, 2, 3, 5)) },
            { ClassName.Sage,           new CharacterClass("Sage",          new WeaponType[] { WeaponType.Tome, WeaponType.Staff },                 new CharacterStatistics(20, 1, 7, 5, 7, 4, 5, 6)) },
            { ClassName.DarkMage,       new CharacterClass("Dark Mage",     new WeaponType[] { WeaponType.Tome },                                   new CharacterStatistics(18, 1, 3, 2, 3, 4, 4, 5)) },
            { ClassName.Sorcerer,       new CharacterClass("Sorcerer",      new WeaponType[] { WeaponType.Tome },                                   new CharacterStatistics(23, 2, 6, 4, 4, 7, 7, 6)) },
            { ClassName.DarkKnight,     new CharacterClass("Dark Knight",   new WeaponType[] { WeaponType.Sword, WeaponType.Tome },                 new CharacterStatistics(25, 2, 6, 4, 4, 7, 7, 6)) },
            { ClassName.Cleric,         new CharacterClass("Cleric",        new WeaponType[] { WeaponType.Staff },                                  new CharacterStatistics(16, 0, 3, 2, 4, 1, 6, 5)) },
            { ClassName.Priest,         new CharacterClass("Priest",        new WeaponType[] { WeaponType.Staff },                                  new CharacterStatistics(16, 0, 3, 2, 4, 1, 6, 5)) },
            { ClassName.WarMonk,        new CharacterClass("War Monk",      new WeaponType[] { WeaponType.Staff, WeaponType.Axe },                  new CharacterStatistics(24, 5, 5, 4, 8, 3, 8, 8)) },
            { ClassName.Troubadour,     new CharacterClass("Troubadour",    new WeaponType[] { WeaponType.Staff },                                  new CharacterStatistics(16, 0, 3, 2, 5, 1, 5, 7)) },
            { ClassName.Valkyrie,       new CharacterClass("Valkyrie",      new WeaponType[] { WeaponType.Tome, WeaponType.Staff },                 new CharacterStatistics(19, 0, 5, 4, 8, 3, 8, 8)) },
            { ClassName.Villager,       new CharacterClass("Villager",      new WeaponType[] { WeaponType.Lance },                                  new CharacterStatistics(16, 1, 0, 1, 1, 1, 0, 5)) },
            { ClassName.Dancer,         new CharacterClass("Dancer",        new WeaponType[] { WeaponType.Sword },                                  new CharacterStatistics(16, 1, 1, 5, 8, 3, 1, 5)) },
            { ClassName.Lodestar,       new CharacterClass("Lodestar",      new WeaponType[] { WeaponType.Sword },                                  new CharacterStatistics(21, 9, 1, 10, 10, 8, 4, 6)) },
            { ClassName.DreadFighter,   new CharacterClass("Dread Fighter", new WeaponType[] { WeaponType.Sword, WeaponType.Axe, WeaponType.Tome }, new CharacterStatistics(22, 8, 4, 7, 9, 7, 10, 6)) },
            { ClassName.Soldier,        new CharacterClass("Soldier",       new WeaponType[] { WeaponType.Sword, WeaponType.Lance },                new CharacterStatistics(18, 3, 0, 3, 3, 3, 0, 5)) },
            { ClassName.Conqueror,      new CharacterClass("Conqueror",     new WeaponType[] { WeaponType.Sword, WeaponType.Lance, WeaponType.Axe },new CharacterStatistics(24, 10, 3, 9, 8, 12, 5, 8)) }
        };
    }
}
