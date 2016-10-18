using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;
using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandEvaluatorTests
{
    [TestClass]
    public class StraightTests
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
        public void HavingTopAceStraightReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Hearts));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingStraightWith6CardsReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Nine, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingStraightWith7CardsReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Nine, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Hearts));

            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingTopAceStraightAndThreeOfAKindReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightAndThreeOfAKindReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Hearts));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingTopAceStraightAndTwoPairsReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Clubs));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightAndTwoPairsReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Clubs));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingTopAceStraightAndAPairReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Clubs));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightAndAPairReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.King, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingStraightWith6CardsAndAPairReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Nine, CardSuit.Clubs));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Straight, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }
    }
}
