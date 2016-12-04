using System.Collections.Generic;

using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.GameContexts
{
    public class EndGameContext
    {
        private IReadOnlyCollection<PotInformation> pots;

        public EndGameContext(IReadOnlyCollection<PotInformation> pots)
        {
            this.Pots = pots;
        }

        public IReadOnlyCollection<PotInformation> Pots
        {
            get { return this.pots; }
            private set { this.pots = value; }
        }
    }
}
