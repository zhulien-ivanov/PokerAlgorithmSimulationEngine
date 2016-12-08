﻿using PokerEngine.Evaluators;
using PokerEngine.Helpers;
using PokerEngine.Logic;
using PokerEngine.Models;
using System.Collections.Generic;

namespace PokerEngine
{
    public class Program
    {
        public static void Main()
        {
            var firstPlayer = new Player("Ivan", 100, new PassivePlayer()); //Dealer, BB
            var secondPlayer = new Player("Damyan", 5, new AggressivePlayer()); //SB

            var players = new List<Player>();

            players.Add(firstPlayer);
            players.Add(secondPlayer);

            var table = new Table(players, new BlindEvaluator(), new PlayerHandEvaluator(), new FileLogger('-', 80));

            table.StartGame();
        }
    }
}
