using System;
using System.Collections.Generic;
using System.Linq;

using PokerEngine.Models;
using PokerEngine.Models.Helpers;

using PokerEngine.Logic.Contracts;

using PokerEngine.Helpers.Contracts;

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

        private ILogger logger;

        private BlindsDrawContext blindsDrawContext;
        
        public Table(List<Player> players, IBlindsEvaluator blindsEvaluator, IPlayerHandEvaluator handEvaluator, ILogger logger)
        {
            this.Players = players;

            this.blindsEvaluator = blindsEvaluator;
            this.handEvaluator = handEvaluator;

            this.logger = logger;

            this.randomGenerator = new Random();
            //this.currentDealerIndex = this.randomGenerator.Next(0, this.Players.Count);
            this.currentDealerIndex = 0;

            this.deck = new Deck();

            this.Draws = new List<Draw>();

            this.blindsDrawContext = new BlindsDrawContext();
        }

        internal List<Player> Players
        {
            get { return this.players; }
            private set { this.players = value; }
        }

        internal List<Draw> Draws
        {
            get { return this.draws; }
            private set { this.draws = value; }
        }

        internal Draw CurrentDraw
        {
            get { return this.currentDraw; }
            private set { this.currentDraw = value; }
        }

        public void StartGame()
        {
            this.LogOpenTableInformation();

            while (this.Players.Count > 1)
            {
                this.CurrentDraw = new Draw(this.Players, this.currentDealerIndex, this.blindsEvaluator.GetBlindAmounts(this.blindsDrawContext), deck, this.handEvaluator, this.logger);

                this.CurrentDraw.StartDraw();

                this.HandleDrawResults(this.CurrentDraw);
            }

            this.logger.AddSeparator();
            this.logger.Log(String.Format("Player \"{0}\" wins the game.", this.GetTableWinner()));
            this.logger.AddSeparator();
        }

        private Player GetTableWinner()
        {
            var winner = this.Draws.Last().Players.FirstOrDefault(x => x.Money > 0);

            return winner;
        }

        private void LogOpenTableInformation()
        {
            this.logger.Log(String.Format("Table opens with {0} players.", this.Players.Count));

            foreach (var player in this.Players)
            {
                this.logger.Log(String.Format("Player \"{0}\" joins the table.", player.Name));
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
