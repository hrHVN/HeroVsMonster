using System;
using System.Collections.Generic;

namespace HeroVsMonster.Content.Models
{
    public class MonsterDAO
    {
        public string Class { get; set; }
        public int Health { get; set; }
        public DefenceType Defence { get; set; }
        public int WeaponProffeciency { get; set; }
        public List<string> PreferedWeapons { get; set; }
    }
}


