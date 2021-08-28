using System;

/*
 * Place The Enums here.
 * 
 * Vurder å utbedre defence logikk - leggtil fleire branching-variabler - Attack/defence base + Clothing type + 
 * Equiped limb + encumbered + agility + osv..
 */

namespace HeroVsMonster
{
    public enum PlayerClass
    {
        Farmer, Warrior, Archer, Mage, Munk
    }

    public enum MonsterRace
    {
        Orc, Gobblin, Giant, Wizard, FallenAngel, DarkElf, BlackDwarves
    }

    public enum DefenceType
    {
        None, Faith, Leather, Archane, Steel, ThickHide, Copper, ChainMail, Holy
    }

    public enum AttackType
    {
        Blunt, Ranged, Magic, Holy, Sharp
    }

    public enum PlayerWeapons
    {
        PitchFork, Sword, GreatSword, Mace, Staff, Wand, Shovel, Bow, CrossBow, Umbrella, Fists
    }

    public enum MonsterWeapon
    {
        WoodenClub, MakeShiftSword, TreeTrunk, GobblinWand, Pebbles, Fists
    }

    public enum Clothing
    {
        Shirt, Robe, PlateArmor, Chanmail, LeatherArmor
    }
    
    public static class EnumHelper
    {
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

        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val;
            return (T)Enum.ToObject(enumType, intValue);
        }
    }
}
