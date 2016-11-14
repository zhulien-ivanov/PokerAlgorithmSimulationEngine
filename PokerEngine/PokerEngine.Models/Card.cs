using PokerEngine.Models.Enumerations;

namespace PokerEngine.Models
{
    public class Card
    {
        private CardFace cardFace;
        private CardSuit cardSuit;

        public Card(CardFace cardFace, CardSuit cardSuit)
        {
            this.CardFace = cardFace;
            this.CardSuit = cardSuit;
        }

        public CardFace CardFace
        {
            get { return this.cardFace; }
            private set { this.cardFace = value; }
        }

        public CardSuit CardSuit
        {
            get { return this.cardSuit; }
            private set { this.cardSuit = value; }
        }

        public override bool Equals(object obj)
        {
            var secondCard = obj as Card;

            if (secondCard == null)
            {
                return false;
            }

            return this.CardFace.Equals(secondCard.CardFace) && this.CardSuit.Equals(secondCard.CardSuit);
        }
    }
}
