using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;
using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandEvaluatorTests
{
    [TestClass]
    public class ThreeOfAKindTests
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
        public void HavingThreeOfAKindReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.ThreeOfAKind, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }
    }
}
