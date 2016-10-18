using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;
using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandEvaluatorTests
{
    [TestClass]
    public class FullHouseTests
    {
        private IHandEvaluator handEvaluator;
        private List<Card> inputCards;
        private Hand outputHand;
        private List<Card> expectedHand;

        [TestInitialize]
        public void SetUp()
        {
            this.handEvaluator = new HandEvaluator();
            this.inputCards = new List<Card>();
            this.expectedHand = new List<Card>();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.inputCards.Clear();
            this.expectedHand.Clear();
        }

        [TestMethod]
        public void HavingFullHouseWithHigherTripsShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Hearts));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.FullHouse, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFullHouseWithLowerTripsShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Hearts));

            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.FullHouse, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFullHouseWithHigherTripsAndDoubleTripsShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.FullHouse, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFullHouseWithLowerTripsAndDoubleTripsShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.FullHouse, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFullHouseWithHigherTripsAndDoublePairsShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.FullHouse, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFullHouseWithLowerTripsAndDoublePairsShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.FullHouse, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }
    }
}
