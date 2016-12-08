using System.Collections.Generic;

namespace PokerEngine.Models.Helpers
{
    public class PotInformation
    {
        private decimal amount;
        private decimal winAmount;
        private IReadOnlyCollection<FullPlayerInformation> winners;

        public PotInformation(decimal amount, IReadOnlyCollection<FullPlayerInformation> winners)
        {
            this.Amount = amount;            
            this.Winners = winners;

            this.WinAmount = this.Amount / this.Winners.Count;
        }

        public decimal Amount
        {
            get { return this.amount; }
            private set { this.amount = value; }
        }

        public decimal WinAmount
        {
            get { return this.winAmount; }
            private set { this.winAmount = value; }
        }

        public IReadOnlyCollection<FullPlayerInformation> Winners
        {
            get { return this.winners; }
            private set { this.winners = value; }
        }
    }
}
