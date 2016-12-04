using System.Collections.Generic;

namespace PokerEngine.Models
{
    public class Pot
    {
        private decimal amount;
        private List<Player> potentialWinners;
        private decimal currentMaxStake;
        private Player currentMaxStakePosition;
        private Dictionary<Player, decimal> currentPotAmount;
        private bool playerWentAllIn;

        public Pot() : this(0, new List<Player>())
        {
        }

        public Pot(decimal amount, List<Player> potentialWinners)
        {
            this.Amount = amount;            
            this.PotentialWinners = potentialWinners;
            this.CurrentMaxStake = 0;

            this.playerWentAllIn = false;

            this.currentPotAmount = new Dictionary<Player, decimal>();
        }

        public decimal Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }

        public List<Player> PotentialWinners
        {
            get { return this.potentialWinners; }
            set { this.potentialWinners = value; }
        }

        public decimal CurrentMaxStake
        {
            get { return this.currentMaxStake; }
            set { this.currentMaxStake = value; }
        }

        public Player CurrentMaxStakePosition
        {
            get { return this.currentMaxStakePosition; }
            set { this.currentMaxStakePosition = value; }
        }

        public Dictionary<Player, decimal> CurrentPotAmount
        {
            get { return this.currentPotAmount; }
            set { this.currentPotAmount = value; }
        }

        public bool PlayerWentAllIn
        {
            get { return this.playerWentAllIn; }
            set { this.playerWentAllIn = value; }
        }
    }
}
