using System.Collections.Generic;

using PokerEngine.Models.Contracts;

namespace PokerEngine.Models
{
    public class Player
    {
        private string name;
        private decimal money;
        private List<Card> cards;
        private Hand hand;
        private bool hasFolded;

        private IDecisionTaker decisionTaker;

        public Player(string name, decimal money, IDecisionTaker decisionTaker)
        {
            this.Name = name;
            this.Money = money;

            this.Cards = new List<Card>();
            this.HasFolded = false;

            this.DecisionTaker = decisionTaker;
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

        public bool HasFolded
        {
            get { return this.hasFolded; }
            internal set { this.hasFolded = value; }
        }

        internal IDecisionTaker DecisionTaker
        {
            get { return this.decisionTaker; }
            private set { this.decisionTaker = value; }
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