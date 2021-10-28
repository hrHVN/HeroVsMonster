using System;
using System.Collections.Generic;
using HeroVsMonster.Content.Models;

/*
 *  To DO
 *  Clean up, improve selection
 *  make game balancing easier
 */

namespace HeroVsMonster
{
    public class Monster : Character
    {
        private static Monster _currentMonster;
        public static Monster Current => _currentMonster;

        public static void Spawn()
        {
            _currentMonster = new Monster();
            _currentMonster.InitiateMonster();
        }

        private void InitiateMonster()
        {
            Random _random = new();
            // Get Monster Level
            int _maxLevel = (Player.Current.Level > 20) ? Player.Current.Level * 3 : Player.Current.Level * 5;
            int _minLevel = (Player.Current.Level < 20) ? 1 : 20;
            Level = Player.Current.Level + _random.Next(_minLevel, _maxLevel);

            // Selecting Monster
            var monsters = ContentFactory.LoadMonsters();
            int randomIndex = _random.Next(0, monsters.Count - 1);
            var monster = monsters[randomIndex];

            Class = monster.Class;
            Health = monster.Health * Level;
            WeaponProffeciency = monster.WeaponProffeciency;
            Wallet = _random.Next(0, Level * 3);

            SetRandomWeapon();

            /*
            // Adds a predefined random selection of items for the monster;
            string[] _ekstraDefence = { "Leather Armor" };
            int _ekstra = _random.Next(1, _ekstraDefence.GetLength(0));
            for (int i = 0; i < _ekstra; i++) { Defence.Add(_ekstraDefence[i]); }
            */
        }
    }

}
