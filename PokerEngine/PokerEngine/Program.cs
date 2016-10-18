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
            var handEvaluator = new HandEvaluator();

            var cards = new List<Card>();

            cards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            cards.Add(new Card(CardFace.Ace, CardSuit.Spades));
            cards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            cards.Add(new Card(CardFace.Four, CardSuit.Diamonds));

            Hand hand;

            hand = handEvaluator.EvaluateHand(cards);

            Console.WriteLine();
        }
    }
}
