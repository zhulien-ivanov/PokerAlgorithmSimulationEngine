using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;
using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandEvaluatorTests
{
    [TestClass]
    public class FlushTests
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
        public void HavingFlushWith5SuitedCardsShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Two, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Flush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFlushWith6SuitedCardsShouldReturnProperResult1()
        {
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Flush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFlushWith7SuitedCardsShouldReturnProperResult1()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Queen, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Two, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Flush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFlushWith5SuitedCardsAndAStraightShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Flush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFlushWith6SuitedCardsAndAStraightShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Six, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Five, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Flush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFlushWith5SuitedCardsAndAThreeOfAKindShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Flush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFlushWith5SuitedCardsAndTwoPairsShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Flush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFlushWith5SuitedCardsAndOnePairShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Clubs));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Flush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }

        [TestMethod]
        public void HavingFlushWith6SuitedCardsAndOnePairShouldReturnProperResult()
        {
            this.expectedHand.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.expectedHand.Add(new Card(CardFace.Six, CardSuit.Spades));

            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Seven, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Jack, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Ace, CardSuit.Hearts));
            this.inputCards.Add(new Card(CardFace.Six, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Eight, CardSuit.Spades));
            this.inputCards.Add(new Card(CardFace.Four, CardSuit.Spades));

            this.outputHand = this.handEvaluator.EvaluateHand(this.inputCards);

            Assert.AreEqual(HandValue.Flush, this.outputHand.HandValue);
            CollectionAssert.AreEqual(this.expectedHand, this.outputHand.Cards);
        }
    }
}
