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
            int _mStartHp = Monsters.Health;
            // Loop
            string _potion = Player.Inventory.Contains("Potion") ? "Use Potion" : "Potions Empty";
            string _trick = Player.Inventory.Contains("Oil") ? "Use Trick" : "No tricks";

            Game.pResponce.SetValue("Throw Dice", 0);
            Game.pResponce.SetValue(_potion, 1);
            Game.pResponce.SetValue(_trick, 2);
            Game.pResponce.SetValue("Save & Exit", 3);

            switch (Game.pChoice) // Try input > valid input 
            {

            }

            // VictoryConditions

            do
            {

                Weapon(Player.Weapon, Player.Class); // Set the Stats
                int damage = (int) DamageGiven(Utility.DiceRoll(), Player.Class); // the initial damage

                // Parry chance for ranged weapons
                if (EnumHelper.IsParsable<RangedWeapons>(Player.Weapon) || EnumHelper.IsParsable<MagicWeapons>(Player.Weapon) || EnumHelper.IsParsable<HolyWeapons>(Player.Weapon))
                {
                    int _parry = _random.Next(0, 1);
                    damage = (int) (damage * (1-0.75));
                }
                // Level Up Weapon skills
                Player.WeaponProffeciency += damage / _mStartHp;

                //Surprise >> Magic + Combustable = Explosion
                if (Monsters.Inventory.Contains("Oil") && (AttackType == "Magic" || AttackType == "Holy")) 
                {
                    int _supriseDamage = Utility.DiceRoll() * (int) AttackStrength;
                    damage += _supriseDamage;
                    Game.subHeader = $"{Monsters.Class} had a vial of oil in it's pocket wich blew up in flames, taking {_supriseDamage}";
                }

                // Deal the damage
                Monsters.Health -= damage;

                if (Monsters.Health < 0) continue;

                Weapon(Monsters.Weapon, Monsters.Class);
            }
            while (Player.Health > 0 && Monsters.Health > 0);

            // Wictory loop
            
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
            }

        } 
    }
}

