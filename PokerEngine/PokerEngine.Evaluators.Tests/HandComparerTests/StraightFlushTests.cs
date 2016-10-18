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
        public void Having1StraightFlushReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs)
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
        public void Having2DifferentStraightFlushesReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            this.inputHands.Add(inputHand1);
            this.inputHands.Add(inputHand2);

            this.expectedHands.Add(inputHand1);

            this.outputHands = this.handComparer.GetWinningHands(this.inputHands);

            Assert.AreEqual(this.expectedHands.Count, this.outputHands.Count);
            CollectionAssert.AreEqual(this.expectedHands, this.outputHands);
        }

        [TestMethod]
        public void Having2EqualStraightFlushesReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs)
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
        public void Having2EqualAndOneDifferentLowerStraightFlushesReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
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
        public void Having2EqualAndOneDifferentHigherStraightFlushesReturnsProperResult()
        {
            var inputHand1 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            var inputHand2 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.Ace, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
                );

            var inputHand3 = new Hand
                (
                    new List<Card>()
                    {
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs)
                    },
                    HandValue.StraightFlush
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
