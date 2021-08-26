﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVsMonster
{ 
    public static class Monsters
    {
        public static string MonsterClass;
        public static string MonsterWeapon;
        public static int MonsterLevel;
        public static int MonsterHealth;
        public static string AttackType;

        public static List<string> PreferedWeapon = new List<string>();
        public static List<string> Defence = new List<string>();
        public static List<string> Inventory = new List<string>();

        public static void Spawn()
        {
            Random _random = new Random();
            
            // Selecting Monster
            int _MonsterClass = _random.Next(0, Enum.GetValues(typeof(MonsterRace)).GetUpperBound(0));
            var Monster = (MonsterRace)_MonsterClass;
            MonsterClass = Monster.ToString();

            // Selecting Weapon
            int _MonsterWeapon = _random.Next(0, Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0));
            var Weapon = (MonsterWeapon)_MonsterWeapon;
            MonsterWeapon = Weapon.ToString();

            switch (MonsterClass)
            {
                case "Orc":
                    Defence.Add(" ");
                    break;
            }
        }

        static void MonsterDialogue()
        {
            
        }
    }

    enum MonsterRace
    {
        Orc, Gobblin, Giant, Wolf, Sheep
    }

    enum MonsterWeapon
    {
        WoodenClub, MakeShiftSword, TreeTrunk, GobblinWand, Pebbles
    }

    class BossMonster
    {
        /*
        * This is the base code, from an .NET tutorial @ microsoftDocs
        * https://docs.microsoft.com/nb-no/learn/modules/csharp-do-while/4-solution#code-try-9
        */
/*
        private int hero = 10;
        private int monster = 10;

        Random dice = new Random();

        static void Boss()
        {
            do
            {
                int roll = dice.Next(1, 11);
                monster -= roll;
                Console.WriteLine($"Monster was damaged and lost {roll} health and now has {monster} health.");

                if (monster <= 0) continue;

                roll = dice.Next(1, 11);
                hero -= roll;
                Console.WriteLine($"Hero was damaged and lost {roll} health and now has {hero} health.");

            } while (hero > 0 && monster > 0);

            Console.WriteLine(hero > monster ? "Hero wins!" : "Monster wins!");
*/
        
    }
}
