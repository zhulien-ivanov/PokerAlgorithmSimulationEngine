using System.Collections.Generic;

using PokerEngine.Models.Enumerations;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.GameContexts
{
    public class FlopStageContext
    {
        private IReadOnlyCollection<Card> tableCards;
        private IReadOnlyCollection<PlayerActionInformation> playerActions;

        public FlopStageContext(IReadOnlyCollection<Card> tableCards, IReadOnlyCollection<PlayerActionInformation> playerActions)
        {
            this.TableCards = tableCards;
            this.PlayerActions = playerActions;
        }

        public GameStage GameStage
        {
            get { return GameStage.Flop; }
        }

        public IReadOnlyCollection<Card> TableCards
        {
            get { return this.tableCards; }
            private set { this.tableCards = value; }
        }

        public IReadOnlyCollection<PlayerActionInformation> PlayerActions
        {
            get { return this.playerActions; }
            private set { this.playerActions = value; }
        }
    }
}
