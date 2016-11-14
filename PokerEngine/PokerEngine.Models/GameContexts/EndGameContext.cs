using System.Collections.Generic;

using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.GameContexts
{
    internal class EndGameContext
    {
        private IReadOnlyCollection<EndGamePlayerInformation> players;
        private IReadOnlyCollection<IReadOnlyCollection<EndGamePlayerInformation>> winningPlayers;

        public EndGameContext(IReadOnlyCollection<EndGamePlayerInformation> players, IReadOnlyCollection<IReadOnlyCollection<EndGamePlayerInformation>> winningPlayers)
        {
            this.Players = players;
            this.WinningPlayers = winningPlayers;
        }

        public IReadOnlyCollection<EndGamePlayerInformation> Players
        {
            get { return this.players; }
            private set { this.players = value; }
        }

        public IReadOnlyCollection<IReadOnlyCollection<EndGamePlayerInformation>> WinningPlayers
        {
            get { return this.winningPlayers; }
            private set { this.winningPlayers = value; }
        }
    }
}
