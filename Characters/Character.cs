using System;
using System.Linq;
using System.Collections.Generic;
using HeroVsMonster.Content.Models;

/*
 *  To DO
 *  Clean up, improve selection
 *  make game balancing easier
 */

namespace HeroVsMonster
{
    public class Character
    {
        public string Class { get; set; }
        public DefenceType Defence { get; set; }
        public int WeaponProffeciency { get; set; }
        public List<String> PreferedWeapons { get; set; }
        public WeaponDAO Weapon;
        public int Level;
        public int Xp;
        public int Health;
        public int MaxHealth;
        public int DeffenceEndurance;
        public int Wallet;

        public string[,] DefenceType = { {"Head", "None" }, { "Chest", "None" }, { "Arms", "None" }, { "Legs", "None" }, { "Boots", "None" }, };
        public List<string> Inventory = new List<string>();

        public int _nextLevel = 100;

        public void LevelUp()
        {
            int _xp = Xp;
            _nextLevel = 100;

            if (_xp >= _nextLevel)
            {
                Level++;
                _nextLevel += (_nextLevel / 3);

                Utility.WriteLine($"Congratulations, you have leveled up!! {Level}");
            }
            else { Utility.WriteLine($"Next Level in {_nextLevel - _xp}!", "cyan"); }
        }

        // Set a random Weapon
        public void SetRandomWeapon()
        {
            Random random = new();
            List<WeaponDAO> weapons = ContentFactory.LoadWeapons().Where(x => PreferedWeapons.Contains(x.Name)).ToList();
            int randomIndex = random.Next(0, weapons.Count - 1);
            Weapon = weapons[randomIndex];
        }
    }

    public class BossMonster : Character
    {
        /*
        * This is the base code, from an .NET tutorial @ microsoftDocs
        * https://docs.microsoft.com/nb-no/learn/modules/csharp-do-while/4-solution#code-try-9
        */
        /*
                private int hero = 10;
                private int monster = 10;

                Random dice = new Random();

                static void Boss()
                {
                    do
                    {
                        int roll = dice.Next(1, 11);
                        monster -= roll;
                        Console.WriteLine($"Monster was damaged and lost {roll} health and now has {monster} health.");

                        if (monster <= 0) continue;

                        roll = dice.Next(1, 11);
                        hero -= roll;
                        Console.WriteLine($"Hero was damaged and lost {roll} health and now has {hero} health.");

                    } while (hero > 0 && monster > 0);

                    Console.WriteLine(hero > monster ? "Hero wins!" : "Monster wins!");
        */

    }
}
