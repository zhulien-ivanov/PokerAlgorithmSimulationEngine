namespace PokerEngine.Models.Helpers
{
    public class DrawInformation
    {
        private int playersCount;
        private decimal potAmount;
        private decimal smallBlindAmount;
        private decimal bigBlindAmount;

        public DrawInformation(int playersCount, decimal potAmount, decimal smallBlindAmount, decimal bigBlindAmount)
        {
            this.PlayersCount = playersCount;
            this.PotAmount = potAmount;
            this.SmallBlindAmount = smallBlindAmount;
            this.BigBlindAmount = bigBlindAmount;
        }

        public int PlayersCount
        {
            get { return this.playersCount; }
            private set { this.playersCount = value; }
        }

        public decimal PotAmount
        {
            get { return this.potAmount; }
            private set { this.potAmount = value; }
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
