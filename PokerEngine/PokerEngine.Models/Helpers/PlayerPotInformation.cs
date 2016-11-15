namespace PokerEngine.Models.Helpers
{
    internal class PlayerPotInformation
    {
        private decimal drawPotAmount;
        private bool isAllIn;

        public PlayerPotInformation(decimal drawPotAmount)
        {
            this.DrawPotAmount = drawPotAmount;
        }

        public decimal DrawPotAmount
        {
            get { return this.drawPotAmount; }
            internal set { this.drawPotAmount = value; }
        }

        public bool IsAllIn
        {
            get { return this.isAllIn; }
            internal set { this.isAllIn = value; }
        }
    }
}
