using System.Collections.Generic;
using System.Linq;

using PokerEngine.Models.Enumerations;
using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Models
{
    public class Draw
    {
        private GameStage gameStage;
        private decimal fullPotAmount;
        private List<Player> players;
        private List<Card> tableCards;
        private List<PlayerAction> playerActions;
        private int dealerIndex;
        private Player dealerPosition;
        private Player smallBlindPosition;
        private Player bigBlindPosition;
        private decimal smallBlindAmount;
        private decimal bigBlindAmount;

        private int firstToBetIndex;

        private int playersAllInCount;
        private int playersFoldCount;

        private Dictionary<string, DrawPlayerPotInformation> playersPotInformation;

        private List<Pot> pots;
        private Pot currentPot;

        private Deck deck;

        private DrawContext drawContext;
        private StartGameContextInformation startGameContext;

        public Draw(List<Player> players, int dealerIndex, decimal smallBlindAmount, Deck deck)
        {
            this.Players = players;

            this.dealerIndex = dealerIndex;

            this.TableCards = new List<Card>();
            this.PlayerActions = new List<PlayerAction>();

            this.DealerPosition = Players[dealerIndex];
            this.SmallBlindPosition = Players[(dealerIndex + 1) % this.Players.Count];
            this.BigBlindPosition = Players[(dealerIndex + 2) % this.Players.Count];

            this.SmallBlindAmount = smallBlindAmount;
            this.BigBlindAmount = this.SmallBlindAmount * 2;

            this.firstToBetIndex = (dealerIndex + 3) % this.Players.Count;

            this.deck = deck;

            this.drawContext = this.BuildInitialContext();
            this.startGameContext = this.BuildStartGameContext();
        }

        public GameStage GameStage
        {
            get { return this.gameStage; }
            internal set { this.gameStage = value; }
        }

        public decimal FullPotAmount
        {
            get { return this.fullPotAmount; }
            internal set { this.fullPotAmount = value; }
        }

        public List<Player> Players
        {
            get { return this.players; }
            internal set { this.players = value; }
        }

        public List<Card> TableCards
        {
            get { return this.tableCards; }
            internal set { this.tableCards = value; }
        }

        public List<PlayerAction> PlayerActions
        {
            get { return this.playerActions; }
            internal set { this.playerActions = value; }
        }

        public Player DealerPosition
        {
            get { return this.dealerPosition; }
            internal set { this.dealerPosition = value; }
        }

        public Player SmallBlindPosition
        {
            get { return this.smallBlindPosition; }
            internal set { this.smallBlindPosition = value; }
        }

        public Player BigBlindPosition
        {
            get { return this.bigBlindPosition; }
            internal set { this.bigBlindPosition = value; }
        }

        public decimal SmallBlindAmount
        {
            get { return this.smallBlindAmount; }
            internal set { this.smallBlindAmount = value; }
        }

        public decimal BigBlindAmount
        {
            get { return this.bigBlindAmount; }
            internal set { this.bigBlindAmount = value; }
        }

        // Build general game context(caching)
        private DrawContext BuildInitialContext()
        {
            DrawContext drawContext;

            var contextPlayers = this.Players.Select(x => new PlayerInformation(x.Name, x.Money)).ToList();
            PlayerInformation dealerPosition = contextPlayers.FirstOrDefault(x => x.Name == this.DealerPosition.Name);
            PlayerInformation smallBlindPosition = contextPlayers.FirstOrDefault(x => x.Name == this.SmallBlindPosition.Name);
            PlayerInformation bigBlindPosition = contextPlayers.FirstOrDefault(x => x.Name == this.BigBlindPosition.Name);

            drawContext = new DrawContext(contextPlayers, dealerPosition, smallBlindPosition, bigBlindPosition);

            return drawContext;
        }

        // Build common start game context
        private StartGameContextInformation BuildStartGameContext()
        {
            StartGameContextInformation context;

            IReadOnlyCollection<PlayerInformation> players = this.drawContext.Players.AsReadOnly();

            PlayerInformation dealerPosition = this.drawContext.DealerPosition;
            PlayerInformation smallBlindPosition = this.drawContext.SmallBlindPosition;
            PlayerInformation bigBlindPosition = this.drawContext.BigBlindPosition;

            decimal smallBlindAmount = this.SmallBlindAmount;
            decimal bigBlindAmount = this.BigBlindAmount;

            context = new StartGameContextInformation(players, dealerPosition, smallBlindPosition, bigBlindPosition, smallBlindAmount, bigBlindAmount);

            return context;
        }

        // Build start game context for each player
        internal StartGameContext GetStartGameContextForPlayer(string playerName)
        {
            IReadOnlyCollection<Card> playerCards = this.Players.FirstOrDefault(x => x.Name == playerName).Cards.AsReadOnly();

            var context = new StartGameContext(this.startGameContext, playerCards);

            return context;
        }

        // Build turn game context for each player
        internal TurnContext GetTurnContextForPlayer(string playerName)
        {
            List<PlayerActionInformation> playerActionsList;
            IReadOnlyCollection<PlayerActionInformation> playerActions;

            var lastActionIndex = this.drawContext.PlayerActions.FindLastIndex(x => x.Player.Name == playerName);

            if (lastActionIndex < 0)
            {
                playerActions = this.drawContext.PlayerActions.AsReadOnly();
            }
            else
            {
                playerActionsList = new List<PlayerActionInformation>();

                for (int i = lastActionIndex + 1; i < this.drawContext.PlayerActions.Count; i++)
                {
                    playerActionsList.Add(this.drawContext.PlayerActions[i]);
                }

                playerActions = playerActionsList.AsReadOnly();
            }

            IReadOnlyCollection<Card> tableCards = this.TableCards.AsReadOnly();

            TurnContext context = new TurnContext(this.GameStage, playerActions, tableCards, this.CurrentPot);

            return context;
        }

        // Build end game context
        // TO DO!!!!

        internal void StartDraw()
        {
            this.Players.ForEach(x => x.HasFolded = false);

            this.playersAllInCount = 0;
            this.playersFoldCount = 0;

            // Create main pot
            this.pots = new List<Pot>();
            this.currentPot = new Pot(0, 0, this.Players);
            this.pots.Add(this.currentPot);

            // Shuffle cards
            this.deck.Shuffle();

            this.AdvanceToPreFlopStage();
            var bettingOutcome = this.AdvanceToBetting(this.firstToBetIndex, true);

            this.AdvanceToFlopStage();
            bettingOutcome = this.AdvanceToBetting(this.firstToBetIndex, false);

            this.AdvanceToTurnStage();
            bettingOutcome = this.AdvanceToBetting(this.firstToBetIndex, false);

            this.AdvanceToRiverStage();
            bettingOutcome = this.AdvanceToBetting(this.firstToBetIndex, false);

            this.AdvanceToShowdownStage();
        }

        private void HandleBettingOutcome(BettingOutcome bettingOutcome)
        {
            if (bettingOutcome == BettingOutcome.WinThroughFold)
            {

            }
            else if (bettingOutcome == BettingOutcome.AllInShowdown)
            {

            }
            else
            {

            }
        }

        private void AdvanceToPreFlopStage()
        {
            this.GameStage = GameStage.PreFlop;

            // SB and BB pay
            this.PaySmallBlind();
            this.PayBigBlind();

            // deal player cards
            this.DealPlayerCards();
        }

        private void AdvanceToFlopStage()
        {
            this.GameStage = GameStage.Flop;

            //deal table cards
        }

        private void AdvanceToTurnStage()
        {
            this.GameStage = GameStage.Turn;

            //deal table card
        }

        private void AdvanceToRiverStage()
        {
            this.GameStage = GameStage.River;

            //deal table card
        }

        private void AdvanceToShowdownStage()
        {
            this.GameStage = GameStage.Showdown;
        }

        private BettingOutcome AdvanceToBetting(int firstToBetIndex, bool blindBetting)
        {
            var playerIndex = firstToBetIndex;            
            var currentMaxStake = this.currentPot.CurrentMaxStake; // Used only in blindBetting.

            Player lastPosition = blindBetting ? this.BigBlindPosition : this.currentPot.CurrentMaxStakePosition;

            while (this.currentPot.PotentialWinners[playerIndex] != lastPosition)
            {
                var currentPlayer = this.currentPot.PotentialWinners[playerIndex];

                if (currentPlayer.HasFolded == false)
                {
                    this.ConcludePlayerDecision(currentPlayer);

                    if (this.Players.Count - 1 == this.playersFoldCount)
                    {
                        return BettingOutcome.WinThroughFold;
                    }

                    var playersLeftInGame = this.Players.Count - this.playersFoldCount;

                    if (playersLeftInGame == this.playersAllInCount || playersLeftInGame == this.playersAllInCount + 1)
                    {
                        return BettingOutcome.AllInShowdown;
                    }
                }

                playerIndex = (playerIndex + 1) % this.currentPot.PotentialWinners.Count;

                if (blindBetting == true)
                {
                    // A player has raised the stake to a higher one => migrate to normal betting finishing with the highest bidder.
                    if (currentMaxStake != this.currentPot.CurrentMaxStake)
                    {
                        return this.AdvanceToBetting(playerIndex, false);
                    }
                }
            }

            return BettingOutcome.ContinueBetting;
        }

        private void ConcludePlayerDecision(Player player)
        {
            var playerContext = this.GetTurnContextForPlayer(player.Name);
            var playerDecisionInformation = player.DecisionTaker.TakeDecision(playerContext);

            var playerDecisionCounter = 0;
            bool isValidDecision = true;

            string decisionMessage = string.Empty;

            while (playerDecisionCounter < 3)
            {
                switch (playerDecisionInformation.Decision)
                {
                    case Decision.Check:
                        isValidDecision = this.PlayerCheck(player);
                        break;
                    case Decision.Fold:
                        this.PlayerFold(player);
                        isValidDecision = true;
                        break;
                    case Decision.Call:
                        isValidDecision = this.PlayerCall(player);
                        break;
                    case Decision.Raise:
                        isValidDecision = this.PlayerRaise(player, playerDecisionInformation.Amount, out decisionMessage);
                        break;
                }

                if (isValidDecision == true)
                {
                    return;
                }
                else
                {
                    playerDecisionCounter++;
                }
            }

            // Take default action if 3 consecutive decisions aren't valid.
            if (this.PlayerCheck(player))
            {
                return;
            }
            else
            {
                this.PlayerFold(player);
            }
        }

        private PlayerInformation GetPlayerInformation(Player player)
        {
            var playerInfo = this.drawContext.Players.FirstOrDefault(x => x.Name == player.Name);

            return playerInfo;
        }

        private void SyncPlayerInformation(Player player)
        {
            var playerToSync = this.drawContext.Players.FirstOrDefault(x => x.Name == player.Name);

            playerToSync.Money = player.Money;
        }

        private void FilePlayerAction(PlayerAction playerAction)
        {
            this.PlayerActions.Add(playerAction);

            var playerActionInformation = new PlayerActionInformation(this.GetPlayerInformation(playerAction.Player), playerAction.Action, playerAction.Amount);

            this.drawContext.PlayerActions.Add(playerActionInformation);
        }

        private void DealPlayerCards()
        {
            var startIndex = this.dealerIndex + 1;
            var finishIndex = startIndex + this.Players.Count;
            int currentPlayerIndex;

            for (int i = startIndex; i < finishIndex; i++)
            {
                currentPlayerIndex = i % this.Players.Count;

                this.Players[currentPlayerIndex].Cards.Add(this.deck.DealCard());
                this.Players[currentPlayerIndex].Cards.Add(this.deck.DealCard());
            }
        }

        private bool InvestToPot(Player player, decimal amountToPay, out decimal amountInvested)
        {
            bool isAllIn = false;

            if (amountToPay < player.Money)
            {
                player.Money -= amountToPay;
                this.currentPot.Amount += amountToPay;

                amountInvested = amountToPay;
            }
            else
            {
                player.Money = 0;
                this.currentPot.Amount += player.Money;
                this.playersPotInformation[player.ToString()].IsAllIn = true;

                amountInvested = player.Money;
                isAllIn = true;

                this.playersAllInCount++;
            }

            if (amountInvested > this.currentPot.CurrentMaxStake)
            {
                this.currentPot.CurrentMaxStake = amountInvested;
            }

            this.currentPot.CurrentPotAmount[player.Name] += amountInvested;

            return isAllIn;
        }

        private void PaySmallBlind()
        {
            decimal amountInvested;

            var isAllIn = this.InvestToPot(this.SmallBlindPosition, this.SmallBlindAmount, out amountInvested);
            this.FilePlayerAction(new PlayerAction(this.SmallBlindPosition, Action.PaySmallBlind, amountInvested, isAllIn));
        }

        private void PayBigBlind()
        {
            decimal amountInvested;

            var isAllIn = this.InvestToPot(this.BigBlindPosition, this.BigBlindAmount, out amountInvested);
            this.FilePlayerAction(new PlayerAction(this.BigBlindPosition, Action.PayBigBlind, amountInvested, isAllIn));
        }

        private bool PlayerCall(Player player)
        {
            if (this.currentPot.CurrentPotAmount[player.Name] < this.currentPot.CurrentMaxStake)
            {
                decimal amountInvested;

                var isAllIn = this.InvestToPot(player, this.currentPot.CurrentMaxStake - this.currentPot.CurrentPotAmount[player.ToString()], out amountInvested);
                this.FilePlayerAction(new PlayerAction(player, Action.Call, amountInvested, isAllIn));

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PlayerRaise(Player player, decimal amountToRaise, out string message)
        {
            message = string.Empty;

            var potStakeDifference = this.currentPot.CurrentMaxStake - this.currentPot.CurrentPotAmount[player.Name];
            var minimalRaiseAmount = potStakeDifference + this.BigBlindAmount;

            if (player.Money <= potStakeDifference)
            {
                return false;
            }

            if (amountToRaise > player.Money)
            {
                message = string.Format("Invalid amount for raise. You are trying to raise {0} but you have only {1}.", amountToRaise, player.Money);
                return false;
            }

            if (amountToRaise < minimalRaiseAmount && player.Money > minimalRaiseAmount)
            {
                message = string.Format("Invalid amount for raise. You must raise with at least {0}.", minimalRaiseAmount);
                return false;
            }

            decimal amountInvested;

            var isAllIn = this.InvestToPot(player, amountToRaise, out amountInvested);
            this.FilePlayerAction(new PlayerAction(player, Action.Raise, amountInvested, isAllIn));

            return true;
        }

        private bool PlayerCheck(Player player)
        {
            if (this.currentPot.CurrentPotAmount[player.Name] == this.currentPot.CurrentMaxStake)
            {
                this.FilePlayerAction(new PlayerAction(player, Action.Check, 0, false));
                return true;
            }
            else
            {
                return false;
            }
        }

        private void PlayerFold(Player player)
        {
            this.FilePlayerAction(new PlayerAction(player, Action.Fold, 0, false));

            player.HasFolded = true;
            this.playersFoldCount++;

            if (player == this.currentPot.PotentialWinners[this.firstToBetIndex])
            {
                var newFirstToBetIndex = this.firstToBetIndex;

                while (true)
                {
                    newFirstToBetIndex = (newFirstToBetIndex + 1) % this.currentPot.PotentialWinners.Count;

                    if (!this.currentPot.PotentialWinners[newFirstToBetIndex].HasFolded)
                    {
                        this.firstToBetIndex = newFirstToBetIndex;
                        break;
                    }
                }
            }
        }

        private void SyncPots()
        {

        }
    }
}
