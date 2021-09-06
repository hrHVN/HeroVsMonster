using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

/*
 *  To DO
 *  Add StoryLine
 *      Add Monster dialogue
 *  Add Action window
 *  Add Navigation
 *  
 */

namespace HeroVsMonster
{
    class Program
    {
        static void Main(string[] args)
        {
            ActionWindow.SplashScreen();
            ActionWindow.WindowAdjust();
            ActionWindow.MainWindow();
            Console.ReadKey();
        }
    }

    public static class Game
    {
        public static int Choice;
        public static string Input;
        public static bool Run;

        public static void StartGame() // Logic to itterate trough the game sequence
        {
            ConsoleStartMenu();
        }

        public static void LoadGame(string _save) 
        {
            string path = @"C:\Users\Andreas\source\repos\HeroVsMonster\saves\";
            StreamReader sr = new StreamReader(_save + ".txt");

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

            Utility.WriteLine(Utility.Center("Game Loaded!"), "red");
            Utility.Pause();
            GamePlay.Loop();
        }

        public static void SaveGame() 
        {
            try
            {
            // C: \Users\Andreas\source\repos\HeroVsMonster\saves\

                string _path = @"C:\Users\Andreas\source\repos\HeroVsMonster\saves\";
                StreamWriter sw = new StreamWriter(_path + $"{Player.Name}.txt", false);
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
            catch(Exception e) { Utility.WriteLine("Exception: " + e.Message); }

            Utility.WriteLine(Utility.Center("Game Saved!"), "cyan");
            Utility.Pause();
        }

        public static void ExitGame() 
        {
            string credits =
                @"


               Thank you for playing HeroVsMonster.

                    This game vas developed 
                               by 
                        Andreas Nesheim.

                              2021


                Art work gathered frow the web:
                   https://www.asciiart.eu/


                Part of the game was inspiered by 
                    various online tutorials:
    http://programmingisfun.com/learn/c-sharp-adventure-game/

";
            Utility.WriteLine(credits, "yellow");
            Utility.Pause();
            Environment.Exit(1); 
        }

        public static void Menu(string _MenuSwitch)
        {
            var _menuList = MenueOptions._menueList(_MenuSwitch);
            Console.Clear();

            int _int = 1; // Menue Choice
            foreach (string M in _menuList)
            {
                Utility.WriteLine(Utility.Center($"\n{M} < {_int} >"));
                _int++;
            }

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

                            break;

                        default:
                            //if a number other than 1-5 is entered, request
                            //player enter a number in that range
                            //wait for them to press enter, then call
                            //the menu again
                            Console.WriteLine("Please enter a number 1-5.");
                            Console.WriteLine("Press enter to continue...");
                            Console.ReadLine();
                            Menu(_MenuSwitch);
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid choice");
                Console.WriteLine("\nPress enter to continue..");
                Console.ReadLine();
                Menu(_MenuSwitch);
            }
        }

        public static void ConsoleStartMenu()
        {
            string[] _menuList = {"New Game", "Load Game", "Exit to Desktop."};
            Console.Clear();
            Utility.Title("Hero Vs Monster" , "Survival of the luckiest...");

            Utility.WriteLine(Utility.Center("\nMain Menue"));
            int _int = 1; // Menue Choice
            foreach (string M in _menuList) 
            { 
                Utility.WriteLine(Utility.Center($"\n{M} < {_int} >")); 
                _int++; 
            }

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
                        case 1: // New Game
                            Console.Clear();
                            Player.PlayerCreation();
                            break;

                        case 2: // Load Game
                            Console.Clear();
                            Utility.Title("Hero Vs Monster", "Survival of the luckiest...");
                            Console.Write("\nLoad Game");
                            try
                            {
                                string[] _saves = Directory.GetFiles(@"C:\Users\Andreas\source\repos\HeroVsMonster\saves\", "*.txt");
                                List<int> _skipDeadpeople = new List<int> { };
                                int _intSaves = 1; // Menue Choice

                                foreach (string S in _saves) 
                                {
                                    StreamReader sr = new StreamReader(S);

                                    string _temporaryName = sr.ReadLine();
                                    int _temporaryHealth = Convert.ToInt16(sr.ReadLine());

                                    if (_temporaryHealth > 0)
                                    {
                                        Utility.WriteLine(Utility.Center($"\n{S} < *Dead* >"));
                                        _skipDeadpeople.Add(_intSaves);
                                        _intSaves++;
                                    }
                                    else
                                    {
                                        Utility.WriteLine(Utility.Center($"\n{S} < {_intSaves} >"));
                                        _intSaves++;
                                    }
                                }

                                string _inputSave = Console.ReadLine();
                                if (int.TryParse(_inputSave, out Choice))
                                {
                                    if ((Choice > _menuList.Length) || _skipDeadpeople.Contains(Choice))
                                    {
                                        Utility.WriteLine($"Please enter a number between 1-{_menuList.Length}. That arent Dead People!");
                                        break;
                                    }
                                    else
                                    {
                                        string S = _saves[_intSaves - 1];
                                        StreamReader sr = new StreamReader(S);
                                        string _temporaryName = sr.ReadLine();

                                        LoadGame(_temporaryName);
                                    }
                                }
                            }
                            catch 
                            {
                                Utility.WriteLine("You have no prior Savegames..");
                                Console.ReadKey();
                                ConsoleStartMenu();
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
                            ConsoleStartMenu();
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid choice");
                Console.WriteLine("\nPress enter to continue..");
                Console.ReadLine();
                ConsoleStartMenu();
            }
        }
    }

    public static class GamePlay
    {
        public static int MonstersBeaten = 0;
        public static int _nextLevel = 100;

        public static void Loop()
        {
            Fight();
        }

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

            Utility.WriteLine($"A {Monsters.Class} appeard at the edge of town..");
            

            // Fight sceen, performes dice roll, delivers damage, recives damage (loop until one survivor)
            do
            {
                // Execute the attack
                Utility.Input("Roll the dice");
                playerAttack = Utility.DiceRoll() * AttackMultiplier(Player.Name);
                damageGiven += playerAttack;

                Utility.WriteLine($"You attack the {Monsters.Class} with your {Player.Weapon}...", "cyan");
                Utility.WriteLine($"The  {Monsters.Class} took {playerAttack} damage!", "red");

                //Update the monster HP
                Monsters.Health -= playerAttack;
                Console.ReadKey();

                if (Monsters.Health <= 0) continue;
                // Monster attack
                monsterAttack = Utility.DiceRoll() * AttackMultiplier(Monsters.Class);
                damageTaken += monsterAttack;

                Utility.WriteLine($"The {Monsters.Class} attacks you with {Monsters.Weapon}...", "cyan");
                Utility.WriteLine($"The  you reccived {monsterAttack} damage!", "red");

                // Update the HeroHP
                Player.Health -= monsterAttack;
                Console.ReadKey();

            }
            while (Player.Health > 0 && Monsters.Health > 0);

            // Wictory loop
            if (Player.Health > Monsters.Health)
            {
                Utility.WriteLine($".. The {Monsters.Class} crumbles to the ground reciving it's final blow from your" +
                    $" {Player.Weapon} taking {playerAttack} damage!");
                //Give Hero the xp and stats.
                Player.Xp += (Monsters.Level < 10) ? (Monsters.Level * 3) : (damageGiven / Monsters.Level);
                MonstersBeaten++;
                Utility.WriteLine($"You dealt {damageGiven} damage to the {Monsters.Class} with your {Player.Weapon}.");
                LevelUp();
                Game.SaveGame();
            }
            //Game Over
            else
            {
                Utility.Heading("Game Over!");
                Utility.WriteLine($"You where beaten by a {Monsters.Class}, it has {Monsters.Health} Hp left...");
                Utility.WriteLine($"\nYou reached Level: {Player.Level} \nYou hav beaten: {MonstersBeaten} Monsters! \nIn the end you delt  " +
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

            if (input == "a") { Utility.WriteLine($"Mighty {Player.Class} You have chosen path {A}!"); }
            else { Utility.WriteLine($"Mighty {Player.Class} You have chosen path {B}!"); }
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

        public static string Input(bool ToUpper = false) // Recives the user input, if true "input as is"
        {
            Console.Write($"{margin}: ");
            string input = Console.ReadLine();
            Console.ResetColor();
            return (!ToUpper) ? input.ToLower() : input;
        }

        public static string Input(string _string, bool ToUpper = false)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{margin}{_string}: ");
            string input = Console.ReadLine();
            Console.ResetColor();
            return (!ToUpper) ? input.ToLower() : input;
        }

        public static void WriteLine(string _string)
        {
            _string = _string.Replace("\n", $"\n {margin}");
            Console.WriteLine(margin + _string);
        }

        public static void WriteLine(string _string, string _color)
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

            WriteLine(_string);
            Console.ResetColor();
        }

        public static void WriteLine(string _string, string _color, string _background)
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

            WriteLine(_string);
            Console.ResetColor();
        }

        public static void Write(string _string)
        {
            _string = _string.Replace("\n", $"\n {margin}");
            Console.Write(margin + _string);
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
