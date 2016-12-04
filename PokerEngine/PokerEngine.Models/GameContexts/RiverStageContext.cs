using System.Collections.Generic;

using PokerEngine.Models.Enumerations;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.GameContexts
{
    public class RiverStageContext
    {
        private Card tableCard;
        private IReadOnlyCollection<PlayerActionInformation> playerActions;

        public RiverStageContext(Card tableCard, IReadOnlyCollection<PlayerActionInformation> playerActions)
        {
            this.TableCard = tableCard;
            this.PlayerActions = playerActions;
        }

        public GameStage GameStage
        {
            get { return GameStage.River; }
        }

        public Card TableCard
        {
            get { return this.tableCard; }
            private set { this.tableCard = value; }
        }

        public IReadOnlyCollection<PlayerActionInformation> PlayerActions
        {
            get { return this.playerActions; }
            private set { this.playerActions = value; }
        }
    }
}
