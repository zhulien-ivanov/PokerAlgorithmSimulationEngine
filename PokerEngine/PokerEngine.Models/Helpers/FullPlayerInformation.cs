using System.Collections.Generic;

namespace PokerEngine.Models.Helpers
{
    public class FullPlayerInformation : PlayerInformation
    {
        private IReadOnlyCollection<Card> cards;
        private Hand hand;

        public FullPlayerInformation(string name, decimal money, IReadOnlyCollection<Card> cards, Hand hand) : base(name, money)
        {
            this.Cards = cards;
            this.Hand = hand;
        }

        public IReadOnlyCollection<Card> Cards
        {
            get { return this.cards; }
            private set { this.cards = value; }
        }

        public Hand Hand
        {
            get { return this.hand; }
            private set { this.hand = value; }
        }
    }
}
