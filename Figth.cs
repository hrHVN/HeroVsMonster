using System.Collections.Generic;
using System;

namespace HeroVsMonster
{
    public static class FigthSceen
    {
        static double AttackStrength;
        static string AttackType;

        static void Weapon()
        {
            string[] bluntWeapons = { "Mace", "Staff", "Fists", "Umbrella", "WoodenClub", "TreeTrunk", "Pebbles", "Staff", "Fists" };
            string[] sharpWeapons = { "PitchFork", "Sword", "GreatSword", "Shovel", "Bow", "CrossBow", "MakeShiftSword" };
            string[] magicWeapons = { "Wand", "GobblinWand", "Staff" };
            string[] holyWeapons = { "Wand", "Umbrella", "Staff" };

            // is eiter Sharp or blunt
            // if magic or holy class -> if magic/holy prefered weapon -> attacktype = magic/holy 
            // else -> attacktype = sharp/blunt

            // if prefered weapon -> Weaponbonus +1 : else - 2

            // double AttackStrength = Weaponprofficency + Weaponbonus

            // Returns AttackStrength and AttackType
        }

        static void Defence()
        {
            // type : (resistance value)
            // type: Blunt : Sharp : Magic : Holy
            // defalut= None = 1 : 1 : 1 : 1 
            string[,] _defenceDamage = new string[6, 5]
            {
                { "None", "1", "1","1", "1" },
                { "Archane" , "1" , "2", "0.3", "0.7"},
                { "Faith", "1", "2", "0.7", "0.3"},
                { "Steel", "0.3", "0.3", "2", "2"},
                { "Leather", "0.7", "0.5", "1.5", "1,5"},
                { "ChainMail", "0.5", "0.3", "2", "2"}
            };

        }

        public static int DamageGiven(int _attackValue)
        {
            int _damage = 0;
            double head = 7.5, chest = 66.0, arms = 16.5, legs = 16.0, boots = 4.0;

            //_attackValue = Dice * AttackStrength
            //int Dn = (_attackValue * Defence.Location[Head] %) * (AttackType[Holy] * DefenceType[Cloth]);

            return _damage;
        }

    /*
        public static void Figth()
        {
            

            // defenceType_Vs_DamageType = (Blunt - Sharp - Magic - Holy)
            string[,] _defenceDamage = new string[6, 5]
            {
                {"None", "1", "1","1", "1" },
                { "Archane" , "1" , "2", "0.3", "0.7"},
                { "Faith", "1", "2", "0.7", "0.3"},
                { "Steel", "0.3", "0.3", "2", "2"},
                { "Leather", "0.7", "0.5", "1.5", "1,5"},
                { "ChainMail", "0.5", "0.3", "2", "2"}
            };

            string _class;
            string _weapon;
            List<string> _prefWeapon = new List<string>();

            string _defence;

            /*
            bool Parry = (DamageType == (ranged || Magic || Holy)) ? false : true;
            _parry = (1 == _random.next(0,1)) ? -0.5 : 0.0;
            */

            /*
            if Inventory.contains("Oil") Then explode + Firedamage
            */

        /*
            do
            {
                if ((Player.Class == "Mage") || (Player.Class == "Munk")) { }

                foreach (string d in _defenceDamage)
                {
                    for (int di = 0; di < 5; di++)
                    {
                        if (_defenceDamage[di, 0] == d)
                        {
                            //if (Player.Weapon) { }
                        }
                    }
                }

                if (Monsters.Health <= 0) continue;

            }
            while (Player.Health > 0 && Monsters.Health > 0);

            // Wictory loop
            /*
            if (Player.Health > Monsters.Health)
            {
                Utility.Write($".. The {Monsters.Class} crumbles to the ground reciving it's final blow from your" +
                    $" {Player.Weapon} taking {playerAttack} damage!");
                //Give Hero the xp and stats.
                Player.Xp += (Monsters.Level % 3);
                MonstersBeaten++;
            }
            //Game Over
            else
            {
                Utility.Heading("Game Over!");
                Utility.Write($"You where beaten by a {Monsters.Class}...");
                Utility.Write($"Final Level: {Player.Level} \nMonsters beaten: {MonstersBeaten}");
            }*/

     //   } 
       /* 
        public static int AttackMultiplier(string _attacker)
        // Function that calculates (Weapon skill + Weapon type - Defence) x  Level, reurns Multiplikasjon(int)
        {
            int _level = 1;

            string _weapon;
            double _weaponValue;

            double _attackTypeMultiplier = 0.2;
            string _attackType;

            List<string> _defenceType = new List<string>();
            List<string> _preferdWeapon = new List<string>();

            // The monster attacks
            if (_attacker != Player.Name)
            {
                _attackType = Monsters.Class;
                _level = Monsters.Level;
                _weapon = Monsters.Weapon;
                foreach (string x in Player.Defence) { _defenceType.Add(x); }
                foreach (string pw in Monsters.PreferedWeapon) { _preferdWeapon.Add(pw); }
            }
            // The hero attacks
            else
            {
                _attackType = Player.Class;
                _level = Player.Level;
                _weapon = Player.Weapon;
                foreach (string x in Monsters.Defence) { _defenceType.Add(x); }
                foreach (string pw in Player.PreferedWeapon) { _preferdWeapon.Add(pw); }
            }

            string[] bluntWeapons = { "Mace", "Staff", "Fists", "Umbrella", "WoodenClub", "TreeTrunk", "Pebbles", "Staff", "Fists" };
            string[] magicWeapons = { "Wand", "GobblinWand", "Staff" };
            string[] holyWeapons = { "Wand", "Umbrella", "Staff" };
            string[] sharpWeapons = { "PitchFork", "Sword", "GreatSword", "Shovel", "Bow", "CrossBow", "MakeShiftSword" };

            // AttackType vs DefenceType value selector
            foreach (string a in _defenceType)
            {
                if (Array.Exists(bluntWeapons, element => element == _attackType))
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier = +0.88;
                            break;
                        case "Leather":
                            _attackTypeMultiplier = +0.45;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier = +0.88;
                            break;
                        case "Steel":
                            _attackTypeMultiplier = -1;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier = +0.56;
                            break;
                        case "Copper":
                            _attackTypeMultiplier = -0.33;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier = -0.52;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier = +0.88;
                            break;
                        default: // None
                            _attackTypeMultiplier = +2;
                            break;
                    }
                }
                else if (Array.Exists(sharpWeapons, element => element == _attackType))
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier = +1.2;
                            break;
                        case "Leather":
                            _attackTypeMultiplier = +0.75;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier = +1.2;
                            break;
                        case "Steel":
                            _attackTypeMultiplier = +0.25;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier = +0.75;
                            break;
                        case "Copper":
                            _attackTypeMultiplier = +0.45;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier = +0.35;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier = +1.2;
                            break;
                        default: // None
                            _attackTypeMultiplier = +1.5;
                            break;
                    }
                }
                else if (Array.Exists(magicWeapons, element => element == _attackType))
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier = +0.38;
                            break;
                        case "Leather":
                            _attackTypeMultiplier = +1.45;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier = -1.0;
                            break;
                        case "Steel":
                            _attackTypeMultiplier = +1.7;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier = +1;
                            break;
                        case "Copper":
                            _attackTypeMultiplier = +1.5;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier = +1.52;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier = +0.28;
                            break;
                        default: // None
                            _attackTypeMultiplier = +2;
                            break;
                    }
                }
                else if (Array.Exists(holyWeapons, element => element == _attackType))
                {
                    switch (a)
                    {
                        case "Faith":
                            _attackTypeMultiplier = +0.38;
                            break;
                        case "Leather":
                            _attackTypeMultiplier = +1.45;
                            break;
                        case "ArchaneRobe":
                            _attackTypeMultiplier = +0.28;
                            break;
                        case "Steel":
                            _attackTypeMultiplier = +1.7;
                            break;
                        case "ThickHide":
                            _attackTypeMultiplier = +1;
                            break;
                        case "Copper":
                            _attackTypeMultiplier = +1.5;
                            break;
                        case "ChainMail":
                            _attackTypeMultiplier = +1.52;
                            break;
                        case "HolyRobe":
                            _attackTypeMultiplier = -1.0;
                            break;
                        default: // None
                            _attackTypeMultiplier = +2;
                            break;
                    }
                }
                else //Ptichfork
                {
                    _attackTypeMultiplier = +2;
                }
            }

            // Adding weapon hit bonus
            switch (_weapon)
            {
                // Farmer tools
                case "Umbrella": _weaponValue = 0.2; break;
                case "Pebbles": _weaponValue = 0.3; break;
                case "PitchFork": _weaponValue = 1.5; break;
                case "Shovel": _weaponValue = 1; break;
                // Magic Tools
                case "Staff": _weaponValue = 0.4; break;
                case "Wand": _weaponValue = 0.42; break;
                case "GobblinWand": _weaponValue = 0.34; break;
                //Warrior Tools
                case "Sword": _weaponValue = 0.75; break;
                case "GreatSword": _weaponValue = 1; break;
                case "Mace": _weaponValue = 0.75; break;
                //Archer Tools
                case "Bow": _weaponValue = 0.67; break;
                case "CrossBow": _weaponValue = 0.85; break;
                //Monster Tools
                case "WoodenClub": _weaponValue = 0.51; break;
                case "MakeShiftSword": _weaponValue = 0.75; break;
                case "TreeTrunk": _weaponValue = 2; break;
                // Fists
                default:
                    _weaponValue = 0.2;
                    break;
            }

            // Loop to add bonus if Preafferd weapon is chosen.
            foreach (string pw in _preferdWeapon) { _weaponValue = (pw == _weapon) ? _weaponValue * 3 : +0; }

            // Adding and multiplying everything
            var _result = (_attackTypeMultiplier + _weaponValue) * (double)_level;

            return (int)_result;
        }
        */
    }
}

