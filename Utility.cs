using System;
using System.Globalization;

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
