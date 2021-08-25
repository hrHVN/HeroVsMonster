using System;
using System.Collections.Generic;
using System.Globalization;

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
        public static int HeroLevel;
        public static int HeroHealth;

        public static List<string> PreferedWeapon = new List<string>();
        public static List<string> Defence = new List<string>();
        public static string AttackType;
        public static List<string> Inventory = new List<string>();

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
            // Name Creation
            /*
            Console.Write("\n\t Enter your name Hero: ");
            HeroName = Console.ReadLine();
            Console.WriteLine($"\nWelcome {HeroName}!");
            Console.WriteLine("\n\n\t Press any Key to continue!");
            Console.ReadKey();
            Console.Clear();
            */
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
                    HeroHealth = 10;
                    PreferedWeapon.Add("GreatSword");PreferedWeapon.Add("Sword"); PreferedWeapon.Add("Mace");
                    Defence.Add("Steel");
                    AttackType = "Sword";
                    break;
                case "Mage":
                    HeroHealth = 10;
                    PreferedWeapon.Add("Wand"); PreferedWeapon.Add("Staff");
                    Defence.Add("Magic");
                    AttackType = "Archane";
                    break;
                case "Archer":
                    HeroHealth = 10;
                    PreferedWeapon.Add("Bow"); PreferedWeapon.Add("CrossBow");
                    Defence.Add("Leather"); 
                    AttackType = "Arrow";
                    break;
                case "Munk":
                    HeroHealth = 10;
                    PreferedWeapon.Add("Staff"); PreferedWeapon.Add("Umbrella");
                    Defence.Add("Faith");
                    AttackType = "Holy";
                    break;
                default:    //Farmer
                    HeroHealth = 10;
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
        //Hero stats
        static int HeroHP = Game.HeroHealth;
        static string HeroWep = Game.HeroWeapon;

        // Monster stats
        static int MonsterHealth;
        static string MonsterWeapon;

        public static void Loop()
        {

        }

        public static void Fight()
        {
            do
            {

            }
            while (HeroHealth < 0 || MonsterHealth < 0);
        }

        public static void MonsterSpawwn()
        {

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

    class Dice
    {
        int roll;
        Random dice = new Random();

        public int diceRoll()
        {
            roll = dice.Next(1, 6);
            return roll;
        }
    }

    class Character
    {
        int attackStrength;
        Dice multiplier = new Dice();

        public int Attack(string attacker, string opponent)
        {
            Console.WriteLine($"{attacker} moves in to attack {opponent} ...");
            int attack = multiplier.diceRoll() * attackStrength;
            return attack;
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
        PitchFork, Sword, GreatSword, Mace, Staff, Wand, Shovel, Bow, CrossBow, Umbrella, Empty
    }

    public enum DefenceType
    {
        None, Faith, Leather, Magic, Steel
    }

    public enum AttackType
    {
        Fist, Holy, Arrow, Archane, Sword
    }
}
