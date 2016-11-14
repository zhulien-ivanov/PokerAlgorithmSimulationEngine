using PokerEngine.Models.Enumerations;

namespace PokerEngine.Models.Helpers
{
    public class PlayerActionInformation
    {
        private PlayerInformation player;
        private Decision decision;
        private decimal amount;

        public PlayerActionInformation(PlayerInformation player, Decision decision, decimal amount)
        {
            this.Player = player;
            this.Decision = decision;
            this.Amount = amount;
        }

        public PlayerInformation Player
        {
            get { return this.player; }
            private set { this.player = value; }
        }

        public Decision Decision
        {
            get { return this.decision; }
            private set { this.decision = value; }
        }

        public decimal Amount
        {
            get { return this.amount; }
            private set
            {
                if (this.Decision == Decision.Check || this.Decision == Decision.Fold)
                {
                    this.amount = 0;
                }
                else
                {
                    this.amount = value;
                }
            }
        }
    }
}
