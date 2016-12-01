using System.Collections.Generic;

using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.GameContexts
{
    public class StartGameContextInformation
    {
        IReadOnlyCollection<PlayerInformation> players;
        PlayerInformation dealerPosition;
        PlayerInformation smallBlindPosition;
        PlayerInformation bigBlindPosition;
        decimal smallBlindAmount;
        decimal bigBlindAmount;

        public StartGameContextInformation(IReadOnlyCollection<PlayerInformation> players, PlayerInformation dealerPosition, PlayerInformation smallBlindPosition, PlayerInformation bigBlindPosition, decimal smallBlindAmount, decimal bigBlindAmount)
        {
            this.Players = players;
            this.DealerPosition = dealerPosition;
            this.SmallBlindPosition = smallBlindPosition;
            this.BigBlindPosition = BigBlindPosition;
            this.SmallBlindAmount = smallBlindAmount;
            this.BigBlindAmount = bigBlindAmount;
        }

        public IReadOnlyCollection<PlayerInformation> Players
        {
            get { return this.players; }
            private set { this.players = value; }
        }

        public PlayerInformation DealerPosition
        {
            get { return this.dealerPosition; }
            private set { this.dealerPosition = value; }
        }

        public PlayerInformation SmallBlindPosition
        {
            get { return this.smallBlindPosition; }
            private set { this.smallBlindPosition = value; }
        }

        public PlayerInformation BigBlindPosition
        {
            get { return this.bigBlindPosition; }
            private set { this.bigBlindPosition = value; }
        }

        public decimal SmallBlindAmount
        {
            get { return this.smallBlindAmount; }
            private set { this.smallBlindAmount = value; }
        }

        public decimal BigBlindAmount
        {
            get { return this.bigBlindAmount; }
            private set { this.bigBlindAmount = value; }
        }
    }
}
