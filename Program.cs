using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

/*
 *  To DO;
 *  Add Monster class switchcases
 *  Add Monster weapon switchcase
 *  Add StoryLine
 *      Add Monster dialogue
 *  Add Action window
 *  Add Navigation
 *  Add save/load function
 *  
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

    public static class Player
    {
        public static string Name;
        public static string Class;
        public static string Weapon;

        public static int Level;
        public static int Xp;
        public static int Health;
        public static int Wallet;

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

            Utility.Heading("\nStarting a New game");
            // Name Creation
            Player.Name = Utility.Center(Utility.Input("\nEnter your Name Hero"));
            Utility.Write($"\nWelcome {Player.Name}! We have been awaiting your arrival for some time now..");
            Console.ReadKey();

            // Class selection
            bool x = false;
            int z = 0;
            int enumMax = Enum.GetValues(typeof(PlayerClass)).GetUpperBound(0); // Gets the max enum value

            // Itterates throug PlayerClass Enum
            while (!x)
            {
                var Current = (PlayerClass)z;
                Utility.Write($"\n{Player.Name} are you a.. " + Current);
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
                            Utility.Write($".. *Sigh*, thank the heavens that you a {Player.Class}! Came to our resque. This will give the " +
                                $"Towns people som safety");
                            x = true;
                            break;
                        case "Archer":
                            Utility.Write($"Great we really need an {Player.Class} to take down those pesky flying things! Just don't die " +
                                $"on us too fast, these monsters are Nasty");
                            x = true;
                            break;
                        case "Mage":
                            Utility.Write($"Wow! A {Player.Class}! I don't know if i should worry or be thankfull! The last {Player.Class} " +
                                $"almost wiped out our city when he sneezed... ");
                            x = true;
                            break;
                        case "Munk":
                            Utility.Write($"A {Player.Class}! Are you shure you'r up for this?? Didn't think \"Holy People\" did much figthing.. ");
                            x = true;
                            break;
                        default:
                            Utility.Write($"*Yikes!!* A {Player.Class}!! What is the king thinking sending YOU of all people, times must " +
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

    public static class Game
    {
        static int Choice;
        static string Input;
        static bool Run;

        public static void StartGame() // Logic to itterate trough the game sequence
        {
            SplashScreen();
            ConsoleStartMenu();
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

        static void LoadGame(string _save) 
        {
            StreamReader sr = new StreamReader($@"\\saves\{_save}.txt");

            Player.Name = sr.ReadLine();
            Player.Health = Convert.ToInt16(sr.ReadLine());
            Player.Class = sr.ReadLine();
            Player.Level = Convert.ToInt16(sr.ReadLine());
            Player.Xp = Convert.ToInt16(sr.ReadLine());
            Player.Weapon = sr.ReadLine();
            Player.Wallet = Convert.ToInt16(sr.ReadLine());
            
            string line = sr.ReadLine();
            while (line != null)
            {
                Player.Inventory.Add(line);
            }
            sr.Close();

            Utility.Write(Utility.Center("Game Loaded!"), "red");
            Utility.Pause();
            GamePlay.Loop();
        }

        public static void SaveGame() 
        {
            try
            {
                StreamWriter sw = new StreamWriter($@"\\saves\{Player.Name}.txt", false);
                sw.WriteLine(Player.Name);
                sw.WriteLine(Player.Health);
                sw.WriteLine(Player.Class);
                sw.WriteLine(Player.Level);
                sw.WriteLine(Player.Xp);
                sw.WriteLine(Player.Weapon);
                sw.WriteLine(Player.Wallet);
                foreach (string inv in Player.Inventory) { sw.WriteLine(inv); }
                sw.Close();
            }
            catch(Exception e) { Utility.Write("Exception: " + e.Message); }

            Utility.Write(Utility.Center("Game Saved!"), "cyan");
            Utility.Pause();
            ConsoleStartMenu();
        }

        static void ExitGame() { Environment.Exit(1); }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Menu options");

            Input = Console.ReadLine();
            if (int.TryParse(Input, out Choice))
            {
                if (Choice >= 5) { Run = false; }
                else
                {
                    switch (Choice)
                    {
                        case 1:

                            break;

                        default:
                            //if a number other than 1-5 is entered, request
                            //player enter a number in that range
                            //wait for them to press enter, then call
                            //the menu again
                            Console.WriteLine("Please enter a number 1-5.");
                            Console.WriteLine("Press enter to continue...");
                            Console.ReadLine();
                            Menu();
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid choice");
                Console.WriteLine("\nPress enter to continue..");
                Console.ReadLine();
                Menu();
            }
        }

        static void ConsoleStartMenu()
        {
            string[] _menuList = {"New Game", "Load Game", "Exit to Desktop."};
            Console.Clear();
            Utility.Title("Hero Vs Monster" , "Survival of the luckiest...");

            Console.Write("\nMain Menue");
            int _int = 1; // Menue Choice
            foreach (string M in _menuList) { Utility.Center($"\n{M} < {_int} >"); _int++; }

            Input = Console.ReadLine();
            if (int.TryParse(Input, out Choice))
            {
                if (Choice >= _menuList.Length) 
                { 
                    Run = false;
                    ExitGame();
                }
                else
                {
                    switch (Choice)
                    {
                        case 1:
                            Console.Clear();
                            Player.PlayerCreation();
                            break;
                        case 2:
                            Console.Clear();
                            string[] _saves = Directory.GetFiles(@"\\saves", "*.txt");

                            Utility.Title("Hero Vs Monster", "Survival of the luckiest...");
                            Console.Write("\nLoad Game");

                            int _intSaves = 1; // Menue Choice
                            foreach (string S in _saves) { Utility.Center($"\n{S} < {_intSaves} >"); _intSaves++; }
                           
                            string _inputSave = Console.ReadLine();
                            if (int.TryParse(_inputSave, out Choice))
                            {
                                if (Choice > _menuList.Length)
                                {
                                    Console.WriteLine($"Please enter a number 1-{_menuList.Length}.");
                                }
                                else
                                {
                                    LoadGame(_saves[Choice]);
                                }
                            }
                            break;
                         default:
                            //if a number other than 1-5 is entered, request
                            //player enter a number in that range
                            //wait for them to press enter, then call
                            //the menu again
                            Console.WriteLine($"Please enter a number 1-{_menuList.Length}.");
                            Console.WriteLine("\nPress enter to continue...");
                            Console.ReadKey();
                            Menu();
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid choice");
                Console.WriteLine("\nPress enter to continue..");
                Console.ReadLine();
                Menu();
            }
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
            int _xp = Player.Xp;
            int _level = Player.Level;

            int _nextLevel = 100;

            if (_xp >= _nextLevel)
            {
                Player.Level++;
                _nextLevel += (_nextLevel / 3);

                Utility.Write($"Congratulations, you have leveled up!! {Player.Level}");
            }
            else { Utility.Write($"Next Level in {_nextLevel - _xp}!", "cyan"); }
        }


        public static int AttackMultiplier(string _attacker)
        // Function that calculates (Weapon skill + Weapon type - Defence) x  Level, reurns Multiplikasjon(int)
        {
            int _level = 1;

            string _weapon;
            double _weaponValue;

            double _attackTypeMultiplier = 0.2;
            string _attackType;

            List<string> _defenceType = new List<string>();
            List<string> _preferdWeapon = new List<string>();

            // The monster attacks
            if (_attacker != Player.Name)
            {
                _attackType = Monsters.Class;
                _level = Monsters.Level;
                _weapon = Monsters.Weapon;
                foreach (string x in Player.Defence) { _defenceType.Add(x); }
                foreach (string pw in Monsters.PreferedWeapon) { _preferdWeapon.Add(pw); }
            }
            // The hero attacks
            else
            {
                _attackType = Player.Class;
                _level = Player.Level;
                _weapon = Player.Weapon;
                foreach (string x in Monsters.Defence) { _defenceType.Add(x); }
                foreach (string pw in Player.PreferedWeapon) { _preferdWeapon.Add(pw); }
            }

            string[] bluntWeapons = { "Mace", "Staff", "Fists", "Umbrella", "WoodenClub", "TreeTrunk", "Pebbles", "Staff", "Fists" };
            string[] magicWeapons = { "Wand", "GobblinWand", "Staff" };
            string[] holyWeapons = { "Wand", "Umbrella", "Staff" };
            string[] sharpWeapons = { "PitchFork", "Sword", "GreatSword", "Shovel", "Bow", "CrossBow", "MakeShiftSword" };

            // AttackType vs DefenceType value selector
            foreach (string a in _defenceType)
            {
                if (Array.Exists(bluntWeapons, element => element ==_attackType))
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier = +0.88;
                            break;
                        case "Leather":
                            _attackTypeMultiplier = +0.45;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier = +0.88;
                            break;
                        case "Steel":
                            _attackTypeMultiplier = -1;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier = +0.56;
                            break;
                        case "Copper":
                            _attackTypeMultiplier = -0.33;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier = -0.52;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier = +0.88;
                            break;
                        default: // None
                            _attackTypeMultiplier = +2;
                            break;
                    }
                }
                else if (Array.Exists(sharpWeapons, element => element == _attackType))
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier = +1.2;
                            break;
                        case "Leather":
                            _attackTypeMultiplier = +0.75;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier = +1.2;
                            break;
                        case "Steel":
                            _attackTypeMultiplier = +0.25;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier = +0.75;
                            break;
                        case "Copper":
                            _attackTypeMultiplier = +0.45;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier = +0.35;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier = +1.2;
                            break;
                        default: // None
                            _attackTypeMultiplier = +1.5;
                            break;
                    }
                }
                else if (Array.Exists(magicWeapons, element => element == _attackType))
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
                            _attackTypeMultiplier = -1.0;
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
                            _attackTypeMultiplier = +0.28;
                            break;
                        default: // None
                            _attackTypeMultiplier = +2;
                            break;
                    }
                }
                else if (Array.Exists(holyWeapons, element => element == _attackType))
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
            foreach (string pw in _preferdWeapon) { _weaponValue = (pw == _weapon) ? _weaponValue * 3 : +0; }

            // Adding and multiplying everything
            var _result = (_attackTypeMultiplier + _weaponValue) * (double)_level;

            return (int)_result;
        }

        public static void Fight()
        {
            Monsters.Spawn(); //Generates the Monster to fight
            int playerAttack;
            int monsterAttack;

            int damageTaken = 0;
            int damageGiven = 0;

            Utility.Write($"A {Monsters.Class} appeard at the edge of town..");
            

            // Fight sceen, performes dice roll, delivers damage, recives damage (loop until one survivor)
            do
            {
                // Execute the attack
                Utility.Input("Roll the dice");
                playerAttack = Utility.DiceRoll() * AttackMultiplier(Player.Name);
                damageGiven += playerAttack;

                Utility.Write($"You attack the {Monsters.Class} with your {Player.Weapon}...", "cyan");
                Utility.Write($"The  {Monsters.Class} took {playerAttack} damage!", "red");

                //Update the monster HP
                Monsters.Health -= playerAttack;
                Console.ReadKey();

                if (Monsters.Health <= 0) continue;
                // Monster attack
                monsterAttack = Utility.DiceRoll() * AttackMultiplier(Monsters.Class);
                damageTaken += monsterAttack;

                Utility.Write($"The {Monsters.Class} attacks you with {Monsters.Weapon}...", "cyan");
                Utility.Write($"The  you reccived {monsterAttack} damage!", "red");

                // Update the HeroHP
                Player.Health -= monsterAttack;
                Console.ReadKey();

            }
            while (Player.Health > 0 && Monsters.Health > 0);

            // Wictory loop
            if (Player.Health > Monsters.Health)
            {
                Utility.Write($".. The {Monsters.Class} crumbles to the ground reciving it's final blow from your" +
                    $" {Player.Weapon} taking {playerAttack} damage!");
                //Give Hero the xp and stats.
                Player.Xp += (Monsters.Level % 3);
                MonstersBeaten++;
                Utility.Write($"You dealt {damageGiven} damage to the {Monsters.Class} with your {Player.Weapon}.");
                LevelUp();
            }
            //Game Over
            else
            {
                Utility.Heading("Game Over!");
                Utility.Write($"You where beaten by a {Monsters.Class}, it has {Monsters.Health} Hp left...");
                Utility.Write($"\nYou reached Level: {Player.Level} \nYou hav beaten: {MonstersBeaten} Monsters! \nIn the end you delt  " +
                    $"{damageGiven} damage to {Monsters.Class}, and took {damageTaken} damage from the monster");
            }

            Utility.Pause(); // Pause before continue the story
        }

        public static void Figth2()
        {
            string[] bluntWeapons = { "Mace", "Staff", "Fists", "Umbrella", "WoodenClub", "TreeTrunk", "Pebbles", "Staff", "Fists" };
            string[] magicWeapons = { "Wand", "GobblinWand", "Staff"};
            string[] holyWeapons = { "Wand", "Umbrella", "Staff" };
            string[] sharpWeapons = { "PitchFork", "Sword", "GreatSword", "Shovel", "Bow", "CrossBow", "MakeShiftSword" };

            // defenceType_Vs_DamageType = (Blunt - Sharp - Magic - Holy)
            string[,] _defenceDamage = new string[6, 5]
            {
                {"None", "1", "1","1", "1" },
                { "Archane" , "1" , "2", "0.3", "0.7"},
                { "Faith", "1", "2", "0.7", "0.3"},
                { "Steel", "0.3", "0.3", "2", "2"},
                { "Leather", "0.7", "0.5", "1.5", "1,5"},
                { "ChainMail", "0.5", "0.3", "2", "2"}
            };

            string _class;
            string _weapon;
            List<string> _prefWeapon = new List<string>();

            string _defence;
            
            /*
            bool Parry = (DamageType == (ranged || Magic || Holy)) ? false : true;
            _parry = (1 == _random.next(0,1)) ? -0.5 : 0.0;
            */
            
            /*
            if Inventory.contains("Oil") Then explode + Firedamage
            */
            
            
            do
            {
                if ((Player.Class == "Mage") || (Player.Class == "Munk")) { }

                foreach (string d in Monsters.Defence)
                {
                    for (int di = 0; di < 5; di++)
                    {
                        if (_defenceDamage[di, 0] == d)
                        {
                            //if (Player.Weapon) { }
                        }
                    }
                }

                if (Monsters.Health <= 0) continue;

            }
            while (Player.Health > 0 && Monsters.Health > 0);

            // Wictory loop
            /*
            if (Player.Health > Monsters.Health)
            {
                Utility.Write($".. The {Monsters.Class} crumbles to the ground reciving it's final blow from your" +
                    $" {Player.Weapon} taking {playerAttack} damage!");
                //Give Hero the xp and stats.
                Player.Xp += (Monsters.Level % 3);
                MonstersBeaten++;
            }
            //Game Over
            else
            {
                Utility.Heading("Game Over!");
                Utility.Write($"You where beaten by a {Monsters.Class}...");
                Utility.Write($"Final Level: {Player.Level} \nMonsters beaten: {MonstersBeaten}");
            }*/

        }

        static void Choice(string A, string B)
        {
            string input = Utility.Input($"{Player.Name}, Which path will you Choose? {A} or {B}");

            if (input == "a") { Utility.Write($"Mighty {Player.Class} You have chosen path {A}!"); }
            else { Utility.Write($"Mighty {Player.Class} You have chosen path {B}!"); }
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
}
