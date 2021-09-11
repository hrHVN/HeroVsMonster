/*
 *  Add a story
 *  Hint: Player can not winn the long game!
 *  
 */

using System;

namespace HeroVsMonster
{
    public static class StoryLine
    {
        // Game start
        // Backstory -> (A) talk to villagers -> (B) fight Loop (10 monsters)

        // A - gives more xp (new lvl), optional loot (wep, def) class variations on respons and dialogue options.
        // talk to vendor a b c 
        // roumage through trash
        // help a villager
        // Figth 5 monsters

        // If survive figth (B) and (A)
        // Story progress - Hint about boss
        // (A) talk to villagers -> (B) fight Loop(10 monsters) -> (C) do something stupid

        // C - risk: Village death -> Boss figth!
        // Boss Figth repeat @ 20-35 monsters

        static void Choice(string A, string B)
        {
            string input = Utility.Input($"{Player.Name}, Which path will you Choose? {A} or {B}");

            if (input == "a") { Utility.WriteLine($"Mighty {Player.Class} You have chosen path {A}!"); }
            else { Utility.WriteLine($"Mighty {Player.Class} You have chosen path {B}!"); }
        }

        public class Item
        {
            public string Name = "Small Stone";
            public string Description = "Unimpressive object.";

            string[] Items = { "Shoe", "Can", "Pair of Chopsticks" };
            string[] Descriptions = { "Looks like some one tryed to eat theese", "Empty can of beans", "Pink plastic chopsticks" };

            public Item()
            {
                Random RandNumber = new Random();
                int number = RandNumber.Next(Items.Length);

                Name = Items[number];
                Description = Descriptions[number];
                Console.WriteLine($"You found a {Name} ({Description})");
            }

        }
}
