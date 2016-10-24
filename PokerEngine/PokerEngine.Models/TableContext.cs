using System.Collections.Generic;

namespace PokerEngine.Models
{
    public class TableContext
    {
        private List<Player> players;
        private List<Draw> draws;

        private Draw currentDraw;

        public TableContext(List<Player> players, List<Draw> draws, Draw currentDraw)
        {
            this.Players = players;
            this.Draws = draws;
            this.CurrentDraw = currentDraw;
        }

        public List<Player> Players
        {
            get { return this.players; }
            set { this.players = value; }
        }

        public List<Draw> Draws
        {
            get { return this.draws; }
            set { this.draws = value; }
        }

        public Draw CurrentDraw
        {
            get { return this.currentDraw; }
            set { this.currentDraw = value; }
        }
    }
}
