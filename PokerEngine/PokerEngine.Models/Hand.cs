using System.Collections.Generic;

using PokerEngine.Models.Enumerations;

namespace PokerEngine.Models
{
    public class Hand
    {
        private List<Card> cards;
        private HandValue handValue;

        public Hand(List<Card> cards, HandValue handValue)
        {
            this.Cards = cards;
            this.HandValue = handValue;
        }

        public List<Card> Cards
        {
            get { return this.cards; }
            private set { this.cards = value; }
        }

        public HandValue HandValue
        {
            get { return this.handValue; }
            private set { this.handValue = value; }
        }

        public override string ToString()
        {
            var handToString = string.Join(", ", this.Cards);

            return handToString;
        }
    }
}
