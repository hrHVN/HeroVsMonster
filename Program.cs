using System;
using System.Collections.Generic;
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
            Game.StoryWindow();
        }
    }

    public static class Game
    {
        //Head
        public static string pName = "";
        public static int pXp;
        public static int pLevel;
        public static string storyline_area = "";
        public static string subHeader; // Write Monster and level up notifications
        public static string MenuName = "Hero Vs Monseters";
        public static string consoleColor = "";
        // Main window
        // 25 Wide + 15 High
        public static string[] AvatarP = { 
            "@@@@@ ##### ##### ##### @@@@@", 
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "@@@@@ ##### ##### ##### @@@@@"
        };

        public static string[] sidebarP = {
            "##### ##### ##### ", 
            " ",
            $" Hp: {Player.Health}",
            " ",
            $" Def: 5.66",
            " ",
            $" Wep: {Player.Weapon}",
            " ",
            $" Att: 25",
            " ",
            " ",
            " ",
            " ",
            " ",
            "##### ##### ##### "
        };

        public static string[] AvatarM = {
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####",
            "##### ##### ##### ##### #####"
        };

        public static string[] sidebarM = {
            "##### ##### ##### ",
            $" Hp: {Monsters.Health} ",
            " ",
            $" Def: 5.66 ",
            " ",
            $" Wep: {Monsters.Weapon} ",
            " ",
            $"  Att: 25 ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            "##### ##### ##### "
        };

        // Dialogue Window
        public static string nPcDialogue = "";  //Dialogue
        public static string[] pResponce = { };  //Options
        public static string pChoice;      //Input

        // Bools
        public static bool update = false;
        public static bool figth = false;
        public static bool GameRun = false;

        public static void StartGame() // Logic to itterate trough the game sequence
        {

        }

        public static void StoryWindow() 
        {
            HeadWindow();
            if (figth)
            {
                for (int i = 0; i < 15; i++)
                {
                    Console.WriteLine(
                        WindowDrawing(sidebarP[i], 20, false, false)
                        + " " 
                        + WindowDrawing(AvatarP[i], 30, false, false) 
                        + "  " 
                        + WindowDrawing(AvatarM[i], 30, false, false) 
                        + " " 
                        + WindowDrawing(sidebarM[i], 20, true, false)
                        );
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    Console.Write(WindowDrawing(AvatarP[i], 30, false, true) + "  " + WindowDrawing(AvatarM[i], 74, true, true));
                }
            }
            Console.WriteLine("\n ");
            DialogueWindow();
        }

        public static void MainMenue () { }

        public static void PlayerCreation() { }

        public static void HeadWindow()
        {
            int lvl = Player.Level + 1;
            int prosent = (Player.Xp / Player._nextLevel) * 100;
            string _xProgress;
            string _XpColor;

            if (GameRun)
            {
                if (prosent <= 100 && prosent > 90) { _xProgress = $"XP: #######|{prosent}%|######> ({lvl})"; _XpColor = "green"; }
                else if (prosent <= 90 && prosent > 80) { _xProgress = $"XP: #######|{prosent}%|#####>  ({lvl})"; _XpColor = "green"; }
                else if (prosent <= 80 && prosent > 70) { _xProgress = $"XP: ######|{prosent}%|#####>   ({lvl})"; _XpColor = "darkgreen"; }
                else if (prosent <= 70 && prosent > 60) { _xProgress = $"XP: ######|{prosent}%|####>    ({lvl})"; _XpColor = "yellow"; }
                else if (prosent <= 60 && prosent > 50) { _xProgress = $"XP: #####|{prosent}%|####>     ({lvl})"; _XpColor = "yellow"; }
                else if (prosent <= 50 && prosent > 40) { _xProgress = $"XP: ####|{prosent}%|###>       ({lvl})"; _XpColor = "darkyellow"; }
                else if (prosent <= 40 && prosent > 30) { _xProgress = $"XP: ####|{prosent}%|##>        ({lvl})"; _XpColor = "darkyellow"; }
                else if (prosent <= 30 && prosent > 20) { _xProgress = $"XP: ###|{prosent}%|##>         ({lvl})"; _XpColor = "red"; }
                else if (prosent <= 20 && prosent > 10) { _xProgress = $"XP: ##|{prosent}%|#>          ({lvl})"; _XpColor = "red"; }
                else if (prosent <= 10 && prosent > 5) { _xProgress = $"XP: #|{prosent}%|#>           ({lvl})"; _XpColor = "darkred"; }
                else { _xProgress = $"XP: |{prosent}%|>             ({lvl})"; _XpColor = "darkred"; }
            }
            else { _xProgress = MenuName; _XpColor = "magenta"; }
            PrintTheBorder("#", consoleColor);
            Console.WriteLine("  ");

            Utility.Write(HeaderTextAlign(pName));
            Utility.Write(HeaderTextAlign(_xProgress), _XpColor);
            Utility.Write(HeaderTextAlign(storyline_area));

            Console.WriteLine("  ");
            PrintTheBorder("#", consoleColor);
            Console.WriteLine("  ");

            if (subHeader == "")
            {
                Utility.WriteLine(Utility.Center(subHeader));
                Console.WriteLine("\t");
                PrintTheBorder("_");
            }
        }
        
        public static void DialogueWindow() 
        {
            PrintTheBorder("#", consoleColor);

            if (nPcDialogue.Length < 1) Utility.WriteLine("\n  \n");
            else Utility.WriteLine("\n" + nPcDialogue + "\n ");
            
            
            int xChoice = 1;
            if (pResponce.Length == 0)
            {
                Console.WriteLine("\n \n "); 
            }
            else
            {
                PrintTheBorder("~", consoleColor);
                
                foreach (string x in pResponce) { Console.WriteLine($"<{xChoice}> " + x); xChoice++; }

                PrintTheBorder("#", consoleColor);
                Console.WriteLine(" ");
                pChoice = Utility.Input("Choice");
            }

            PrintTheBorder("#", consoleColor);
        }

        static string HeaderTextAlign(string _string)
        {
            int totalLength = 88;
            int error = _string.Length;
            int difference = (totalLength / 3) - error;
            int padding = difference / 2;
            string _space = "";

            for (int i = 0; i < padding; i++) _space += " ";

            return _string = _space + _string + _space;
        }

        static string WindowDrawing(string _string, int _size, bool _reversed, bool _centered)
        {
            int error = _string.Length;
            int _spaces = _size - error;
            string _centerA = "";
            string _centerB = "";

            if (_reversed || _centered)
            {
                if (_centered)
                {
                    for (int i = 0; i < (_spaces / 2); i++)
                    {
                        _centerA += " ";
                        _centerB += " ";
                    }
                }
                else for (int i = 0; i < _spaces; i++) _centerA += " ";
            }
            else { for (int i = 0; i < _spaces; i++) _centerB += " "; }

            string _stringB = _centerA + _string + _centerB;
            _string = _string + _centerB;
            return _reversed ? _stringB : _string;
        }

        static void PrintTheBorder(string _border)
        {
            for (int i = 0; i <= 104; i++) Console.Write(_border);
        }

        static void PrintTheBorder(string _border, string _color)
        {
            _border = _border.Replace("\n", $"\n");

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

            for (int i = 0; i <= 104; i++) Console.Write(_border);
            Console.ResetColor();
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
            catch (Exception e) { Utility.WriteLine("Exception: " + e.Message); }

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

        public static void SplashScreen() // Splash screen to display Game Title and first impression
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
            Utility.Pause();
        }
    }
}
