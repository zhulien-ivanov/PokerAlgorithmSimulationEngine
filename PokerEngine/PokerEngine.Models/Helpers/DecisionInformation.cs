using PokerEngine.Models.Enumerations;

namespace PokerEngine.Models.Helpers
{
    public class DecisionInformation
    {
        private Decision decision;
        private decimal amount;

        public DecisionInformation(Decision decision, decimal amount)
        {
            this.Decision = decision;
            this.Amount = amount;
        }

        public Decision Decision
        {
            get { return this.decision; }
            private set { this.decision = value; }
        }

        public decimal Amount
        {
            get { return this.amount; }

            private set
            {
                var amountToSet = value;

                if (amountToSet < 0)
                {
                    amountToSet = 0;
                }

                this.amount = amountToSet;
            }
        }
    }
}
