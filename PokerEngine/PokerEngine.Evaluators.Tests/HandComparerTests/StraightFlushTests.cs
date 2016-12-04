using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokerEngine.Evaluators.Contracts;

using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators.Tests.HandComparerTests
{
    [TestClass]
    public class StraightFlushTests
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
        public void Having1StraightFlushHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs),
                        new Card(CardFace.Eight, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            this.inputHands.Add(inputHand1);

            this.expectedHands.Add(inputHand1);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2DifferentStraightFlushHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs),
                        new Card(CardFace.Eight, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs),
                        new Card(CardFace.Eight, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);

            this.expectedHands.Add(inputHand2);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }
        
        [TestMethod]
        public void Having2SameStraightFlushHandsReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs),
                        new Card(CardFace.Eight, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs),
                        new Card(CardFace.Eight, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
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
        public void Having1StraightFlushHandVs1FourOfAKindHandVs1FullHouseHandVs1FlushHandVs1StraightHandVs1ThreeOfAKindHandVs1TwoPairHandVs1OnePairHandVsHighCardHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Diamonds),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Three, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Diamonds),
                        new Card(CardFace.Three, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.ThreeOfAKind
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
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Eight, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Clubs),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Clubs)
                    },
                    HandValue.Flush
                );

            var inputHand5 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Diamonds),
                        new Card(CardFace.Ace, CardSuit.Hearts),
                        new Card(CardFace.Three, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
                    },
                    HandValue.FullHouse
                );

            var inputHand6 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Four, CardSuit.Diamonds),
                        new Card(CardFace.Four, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.King, CardSuit.Spades)
                    },
                    HandValue.FourOfAKind
                );

            var inputHand7 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Diamonds),
                        new Card(CardFace.Jack, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand8 = new Hand
            (
                new List<Card>()
                {
                    new Card(CardFace.Eight, CardSuit.Clubs),
                    new Card(CardFace.Seven, CardSuit.Clubs),
                    new Card(CardFace.Six, CardSuit.Clubs),
                    new Card(CardFace.Five, CardSuit.Clubs),
                    new Card(CardFace.Four, CardSuit.Clubs)
                },
                HandValue.StraightFlush
            );

            var inputHand9 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Spades)
                    },
                    HandValue.Straight
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            this.inputHands.Add(inputHand3);
            this.inputHands.Add(inputHand4);
            this.inputHands.Add(inputHand5);
            this.inputHands.Add(inputHand6);
            this.inputHands.Add(inputHand7);
            this.inputHands.Add(inputHand8);
            this.inputHands.Add(inputHand9);

            this.expectedHands.Add(inputHand8);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2EqualStraightFlushHandsVs1FourOfAKindHandsVs1FullHouseHandVs1FlushHandVs1StraightHandVs1ThreeOfAKindHandVs1TwoPairHandVs1OnePairHandVsHighCardHandReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Diamonds),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.TwoPairs
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Three, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Diamonds),
                        new Card(CardFace.Three, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Spades)
                    },
                    HandValue.ThreeOfAKind
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
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Eight, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Clubs),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Two, CardSuit.Clubs)
                    },
                    HandValue.Flush
                );

            var inputHand5 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Diamonds),
                        new Card(CardFace.Ace, CardSuit.Hearts),
                        new Card(CardFace.Three, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
                    },
                    HandValue.FullHouse
                );

            var inputHand6 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Four, CardSuit.Diamonds),
                        new Card(CardFace.Four, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.King, CardSuit.Spades)
                    },
                    HandValue.FourOfAKind
                );

            var inputHand7 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Diamonds),
                        new Card(CardFace.Jack, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Spades)
                    },
                    HandValue.HighCard
                );

            var inputHand8 = new Hand
            (
                new List<Card>()
                {
                    new Card(CardFace.Eight, CardSuit.Clubs),
                    new Card(CardFace.Seven, CardSuit.Clubs),
                    new Card(CardFace.Six, CardSuit.Clubs),
                    new Card(CardFace.Five, CardSuit.Clubs),
                    new Card(CardFace.Four, CardSuit.Clubs)
                },
                HandValue.StraightFlush
            );

            var inputHand9 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Spades)
                    },
                    HandValue.Straight
                );

            var inputHand10 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Eight, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Clubs),
                        new Card(CardFace.Six, CardSuit.Clubs),
                        new Card(CardFace.Five, CardSuit.Clubs),
                        new Card(CardFace.Four, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);
            this.inputHands.Add(inputHand3);
            this.inputHands.Add(inputHand4);
            this.inputHands.Add(inputHand5);
            this.inputHands.Add(inputHand6);
            this.inputHands.Add(inputHand7);
            this.inputHands.Add(inputHand8);
            this.inputHands.Add(inputHand9);
            this.inputHands.Add(inputHand10);

            this.expectedHands.Add(inputHand8);
            this.expectedHands.Add(inputHand10);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }
    }
}
