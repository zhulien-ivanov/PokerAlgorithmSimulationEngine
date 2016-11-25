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
                playerActionsList  = new List<PlayerActionInformation>();

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
            this.GameStage = GameStage.PreFlop;

            // Create main pot
            this.pots = new List<Pot>();
            this.currentPot = new Pot(0, 0, this.Players);
            this.pots.Add(this.currentPot);

            // Shuffle cards
            this.deck.Shuffle();

            // SB and BB pay
            this.PaySmallBlind();
            this.PayBigBlind();

            // deal player cards
            this.DealPlayerCards();            
        }

        internal void AdvanceToBetting()
        {

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

        private void PlayerCall(Player player)
        {
            decimal amountInvested;

            var isAllIn = this.InvestToPot(player, this.currentPot.CurrentMaxStake - this.currentPot.CurrentPotAmount[player.ToString()], out amountInvested);
            this.FilePlayerAction(new PlayerAction(player, Action.Call, amountInvested, isAllIn));
        }

        private DecisionFeedback PlayerRaise(Player player, decimal amountToRaise)
        {
            var wasOperationSuccessful = true;
            var message = string.Empty;
            var isAllIn = false;

            // CONSIDER DIFFERENCE FOR CALL + BB
            var minimalRaiseAmount = (this.currentPot.CurrentMaxStake - this.currentPot.CurrentPotAmount[player.Name]) + this.BigBlindAmount;
                        
            if (amountToRaise < minimalRaiseAmount && player.Money > minimalRaiseAmount)
            {
                wasOperationSuccessful = false;
                message = string.Format("Invalid amount for raise. You must raise with at least {0}.", minimalRaiseAmount);
            }
            else
            {
                decimal amountInvested;

                isAllIn = this.InvestToPot(player, amountToRaise, out amountInvested);
                this.FilePlayerAction(new PlayerAction(player, Action.Raise, amountInvested, isAllIn));
            }

            var decisionFeedback = new DecisionFeedback(wasOperationSuccessful, isAllIn, message);

            return decisionFeedback;
        }

        private void PlayerCheck(Player player)
        {
            this.FilePlayerAction(new PlayerAction(player, Action.Check, 0, false));
        }

        private void PlayerFold(Player player)
        {
            this.FilePlayerAction(new PlayerAction(player, Action.Fold, 0, false));

            // remove player from list
        }

        private void SyncPots()
        {

        }
    }
}
