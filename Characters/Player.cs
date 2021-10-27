using System;
using System.Collections.Generic;
using System.Linq;
using HeroVsMonster.Content.Models;

/*
 *  To DO
 *  Clean up, improve selection
 *  make game balancing easier
 */

namespace HeroVsMonster
{
    public class Player : Character
    {
        public static Player Current;
        public string Name;
        public int Monstersdefeated;

        // Player creation, name + class = Starting specs
        public static void PlayerCreation()
        {
            Utility.Heading(Utility.Center("\nStarting a New game"));

            Current = new Player();
            Current.OnboardNewPlayer();

            Game.SaveGame();
        }

        private void OnboardNewPlayer()
        {
            SetBaseStats();
            SetRandomWeapon();
            SelectName();
            SelectClass();
        }

        // Setting base stats for a new character on the first level
        private void SetBaseStats()
        {
            Level = 1;
            Xp = 0;
            Wallet = 50;
            Health = 100;
        }

        // Name Creation
        private void SelectName()
        {
            Console.Write("\n\tEnter your Name Hero: ");
            Name = Console.ReadLine();
            Utility.WriteLine($"\nWelcome {Name}! We have been awaiting your arrival for some time now..");
            Console.ReadKey();
        }

        // Class selection
        private void SelectClass()
        {
            int currentSelection = 0;
            List<CharacterDAO> classes = ContentFactory.LoadClasses();

            void next()
            {
                currentSelection++;
                if (currentSelection >= classes.Count)
                    currentSelection = 0;
            }

            while (Class == null)
            {
                var Current = classes[currentSelection];
                Utility.WriteLine($"\n{Name} are you a.. " + Current.Class);
                string input = Utility.Input("Select Yes or No (Y/N)").ToLower();
                Console.Clear();

                if (input != "y")
                {
                    next();
                }
                else
                {
                    Utility.WriteLine(Current.Greeting);
                    Class = Current.Class;
                    Health = Current.Health;
                    Defence = Current.Defence;
                    PreferedWeapons = Current.PreferedWeapons;
                    WeaponProffeciency = Current.WeaponProffeciency;
                }
            }
        }
    }

}
