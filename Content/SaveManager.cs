using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace HeroVsMonster.Content
{
    public class SaveManager
    {
        static string folder = "savegames";

        static IDataParser dataParser = new DataParser();

        public static void Save(Player player)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string saveName = createSaveName(player.Name);
            string path = createPathFor(saveName);

            var saveGame = new SaveGame
            {
                Name = saveName,
                Player = player,
                IsAlive = player.Health > 0
            };

            dataParser.Encode(saveGame, path);
        }

        public static List<SaveGame> LoadSaveGames(bool includeDeadPeople)
        {
            var files = Directory.GetFiles(folder);
            return files
                .Select(x => loadSaveGame(x))
                .Where(x => {
                    if (includeDeadPeople)
                        return true;
                    return x.IsAlive;
                })
                .ToList();
        }

        private static SaveGame loadSaveGame(string filename)
        {
            return dataParser.Decode<SaveGame>(filename);
        }

        private static string createSaveName(string playerName)
        {
            string timestamp = DateTime.Now.ToFileTimeUtc().ToString();
            return playerName + "_" + timestamp;
        }

        private static string createPathFor(string saveName)
        {
            return folder + "/" + saveName + ".json";
        }
    }

    public class SaveGame
    {
        public string Name;
        public Boolean IsAlive;
        public Player Player;
    }
}
