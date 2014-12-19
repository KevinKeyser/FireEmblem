﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public enum AnimationType
    {
        Normal,
        Reverse,
        PingPong,
        Skip
    }

    public enum AnimationName
    {
        Walking,
        Attacking
    }

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
        StatModifier,
        Health,
        Ect
    }

    public enum UnitType
    {
        Beasts,
        Armored,
        Flying,
        Monsters,
        Dragons,
        FellDragons
    }
    public enum WeaponSpecial
    {
        None,
        ExtraStats,
        Recover,
        Skill,
        Magic,
    }

    public enum ItemName
    {
        Vulnerary,
        Concoction,
        Elixir,
        PureWater,
        HPTonic,
        StrengthTonic,
        MagicTonic,
        SkillTonic,
        SpeedTonic,
        LuckTonic,
        DefenceTonic,
        ResistanceTonic,
        DoorKey,
        ChestKey,
        MasterKey,
        SeraphRobe,
        EnergyDrop,
        SpiritDust,
        SecretBook,
        Speedwing,
        GoddessIcon,
        Dracoshield,
        Talisman,
        NagasTear,
        Boots,
        ArmsScroll,
        MasterSeal,
        SecondSeal,
        SmallBullion,
        MediumBullion,
        LargeBullion,
        SweetTincture,
        GaiussConfect,
        KrissConfect,
        TikisTear,
        SeedofTrust,
        ReekingBox,
        RiftDoor,
        SupremeEmblem,
        DreadScroll,
        WeddingBouquet,
        AllStats,
        Paragon,
        IotesShield,
        LimitBreaker,
        SilverCard,
        OutrealmItem,

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
        SolKatti,
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
        AlmsBlade,

        BronzeLance,
        IronLance,
        SteelLance,
        SilverLance,
        BraveLance,
        Javelin,
        ShortSpear,
        Spear,
        BeastKiller,
        BlessedLance,
        KillerLance,
        Luna,
        Gradivus,
        GaeBolg,
        Gungnir,
        Log,
        GlassLance,
        MiniatureLance,
        Shockstick,
        SuperiorLance,
        FinnsLance,
        EphraimsLance,
        SigurdsLance,

        BronzeAxe,
        IronAxe,
        SteelAxe,
        SilverAxe,
        BraveAxe,
        HandAxe,
        ShortAxe,
        Tomahawk,
        Hammer,
        BoltAxe,
        KillerAxe,
        Vegeance,
        WolfBerg,
        Hauteclere,
        Helswath,
        Armads,
        Ladle,
        GlassAxe,
        ImposingAxe,
        VolantAxe,
        SuperiorAxe,
        OrsinsHatchet,
        TitaniasAxe,
        HectorsAxe,

        BronzeBow,
        IronBow,
        SteelBow,
        SilverBow,
        BraveBow,
        BlessedBow,
        KillerBow,
        Longbow,
        Astra,
        Parthia,
        Yewfelle,
        Nidhogg,
        DoubleBow,
        SlackBow,
        GlassBow,
        ToweringBow,
        UnderdogBow,
        SuperiorBow,
        WoltsBow,
        InnesBow,

        Fire,
        Elfire,
        Arcfire,
        Bolganone,
        Valflame,
        Thunder,
        Elthunder,
        Arcthunder,
        Thoron,
        Mjolnir,
        Wind,
        Elwind,
        Arcwind,
        Rexcalibur,
        Excalibur,
        Forseti,
        BookofNaga,
        Flux,
        Nosferatu,
        Ruin,
        Waste,
        Goetia,
        GrimasTruth,
        Mire,
        DyingBlaze,
        MicaiahsPyre,
        SuperiorJolt,
        KatarinasBolt,
        Wilderwind,
        CelicasGale,
        AversasNight,

        Heal,
        Mend,
        Physic,
        Recover,
        Fortify,
        GoddessStaff,
        Rescue,
        Ward,
        Hammerne,
        Kneader,
        BalmwoodStaff,
        Catharsis
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

    public enum CharacterName
    {
        Anna,
        Aversa,
        Basilio,
        Brady,
        Cherche,
        Chrom,
        Cordelia,
        Donnel,
        Emmeryn,
        Excellus,
        Flavia,
        Gaius,
        Gangrel,
        Gerome,
        Gregor,
        Henry,
        Inigo,
        Kellam,
        Kjelle,
        Laurent,
        Libra,
        Lissa,
        Lucina,
        Maribelle,
        Miriel,
        Nah,
        Noire,
        Nowi,
        Olivia,
        Owain,
        Panne,
        Priam,
        Ricken,
        Sayri,
        Severa,
        Stahl,
        Sully,
        Tharja,
        Tiki,
        Vaike,
        Validar,
        Virion,
        Yarne,
        Yenfay
    }
}
