using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;

using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandComparerTests
{
    [TestClass]
    public class HighCardTests
    {
        private IHandComparer handComparer;
        private List<Hand> inputHands;
        private List<Hand> outputHands;
        private List<Hand> expectedHands;

        [TestInitialize]
        public void SetUp()
        {
            this.handComparer = new HandComparer();
            this.inputHands = new List<Hand>();
            this.outputHands = new List<Hand>();
            this.expectedHands = new List<Hand>();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.inputHands.Clear();
            this.outputHands.Clear();
            this.expectedHands.Clear();
        }

        [TestMethod]
        public void Having1HighCardHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);

            this.expectedHands.Add(inputHand1);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2DifferentHighCardHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Nine, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);

            this.expectedHands.Add(inputHand1);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2EqualHighCardHandsWithTheSameCardsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);

            this.expectedHands.Add(inputHand1);
            this.expectedHands.Add(inputHand2);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2EqualHighCardHandsWithDifferentCardsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);

            this.expectedHands.Add(inputHand1);
            this.expectedHands.Add(inputHand2);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2EqualHighCardHandsWithDifferentCardsAndALowerHighCardHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            this.inputHands.Add(inputHand3);

            this.expectedHands.Add(inputHand1);
            this.expectedHands.Add(inputHand3);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2EqualHighCardHandsWithDifferentCardsAndAHigherHighCardHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            this.inputHands.Add(inputHand3);

            this.expectedHands.Add(inputHand2);           

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2EqualHighCardHandsWithTheSameCardsAndALowerHighCardHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            this.inputHands.Add(inputHand3);

            this.expectedHands.Add(inputHand1);
            this.expectedHands.Add(inputHand3);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2EqualHighCardHandsWithTheSameCardsAndAHigherHighCardHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            this.inputHands.Add(inputHand3);

            this.expectedHands.Add(inputHand2);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2EqualHighCardHandsCardsAndAnother2LowerHighCardHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand4 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Spades),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            this.inputHands.Add(inputHand3);
            this.inputHands.Add(inputHand4);

            this.expectedHands.Add(inputHand2);
            this.expectedHands.Add(inputHand4);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having5DifferentHighCardHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Six, CardSuit.Diamonds),
                        new Card(CardFace.Five, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Six, CardSuit.Diamonds),
                        new Card(CardFace.Five, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Six, CardSuit.Diamonds),
                        new Card(CardFace.Five, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand4 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Six, CardSuit.Diamonds),
                        new Card(CardFace.Five, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand5 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Seven, CardSuit.Clubs),
                        new Card(CardFace.Six, CardSuit.Diamonds),
                        new Card(CardFace.Five, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            this.inputHands.Add(inputHand3);
            this.inputHands.Add(inputHand4);
            this.inputHands.Add(inputHand5);

            this.expectedHands.Add(inputHand3);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }
    }
}
