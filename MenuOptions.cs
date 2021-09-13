using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
 *  When adding a new Menue:
 *  1) Add the menue options in _menueSwitch
 *  2) create the switch logic in _theChoice
 *  3) Improve
 */


namespace HeroVsMonster
{
    public static class MenueOptions
    {
        private static int _choice;
        private static string[] theList = { };

        public static string[] _menueList(string _menueSwitch)
        {
            switch (_menueSwitch)
            {
                case "Dialogue":
                    if (theList.Length > 0) Array.Clear(theList, 0, theList.GetUpperBound(0));

                    List<string> _DList = new List<string>(theList.ToList());
                    string[] _DArr = { "NewList" };
                    foreach (string a in _DArr) _DList.Add(a);

                    theList = _DList.ToArray();

                    return theList;

                default: //Main Menue
                    // Empty theList array
                    if (theList.Length > 0) Array.Clear(theList, 0, theList.GetUpperBound(0));
                    
                    // Using a list to populate the array
                    List<string> _mmList = new List<string>(theList.ToList());
                    string[] _mmArr = { "New Game", "Load Game", "Exit to Desktop." };
                    foreach (string mma in _mmArr) _mmList.Add(mma);

                    //Populating theList Array
                    theList = _mmList.ToArray();
                    
                    return theList;
            }
        }

        public static void _theChoice(string _menueName, int _choice)
        {
            switch (_menueName)
            {
                case "Dialogue":

                    break;

                case "Vendor A":

                    break;

                case "Rumage through trash":

                    break;

                case "Help a Villager":

                    break;

                default: //MainMenu
                    switch (_choice)
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
                                if (int.TryParse(_inputSave, out _choice))
                                {
                                    if ((_choice > theList.Length) || _skipDeadpeople.Contains(_choice))
                                    {
                                        Utility.WriteLine($"Please enter a number between 1-{theList.Length}. That arent Dead People!");
                                        break;
                                    }
                                    else
                                    {
                                        string S = _saves[_intSaves - 1];
                                        StreamReader sr = new StreamReader(S);
                                        string _temporaryName = sr.ReadLine();

                                        Game.LoadGame(_temporaryName);
                                    }
                                }
                            }
                            catch
                            {
                                Utility.WriteLine("You have no prior Savegames..");
                                Console.ReadKey();
                                //Game.ConsoleStartMenu();
                            }
                            break;

                        default:
                            //if a number other than 1-5 is entered, request
                            //player enter a number in that range
                            //wait for them to press enter, then call
                            //the menu again
                            Console.WriteLine($"Please enter a number 1-{theList.Length}.");
                            Console.WriteLine("\nPress enter to continue...");
                            Console.ReadKey();
                            break;
                    }
                    break;
            }
        }
    }
}
