using System;
using System.Collections.Generic;
using System.Linq;

using PokerEngine.Models;
using PokerEngine.Models.Helpers;

using PokerEngine.Logic.Contracts;

namespace PokerEngine.Logic
{
    public class Table
    {
        private Random randomGenerator;
        private int currentDealerIndex;

        private Deck deck;

        private List<Player> players;
        private List<Draw> draws;

        private Draw currentDraw;

        private IBlindsEvaluator blindsEvaluator;
        private IPlayerHandEvaluator handEvaluator;

        private BlindsDrawContext blindsDrawContext;
        private List<DrawInformation> drawsInfo;

        public Table(List<Player> players, IBlindsEvaluator blindsEvaluator, IPlayerHandEvaluator handEvaluator)
        {
            this.randomGenerator = new Random();
            this.currentDealerIndex = this.randomGenerator.Next(0, this.Players.Count);

            this.deck = new Deck();

            this.Players = players;
            this.Draws = new List<Draw>();

            this.blindsEvaluator = blindsEvaluator;
            this.handEvaluator = handEvaluator;

            this.blindsDrawContext = new BlindsDrawContext();
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

        internal void StartGame()
        {
            while (this.Players.Count > 1)
            {
                this.CurrentDraw = new Draw(this.Players, this.currentDealerIndex, this.blindsEvaluator.GetBlindAmounts(this.blindsDrawContext), deck, this.handEvaluator);

                this.CurrentDraw.StartDraw();

                this.HandleDrawResults(this.CurrentDraw);
            }
        }

        private void HandleDrawResults(Draw draw)
        {
            this.SaveDrawInformation(draw);
            
            List<Player> playersLeft = new List<Player>();

            for (int i = 0; i < draw.Players.Count; i++)
            {
                if (draw.Players[i].Money > 0)
                {
                    playersLeft.Add(draw.Players[i]);
                }
                else
                {
                    // Logger - player leave the table (bankrupt)
                }
            }

            this.Players = new List<Player>(playersLeft);

            this.currentDealerIndex = (this.currentDealerIndex + 1) % this.Players.Count;
        }

        private void SaveDrawInformation(Draw draw)
        {
            this.Draws.Add(draw);

            var drawInfo = new DrawInformation(draw.Players.Count, draw.FullPotAmount, draw.SmallBlindAmount, draw.BigBlindAmount);

            this.blindsDrawContext.DrawsInfo.Add(drawInfo);
        }
    }
}
