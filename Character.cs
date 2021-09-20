using System;
using System.Collections.Generic;

/*
 *  To DO
 *  Clean up, improve selection
 *  make game balancing easier
 */

namespace HeroVsMonster
{
    public class Character 
    {
        public static string Class;
        public static string Weapon;
        public static int Level;
        public static int Xp;
        public static int Health;
        public static int MaxHealth;
        public static int WeaponProffeciency;
        public static int DeffenceEndurance;
        public static int Wallet;

        public static List<string> PreferedWeapon = new List<string>();
        public static string[,] DefenceType = { {"Head", "None" }, { "Chest", "None" }, { "Arms", "None" }, { "Legs", "None" }, { "Boots", "None" }, };
        public static List<string> Inventory = new List<string>();

        public static int _nextLevel = 100;

        public static void LevelUp()
        {
            int _xp = Player.Xp;
            _nextLevel = 100;

            if (_xp >= _nextLevel)
            {
                Player.Level++;
                _nextLevel += (_nextLevel / 3);

                Utility.WriteLine($"Congratulations, you have leveled up!! {Player.Level}");
            }
            else { Utility.WriteLine($"Next Level in {_nextLevel - _xp}!", "cyan"); }
        }
    }

    public class Player : Character
    {
        public static string Name;
        public static int Monstersdefeated;
     
        public static void PlayerCreation() // Player creation, name + class = Starting specs
        {
            Random _random = new Random();

            // Starting Level
            Player.Level = 1;
            Player.Xp = 0;
            Player.Wallet = 50;
            Player.Health = 100;

            // Selecting Weapon
            int _PlayerWeapon = _random.Next(0, Enum.GetValues(typeof(PlayerWeapons)).GetUpperBound(0));
            Player.Weapon = ((PlayerWeapons)_PlayerWeapon).ToString();

            Utility.Heading(Utility.Center("\nStarting a New game"));
            // Name Creation
            Console.Write("\n\tEnter your Name Hero: ");
            Player.Name = Console.ReadLine();
            Utility.WriteLine($"\nWelcome {Player.Name}! We have been awaiting your arrival for some time now..");
            Console.ReadKey();

            // Class selection
            bool x = false;
            int z = 0;
            int enumMax = Enum.GetValues(typeof(PlayerClass)).GetUpperBound(0); // Gets the max enum value

            // Itterates throug PlayerClass Enum
            while (!x)
            {
                var Current = (PlayerClass)z;
                Utility.WriteLine($"\n{Player.Name} are you a.. " + Current);
                string input = Utility.Input("Select Yes or No (Y/N)");
                Console.Clear();

                if ((input != "y") && (z < enumMax)) { z++; }
                else if ((input != "y") && (z == enumMax)) { z = 0; }
                else
                {
                    Player.Class = Current.ToString();
                    switch (Player.Class)
                    {
                        case "Warrior":
                            Utility.WriteLine($".. *Sigh*, thank the heavens that you a {Player.Class}! Came to our resque. This will give the " +
                                $"Towns people som safety");
                            x = true;
                            break;
                        case "Archer":
                            Utility.WriteLine($"Great we really need an {Player.Class} to take down those pesky flying things! Just don't die " +
                                $"on us too fast, these monsters are Nasty");
                            x = true;
                            break;
                        case "Mage":
                            Utility.WriteLine($"Wow! A {Player.Class}! I don't know if i should worry or be thankfull! The last {Player.Class} " +
                                $"almost wiped out our city when he sneezed... ");
                            x = true;
                            break;
                        case "Munk":
                            Utility.WriteLine($"A {Player.Class}! Are you shure you'r up for this?? Didn't think \"Holy People\" did much figthing.. ");
                            x = true;
                            break;
                        default:
                            Utility.WriteLine($"*Yikes!!* A {Player.Class}!! What is the king thinking sending YOU of all people, times must " +
                                $"really be dire...");
                            x = true;
                            break;
                    }
                }
            }

            //Adding Player base stats
            switch (Player.Class)
            {
                case "Warrior":
                    //Player.Defence.Add("Steel");
                    Player.PreferedWeapon.Add("GreatSword");
                    Player.PreferedWeapon.Add("Sword");
                    Player.PreferedWeapon.Add("Mace");
                    break;

                case "Mage":
                    //Player.Defence.Add("Archane");
                    Player.PreferedWeapon.Add("Wand");
                    Player.PreferedWeapon.Add("Staff");
                    break;

                case "Archer":
                    //Player.Defence.Add("Leather");
                    Player.PreferedWeapon.Add("Bow");
                    Player.PreferedWeapon.Add("CrossBow");
                    break;

                case "Munk":
                    //Player.Defence.Add("Faith");
                    Player.PreferedWeapon.Add("Staff");
                    Player.PreferedWeapon.Add("Umbrella");
                    Player.PreferedWeapon.Add("Fists");
                    break;

                default:    //Farmer
                    //Player.Defence.Add(((DefenceType)_random.Next(0, Enum.GetValues(typeof(DefenceType)).GetUpperBound(0))).ToString());
                    Player.Health += 100;
                    Player.PreferedWeapon.Add("Pitchfork");
                    Player.PreferedWeapon.Add("Shovel");
                    Player.PreferedWeapon.Add("Fists");
                    break;
            }

            Game.SaveGame();
        }
    }

    public class Monsters : Character
    {
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
                    //Defence.Add("ThickHide");
                    Health = 75 * Level;
                    Wallet = _random.Next(0, (Level * 3));

                    //Adds DefenceAttributes
                    /*
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
                    */

                    // Selecting Weapon
                    int _MonsterWeapon = _random.Next(0, Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0));
                    Weapon = ((MonsterWeapon)_MonsterWeapon).ToString();

                    break;

                case "Gobblin":
                    //Defence.Add("Leather");
                    Health = 35 * Level;
                    Wallet = _random.Next(0, (Level * 3));
                    /*
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
                    */
                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("GobblinWand"); PreferedWeapon.Add("Pebbles"); PreferedWeapon.Add("WoodenClub");
                    // Inventory.Add();

                    // Selecting Weapon
                    _MonsterWeapon = _random.Next(0, Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0));
                    Weapon = ((MonsterWeapon)_MonsterWeapon).ToString();

                    break;

                case "Giant":
                    //Defence.Add("ThickHide");
                    Health = 105 * Level;
                    Wallet = _random.Next(0, (Level * 3));
                    /*
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
                    */
                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("TreeTrunk"); PreferedWeapon.Add("WoodenClub");
                    // Inventory.Add();

                    // Selecting Weapon
                    _MonsterWeapon = _random.Next(0, Enum.GetValues(typeof(MonsterWeapon)).GetUpperBound(0));
                    Weapon = ((MonsterWeapon)_MonsterWeapon).ToString();

                    break;

                case "Wizard":
                    //Defence.Add("Archane");
                    Health = 75 * Level;
                    Wallet = _random.Next(0, (Level * 3));
                    /*
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
                    */
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
                    Weapon = _weaponsToChoose[_random.Next(0, _weaponsToChoose.Count)];

                    break;

                case "FallenAngel":
                    //Defence.Add("Archane");
                    Health = 95 * Level;
                    Wallet = _random.Next(0, (Level * 3));
                    /*
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
                    */
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
                    Weapon = _weaponsToChoose[_random.Next(0, _weaponsToChoose.Count)];

                    break;

                case "DarkElf":
                    //Defence.Add("ChainMail");
                    Health = 85 * Level;
                    Wallet = _random.Next(0, (Level * 3));
                    /*
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
                    */
                    // PreferedWeapon.Add();
                    PreferedWeapon.Add("Staff"); PreferedWeapon.Add("Bow"); PreferedWeapon.Add("GreatSword");
                    // Inventory.Add();
                    break;

                case "BlackDwarves":
                    //Defence.Add("Steel");
                    Health = 105 * Level;
                    Wallet = _random.Next(0, (Level * 3));
                    /*
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
                    */
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
                    Weapon = _weaponsToChoose[_random.Next(0, _weaponsToChoose.Count)];

                    break;
            }
            /*
            // Adds a predefined random selection of items for the monster;
            string[] _ekstraDefence = { "Leather Armor" };
            int _ekstra = _random.Next(1, _ekstraDefence.GetLength(0));
            for (int i = 0; i < _ekstra; i++) { Defence.Add(_ekstraDefence[i]); }
            */
        }

    }

    public class BossMonster : Character
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

    public class NPC { }

}
