using System;
using System.Collections.Generic;

using PokerEngine.Models;
using PokerEngine.Models.Contracts;


namespace PokerEngine.Logic
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
            internal set { this.players = value; }
        }

        public List<Draw> Draws
        {
            get { return this.draws; }
            internal set { this.draws = value; }
        }

        public Draw CurrentDraw
        {
            get { return this.currentDraw; }
            internal set { this.currentDraw = value; }
        }

        private void AdvanceToNextDraw()
        {
            // add finished draw to the draw history
            this.Draws.Add(this.CurrentDraw);

            this.currentDealerIndex = (this.currentDealerIndex + 1) % this.Players.Count;

            // remove bankrupt players + check for winner -- NO!            

            // create new draw
            // should increase blind amounts? -- calculate new blinds via the blindEvaluator

            // prepare new draw
            //this.CurrentDraw = new Draw(this.CurrentDraw.Players, this.currentDealerIndex, this.blindsEvaluator.GetSmallBlindAmount(this.BuildContext()));
        }
    }
}
