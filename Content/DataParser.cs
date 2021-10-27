using System.IO;
using Newtonsoft.Json;

namespace HeroVsMonster
{
    public class DataParser : IDataParser
    {
        public T Decode<T>(string filename)
        {
            // read JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                T deserialized = JsonConvert.DeserializeObject<T>(file.ReadToEnd());
                return deserialized;
            }
        }

        public void Encode(object item, string filename)
        {
            string serialized = JsonConvert.SerializeObject(item);
            File.WriteAllText(filename, serialized);
        }
    }
}
