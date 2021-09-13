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
        static string pName = "A";
        static int pXp;
        static int pLevel;
        static string storyline_area = "Error level";
        static string subHeader; // Write Monster and level up notifications

        // Main window
        // 25 Wide + 15 High
        static string[] AvatarP = { 
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

        static string[] sidebarP = {
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
        
        static string[] AvatarM = {
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

        static string[] sidebarM = {
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
        static string nPcDialogue;  //Dialogue
        static string[] pResponce;  //Options
        static string pChoice;      //Input

        // Bools
        public static bool update = false;
        public static bool figth = true;

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
                    Console.WriteLine(WindowDrawing(sidebarP[i], 20, false, false)+" " + WindowDrawing(AvatarP[i], 30, false, false) + "  " 
                        + WindowDrawing(AvatarM[i], 30, false, false) + " " + WindowDrawing(sidebarM[i], 20, true, false));
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    Console.Write(WindowDrawing(AvatarP[i], 30, false, false) + "  " + WindowDrawing(AvatarM[i], 70, true, true));
                }
            }
            DialogueWindow();
        }

        public static void MainMenue () { }

        public static void PlayerCreation() { }

        public static void HeadWindow() 
        {
            int lvl = Player.Level + 1;
            int prosent = (Player.Xp / Player._nextLevel) * 100;
            string _xProgress = "";

            if (prosent <= 100 && prosent > 90) _xProgress = $"XP: >~~~~~~|{prosent}%|~~~~~~>({lvl})";
            else if(prosent <= 10 && prosent > 5) _xProgress = $"XP: >~|{prosent}%|~>           ({lvl})";
            else if (prosent <= 20 && prosent > 10) _xProgress = $"XP: >~~|{prosent}%|~>          ({lvl})";
            else if (prosent <= 30 && prosent > 20) _xProgress = $"XP: >~~|{prosent}%|~~>         ({lvl})";
            else if(prosent <= 40 && prosent > 30) _xProgress = $"XP: >~~~|{prosent}%|~~>        ({lvl})";
            else if(prosent <= 50 && prosent > 40) _xProgress = $"XP: >~~~|{prosent}%|~~~>       ({lvl})";
            else if(prosent <= 60 && prosent > 50) _xProgress = $"XP: >~~~~|{prosent}%|~~~~>     ({lvl})";
            else if(prosent <= 70 && prosent > 60) _xProgress = $"XP: >~~~~~|{prosent}%|~~~~>    ({lvl})";
            else if(prosent <= 80 && prosent > 70) _xProgress = $"XP: >~~~~~|{prosent}%|~~~~~>   ({lvl})";
            else if(prosent <= 90 && prosent > 80) _xProgress = $"XP: >~~~~~~|{prosent}%|~~~~~>  ({lvl})";
            else _xProgress = $"XP: >|{prosent}%|>             ({lvl})";

            PrintTheBorder("#");
            Console.WriteLine("\t");

            Console.WriteLine(
                HeaderTextAlign(pName) +
                HeaderTextAlign(_xProgress) +
                HeaderTextAlign(storyline_area)
                ); ;

            Console.WriteLine("\t");
            PrintTheBorder("#");
            Console.WriteLine("\t");

            if (subHeader == "")
            {
                Utility.WriteLine(Utility.Center(subHeader));
                Console.WriteLine("\t");
                PrintTheBorder("_");
            }
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

            string _stringB = "";

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
            else { for (int i = 0; i < _spaces; i++) _string += " "; }

            _stringB = _centerA + _string + _centerB;

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

            for (int i = 0; i <= 88; i++) Console.Write(_border);
            Console.ResetColor();
        }

        public static void DialogueWindow() { }

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

    }
}
