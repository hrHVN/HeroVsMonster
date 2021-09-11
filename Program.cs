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

        }
    }

    public static class Game
    {
        //Head
        static string pName;
        static int pXp;
        static int pLevel;
        static string storyline_area;
        static string subHeader; // Write Monster and level up notifications

        // Main window
        static string pAvatar = @""; // 50 + 3 Wide + 30 High
        static string npcAvatar = @"";

        // Dialogue Window
        static string nPcDialogue;  //Dialogue
        static string[] pResponce;  //Options
        static string pChoice;      //Input
        
        public static void StartGame() // Logic to itterate trough the game sequence
        {
            
        }

        public static void FightWindow() { }

        public static void VendorWindow() { }

        public static void StoryWindow() { }

        public static void MainMenue () { }

        public static void PlayerCreation() { }

        public static void HeadWindow() { }

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
