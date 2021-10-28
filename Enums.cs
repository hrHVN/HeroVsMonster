using System;

/*
 * Place The Enums here.
 * 
 * Vurder å utbedre defence logikk - leggtil fleire branching-variabler - Attack/defence base + Clothing type + 
 * Equiped limb + encumbered + agility + osv..
 */

namespace HeroVsMonster
{
    public enum PlayerClass { Farmer, Warrior, Archer, Mage, Munk, Priest, Nun }

    public enum MonsterRace { Orc, Gobblin, Giant, Wizard, FallenAngel, DarkElf, BlackDwarves, Pixie }

    public enum MagicClass { Mage, Wizard, Pixie }

    public enum HolyClass { Munk, Priest, Nun, FallenAngel}

    public enum DefenceType
    {
        None, ThickHide, Leather, Archane, Steel, Copper, ChainMail, Faith
    }

    public enum AttackType
    {
        Blunt, Magic, Holy, Sharp
    }

    public enum PlayerWeapons { PitchFork, Sword, GreatSword, Mace, Staff, Wand, Shovel, Bow, CrossBow, Umbrella, Fists }

    public enum MonsterWeapon { WoodenClub, MakeShiftSword, TreeTrunk, GobblinWand, Pebbles, Fists }

    public enum SharpWeapons { PitchFork, Sword, GreatSword, Shovel, Bow, CrossBow, MakeShiftSword, Javelin }

    public enum BluntWeapons { Mace, Staff, Fists, Umbrella, WoodenClub, TreeTrunk, Pebbles }

    public enum MagicWeapons { Wand, GobblinWand, Staff }

    public enum HolyWeapons { Wand, Umbrella, Staff }

    public enum RangedWeapons { Bow, CrossBow, Pebbles, Javelin }

    public enum FarmerTools { PitchFork , Shovel }

    public enum Clothing
    {
        Shirt, Robe, PlateArmor, Chanmail, LeatherArmor
    }
    
    public static class EnumHelper
    {
        // Retrieve the enum (int)value based on string
        public static T GetEnumValue<T>(string str) where T : struct, IConvertible
        {
            Type enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val;
            return Enum.TryParse<T>(str, true, out val) ? val : default(T);
        }

        // Retrieve the enum (string)value based on int
        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            return (T)Enum.ToObject(enumType, intValue);
        }

        //Check if a value exist in the enum
        public static bool IsParsable<T>(this string value) where T : struct
        {
            return Enum.TryParse<T>(value, true, out _);
        }
    }
}
