namespace PokerEngine.Models.Helpers
{
    public class DecisionFeedback
    {
        private bool wasActionSuccessful;
        private bool wentAllIn;
        private string message;
        
        public DecisionFeedback(bool wasActionSuccessful, bool wentAllIn, string message)
        {
            this.WasActionSuccessful = wasActionSuccessful;
            this.WentAllIn = wentAllIn;
            this.Message = message;
        }

        public bool WasActionSuccessful
        {
            get { return this.wasActionSuccessful; }
            private set { this.wasActionSuccessful = value; }
        }

        public bool WentAllIn
        {
            get { return this.wentAllIn; }
            private set { this.wentAllIn = value; }
        }

        public string Message
        {
            get { return this.message; }
            private set { this.message = value; }
        }
    }
}
