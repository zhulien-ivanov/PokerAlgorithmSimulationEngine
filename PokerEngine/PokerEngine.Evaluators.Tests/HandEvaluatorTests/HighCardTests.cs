using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;
using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandEvaluatorTests
{
    [TestClass]
    public class HighCardTests
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
        public void HavingHighCardReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Nine, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.HighCard, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }
    }
}
