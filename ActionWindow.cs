using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVsMonster
{
    public static class ActionWindow
    {
        static int screenWidth = Console.WindowWidth;
        static int screenHeigth = Console.WindowHeight;

        static string margin = "\t";
        static string indent = "\t\t";

        static bool UpdateWindow = false; //Default  is false, loop wil make true
        // Headerbar
        static string _pName = (Game.Run) ? Player.Name : "Hero...";
        static string _pLevel = (Game.Run) ? Player.Level.ToString() : "The";
        static string _nPc = (Game.Run) ? Monsters.Class : "Time to Fight";

        // ChatWindow
        public static string _MenuSwitch = "";
        public static string _npcVoice = "";


        public static void WindowAdjust()
        {
            Utility.WriteLine(Utility.Center("Adjust the window so that the Line below to fit on one line"));
            PrintTheBorder();
            Utility.Pause();
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

        public static void MainWindow() 
        { 
            if (!UpdateWindow)
            {
                Console.Clear();
                HeaderBar();
                GameWindow();
                DialogueWindow();

                UpdateWindow = true;
            }
        }

        public static void HeaderBar()
        {
            string _progressBar = "Adventure is waiting";
   
            // ProgressBar Loop
            int _divisor = (GamePlay._nextLevel / 5);
            if (Game.Run)
            {
                if (Player.Xp <= _divisor) { _progressBar = "Xp: |>"; }
                if (Player.Xp <= (_divisor * 2)) { _progressBar = "Xp: |==>"; }
                if (Player.Xp <= (_divisor * 3)) { _progressBar = "Xp: |==|==>"; }
                if (Player.Xp <= (_divisor * 4)) { _progressBar = "Xp: |==|==|==>"; }
                if (Player.Xp <= (_divisor * 5)) { _progressBar = "Xp: |==|==|==|==>"; }
            }

            Utility.Title("Hero vs Monster", "Surviaval of the luckiest ...");
            PrintTheBorder();
            Console.WriteLine(indent);

            headerSpaces(_pName);          //Player Name
            headerSpaces(_pLevel);         //Player Level
            headerSpaces(_progressBar);    //Player Progress bar(xp)
            headerSpaces(_nPc);            //NPC / Oponent monster

            Console.WriteLine(indent);
            PrintTheBorder();
        }

        public static void GameWindow()
        {
            
        }

        public static void DialogueWindow() 
        {
            string[] _menuList = MenueOptions._menueList(_MenuSwitch);
            int _int = 1; // Menue Choice

            PrintTheBorder();
            Console.WriteLine(margin);
            //Print the NPC dialogue
            Utility.WriteLine(_npcVoice);

            PrintTheBorder("-");
            Console.WriteLine(margin);
            //List the response option
            foreach (string M in _menuList)
            {
                Utility.WriteLine($"< {_int} > {M} ");
                _int++;
            }
            Console.WriteLine(margin);
            
            PrintTheBorder("-");
            Console.WriteLine(margin);
            Game.Input = Utility.Input("Choice");

            if (int.TryParse(Game.Input, out Game.Choice)) 
            {
                MenueOptions._theChoice(_MenuSwitch, Game.Choice);
            }
            else
            {
                Utility.WriteLine("Invalid Choice!!");
                Utility.WriteLine("Press enter to try again");
                Console.ReadKey();
                DialogueWindow();
            }

            Console.WriteLine(margin);
            PrintTheBorder();
        }

        public static void PrintTheBorder(string _border ="#", string _color = " ")
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

            for (int i = 0; i <= screenWidth; i++) Console.Write(_border);
            Console.ResetColor();
            Console.WriteLine(margin);
        }

        public static void headerSpaces(string _stringToSpace)
        {
            int length = _stringToSpace.Length;
            int _sPN = (screenWidth / 8) - (length / 2);
            for (int i = 0; i < _sPN; i++) Console.Write(" ");
            Utility.Write(_stringToSpace);
            for (int i = 0; i < _sPN; i++) Console.Write(" ");
        }

        public static void headerSpaces(string _stringToSpace, string _color)
        {
            int length = _stringToSpace.Length;
            int _sPN = (screenWidth / 8) - (length / 2);
            for (int i = 0; i < _sPN; i++) Console.Write(" ");
            Utility.Write(_stringToSpace, _color);
            for (int i = 0; i < _sPN; i++) Console.Write(" ");
        }
    }
}
