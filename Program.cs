using System;
using System.Collections.Generic;
using System.Globalization;

/*
 *  To DO;
 *  Add Monster class switchcases
 *  Add Monster weapon switchcase
 *  Add StoryLine
 *  Add Action window
 *  Add Navigation
 *  Add save/load function
 */

namespace HeroVsMonster
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.StartGame();
            Console.ReadKey();
        }
    }

    public static class Game
    {
        public static string HeroName;
        public static string HeroClass;
        public static string HeroWeapon;
        public static string AttackType;

        public static int HeroLevel;
        public static int HeroXp;
        public static int HeroHealth;

        public static List<string> PreferedWeapon = new List<string>();
        public static List<string> Defence = new List<string>();
        public static List<string> Inventory = new List<string>();
        public static Double Wallet;

        public static void StartGame() // Logic to itterate trough the game sequence
        {
            SplashScreen();

            Utility.Write("Let's Begin!");
            Utility.Pause();
            
            PlayerCreation();
            Utility.Pause();

            GamePlay.Loop();
        }

        static void SplashScreen() // Splash screen to display Game Title and first impression
        {
            string Title = @"

         ██░ ██ ▓█████  ██▀███   ▒█████    ██████     ██▒   █▓  ██████            
        ▓██░ ██▒▓█   ▀ ▓██ ▒ ██▒▒██▒  ██▒▒██    ▒    ▓██░   █▒▒██    ▒            
        ▒██▀▀██░▒███   ▓██ ░▄█ ▒▒██░  ██▒░ ▓██▄       ▓██  █▒░░ ▓██▄              
        ░▓█ ░██ ▒▓█  ▄ ▒██▀▀█▄  ▒██   ██░  ▒   ██▒     ▒██ █░░  ▒   ██▒           
        ░▓█▒░██▓░▒████▒░██▓ ▒██▒░ ████▓▒░▒██████▒▒      ▒▀█░  ▒██████▒▒           
         ▒ ░░▒░▒░░ ▒░ ░░ ▒▓ ░▒▓░░ ▒░▒░▒░ ▒ ▒▓▒ ▒ ░      ░ ▐░  ▒ ▒▓▒ ▒ ░           
         ▒ ░▒░ ░ ░ ░  ░  ░▒ ░ ▒░  ░ ▒ ▒░ ░ ░▒  ░ ░      ░ ░░  ░ ░▒  ░ ░           
         ░  ░░ ░   ░     ░░   ░ ░ ░ ░ ▒  ░  ░  ░          ░░  ░  ░  ░             
         ░  ░  ░   ░  ░   ░         ░ ░        ░           ░        ░             
                                                          ░                       
         ███▄ ▄███▓ ▒█████   ███▄    █   ██████ ▄▄▄█████▓▓█████  ██▀███    ██████ 
        ▓██▒▀█▀ ██▒▒██▒  ██▒ ██ ▀█   █ ▒██    ▒ ▓  ██▒ ▓▒▓█   ▀ ▓██ ▒ ██▒▒██    ▒ 
        ▓██    ▓██░▒██░  ██▒▓██  ▀█ ██▒░ ▓██▄   ▒ ▓██░ ▒░▒███   ▓██ ░▄█ ▒░ ▓██▄   
        ▒██    ▒██ ▒██   ██░▓██▒  ▐▌██▒  ▒   ██▒░ ▓██▓ ░ ▒▓█  ▄ ▒██▀▀█▄    ▒   ██▒
        ▒██▒   ░██▒░ ████▓▒░▒██░   ▓██░▒██████▒▒  ▒██▒ ░ ░▒████▒░██▓ ▒██▒▒██████▒▒
        ░ ▒░   ░  ░░ ▒░▒░▒░ ░ ▒░   ▒ ▒ ▒ ▒▓▒ ▒ ░  ▒ ░░   ░░ ▒░ ░░ ▒▓ ░▒▓░▒ ▒▓▒ ▒ ░
        ░  ░      ░  ░ ▒ ▒░ ░ ░░   ░ ▒░░ ░▒  ░ ░    ░     ░ ░  ░  ░▒ ░ ▒░░ ░▒  ░ ░
        ░      ░   ░ ░ ░ ▒     ░   ░ ░ ░  ░  ░    ░         ░     ░░   ░ ░  ░  ░  
               ░       ░ ░           ░       ░              ░  ░   ░           ░  
                                                                                                                                    
          ";
            Console.WriteLine(Title);
            Utility.Title("Hero Vs Monsters");
            Utility.Pause();
        }

        static void PlayerCreation() // Player creation, name + class = Starting specs
        {
            // Starting Level
            HeroLevel = 1;
            HeroXp = 0;
            Wallet = 50.00;

            // Name Creation
            HeroName = Utility.Input("Enter your Name Hero");
            Utility.Write($"Welcome {HeroName}! \n We have been awaiting your arrival for some time now..", "red");
            Utility.Pause();

            // Class selection
            bool x = false;
            int z = 0;
            int enumMax = Enum.GetValues(typeof(PlayerClass)).GetUpperBound(0); // Gets the max enum value

            while (!x) // Itterates throug PlayerClass Enum
            {
                var Current = (PlayerClass)z;
                Utility.Write($"\n{HeroName} are you a.. " + Current, "red");
                string input = Utility.Input("Select Yes or No (Y/N)");
                Console.Clear();

                if ((input != "y") && (z < enumMax)) { z++; }
                else if ((input != "y") && (z == enumMax)) { z = 0; }
                else
                {
                    HeroClass = Current.ToString();
                    Utility.Write($"Great we really needed a {HeroClass} at this moment!", "red");
                    x = true;
                }             
            }
            //Adding stats
            switch (HeroClass)
            {
                case "Warrior":
                    HeroHealth = 120;
                    PreferedWeapon.Add("GreatSword");PreferedWeapon.Add("Sword"); PreferedWeapon.Add("Mace");
                    Defence.Add("Steel");
                    AttackType = "Sword";
                    break;
                case "Mage":
                    HeroHealth = 75;
                    PreferedWeapon.Add("Wand"); PreferedWeapon.Add("Staff");
                    Defence.Add("Magic");
                    AttackType = "Archane";
                    break;
                case "Archer":
                    HeroHealth = 80;
                    PreferedWeapon.Add("Bow"); PreferedWeapon.Add("CrossBow");
                    Defence.Add("Leather"); 
                    AttackType = "Arrow";
                    break;
                case "Munk":
                    HeroHealth = 80;
                    PreferedWeapon.Add("Staff"); PreferedWeapon.Add("Umbrella");
                    Defence.Add("Faith");
                    AttackType = "Holy";
                    break;
                default:    //Farmer
                    HeroHealth = 150;
                    PreferedWeapon.Add("Pitchfork"); PreferedWeapon.Add("Shovel");
                    Defence.Add("None");
                    AttackType = "Fist";
                    break;
            }
        }

        static void Choice(string A, string B)
        {
            string input = Utility.Input($"{HeroName}, Which path will you Choose? {A} or {B}");
            
            if (input == "a") { Utility.Write($"Mighty {HeroClass} You have chosen path {A}!"); }
            else { Utility.Write($"Mighty {HeroClass} You have chosen path {B}!"); }
        }
    }

    public static class GamePlay
    {
        public static int MonstersBeaten;

        public static void Loop()
        {
            Fight();
        }

        public static void LevelUp()
        {
            int _xp = Game.HeroXp;
            int _level = Game.HeroLevel;

            int _nextLevel = 100;

            if (_xp >= _nextLevel) 
            { 
                Game.HeroLevel++;
                _nextLevel += (_nextLevel / 3);

                Utility.Write($"Congratulations, you have leveled up!! {Game.HeroLevel}");
            }
            else { Utility.Write($"Next Level in {_nextLevel - _xp}!", "cyan"); }
        }

        // Function that calculates (Weapon skill + Weapon type - Defence) x  Level, reurns Multiplikasjon(int)
        public static int AttackMultiplier(string _attacker)
        {
            int _level = 1;

            string _weapon;
            double _weaponValue;

            double _attackTypeMultiplier = 0.2;
            string _attackType;

            List<string> _defenceType = new List<string>();
            List<string> _preferdWeapon = new List<string>();

            // The monster attacks
            if (_attacker != Game.HeroName) 
            {
                _attackType = Monsters.AttackType;
                _level = Monsters.MonsterLevel;
                _weapon = Monsters.MonsterWeapon;
                foreach (string x in Game.Defence) { _defenceType.Add(x); }
                foreach (string pw in Monsters.PreferedWeapon) { _preferdWeapon.Add(pw); }
            }
            // The hero attacks
            else
            {
                _attackType = Game.AttackType;
                _level = Game.HeroLevel;
                _weapon = Game.HeroWeapon;
                foreach (string x in Monsters.Defence) { _defenceType.Add(x); }
                foreach (string pw in Game.PreferedWeapon) { _preferdWeapon.Add(pw); }
            }
            
            // AttackType vs DefenceType value selector
            foreach (string a in _defenceType)
            {
                if ((_attackType == "Blunt") || (_attackType == "BluntFist") || (_attackType == "RangedBlunt"))
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier =+ 0.88;
                            break;
                        case "Leather":
                            _attackTypeMultiplier =+ 0.45;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier =+ 0.88;
                            break;
                        case "Steel":
                            _attackTypeMultiplier =- 1  ;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier = + 0.56;
                            break;
                        case "Copper":
                            _attackTypeMultiplier = - 0.33;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier = - 0.52;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier = + 0.88;
                            break;
                        default: // None
                            _attackTypeMultiplier = + 2;
                            break;
                    }
                }
                else if ((Game.AttackType == "RangedSharp") || (Game.AttackType == "Sharp"))
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier =+ 1.2;
                            break;
                        case "Leather":
                            _attackTypeMultiplier =+ 0.75;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier =+ 1.2;
                            break;
                        case "Steel":
                            _attackTypeMultiplier =+ 0.25;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier =+ 0.75;
                            break;
                        case "Copper":
                            _attackTypeMultiplier =+ 0.45;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier =+ 0.35;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier =+ 1.2;
                            break;
                        default: // None
                            _attackTypeMultiplier =+ 1.5;
                            break;
                    }
                }
                else if ((Game.AttackType == "MagicArchane"))
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier =+ 0.38;
                            break;
                        case "Leather":
                            _attackTypeMultiplier =+ 1.45;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier =- 1.0;
                            break;
                        case "Steel":
                            _attackTypeMultiplier =+ 1.7;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier =+ 1;
                            break;
                        case "Copper":
                            _attackTypeMultiplier =+ 1.5;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier =+ 1.52;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier =+ 0.28;
                            break;
                        default: // None
                            _attackTypeMultiplier = +2;
                            break;
                    }
                }
                else if ((Game.AttackType == "MagicHoly")) 
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier = +0.38;
                            break;
                        case "Leather":
                            _attackTypeMultiplier = +1.45;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier = +0.28;
                            break;
                        case "Steel":
                            _attackTypeMultiplier = +1.7;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier = +1;
                            break;
                        case "Copper":
                            _attackTypeMultiplier = +1.5;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier = +1.52;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier = -1.0;
                            break;
                        default: // None
                            _attackTypeMultiplier = +2;
                            break;
                    }
                }
                else //Ptichfork
                {
                    _attackTypeMultiplier = +2;
                }
            }

            // Adding weapon hit bonus
            switch (_weapon)
            {
                    // Farmer tools
                case "Umbrella": _weaponValue = 0.2; break;
                case "Pebbles": _weaponValue = 0.3; break;
                case "PitchFork": _weaponValue = 1.5; break;
                case "Shovel": _weaponValue = 1; break;
                    // Magic Tools
                case "Staff": _weaponValue = 0.4; break;
                case "Wand": _weaponValue = 0.42; break;
                case "GobblinWand": _weaponValue = 0.34; break;
                    //Warrior Tools
                case "Sword": _weaponValue = 0.75; break;
                case "GreatSword": _weaponValue = 1; break;
                case "Mace": _weaponValue = 0.75; break;
                    //Archer Tools
                case "Bow": _weaponValue = 0.67; break;
                case "CrossBow": _weaponValue = 0.85; break;
                    //Monster Tools
                case "WoodenClub": _weaponValue = 0.51; break;
                case "MakeShiftSword": _weaponValue = 0.75; break;
                case "TreeTrunk": _weaponValue = 2; break;
                    // Fists
                default:
                    _weaponValue = 0.2;
                    break;
            }

            // Loop to add bonus if Preafferd weapon is chosen.
            foreach (string pw in _preferdWeapon) { _weaponValue = (pw == _weapon) ? _weaponValue * 3 : + 0; }
            
            // Adding and multiplying everything
            var _result = (_attackTypeMultiplier + _weaponValue) * (double)_level;

            return (int)_result;
        }

        public static void Fight()
        {
            Monsters.Spawn(); //Generates the Monster to fight
            int playerAttack;
            int monsterAttack;

            Utility.Write($"A {Monsters.MonsterClass} appeard at the edge of town..");

            // Fight sceen, performes dice roll, delivers damage, recives damage (loop until one survivor)
            do
            {
                // Execute the attack
                Utility.Input("Roll the dice");
                playerAttack = Utility.DiceRoll() * AttackMultiplier(Game.HeroName);

                Utility.Write($"You attack the {Monsters.MonsterClass} with your {Game.HeroWeapon}, dealing {Game.AttackType}...", "cyan");
                Utility.Write($"The  {Monsters.MonsterClass} took {playerAttack} damage!", "red");

                //Update the monster HP
                Monsters.MonsterHealth -= playerAttack;

                if (Monsters.MonsterHealth <= 0) continue;
                // Monster attack
                monsterAttack = Utility.DiceRoll() * AttackMultiplier(Monsters.MonsterClass);

                Utility.Write($"The {Monsters.MonsterClass} attacks you with {Monsters.MonsterWeapon}, dealing {Monsters.AttackType}...", "cyan");
                Utility.Write($"The  you reccived {monsterAttack} damage!", "red");
                
                // Update the HeroHP
                Game.HeroHealth -= monsterAttack;
            }
            while (Game.HeroHealth > 0 && Monsters.MonsterHealth > 0);

            // Wictory loop
            if (Game.HeroHealth > Monsters.MonsterHealth) 
            { 
                Utility.Write($".. The {Monsters.MonsterClass} crumbles to the ground reciving it's final blow from your" +
                    $" {Game.HeroWeapon} taking {playerAttack} damage!");
                //Give Hero the xp and stats.
                Game.HeroXp += (Monsters.MonsterLevel % 3);
                MonstersBeaten++;
            }
            //Game Over
            else 
            { 
                Utility.Heading("Game Over!");
                Utility.Write($"You where beaten by a {Monsters.MonsterClass}...");
                Utility.Write($"Final Level: {Game.HeroLevel} \nMonsters beaten: {MonstersBeaten}");
            }
            
            LevelUp();
            Utility.Pause(); // Pause before continue the story
        }
    }

    public class Item
    {
        public string Name = "Small Stone";
        public string Description = "Unimpressive object.";

        string[] Items = { "Shoe", "Can", "Pair of Chopsticks" };
        string[] Descriptions = { "Looks like some one tryed to eat theese", "Empty can of beans", "Pink plastic chopsticks"};

        public Item()
        {
            Random RandNumber = new Random();
            int number = RandNumber.Next(Items.Length);

            Name = Items[number];
            Description = Descriptions[number];
            Console.WriteLine($"You found a {Name} ({Description})");
        }
    }

    class Utility
    {
        static string margin = "\t";
        static string indent = "\t\t";

        public static string Input() // Recives the user input
        {
            Console.Write($"{margin}: ");
            string input = Console.ReadLine();
            Console.ResetColor();
            return input.ToLower();
        }

        public static string Input(string _string)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{margin}{_string}: ");
            string input = Console.ReadLine();
            Console.ResetColor();
            return input.ToLower();
        }

        public static void Write(string _string)
        {
            _string = _string.Replace("\n", $"\n {margin}");
            Console.WriteLine(margin + _string);
        }

        public static void Write(string _string, string _color)
        {
            _string = _string.Replace("\n", $"\n {margin}");

            switch (_color)
            {
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "darkyellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "darkred":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "darkgreen":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "darkgray":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "darkcyan":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "darkblue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Write(_string);
            Console.ResetColor();
        }

        public static void Write(string _string, string _color, string _background)
        {
            _string = _string.Replace("\n", $"\n {margin}");

            switch (_color)
            {
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "darkyellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "darkred":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "darkgreen":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "darkgray":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "darkcyan":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "darkblue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            switch (_background)
            {
                case "blue":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case "yellow":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case "red":
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "magenta":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case "green":
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case "gray":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case "darkyellow":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case "darkred":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case "darkgreen":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case "darkgray":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case "darkcyan":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case "darkblue":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case "cyan":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
            }

            Write(_string);
            Console.ResetColor();
        }

        public static void Heading(string _string)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(margin + indent + _string);
            Console.ResetColor();
        }

        public static void Pause()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{margin}Press enter to continue...");
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
        }

        public static void Title(string _title, string _subheading)
        {
            Console.Title = _title;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Center("Welcome to"));
            Console.WriteLine("\n" + Center($"*´·._.· {_title} ·._.·`* \n"));
            Console.WriteLine(Center(_subheading));
            Console.ResetColor();
        }

        public static void Title(string _title)
        {
            Console.Title = _title;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Center("Welcome to"));
            Console.WriteLine("\n" + Center($" *´·._.· {_title} ·._.·`* \n"));
            Console.ResetColor();
        }

        public static string Center(string _string)
        {
            int screenWidth = Console.WindowWidth;
            int stringWidth = _string.Length;
            int spaces = (screenWidth / 2) + (stringWidth / 2);
            return _string.PadLeft(spaces);
        }

        public static string TitleCase(string _string)
        {
            TextInfo TitleCase = new CultureInfo("en-US", false).TextInfo;
            _string = TitleCase.ToTitleCase(_string);
            return _string;
        }

        //modification of method at https://msdn.microsoft.com/en-us/library/d9hy2xwa(v=vs.110).
        public static bool Search(string[] _array, string _string)
        {
            bool result = false;
            int i = 0;

            foreach (string s in _array) { _array[i] = s.ToLower(); i++; }

            if (Array.Find(_array, element => element == _string) == _string) { result = true; }
            else { result = false; }
            return result;
        }

        // Prints all values in an array
        public static void AllValues(string[] _array)
        {
            for (int i = _array.GetLowerBound(0); i <= _array.GetUpperBound(0); i++)
            {
                Console.WriteLine(margin + _array[i]);
            }
            Console.WriteLine();
        }

        public static int DiceRoll()
        {
            Random dice = new Random();
            int roll = dice.Next(1, 6);
            return roll;
        }
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

    public enum PlayerClass
    {
        Farmer, Warrior, Mage, Archer, Munk
    }

    public enum PlayerWeapons
    {
        PitchFork, Sword, GreatSword, Mace, Staff, Wand, Shovel, Bow, CrossBow, Umbrella, Fists
    }

    public enum DefenceType
    {
        None, Faith, Leather, ArchaneRobe, Steel, ThickHide, Copper, ChainMail, HolyRobe
    }

    public enum AttackType
    {
        BluntFist, Blunt, RangedSharp, RangedBlunt, MagicArchane, MagicHoly, Sharp
    }
}
