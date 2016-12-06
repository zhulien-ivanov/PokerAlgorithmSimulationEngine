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

        public override string ToString()
        {
            string face;

            switch (this.CardFace)
            {
                case CardFace.Two:
                    face = "2";
                    break;
                case CardFace.Three:
                    face = "3";
                    break;
                case CardFace.Four:
                    face = "4";
                    break;
                case CardFace.Five:
                    face = "5";
                    break;
                case CardFace.Six:
                    face = "6";
                    break;
                case CardFace.Seven:
                    face = "7";
                    break;
                case CardFace.Eight:
                    face = "8";
                    break;
                case CardFace.Nine:
                    face = "9";
                    break;
                case CardFace.Ten:
                    face = "10";
                    break;
                case CardFace.Jack:
                    face = "J";
                    break;
                case CardFace.Queen:
                    face = "Q";
                    break;
                case CardFace.King:
                    face = "K";
                    break;
                case CardFace.Ace:
                    face = "A";
                    break;
                default:
                    face = null;
                    break;
            }

            string suit;

            switch (this.CardSuit)
            {
                case CardSuit.Clubs:
                    suit = "♣";
                    break;
                case CardSuit.Diamonds:
                    suit = "♦";
                    break;
                case CardSuit.Hearts:
                    suit = "♥";
                    break;
                case CardSuit.Spades:
                    suit = "♠";
                    break;
                default:
                    suit = null;
                    break;
            }

            var cardToString = string.Format("{0}{1}", face, suit);

            return cardToString;
        }
    }
}
