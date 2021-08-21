using System;

namespace HeroVsMonster
{
    class Program
    {
        static void Main(string[] args)
        {
            Player hero = new Player();
            Monster monster1 = new Monster();

            //Welcome message, player initiation
            Console.WriteLine("Welcome \"Player\", to \"Hero vs Monster\"");
            Console.WriteLine("enter your name:  ");
            hero.Name = Console.ReadLine();

            //Class selection
            
            bool x = false;
            
            
            Console.WriteLine("Select your Class!");
            while (!x)
            {
                int z = 0;
                PlayerClass Current = EnumHelper.GetEnumValue<PlayerClass>(z);
                Console.WriteLine($"Press \"Y\" to select {Current} class, or " + "press \"N\" to se next option");
                string input = Console.ReadLine();

                if (input != "y") { z = (z == 4) ?  0 : z++; }
            }


            int monstersBeaten;

            

            /*
             * This is the base code, from an .NET tutorial @ microsoftDocs
             * https://docs.microsoft.com/nb-no/learn/modules/csharp-do-while/4-solution#code-try-9
             * 
            int hero = 10;
            int monster = 10;

            Random dice = new Random();

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

    class Dice
    {
        int roll;
        Random dice = new Random();

        public int diceRoll()
        {
            roll = dice.Next(1, 6);
            return roll;
        }
    }

    class Character
    {
        public string name { get; set; }

        int health { get; }
        int attackStrength { get; }
        int level { get; }
        int defence { get; }

        Dice multiplier = new Dice();

        public int Attack(string attacker, string opponent)
        {
            Console.WriteLine($"{attacker} moves in to attack {opponent} ...");
            int attack = multiplier.diceRoll() * attackStrength;
            return attack;
        }

    }

    class Player : Character
    {
        public string Name { get;  set; }
        public int Health { get; }

        public string pClass;

        
    }

    class Monster : Character
    {
        public string Name { get; set; }
        public int Health { get; }
    }

    public static class EnumHelper
    {
        public static T GetEnumValue<T>(string str) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val;
            return Enum.TryParse<T>(str, true, out val) ? val : default(T);
        }

        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val;
            return (T)Enum.ToObject(enumType, intValue);
        }
    }

    public enum PlayerClass
    {
        Farmer, Warrior, Mage, Archer
    }
}
