using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using PokerEngine.Models.Helpers;

namespace PokerEngine.Models
{
    public class DrawContext
    {
        private List<PlayerInformation> players;
        private PlayerInformation dealerPosition;
        private PlayerInformation smallBlindPosition;
        private PlayerInformation bigBlindPosition;

        private List<PlayerActionInformation> playerActions;

        public DrawContext(List<PlayerInformation> players, PlayerInformation dealerPosition, PlayerInformation smallBlindPosition, PlayerInformation bigBlindPosition)
        {
            this.Players = players;
            this.DealerPosition = dealerPosition;
            this.SmallBlindPosition = smallBlindPosition;
            this.BigBlindPosition = bigBlindPosition;

            this.PlayerActions = new List<PlayerActionInformation>();
        }

        public List<PlayerInformation> Players
        {
            get { return this.players; }
            internal set { this.players = value; }
        }

        public PlayerInformation DealerPosition
        {
            get { return this.dealerPosition; }
            internal set { this.dealerPosition = value; }
        }

        public PlayerInformation SmallBlindPosition
        {
            get { return this.smallBlindPosition; }
            internal set { this.smallBlindPosition = value; }
        }

        public PlayerInformation BigBlindPosition
        {
            get { return this.bigBlindPosition; }
            internal set { this.bigBlindPosition = value; }
        }

        public List<PlayerActionInformation> PlayerActions
        {
            get { return this.playerActions; }
            internal set { this.playerActions = value; }
        }
    }
}
