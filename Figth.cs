using System.Collections.Generic;
using System;

namespace HeroVsMonster
{
    public static class FigthSceen
    {
        static double AttackStrength;
        static string AttackType;
        static int _defenceArray;

        static void Weapon(string _weapon, string _class) // Function Returns AttackStrength and AttackType 
        {
            int WeaponBonus;
            // if magic or holy class -> if weapon is prefered weapon -> attacktype = magic/holy >> else -> attacktype = sharp/blunt
            if (EnumHelper.IsParsable<MagicClass>(_class) || EnumHelper.IsParsable<HolyClass>(_class))
            {
                if (Player.PreferedWeapon.Contains(_weapon) || Monsters.PreferedWeapon.Contains(_weapon))
                {
                    AttackType = EnumHelper.IsParsable<MagicClass>(_class) ? "Magic" : "Holy";
                    _defenceArray = EnumHelper.IsParsable<MagicClass>(_class) ? 3 : 4;
                }
            }
            else // is eiter Sharp or blunt
            {
                if (EnumHelper.IsParsable<SharpWeapons>(_weapon)) // Does Enum Sharp Weapons contain _weapon??
                {
                    AttackType = "Sharp";
                    _defenceArray = 2;
                }
                AttackType = "Blunt";
                _defenceArray = 1;
            }

            // if prefered weapon -> Weaponbonus +1 : else - 2
            if (Player.PreferedWeapon.Contains(_weapon) || Monsters.PreferedWeapon.Contains(_weapon)) { WeaponBonus = 1; }
            else { WeaponBonus = -2; }

            // double AttackStrength = Weaponprofficency + Weaponbonus
            AttackStrength = EnumHelper.IsParsable<PlayerClass>(_class) ? (Player.WeaponProffeciency + WeaponBonus) : (Monsters.WeaponProffeciency + WeaponBonus);
        }

        static double Defence(string _class, string _armorPice) // Function returns defence value for specified armorpice
        {
            double _defVal = 0.0;

            // Def_type > Blunt : Sharp : Magic : Holy
            // defalut is None/ThickHide = 1 : 1 : 1 : 1 
            string[,] _defenceDamage =
            {
                { "None", "1", "1","1", "1" }, { "ThickHide", "1", "1","1", "1" },
                { "Archane", "1" , "2", "0.3", "0.7"},
                { "Faith", "1", "2", "0.7", "0.3"},
                { "Steel", "0.3", "0.3", "2", "2"},
                { "Leather", "0.7", "0.5", "1.5", "1,5"},
                { "ChainMail", "0.5", "0.3", "2", "2"}
            };

            // Loop throug bodypart_armor to find right value
            for (int i = 0; i <= 5; i++)
            {
                if (EnumHelper.IsParsable<PlayerClass>(_class)) // If player
                { 
                    if (Player.DefenceType[i, 0].ToLower() == _armorPice.ToLower()) // If head == Head
                    {
                        int d = 0;
                        while (Player.DefenceType[i, 1].ToLower() != _defenceDamage[d, 0].ToLower()) // Find array pos == ArmorType("Leather")
                        { 
                            d++; 
                        } 
                        _defVal = Convert.ToDouble(_defenceDamage[d, _defenceArray]);  // Set the defence value(Leather) vs AttackType(Magic) = 1.5;
                        break;
                    }
                } 
                else 
                { 
                    if (Monsters.DefenceType[i, 0].ToLower() == _armorPice.ToLower())
                    {
                        int d = 0;
                        while (Player.DefenceType[i, 1].ToLower() != _defenceDamage[d, 0].ToLower())
                        {
                            d++;
                        }
                        _defVal = Convert.ToDouble(_defenceDamage[d, _defenceArray]);
                        break;
                    } 
                }

            }

            return _defVal;
        }

        public static double DamageGiven(int _dice, string _class) // 
        {
            double _damage = 0;
            // Double value = Percentage of damage reccived at location (aka defence priority)
            string[,] _armorSlot = { { "head", "7.5" }, { "chest", "66.0" }, { "arms", "16.5" }, { "legs", "16.0" }, { "boots", "4.0" } };

            double _attackValue = _dice * AttackStrength;

            for (int i = 0; i <= 5; i++)
            {
                _damage += _attackValue * Convert.ToDouble(_armorSlot[i, 1]) * Defence(_class, _armorSlot[i, 0]);
            }

            return _damage;
        }

        public static void InitializeFight()
        {
            double _defVal = 0;
            string[] _armorSlot = { "head", "chest","arms","legs", "boots"};

            for (int i = 0; i <= 5; i++)
            {
                _defVal += Defence(Player.Class, _armorSlot[i]);
            }
            Weapon(Player.Weapon, Player.Class);
            Game.pAttStr = AttackStrength;
            Game.pDefValue = _defVal;

            _defVal = 0;
            for (int i = 0; i <= 5; i++)
            {
                _defVal += Defence(Monsters.Class, _armorSlot[i]);
            }
            Weapon(Monsters.Weapon, Monsters.Class);
            Game.mAttStr = AttackStrength;
            Game.mDefValue = _defVal;
        }

        public static void Figth()
        {
            Random _random = new Random();
            Game.figth = true;
            
            // Spawn Monster
            // Save Game
            // Calculate the Defence and attack values
            InitializeFight();
            Game.update = true;
            int damage;

            // Loop
            string _potion = Player.Inventory.Contains("Potion") ? "Use Potion" : "Potions Empty";
            string _trick = Player.Inventory.Contains("Oil") ? "Use Trick" : "No tricks";

            if (Player.Health > 0 && Monsters.Health > 0) 
            {
                Game.pResponce.SetValue("Throw Dice", 0);
                Game.pResponce.SetValue(_potion, 1);
                Game.pResponce.SetValue(_trick, 2);
                Game.pResponce.SetValue("Save & Exit", 3);

                switch (Game.pChoice) // Try input > valid input 
                {
                    case "1": // DiceRoll
                        Weapon(Player.Weapon, Player.Class); // Set the Stats
                        damage = (int)DamageGiven(Utility.DiceRoll(), Player.Class); // the initial damage

                        // Parry chance for ranged weapons
                        if (EnumHelper.IsParsable<RangedWeapons>(Player.Weapon) || EnumHelper.IsParsable<MagicWeapons>(Player.Weapon) || EnumHelper.IsParsable<HolyWeapons>(Player.Weapon))
                        {
                            int _parry = _random.Next(0, 1);
                            damage = (int)(damage * (1 - 0.75));
                        }
                        
                        // Level Up Weapon skills
                        Player.WeaponProffeciency += damage / Monsters.MaxHealth;

                        //Surprise >> Magic + Combustable = Explosion
                        if (Monsters.Inventory.Contains("Oil") && (AttackType == "Magic" || AttackType == "Holy"))
                        {
                            int _supriseDamage = Utility.DiceRoll() * (int)AttackStrength;
                            Monsters.Health -= _supriseDamage;
                            Game.subHeader = $"{Monsters.Class} had a vial of oil in it's pocket wich blew up in flames, taking {_supriseDamage}";
                        }

                        // Deal the damage
                        Monsters.Health -= damage;
                        
                        // SaveGame and continue
                        break;

                    case "2": // Use Potion
                        if (Player.Inventory.Contains("Potion"))
                        {
                            double _potionHp = Player.MaxHealth * (1 + (_random.Next(5, 50) / 100));
                            Player.Health += (int)_potionHp;
                            Game.subHeader = $"Thats refreshing, the potion heals you with {_potionHp} Hp!";
                        }
                        else Game.subHeader = "You have no Potions left...";
                        // SaveGame
                        break;

                    case "3": // Use Trick
                        if (Player.Inventory.Contains("oil"))
                        {
                            int _trickDamage = Utility.DiceRoll() * (int)AttackStrength;

                            // Parry chance - if parry >> 75% damage
                            _trickDamage = (_random.Next(0, 1) == 0) ? (int)(_trickDamage * (1 - 0.75)) : _trickDamage;

                            Monsters.Health -= _trickDamage;
                        }
                        else Game.subHeader = "You have no tricks to use";
                        // SaveGame
                        break;
                    default:
                        // Save
                        // Exit
                        break;
                }

                // Monster attacks
                if (Monsters.Health < 0)
                {
                    Array.Clear(Game.pResponce, 0, Game.pResponce.Length);
                    Game.pResponce.SetValue("Continue", 0);
                    Game.pResponce.SetValue("Save & Exit", 1);

                    switch (Game.pChoice) // Try input > valid input 
                    {
                        case "1":
                            Weapon(Monsters.Weapon, Monsters.Class); // Set the Stats
                            damage = (int)DamageGiven(Utility.DiceRoll(), Monsters.Class); // the initial damage

                            // Parry chance for ranged weapons
                            if (EnumHelper.IsParsable<RangedWeapons>(Monsters.Weapon) || EnumHelper.IsParsable<MagicWeapons>(Monsters.Weapon) || EnumHelper.IsParsable<HolyWeapons>(Monsters.Weapon))
                            {
                                int _parry = _random.Next(0, 1);
                                damage = (int)(damage * (1 - 0.75));
                            }
                            // Level Up Weapon skills
                            Monsters.WeaponProffeciency += damage / Player.MaxHealth;

                            //Surprise >> Magic + Combustable = Explosion
                            if (Player.Inventory.Contains("Oil") && (AttackType == "Magic" || AttackType == "Holy"))
                            {
                                int _supriseDamage = Utility.DiceRoll() * (int)AttackStrength;
                                damage += _supriseDamage;
                                Game.subHeader = $"One vial of oil in your pocket exploded, giving you {_supriseDamage} damage..";
                            }

                            // Deal the damage
                            Player.Health -= damage;

                            break;
                        default:
                            // Save
                            // Exit
                            break;
                    }
                }
            }

            // VictoryConditions

            else if (Player.Health > Monsters.Health) // Player Wins
            {
                // add xp
                // add loot
                // chance new wep >> if new wep >> reset WeapProfficency
                // Wictory message
                // SaveGame
                // Next Monster
            }
            //Game Over
            else
            {
                // SaveGame
                // Looser message
                // Display stats
                    // Total Monsters beaten
                    // Level
                    // Damage done
                    // Monsterclasses beaten + int
                    // Potions used + healed?
                    // Tricks used + damage?
            }

        } 
    }
}

