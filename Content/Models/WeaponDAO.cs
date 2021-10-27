using System;
namespace HeroVsMonster.Content.Models
{
    public enum DamageType
    {
        Sharp, Blunt, Ranged, Holy, Magic
    }

    public class WeaponDAO
    {
        public string Name;
        public DamageType Type;
    }
}
