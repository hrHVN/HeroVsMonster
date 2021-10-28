using System.Collections.Generic;
using System;
using HeroVsMonster.Content.Models;

namespace HeroVsMonster
{
    public static class FigthSceen
    {
        static double AttackStrength;
        static DamageType AttackType;
        static int _defenceArray;

        // Function Sets AttackStrength and AttackType 
        static void Weapon(Character character)
        {
            AttackType = character.Weapon.Type;

            int WeaponBonus;
            // if prefered weapon -> Weaponbonus +1 : else - 2
            if (character.PreferedWeapons.Contains(character.Weapon.Name))
            {
                WeaponBonus = 1;
            }
            else
            {
                WeaponBonus = -2;
            }

            AttackStrength = character.WeaponProffeciency + WeaponBonus;

            switch (character.Weapon.Type)
            {
                case DamageType.Blunt:
                    _defenceArray = 1;
                    break;
                case DamageType.Sharp:
                    _defenceArray = 2;
                    break;
                case DamageType.Magic:
                    _defenceArray = 3;
                    break;
                case DamageType.Holy:
                    _defenceArray = 4;
                    break;
                case DamageType.Ranged:
                    _defenceArray = 5;
                    break;
            }
        }

        static double Defence(string _class, string _armorPice) // Function returns defence value for specified armorpice
        {
            double _defVal = 0.0;

            // Def_type > Blunt : Sharp : Magic : Holy
            // defalut is None/ThickHide = 1 : 1 : 1 : 1 
            string[,] _defenceDamage =
            {
                { "None", "1", "1","1", "1", "1" }, { "ThickHide", "1", "1","1", "1", "1" },
                { "Archane", "1" , "2", "0.3", "0.7", "0.7" },
                { "Faith", "1", "2", "0.7", "0.3", "0.3"},
                { "Steel", "0.3", "0.3", "2", "2", "2"},
                { "Leather", "0.7", "0.5", "1.5", "1,5", "1.5"},
                { "ChainMail", "0.5", "0.3", "2", "2", "2"}
            };

            // Loop throug bodypart_armor to find right value
            for (int i = 0; i <= 5; i++)
            {
                if (EnumHelper.IsParsable<PlayerClass>(_class)) // If player
                { 
                    if (Player.Current.DefenceType[i, 0].ToLower() == _armorPice.ToLower()) // If head == Head
                    {
                        int d = 0;
                        while (Player.Current.DefenceType[i, 1].ToLower() != _defenceDamage[d, 0].ToLower()) // Find array pos == ArmorType("Leather")
                        { 
                            d++; 
                        } 
                        _defVal = Convert.ToDouble(_defenceDamage[d, _defenceArray]);  // Set the defence value(Leather) vs AttackType(Magic) = 1.5;
                        break;
                    }
                } 
                else 
                { 
                    if (Monster.Current.DefenceType[i, 0].ToLower() == _armorPice.ToLower())
                    {
                        int d = 0;
                        while (Monster.Current.DefenceType[i, 1].ToLower() != _defenceDamage[d, 0].ToLower())
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
                _defVal += Defence(Player.Current.Name, _armorSlot[i]);
            }
            Weapon(Player.Current);
            Game.pAttStr = AttackStrength;
            Game.pDefValue = _defVal;

            _defVal = 0;
            for (int i = 0; i <= 5; i++)
            {
                _defVal += Defence(Monster.Current.Class, _armorSlot[i]);
            }
            Weapon(Monster.Current);
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
            string _potion = Player.Current.Inventory.Contains("Potion") ? "Use Potion" : "Potions Empty";
            string _trick = Player.Current.Inventory.Contains("Oil") ? "Use Trick" : "No tricks";

            if (Player.Current.Health > 0 && Monster.Current.Health > 0) 
            {
                Game.pResponce.SetValue("Throw Dice", 0);
                Game.pResponce.SetValue(_potion, 1);
                Game.pResponce.SetValue(_trick, 2);
                Game.pResponce.SetValue("Save & Exit", 3);

                switch (Game.pChoice) // Try input > valid input 
                {
                    case "1": // DiceRoll
                        Weapon(Player.Current); // Set the Stats
                        damage = (int)DamageGiven(Utility.DiceRoll(), Player.Current.Class); // the initial damage

                        // Parry chance for ranged weapons
                        if (Player.Current.Weapon.Type == DamageType.Ranged || Player.Current.Weapon.Type == DamageType.Magic || Player.Current.Weapon.Type == DamageType.Holy)
                        {
                            int _parry = _random.Next(0, 1);
                            damage = (int)(damage * (1 - 0.75));
                        }
                        
                        // Level Up Weapon skills
                        Player.Current.WeaponProffeciency += damage / Monster.Current.MaxHealth;

                        //Surprise >> Magic + Combustable = Explosion
                        if (Monster.Current.Inventory.Contains("Oil") && (AttackType == DamageType.Magic || AttackType == DamageType.Holy))
                        {
                            int _supriseDamage = Utility.DiceRoll() * (int)AttackStrength;
                            Monster.Current.Health -= _supriseDamage;
                            Game.subHeader = $"{Monster.Current.Class} had a vial of oil in it's pocket wich blew up in flames, taking {_supriseDamage}";
                        }

                        // Deal the damage
                        Monster.Current.Health -= damage;
                        
                        // SaveGame and continue
                        break;

                    case "2": // Use Potion
                        if (Player.Current.Inventory.Contains("Potion"))
                        {
                            double _potionHp = Player.Current.MaxHealth * (1 + (_random.Next(5, 50) / 100));
                            Player.Current.Health += (int)_potionHp;
                            Game.subHeader = $"Thats refreshing, the potion heals you with {_potionHp} Hp!";
                        }
                        else Game.subHeader = "You have no Potions left...";
                        // SaveGame
                        break;

                    case "3": // Use Trick
                        if (Player.Current.Inventory.Contains("oil"))
                        {
                            int _trickDamage = Utility.DiceRoll() * (int)AttackStrength;

                            // Parry chance - if parry >> 75% damage
                            _trickDamage = (_random.Next(0, 1) == 0) ? (int)(_trickDamage * (1 - 0.75)) : _trickDamage;

                            Monster.Current.Health -= _trickDamage;
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
                if (Monster.Current.Health < 0)
                {
                    Array.Clear(Game.pResponce, 0, Game.pResponce.Length);
                    Game.pResponce.SetValue("Continue", 0);
                    Game.pResponce.SetValue("Save & Exit", 1);

                    switch (Game.pChoice) // Try input > valid input 
                    {
                        case "1":
                            Weapon(Monster.Current); // Set the Stats
                            damage = (int)DamageGiven(Utility.DiceRoll(), Monster.Current.Class); // the initial damage

                            // Parry chance for ranged weapons
                            if (Monster.Current.Weapon.Type == DamageType.Ranged || Monster.Current.Weapon.Type == DamageType.Magic || Monster.Current.Weapon.Type == DamageType.Holy)
                            {
                                int _parry = _random.Next(0, 1);
                                damage = (int)(damage * (1 - 0.75));
                            }
                            // Level Up Weapon skills
                            Monster.Current.WeaponProffeciency += damage / Player.Current.MaxHealth;

                            //Surprise >> Magic + Combustable = Explosion
                            if (Player.Current.Inventory.Contains("Oil") && (AttackType == DamageType.Magic || AttackType == DamageType.Holy))
                            {
                                int _supriseDamage = Utility.DiceRoll() * (int)AttackStrength;
                                damage += _supriseDamage;
                                Game.subHeader = $"One vial of oil in your pocket exploded, giving you {_supriseDamage} damage..";
                            }

                            // Deal the damage
                            Player.Current.Health -= damage;

                            break;
                        default:
                            // Save
                            // Exit
                            break;
                    }
                }
            }

            // VictoryConditions

            else if (Player.Current.Health > Monster.Current.Health) // Player Wins
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

