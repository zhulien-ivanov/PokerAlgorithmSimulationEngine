using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;

using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandComparerTests
{
    [TestClass]
    public class TwoPairTests
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
        public void Having1TwoPairHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Two, CardSuit.Hearts),
                        new Card(CardFace.Two, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            this.inputHands.Add(inputHand1);

            this.expectedHands.Add(inputHand1);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2DifferentTwoPairHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Diamonds),
                        new Card(CardFace.Two, CardSuit.Hearts),
                        new Card(CardFace.Two, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Hearts),
                        new Card(CardFace.King, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);

            this.expectedHands.Add(inputHand1);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2SameTwoPairHandsWithTheSamePairSuitReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
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
        public void Having2SameTwoPairHandsWithDifferentPairSuitReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Spades),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Queen, CardSuit.Spades),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
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
        public void Having5DifferentTwoPairHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Four, CardSuit.Diamonds),
                        new Card(CardFace.Two, CardSuit.Hearts),
                        new Card(CardFace.Two, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Seven, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Diamonds),
                        new Card(CardFace.Four, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Diamonds),
                        new Card(CardFace.Four, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand4 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Diamonds),
                        new Card(CardFace.Four, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand5 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
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
        public void Having2SameTwoPairHandsWithDifferentHighCardReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Jack, CardSuit.Hearts),
                        new Card(CardFace.Jack, CardSuit.Clubs),
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
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Spades)
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
        public void Having1TwoPairHandVs1OnePairHandVsHighCardHandReturnsProperResult()
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
                        new Card(CardFace.Three, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Diamonds),
                        new Card(CardFace.Two, CardSuit.Hearts),
                        new Card(CardFace.Two, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Diamonds),
                        new Card(CardFace.King, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
                    },
                    HandValue.OnePair
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
        public void Having2EqualTwoPairHandsVs1OnePairHandVsHighCardHandReturnsProperResult()
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
                        new Card(CardFace.Three, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Diamonds),
                        new Card(CardFace.Two, CardSuit.Hearts),
                        new Card(CardFace.Two, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Diamonds),
                        new Card(CardFace.King, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
                    },
                    HandValue.OnePair
                );

            var inputHand4 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Three, CardSuit.Hearts),
                        new Card(CardFace.Three, CardSuit.Diamonds),
                        new Card(CardFace.Two, CardSuit.Hearts),
                        new Card(CardFace.Two, CardSuit.Spades),
                        new Card(CardFace.Seven, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
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
    }
}
