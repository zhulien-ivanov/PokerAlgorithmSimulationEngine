using System.Collections.Generic;

namespace PokerEngine.Models
{
    internal class Pot
    {
        private decimal amount;
        private decimal currentMaxStake;
        private Player currentMaxStakePosition;
        private List<Player> potentialWinners;
        private Dictionary<Player, decimal> currentPotAmount;
        private bool playerWentAllIn;

        public Pot(decimal amount, decimal currentMaxStake, List<Player> potentialWinners)
        {
            this.Amount = amount;
            this.CurrentMaxStake = currentMaxStake;
            this.PotentialWinners = potentialWinners;
            this.playerWentAllIn = false;

            this.currentPotAmount = new Dictionary<Player, decimal>();
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

        public Player CurrentMaxStakePosition
        {
            get { return this.currentMaxStakePosition; }
            internal set { this.currentMaxStakePosition = value; }
        }

        public List<Player> PotentialWinners
        {
            get { return this.potentialWinners; }
            internal set { this.potentialWinners = value; }
        }

        public Dictionary<Player, decimal> CurrentPotAmount
        {
            get { return this.currentPotAmount; }
            internal set { this.currentPotAmount = value; }
        }

        public bool PlayerWentAllIn
        {
            get { return this.playerWentAllIn; }
            internal set { this.playerWentAllIn = value; }
        }
    }
}
