namespace PokerEngine.Models.Helpers
{
    public class BlindsInformation
    {
        private decimal smallBlindAmount;
        private decimal bigBlindAmount;

        public BlindsInformation(decimal smallBlindAmount, decimal bigBlindAmount)
        {
            this.SmallBlindAmount = smallBlindAmount;
            this.BigBlindAmount = bigBlindAmount;
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
