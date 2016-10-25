using System;
using System.Collections.Generic;

using PokerEngine.Evaluators;
using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();

            Console.WriteLine();

            deck.Shuffle();

            Console.WriteLine();
        }
    }
}
