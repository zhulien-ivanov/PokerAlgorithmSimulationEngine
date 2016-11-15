using System.Collections.Generic;

namespace PokerEngine.Models
{
    public class Player
    {
        private string name;
        private decimal money;
        private List<Card> cards;
        private Hand hand;

        public Player(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;

            this.Cards = new List<Card>();
        }

        public string Name
        {
            get { return this.name; }
            private set { this.name = value; }
        }

        public decimal Money
        {
            get { return this.money; }
            internal set { this.money = value; }
        }        

        public List<Card> Cards
        {
            get { return this.cards; }
            internal set { this.cards = value; }
        }

        public Hand Hand
        {
            get { return this.hand; }
            internal set { this.hand = value; }
        }

        public override bool Equals(object secondPlayerObject)
        {
            var secondPlayer = secondPlayerObject as Player;

            return this.Name.Equals(secondPlayer.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}