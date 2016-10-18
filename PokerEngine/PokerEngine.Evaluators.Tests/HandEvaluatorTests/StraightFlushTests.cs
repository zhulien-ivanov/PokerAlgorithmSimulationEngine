using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;
using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandEvaluatorTests
{
    [TestClass]
    public class StraightFlushTests
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
        public void HavingTopAceStraightFlushReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightFlushReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingStraightFlushWith6CardsReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingStraightFlushWith7CardsReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingStraightFlushWith6CardsAndStraightReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingTopAceStraightFlushAndFlushReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightFlushAndFlushReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingTopAceStraightFlushAndStraightReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightFlushAndStraightReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingTopAceStraightFlushAndThreeOfAKindReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightFlushAndThreeOfAKindReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingTopAceStraightFlushAndTwoPairsReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightFlushAndTwoPairsReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingTopAceStraightFlushAndAPairReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.King, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingBottomAceStraightFlushAndAPairReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.King, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Three, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingStraightFlushWith6CardsAndAPairReturnsProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Ten, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Nine, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Diamonds));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Diamonds));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.StraightFlush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }
    }
}
