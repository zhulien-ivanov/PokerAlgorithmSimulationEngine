using System.Collections.Generic;

namespace PokerEngine.Models.Helpers
{
    public class BlindsDrawContext
    {
        private List<DrawInformation> drawsInfo;
        
        public BlindsDrawContext()
        {
            this.DrawsInfo = new List<DrawInformation>();
        }

        public List<DrawInformation> DrawsInfo
        {
            get { return this.drawsInfo; }
            private set { this.drawsInfo = value; }
        }
    }
}
