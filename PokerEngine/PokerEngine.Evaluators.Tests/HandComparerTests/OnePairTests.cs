using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;

using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandComparerTests
{
    [TestClass]
    public class OnePairTests
    {
        private IHandComparer handComparer;
        private List<Hand> inputHands;
        private List<Hand> outputHands;
        private List<Hand> expectedHands;

        [TestInitialize]
        public void SetUp()
        {
            this.handComparer = new PlayerHandComparer();
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
        public void Having1OnePairHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            this.inputHands.Add(inputHand1);

            this.expectedHands.Add(inputHand1);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2DifferentOnePairHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Queen, CardSuit.Diamonds),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);

            this.expectedHands.Add(inputHand2);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2SameOnePairHandsWithTheSamePairSuitReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
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
        public void Having2SameOnePairHandsWithDifferentPairSuitReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Spades),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
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
        public void Having5DifferentOnePairHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Two, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Diamonds),
                        new Card(CardFace.Ace, CardSuit.Hearts),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Ace, CardSuit.Hearts),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Nine, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Diamonds),
                        new Card(CardFace.Ace, CardSuit.Hearts),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand4 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Diamonds),
                        new Card(CardFace.Jack, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand5 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Diamonds),
                        new Card(CardFace.Ace, CardSuit.Hearts),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            this.inputHands.Add(inputHand3);
            this.inputHands.Add(inputHand4);
            this.inputHands.Add(inputHand5);

            this.expectedHands.Add(inputHand4);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2SameOnePairHandsWithDifferentFirstHighCardReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Jack, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            
            this.expectedHands.Add(inputHand2);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2SameOnePairHandsWithDifferentLastHighCardReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Jack, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Jack, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);

            this.expectedHands.Add(inputHand2);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having1OnePairHandVsHighCardHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Diamonds),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Jack, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Diamonds),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
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
    }
}
