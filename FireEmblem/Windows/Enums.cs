using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public enum TurnOrder
    {
        Picking,
        Moving,
        BattleChoice,
        Attack,
        Items,
        End
    }
    public enum Terrian
    {
        Plain,
        Mud,
        Water,
        Forest,
        Gravel
    }
    public enum WeaponType
    {
        Sword,
        Lance,
        Axe,
        Bow,
        Tome,
        Staff,
        Stone
    }

    public enum ItemType
    {
        Weapon,
        Usable,
        Ect
    }

    public enum UnitType
    {
        Beasts,
        Armored,
        Flying,
        Monsters,
        Dragons,
        FellDragon
    }
    public enum WeaponSpecial
    {
        None,
        ExtraStats,
        Recover,
        Skill,
        Magic,
    }
    public enum WeaponName
    {
        BronzeSword,
        IronSword,
        SteelSword,
        SilverSword,
        BraveSword,
        Armourslayer,
        Wyrmslayer,
        KillingEdge,
        LevinSword,
        Rapier,
        NobleRapier,
        Missiletainn,
        Sol,
        Amatsu,
        Falchion,
        ExaltedFalchion,
        ParallelFalchion,
        Mercurius,
        Tyfing,
        Balmung,
        Mystletainn,
        DolKatti,
        Ragnell,
        TreeBranch,
        GlassSword,
        SoothingSword,
        SuperiorEdge,
        LeifsBlade,
        RoysBlade,
        EliwoodsBlade,
        EirikasBlade,
        SeliphsBlade,
        AlmsBlade
    }

    public enum ClassName
    {
        Lord,
        GreatLord,
        Tactician,
        Grandmaster,
        Cavalier,
        Paladin,
        GreatKnight,
        Knight,
        General,
        Myrmidon,
        Swordmaster,
        Mercenary,
        Hero,
        Fighter,
        Warrior,
        Barbarian,
        Berserker,
        Archer,
        Sniper,
        BowKnight,
        Thief,
        Assassin,
        Trickster,
        PegasusKnight,
        FalconKnight,
        DarkFlier,
        WyvernRider,
        WyvernLord,
        GriffonRider,
        Mage,
        Sage,
        DarkMage,
        Sorcerer,
        DarkKnight,
        Cleric,
        Priest,
        WarMonk,
        Troubadour,
        Valkyrie,
        Villager,
        Dancer,
        Lodestar,
        DreadFighter,
        Soldier,
        Conqueror,
    }
}
