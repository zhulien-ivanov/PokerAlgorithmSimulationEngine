using PokerEngine.Models.Enumerations;

namespace PokerEngine.Models
{
    public class PlayerAction
    {
        private Player player;
        private Action action;
        private decimal amount;
        private bool isAllIn;

        public PlayerAction(Player player, Action action, decimal amount, bool isAllIn)
        {
            this.Player = player;
            this.Action = action;
            this.Amount = amount;
            this.IsAllIn = isAllIn;
        }

        public Player Player
        {
            get { return this.player; }
            internal set { this.player = value; }
        }

        public Action Action
        {
            get { return this.action; }
            internal set { this.action = value; }
        }

        public decimal Amount
        {
            get { return this.amount; }
            internal set
            {
                if (this.Action == Action.Check || this.Action == Action.Fold)
                {
                    this.amount = 0;
                }
                else
                {
                    this.amount = value;
                }                
            }
        }

        public bool IsAllIn
        {
            get { return this.isAllIn; }
            private set
            {
                if (this.Action == Action.Check || this.Action == Action.Fold)
                {
                    this.isAllIn = false;
                }
                else
                {
                    this.isAllIn = value;
                }
            }
        }
    }
}
