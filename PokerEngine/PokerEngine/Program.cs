using PokerEngine.Helpers;
using PokerEngine.Logic;
using PokerEngine.Models;

using PokerEngine.Strategies;

using System.Collections.Generic;

namespace PokerEngine
{
    public class Program
    {
        public static void Main()
        {
            var firstPlayer = new Player("A", 100, new PassivePlayer());
            var secondPlayer = new Player("B", 100, new AggressivePlayer());
            var thirdPlayer = new Player("C", 100, new RandomPlayer());
            var forthPlayer = new Player("D", 100, new RandomPlayer());
            var fifthPlayer = new Player("E", 100, new AggressivePlayer());

            var players = new List<Player>();

            players.Add(firstPlayer);
            players.Add(secondPlayer);
            players.Add(thirdPlayer);
            players.Add(forthPlayer);
            players.Add(fifthPlayer);

            var table = new Table(players, new BlindEvaluator(), new PlayerHandEvaluator(), new FileLogger('-', 100));

            table.StartGame();
        }
    }
}
