﻿using PokerEngine.Models.Enumerations;

namespace PokerEngine.Models
{
    public class PlayerAction
    {
        private Player player;
        private Decision decision;
        private decimal amount;

        public PlayerAction(Player player, Decision decision, decimal amount)
        {
            this.Player = player;
            this.Decision = decision;
            this.Amount = amount;
        }

        public Player Player
        {
            get { return this.player; }
            internal set { this.player = value; }
        }

        public Decision Decision
        {
            get { return this.decision; }
            internal set { this.decision = value; }
        }

        public decimal Amount
        {
            get { return this.amount; }
            internal set
            {
                if (this.Decision == Decision.Check || this.Decision == Decision.Fold)
                {
                    this.amount = 0;
                }
                else
                {
                    this.amount = value;
                }                
            }
        }
    }
}
