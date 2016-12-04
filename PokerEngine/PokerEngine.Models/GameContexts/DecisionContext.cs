using System.Collections.Generic;

using PokerEngine.Models.Enumerations;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.GameContexts
{
    public class DecisionContext
    {
        private IReadOnlyCollection<PlayerActionInformation> playerActions;
        private decimal currentPot;

        public DecisionContext(IReadOnlyCollection<PlayerActionInformation> playerActions, decimal currentPot)
        {
            this.PlayerActions = playerActions;
            this.CurrentPot = currentPot;
        }

        public IReadOnlyCollection<PlayerActionInformation> PlayerActions
        {
            get { return this.playerActions; }
            private set { this.playerActions = value; }
        }

        public decimal CurrentPot
        {
            get { return this.currentPot; }
            private set { this.currentPot = value; }
        }
    }
}
