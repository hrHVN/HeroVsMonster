using System;
using System.Collections.Generic;

namespace HeroVsMonster.Content.Models
{
    public class CharacterDAO
    {
        public string Class { get; set; }
        public DefenceType Defence { get; set; }
        public int Health { get; set; }
        public string Greeting { get; set; }
        public int WeaponProffeciency { get; set; }
        public List<String> PreferedWeapons { get; set; }
    }
}
