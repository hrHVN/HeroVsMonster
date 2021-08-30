using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVsMonster
{ 
    public static class Monsters
    {
        public static string Class;
        public static string Weapon;
        public static int Level;
        public static int Health;

        public static List<string> PreferedWeapon = new List<string>();
        public static List<string> Defence = new List<string>();
        public static List<string> Inventory = new List<string>();
        public static double Purse;

        public static void Spawn()
        {
            Random _random = new Random();
            
            // Selecting Monster
            int _MonsterClass = _random.Next(0, Enum.GetValues(typeof(MonsterRace)).GetUpperBound(0));
            var Monster = (MonsterRace)_MonsterClass;
            Class = Monster.ToString();

            //Setting the Monster Level
            int _maxLevel = (Player.Level > 20) ? Player.Level * 3 : Player.Level * 5;
            int _minLevel = (Player.Level < 20) ? 1 : 20;
            Level = Player.Level + _random.Next(_minLevel, _maxLevel);

            switch (Class)
            {
                default: //"Orc"
                    Defence.Add("ThickHide");
                    Health = 75 * Level;
                    Purse = _random.Next(0, (Level * 3));

                    //Adds DefenceAttributes
                    if (Level % 3 > 0)
                    {
                        for (int i = 0; i < (Level % 3); i++)
                        {
                            int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                            string _defT = ((DefenceType)_defence).ToString();

                            if (Defence.Contains(_defT)) { i--; continue; }
                            else { Defence.Add(_defT); }
                        }
                    }
                    else
                    {
                        int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                        Defence.Add(((DefenceType)_defence).ToString());
                    }

                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("MakeShiftSword"); PreferedWeapon.Add("Fists"); PreferedWeapon.Add("Mace");
                    // Inventory.Add();

                    // Selecting Weapon
                    int _MonsterWeapon = _random.Next(0, Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0));
                    Weapon = ((MonsterWeapon)_MonsterWeapon).ToString();

                    break;

                case "Gobblin":
                    Defence.Add("Leather");
                    Health = 35 * Level;
                    Purse = _random.Next(0, (Level * 3));

                    //Adds DefenceAttributes
                    if (Level % 3 > 0)
                    {
                        for (int i = 0; i < (Level % 3); i++)
                        {
                            int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                            string _defT = ((DefenceType)_defence).ToString();

                            if (Defence.Contains(_defT)) { i--; continue; }
                            else { Defence.Add(_defT); }
                        }
                    }
                    else
                    {
                        int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                        Defence.Add(((DefenceType)_defence).ToString());
                    }

                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("GobblinWand"); PreferedWeapon.Add("Pebbles"); PreferedWeapon.Add("WoodenClub");
                    // Inventory.Add();

                    // Selecting Weapon
                    _MonsterWeapon = _random.Next(0, Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0));
                    Weapon = ((MonsterWeapon)_MonsterWeapon).ToString();

                    break;

                case "Giant":
                    Defence.Add("ThickHide");
                    Health = 105 * Level;
                    Purse = _random.Next(0, (Level * 3));

                    //Adds DefenceAttributes
                    if (Level % 3 > 0)
                    {
                        for (int i = 0; i < (Level % 3); i++)
                        {
                            int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                            string _defT = ((DefenceType)_defence).ToString();

                            if (Defence.Contains(_defT)) { i--; continue; }
                            else { Defence.Add(_defT); }
                        }
                    }
                    else
                    {
                        int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                        Defence.Add(((DefenceType)_defence).ToString());
                    }

                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("TreeTrunk"); PreferedWeapon.Add("WoodenClub");
                    // Inventory.Add();

                    // Selecting Weapon
                    _MonsterWeapon = _random.Next(0, Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0));
                    Weapon = ((MonsterWeapon)_MonsterWeapon).ToString();

                    break;

                case "Wizard":
                    Defence.Add("Archane");
                    Health = 75 * Level;
                    Purse = _random.Next(0, (Level * 3));

                    //Adds DefenceAttributes
                    if (Level % 3 > 0)
                    {
                        for (int i = 0; i < (Level % 3); i++)
                        {
                            int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                            string _defT = ((DefenceType)_defence).ToString();

                            if (Defence.Contains(_defT)) { i--; continue; }
                            else { Defence.Add(_defT); }
                        }
                    }
                    else
                    {
                        int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                        Defence.Add(((DefenceType)_defence).ToString());
                    }

                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("GobblinWand"); PreferedWeapon.Add("Wand"); PreferedWeapon.Add("Staff");
                    // Inventory.Add();

                    // Selecting Weapon
                    List<string> _weaponsToChoose = new List<string>();
                    for (int i = 0; i < Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0); i++) 
                    {
                        _weaponsToChoose.Add(((MonsterWeapon)i).ToString());
                    }
                    for (int i = 0; i < Enum.GetValues(typeof(PlayerWeapons)).GetUpperBound(0); i++)
                    {
                        _weaponsToChoose.Add(((PlayerWeapons)i).ToString());
                    }
                    Weapon = _weaponsToChoose[_random.Next(0, _weaponsToChoose.Count())];

                    break;

                case "FallenAngel":
                    Defence.Add("Archane");
                    Health = 95 * Level;
                    Purse = _random.Next(0, (Level * 3));

                    //Adds DefenceAttributes
                    if (Level % 3 > 0)
                    {
                        for (int i = 0; i < (Level % 3); i++)
                        {
                            int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                            string _defT = ((DefenceType)_defence).ToString();

                            if (Defence.Contains(_defT)) { i--; continue; }
                            else { Defence.Add(_defT); }
                        }
                    }
                    else
                    {
                        int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                        Defence.Add(((DefenceType)_defence).ToString());
                    }

                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("Sword"); PreferedWeapon.Add("Bow"); PreferedWeapon.Add("CrossBow");
                    PreferedWeapon.Add("GreatSword");
                    // Inventory.Add();

                    // Selecting Weapon
                    _weaponsToChoose = new List<string>();
                    for (int i = 0; i < Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0); i++)
                    {
                        _weaponsToChoose.Add(((MonsterWeapon)i).ToString());
                    }
                    for (int i = 0; i < Enum.GetValues(typeof(PlayerWeapons)).GetUpperBound(0); i++)
                    {
                        _weaponsToChoose.Add(((PlayerWeapons)i).ToString());
                    }
                    Weapon = _weaponsToChoose[_random.Next(0, _weaponsToChoose.Count())];

                    break;

                case "DarkElf":
                    Defence.Add("ChainMail");
                    Health = 85 * Level;
                    Purse = _random.Next(0, (Level * 3));

                    //Adds DefenceAttributes
                    if (Level % 3 > 0)
                    {
                        for (int i = 0; i < (Level % 3); i++)
                        {
                            int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                            string _defT = ((DefenceType)_defence).ToString();

                            if (Defence.Contains(_defT)) { i--; continue; }
                            else { Defence.Add(_defT); }
                        }
                    }
                    else
                    {
                        int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                        Defence.Add(((DefenceType)_defence).ToString());
                    }

                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("Staff"); PreferedWeapon.Add("Bow"); PreferedWeapon.Add("GreatSword");
                    // Inventory.Add();
                    break;

                case "BlackDwarves":
                    Defence.Add("Steel");
                    Health = 105 * Level;
                    Purse = _random.Next(0, (Level * 3));

                    //Adds DefenceAttributes
                    if (Level % 3 > 0)
                    {
                        for (int i = 0; i < (Level % 3); i++)
                        {
                            int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                            string _defT = ((DefenceType)_defence).ToString();

                            if (Defence.Contains(_defT)) { i--; continue; }
                            else { Defence.Add(_defT); }
                        }
                    }
                    else
                    {
                        int _defence = _random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0));
                        Defence.Add(((DefenceType)_defence).ToString());
                    }

                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("Sword"); PreferedWeapon.Add("CrossBow"); PreferedWeapon.Add("GreatSword");
                    PreferedWeapon.Add("Mace"); PreferedWeapon.Add("Fists");
                    // Inventory.Add();

                    // Selecting Weapon
                    _weaponsToChoose = new List<string>();
                    for (int i = 0; i < Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0); i++)
                    {
                        _weaponsToChoose.Add(((MonsterWeapon)i).ToString());
                    }
                    for (int i = 0; i < Enum.GetValues(typeof(PlayerWeapons)).GetUpperBound(0); i++)
                    {
                        _weaponsToChoose.Add(((PlayerWeapons)i).ToString());
                    }
                    Weapon = _weaponsToChoose[_random.Next(0, _weaponsToChoose.Count())];

                    break;
            }

            // Adds a predefined random selection of items for the monster;
            string[] _ekstraDefence = { "Leather Armor" };
            int _ekstra = _random.Next(1, _ekstraDefence.GetLength(0));
            for (int i = 0; i < _ekstra; i++) { Defence.Add(_ekstraDefence[i]); }
        }

        static void MonsterDialogue()
        {
            
        }
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

