using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroVsMonster
{ 
    class Monsters : Character
    {
        static void MonsterDialogue(int d)
        {
            switch(d)
            {
             case 1:
                Console.WriteLine("... ");
                break;

             case 2:
                Console.WriteLine("... ");
                break;

             default:
                Console.WriteLine("... ");
                break;
            }
        }
    }

    enum MonsterRace
    {
        Orc, Gobblin, Giant, Wolf, Sheep
    }

    enum MonsterWeapon
    {

    }

    class BossMonster : Monsters
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

