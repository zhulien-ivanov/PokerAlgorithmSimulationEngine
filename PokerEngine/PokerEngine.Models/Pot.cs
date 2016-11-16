using System.Collections.Generic;

namespace PokerEngine.Models
{
    public class Pot
    {
        private decimal amount;
        private decimal currentMaxStake;
        private List<Player> potentialWinners;
        private Dictionary<string, decimal> currentPotAmount;

        public Pot(decimal amount, decimal currentMaxStake, List<Player> potentialWinners)
        {
            this.Amount = amount;
            this.CurrentMaxStake = currentMaxStake;
            this.PotentialWinners = potentialWinners;
        }

        public decimal Amount
        {
            get { return this.amount; }
            internal set { this.amount = value; }
        }

        public decimal CurrentMaxStake
        {
            get { return this.currentMaxStake; }
            internal set { this.currentMaxStake = value; }
        }

        public List<Player> PotentialWinners
        {
            get { return this.potentialWinners; }
            private set { this.potentialWinners = value; }
        }

        public Dictionary<string, decimal> CurrentPotAmount
        {
            get { return this.currentPotAmount; }
            private set { this.currentPotAmount = value; }
        }
    }
}
