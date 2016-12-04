using System.Collections.Generic;

using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.GameContexts
{
    public class AllFoldContext
    {
        private PlayerInformation winner;
        private decimal potAmount;
        private IReadOnlyCollection<PlayerActionInformation> playerActions;

        public AllFoldContext(PlayerInformation winner, decimal potAmount, IReadOnlyCollection<PlayerActionInformation> playerActions)
        {
            this.Winner = winner;
            this.PotAmount = potAmount;
            this.PlayerActions = playerActions;
        }

        public PlayerInformation Winner
        {
            get { return this.winner; }
            private set { this.winner = value; }
        }

        public decimal PotAmount
        {
            get { return this.potAmount; }
            private set { this.potAmount = value; }
        }

        public IReadOnlyCollection<PlayerActionInformation> PlayerActions
        {
            get { return this.playerActions; }
            private set { this.playerActions = value; }
        }
    }
}
