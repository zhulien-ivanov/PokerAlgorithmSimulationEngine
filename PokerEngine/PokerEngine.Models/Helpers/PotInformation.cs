using System.Collections.Generic;

namespace PokerEngine.Models.Helpers
{
    public class PotInformation
    {
        private decimal amount;
        private IReadOnlyDictionary<EndGamePlayerInformation, decimal> amountInvested;
        private IReadOnlyCollection<EndGamePlayerInformation> winners;

        public PotInformation(decimal amount, IReadOnlyDictionary<EndGamePlayerInformation, decimal> amountInvested, IReadOnlyCollection<EndGamePlayerInformation> winners)
        {
            this.Amount = amount;
            this.AmountInvested = amountInvested;
            this.Winners = winners;
        }

        public decimal Amount
        {
            get { return this.amount; }
            private set { this.amount = value; }
        }

        public IReadOnlyDictionary<EndGamePlayerInformation, decimal> AmountInvested
        {
            get { return this.amountInvested; }
            private set { this.amountInvested = value; }
        }

        public IReadOnlyCollection<EndGamePlayerInformation> Winners
        {
            get { return this.winners; }
            private set { this.winners = value; }
        }
    }
}
