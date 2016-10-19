using System;
using System.Collections.Generic;
using System.Linq;

using PokerEngine.Models.Enumerations;

namespace PokerEngine.Models
{
    public class Deck
    {
        private List<Card> cards;
        private Random randomGenerator;

        public Deck()
        {
            this.cards = new List<Card>(52)
                    {
                        new Card(CardFace.Two, CardSuit.Clubs),
                        new Card(CardFace.Three, CardSuit.Clubs),
                        new Card(CardFace.Four, CardSuit.Clubs),
                        new Card(CardFace.Five, CardSuit.Clubs),
                        new Card(CardFace.Six, CardSuit.Clubs),
                        new Card(CardFace.Seven, CardSuit.Clubs),
                        new Card(CardFace.Eight, CardSuit.Clubs),
                        new Card(CardFace.Nine, CardSuit.Clubs),
                        new Card(CardFace.Ten, CardSuit.Clubs),
                        new Card(CardFace.Jack, CardSuit.Clubs),
                        new Card(CardFace.Queen, CardSuit.Clubs),
                        new Card(CardFace.King, CardSuit.Clubs),
                        new Card(CardFace.Ace, CardSuit.Clubs),

                        new Card(CardFace.Two, CardSuit.Diamonds),
                        new Card(CardFace.Three, CardSuit.Diamonds),
                        new Card(CardFace.Four, CardSuit.Diamonds),
                        new Card(CardFace.Five, CardSuit.Diamonds),
                        new Card(CardFace.Six, CardSuit.Diamonds),
                        new Card(CardFace.Seven, CardSuit.Diamonds),
                        new Card(CardFace.Eight, CardSuit.Diamonds),
                        new Card(CardFace.Nine, CardSuit.Diamonds),
                        new Card(CardFace.Ten, CardSuit.Diamonds),
                        new Card(CardFace.Jack, CardSuit.Diamonds),
                        new Card(CardFace.Queen, CardSuit.Diamonds),
                        new Card(CardFace.King, CardSuit.Diamonds),
                        new Card(CardFace.Ace, CardSuit.Diamonds),

                        new Card(CardFace.Two, CardSuit.Hearts),
                        new Card(CardFace.Three, CardSuit.Hearts),
                        new Card(CardFace.Four, CardSuit.Hearts),
                        new Card(CardFace.Five, CardSuit.Hearts),
                        new Card(CardFace.Six, CardSuit.Hearts),
                        new Card(CardFace.Seven, CardSuit.Hearts),
                        new Card(CardFace.Eight, CardSuit.Hearts),
                        new Card(CardFace.Nine, CardSuit.Hearts),
                        new Card(CardFace.Ten, CardSuit.Hearts),
                        new Card(CardFace.Jack, CardSuit.Hearts),
                        new Card(CardFace.Queen, CardSuit.Hearts),
                        new Card(CardFace.King, CardSuit.Hearts),
                        new Card(CardFace.Ace, CardSuit.Hearts),

                        new Card(CardFace.Two, CardSuit.Spades),
                        new Card(CardFace.Three, CardSuit.Spades),
                        new Card(CardFace.Four, CardSuit.Spades),
                        new Card(CardFace.Five, CardSuit.Spades),
                        new Card(CardFace.Six, CardSuit.Spades),
                        new Card(CardFace.Seven, CardSuit.Spades),
                        new Card(CardFace.Eight, CardSuit.Spades),
                        new Card(CardFace.Nine, CardSuit.Spades),
                        new Card(CardFace.Ten, CardSuit.Spades),
                        new Card(CardFace.Jack, CardSuit.Spades),
                        new Card(CardFace.Queen, CardSuit.Spades),
                        new Card(CardFace.King, CardSuit.Spades),
                        new Card(CardFace.Ace, CardSuit.Spades)
                    };

            this.randomGenerator = new Random();

            this.Shuffle();
        }

        public List<Card> Cards
        {
            get { return new List<Card>(this.cards); }
            private set { this.cards = value; }
        }

        public void Shuffle()
        {
            Card temp;
            int cardIndex;

            for (int i = 0; i < this.cards.Count; i++)
            {
                cardIndex = randomGenerator.Next(i, this.cards.Count);

                temp = this.cards[i];
                this.cards[i] = this.cards[cardIndex];
                this.cards[cardIndex] = temp;
            }
        }
    }
}
