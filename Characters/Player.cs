using System;
using System.Collections.Generic;

/*
 *  To DO
 *  Clean up, improve selection
 *  make game balancing easier
 */

namespace HeroVsMonster
{
    public static class Player : ICharacter
    {
        public static string Weapon;

        public static int Level;
        public static int Xp;
        public static int Health;
        public static int Wallet;

        public static double DefenceValue;

        public static List<string> PreferedWeapon = new List<string>();
        public static List<string> Defence = new List<string>();
        public static List<string> Inventory = new List<string>();

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
                    Player.Defence.Add("Steel");
                    Player.PreferedWeapon.Add("GreatSword");
                    Player.PreferedWeapon.Add("Sword");
                    Player.PreferedWeapon.Add("Mace");
                    break;

                case "Mage":
                    Player.Defence.Add("Archane");
                    Player.PreferedWeapon.Add("Wand");
                    Player.PreferedWeapon.Add("Staff");
                    break;

                case "Archer":
                    Player.Defence.Add("Leather");
                    Player.PreferedWeapon.Add("Bow");
                    Player.PreferedWeapon.Add("CrossBow");
                    break;

                case "Munk":
                    Player.Defence.Add("Faith");
                    Player.PreferedWeapon.Add("Staff");
                    Player.PreferedWeapon.Add("Umbrella");
                    Player.PreferedWeapon.Add("Fists");
                    break;

                default:    //Farmer
                    Player.Defence.Add(((DefenceType)_random.Next(0, Enum.GetValues(typeof(DefenceType))
                        .GetUpperBound(0))).ToString());
                    Player.Health += 100;
                    Player.PreferedWeapon.Add("Pitchfork");
                    Player.PreferedWeapon.Add("Shovel");
                    Player.PreferedWeapon.Add("Fists");
                    break;
            }

            Game.SaveGame();
            GamePlay.Loop();
        }
    }
}
