using System.Collections.Generic;
using HeroVsMonster.Content.Models;

namespace HeroVsMonster
{
    public interface IDataParser
    {
        public T Decode<T>(string filename);
        public void Encode(object item, string filename);
    }

    public class ContentFactory
    {
        private static IDataParser dataParser = new DataParser();

        private static List<WeaponDAO> weaponsCache = null;
        private static List<MonsterDAO> monsterCache = null;

        public static List<CharacterDAO> LoadClasses()
        {
            string filename = "Content/Classes.json";
            return dataParser.Decode<List<CharacterDAO>>(filename);
        }

        public static List<MonsterDAO> LoadMonsters()
        {
            if (monsterCache != null)
                return monsterCache;

            string filename = "Content/Monsters.json";
            monsterCache = dataParser.Decode<List<MonsterDAO>>(filename);
            return monsterCache;
        }

        public static List<WeaponDAO> LoadWeapons()
        {
            if (weaponsCache != null)
                return weaponsCache;

            string filename = "Content/Weapons.json";
            weaponsCache = dataParser.Decode<List<WeaponDAO>>(filename);
            return weaponsCache;
        }
    }
}
