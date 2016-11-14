using System.Collections.Generic;

using PokerEngine.Models.Enumerations;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.GameContexts
{
    internal class TurnContext
    {
        private GameStage gameStage;
        private IReadOnlyCollection<PlayerActionInformation> playerActions;                
        private IReadOnlyCollection<Card> tableCards;
        private decimal currentPot;

        public TurnContext(GameStage gameStage, IReadOnlyCollection<PlayerActionInformation> playerActions, IReadOnlyCollection<Card> tableCards, decimal currentPot)
        {
            this.GameStage = gameStage;
            this.PlayerActions = playerActions;
            this.TableCards = tableCards;
            this.CurrentPot = currentPot;
        }

        public GameStage GameStage
        {
            get { return this.gameStage; }
            private set { this.gameStage = value; }
        }

        public IReadOnlyCollection<PlayerActionInformation> PlayerActions
        {
            get { return this.playerActions; }
            private set { this.playerActions = value; }
        }
        
        public IReadOnlyCollection<Card> TableCards
        {
            get { return this.tableCards; }
            private set { this.tableCards = value; }
        }

        public decimal CurrentPot
        {
            get { return this.currentPot; }
            private set { this.currentPot = value; }
        }
    }
}
