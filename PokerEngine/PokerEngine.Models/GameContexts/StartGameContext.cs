using System.Collections.Generic;

namespace PokerEngine.Models.GameContexts
{
    public class StartGameContext
    {
        private StartGameContextInformation drawInformation;
        private IReadOnlyCollection<Card> myCards;

        public StartGameContext(StartGameContextInformation drawInformation, IReadOnlyCollection<Card> myCards)
        {
            this.DrawInformation = drawInformation;
            this.MyCards = myCards;
        }

        public StartGameContextInformation DrawInformation
        {
            get { return this.drawInformation; }
            private set { this.drawInformation = value; }
        }

        public IReadOnlyCollection<Card> MyCards
        {
            get { return this.myCards; }
            private set { this.myCards = value; }
        }
    }
}
