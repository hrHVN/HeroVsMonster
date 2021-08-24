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
            int z = 0;
            
            Console.WriteLine($"Select your Class! {hero.Name}");
            while (!x)
            {
                var Current = (PlayerClass)z;
                //PlayerClass Current = EnumHelper.GetEnumValue<PlayerClass>(z);
                
                Console.WriteLine($"Press \"Y\" to select: \t{Current}." + "(press \"N\" to se next option)");
                string input = Console.ReadLine();

                if ((input != "y") && (z < 3)) { z++; }
                else if ((input != "y") && (z == 4)) { z = 0; }
                else 
                {
                    hero.pClass = Current.ToString();
                    Console.WriteLine($"You have chosen {hero.pClass}!");
                    x = true; 
                }
            }

            Console.WriteLine(Player.defaultValues());
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
        public string name;

        int health;
        int attackStrength;
        int level;
        int defence;

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
        public string Name { get; set; }
        public string Health { get; set; }
        public string Level { get; set; }

        public string pClass;
        public int monstersBeaten;

        static void defaultValues()
        {
            Console.WriteLine(pClass);
        }

        static void Race()
        {
            switch () { }
        }
        /*switch ()
            {
                case 1:
                break;
                default:    //Farmer
                    Hp = 10;
                break;
            }*/
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

    public enum MonsterClass
    {
        Orc, Gobblin, Giant, Wolf, Sheep
    }
}
