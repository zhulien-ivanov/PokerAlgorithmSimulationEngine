using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokerEngine.Models.Contracts;

namespace PokerEngine.Models
{
    public class Table
    {
        private Random randomGenerator;
        private int currentDealerIndex;

        private List<Player> players;        
        private List<Draw> draws;

        private Draw currentDraw;

        private IBlindsEvaluator blindsEvaluator;

        public Table(List<Player> players, IBlindsEvaluator blindsEvaluator)
        {
            this.randomGenerator = new Random();
            this.currentDealerIndex = this.randomGenerator.Next(0, this.Players.Count);

            this.Players = players;
            this.Draws = new List<Draw>();

            this.blindsEvaluator = blindsEvaluator;
            //this.CurrentDraw = new Draw(this.Players, this.currentDealerIndex, this.blindsEvaluator.GetSmallBlindAmount(this.BuildContext()));
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

        private void AdvanceToNextDraw()
        {
            this.currentDealerIndex = (this.currentDealerIndex + 1) % this.Players.Count;

            // add finished draw to the draw history

            // remove bankrupt players + check for winner
            // create new draw
            // should increase blind amounts?

            // prepare new draw
        }        

        private TableContext BuildContext()
        {
            throw new NotImplementedException();
        }
    }
}
