using System.Collections.Generic;

namespace PokerEngine.Models
{
    public class Player
    {
        private string name;
        private decimal money;
        private List<Card> cards;

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
            set { this.money = value; }
        }

        public List<Card> Cards
        {
            get { return this.cards; }
            set { this.cards = value; }
        }
    }
}