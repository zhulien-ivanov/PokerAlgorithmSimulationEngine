using System;
using System.Collections.Generic;

using PokerEngine.Models.Enumerations;

namespace PokerEngine.Models
{
    public class Draw
    {
        private GameStage gameStage;
        private decimal currentPot;
        private List<Player> players;
        private List<Card> tableCards;
        private List<PlayerAction> playerActions;
        private Player dealerPosition;
        private Player smallBlindPosition;
        private Player bigBlindPosition;
        private decimal smallBlindAmount;
        private decimal bigBlindAmount;

        public Draw(List<Player> players, int dealerIndex, decimal smallBlindAmount)
        {
            this.GameStage = GameStage.PreFlop;

            this.Players = players;

            this.TableCards = new List<Card>();
            this.PlayerActions = new List<PlayerAction>();
            
            this.DealerPosition = Players[dealerIndex];
            this.SmallBlindPosition = Players[(dealerIndex + 1) % this.Players.Count];
            this.BigBlindPosition = Players[(dealerIndex + 2) % this.Players.Count];

            this.SmallBlindAmount = smallBlindAmount;
            this.BigBlindAmount = this.SmallBlindAmount * 2;
        }

        public GameStage GameStage
        {
            get { return this.gameStage; }
            set { this.gameStage = value; }
        }

        public decimal CurrentPot
        {
            get { return this.currentPot; }
            set { this.currentPot = value; }
        }

        public List<Player> Players
        {
            get { return this.players; }
            set { this.players = value; }
        }

        public List<Card> TableCards
        {
            get { return this.tableCards; }
            set { this.tableCards = value; }
        }

        public List<PlayerAction> PlayerActions
        {
            get { return this.playerActions; }
            set { this.playerActions = value; }
        }

        public Player DealerPosition
        {
            get { return this.dealerPosition; }
            set { this.dealerPosition = value; }
        }

        public Player SmallBlindPosition
        {
            get { return this.smallBlindPosition; }
            set { this.smallBlindPosition = value; }
        }

        public Player BigBlindPosition
        {
            get { return this.bigBlindPosition; }
            set { this.bigBlindPosition = value; }
        }

        public decimal SmallBlindAmount
        {
            get { return this.smallBlindAmount; }
            set { this.smallBlindAmount = value; }
        }

        public decimal BigBlindAmount
        {
            get { return this.bigBlindAmount; }
            set { this.bigBlindAmount = value; }
        }
    }
}
