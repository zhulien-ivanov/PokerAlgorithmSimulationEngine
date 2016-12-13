using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using PokerEngine.Models;
using PokerEngine.Models.Enumerations;
using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;
using PokerEngine.Logic.Contracts;
using PokerEngine.Helpers.Contracts;

[assembly: InternalsVisibleTo("PokerEngine.Logic.Tests")]
namespace PokerEngine.Logic
{    
    internal class Draw
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

        private Dictionary<Player, decimal> currentDrawAmount;

        private Queue<Pot> pots;
        private Pot currentPot;

        private Deck deck;

        private IPlayerHandEvaluator handEvaluator;

        private ILogger logger;

        private DrawContext drawContext; //?
        private StartGameContextInformation startGameContext; //?

        private Dictionary<Player, int> lastActionIndexSent;

        internal Draw(List<Player> players, int dealerIndex, BlindsInformation blindsInformation, Deck deck, IPlayerHandEvaluator handEvaluator, ILogger logger)
        {
            this.Players = players;

            this.dealerIndex = dealerIndex;

            this.TableCards = new List<Card>();
            this.PlayerActions = new List<PlayerAction>();

            var smallBlindIndex = (dealerIndex + 1) % this.Players.Count;
            var bigBlindIndex = (dealerIndex + 2) % this.Players.Count;

            this.DealerPosition = Players[dealerIndex];
            this.SmallBlindPosition = Players[smallBlindIndex];
            this.BigBlindPosition = Players[bigBlindIndex];

            this.SmallBlindAmount = blindsInformation.SmallBlindAmount;
            this.BigBlindAmount = blindsInformation.BigBlindAmount;

            this.firstToBetIndex = smallBlindIndex;

            this.currentDrawAmount = new Dictionary<Player, decimal>();

            foreach (var player in this.Players)
            {
                currentDrawAmount[player] = 0;
            }

            this.deck = deck;

            this.handEvaluator = handEvaluator;

            this.logger = logger;

            this.drawContext = this.BuildInitialContext();
            this.startGameContext = this.BuildStartGameContext();
        }

        internal GameStage GameStage
        {
            get { return this.gameStage; }
            private set { this.gameStage = value; }
        }

        internal decimal FullPotAmount
        {
            get { return this.fullPotAmount; }
            private set { this.fullPotAmount = value; }
        }

        internal List<Player> Players
        {
            get { return this.players; }
            private set { this.players = value; }
        }

        internal List<Card> TableCards
        {
            get { return this.tableCards; }
            private set { this.tableCards = value; }
        }

        internal List<PlayerAction> PlayerActions
        {
            get { return this.playerActions; }
            private set { this.playerActions = value; }
        }

        internal Player DealerPosition
        {
            get { return this.dealerPosition; }
            private set { this.dealerPosition = value; }
        }

        internal Player SmallBlindPosition
        {
            get { return this.smallBlindPosition; }
            private set { this.smallBlindPosition = value; }
        }

        internal Player BigBlindPosition
        {
            get { return this.bigBlindPosition; }
            private set { this.bigBlindPosition = value; }
        }

        internal decimal SmallBlindAmount
        {
            get { return this.smallBlindAmount; }
            private set { this.smallBlindAmount = value; }
        }

        internal decimal BigBlindAmount
        {
            get { return this.bigBlindAmount; }
            private set { this.bigBlindAmount = value; }
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

            IReadOnlyCollection<PlayerInformation> players = this.drawContext.PlayersInformation.AsReadOnly();

            PlayerInformation dealerPosition = this.drawContext.DealerPosition;
            PlayerInformation smallBlindPosition = this.drawContext.SmallBlindPosition;
            PlayerInformation bigBlindPosition = this.drawContext.BigBlindPosition;

            decimal smallBlindAmount = this.SmallBlindAmount;
            decimal bigBlindAmount = this.BigBlindAmount;

            context = new StartGameContextInformation(players, dealerPosition, smallBlindPosition, bigBlindPosition, smallBlindAmount, bigBlindAmount);

            return context;
        }

        // Build all fold context for each player
        private AllFoldContext GetAllFoldContextForEachPlayer(Player player)
        {
            IReadOnlyCollection<PlayerActionInformation> playerActions = this.GetPlayerLastActions(player);

            var winner = this.Players.FirstOrDefault(x => !x.HasFolded);

            AllFoldContext context = new AllFoldContext(this.GetPlayerInformation(winner), this.FullPotAmount, playerActions);

            return context;
        }

        // Build start game context for each player
        private StartGameContext GetStartGameContextForPlayer(Player player)
        {
            IReadOnlyCollection<Card> playerCards = this.Players.FirstOrDefault(x => x.Name == player.Name).Cards.AsReadOnly();

            var context = new StartGameContext(this.startGameContext, playerCards);

            return context;
        }

        // Build decision game context for each player
        private DecisionContext GetDecisionContextForPlayer(Player player)
        {
            IReadOnlyCollection<PlayerActionInformation> playerActions = this.GetPlayerLastActions(player);

            DecisionContext context = new DecisionContext(playerActions, this.currentPot.Amount);

            return context;
        }

        // Build flop game context for each player
        private FlopStageContext GetFlopStageContextForPlayer(Player player)
        {
            IReadOnlyCollection<Card> tableCards = this.TableCards.AsReadOnly();

            IReadOnlyCollection<PlayerActionInformation> playerActions = this.GetPlayerLastActions(player);

            FlopStageContext context = new FlopStageContext(tableCards, playerActions);

            return context;
        }

        // Build turn game context for each player
        private TurnStageContext GetTurnStageContextForPlayer(Player player)
        {
            Card tableCard = this.TableCards[this.TableCards.Count - 1];

            IReadOnlyCollection<PlayerActionInformation> playerActions = this.GetPlayerLastActions(player);

            TurnStageContext context = new TurnStageContext(tableCard, playerActions);

            return context;
        }

        // Build river game context for each player
        private RiverStageContext GetRiverStageContextForPlayer(Player player)
        {
            Card tableCard = this.TableCards[this.TableCards.Count - 1];

            IReadOnlyCollection<PlayerActionInformation> playerActions = this.GetPlayerLastActions(player);

            RiverStageContext context = new RiverStageContext(tableCard, playerActions);

            return context;
        }

        // Build common end game context
        private EndGameContext BuildEndGameContext()
        {
            var pots = new List<PotInformation>();

            PotInformation potInformation;

            IReadOnlyCollection<FullPlayerInformation> winners;

            foreach (var pot in this.pots)
            {
                winners = this.GetWinnersForPot(pot).AsReadOnly();

                potInformation = new PotInformation(pot.Amount, winners);

                pots.Add(potInformation);
            }

            var context = new EndGameContext(pots.AsReadOnly());

            return context;
        }

        private IReadOnlyCollection<PlayerActionInformation> GetPlayerLastActions(Player player)
        {
            List<PlayerActionInformation> playerActionsList = new List<PlayerActionInformation>();
            IReadOnlyCollection<PlayerActionInformation> playerActions;

            var startIndex = this.lastActionIndexSent[player] + 1;

            for (int i = startIndex; i < this.drawContext.PlayerActions.Count; i++)
            {
                playerActionsList.Add(this.drawContext.PlayerActions[i]);
            }

            this.lastActionIndexSent[player] = this.drawContext.PlayerActions.Count - 1;

            playerActions = playerActionsList.AsReadOnly();

            return playerActions;
        } //DONE

        internal void StartDraw() //DONE
        {
            this.lastActionIndexSent = this.Players.ToDictionary(x => x, x => -1);

            this.Players.ForEach(x => x.HasFolded = false);
            this.Players.ForEach(x => x.IsAllIn = false);
            this.Players.ForEach(x => x.Cards.Clear());
            this.Players.ForEach(x => x.Hand = null);

            this.playersAllInCount = 0;
            this.playersFoldCount = 0;

            this.FullPotAmount = 0;

            // Create main pot
            this.pots = new Queue<Pot>();
            this.currentPot = new Pot(0, this.Players);
            this.pots.Enqueue(this.currentPot);

            // Shuffle cards
            this.deck.Shuffle();

            this.LogDrawStartInformation();

            var blindFirstToBetIndex = (this.dealerIndex + 3) % this.Players.Count;

            this.AdvanceToPreFlopStage();

            if (this.AreAllPlayersAllIn())
            {
                this.HandleBettingOutcome(BettingOutcome.AllInShowdown);
            }
            else
            {
                var bettingOutcome = this.AdvanceToBetting(blindFirstToBetIndex, true);

                this.HandleBettingOutcome(bettingOutcome);
            }
        }

        private void HandleBettingOutcome(BettingOutcome bettingOutcome)
        {
            if (bettingOutcome == BettingOutcome.ContinueBetting)
            {
                var hasBettingStage = this.AdvanceToNextStage();

                if (hasBettingStage)
                {
                    this.HandleBettingOutcome(this.AdvanceToBetting(this.firstToBetIndex, false));
                }
                else
                {
                    return;
                }
            }
            else if (bettingOutcome == BettingOutcome.WinThroughFold)
            {
                this.AdvanceToAllFoldStage();
            }
            else if (bettingOutcome == BettingOutcome.AllInShowdown)
            {
                this.logger.AddSeparator();
                this.logger.Log("All players went all-in.");
                this.logger.AddSeparator();

                while (this.GameStage != GameStage.Showdown)
                {
                    this.AdvanceToNextStage();
                }
            }
        }

        private void AdvanceToAllFoldStage() //DONE
        {
            var lastStandingPlayer = this.currentPot.PotentialWinners.FirstOrDefault(x => !x.HasFolded);

            lastStandingPlayer.Money += this.FullPotAmount;

            this.logger.AddSeparator();
            this.logger.Log(String.Format("Only one player left in the game: \"{0}\".", lastStandingPlayer));
            this.logger.Log(String.Format("\"{0}\" wins {1}.", lastStandingPlayer, this.FullPotAmount));
            this.logger.AddSeparator();

            foreach (var player in this.Players)
            {
                player.DecisionTaker.HandleAllFoldContext(this.GetAllFoldContextForEachPlayer(player), this.GetFullPlayerInformation(player));
            }            
        }

        private bool AdvanceToNextStage()
        {
            var hasBettingStage = false;

            switch (this.GameStage)
            {
                case GameStage.PreFlop:
                    this.AdvanceToFlopStage();
                    hasBettingStage = true;
                    break;
                case GameStage.Flop:
                    this.AdvanceToTurnStage();
                    hasBettingStage = true;
                    break;
                case GameStage.Turn:
                    this.AdvanceToRiverStage();
                    hasBettingStage = true;
                    break;
                case GameStage.River:
                    this.AdvanceToShowdownStage();
                    hasBettingStage = false;
                    break;
                default:
                    hasBettingStage = false;
                    break;
            }

            return hasBettingStage;
        } // NO LOGGER

        private void AdvanceToPreFlopStage()
        {            
            this.GameStage = GameStage.PreFlop;

            this.LogStageInformation();

            // SB and BB pay
            this.PaySmallBlind();
            this.PayBigBlind();

            this.currentPot.CurrentMaxStakePosition = this.BigBlindPosition;

            // deal player cards
            this.DealPlayerCards();

            foreach (var player in this.Players)
            {
                player.DecisionTaker.HandleStartGameContext(this.GetStartGameContextForPlayer(player), this.GetPlayerInformation(player));
            }
        } //DONE

        private void AdvanceToFlopStage()
        {
            this.GameStage = GameStage.Flop;

            this.LogStageInformation();

            var newCards = this.deck.DealMultipleCards(3);

            this.TableCards.AddRange(newCards);

            this.logger.Log(String.Format("New cards: {0}.", String.Join(" ", newCards)));

            LogTableCards();

            // Evaluate player hands
            this.EvaluatePlayersHands();

            foreach (var player in this.Players)
            {
                player.DecisionTaker.HandleFlopStageContext(this.GetFlopStageContextForPlayer(player), this.GetFullPlayerInformation(player));
            }
        } //DONE

        private void AdvanceToTurnStage() //DONE
        {
            this.GameStage = GameStage.Turn;

            this.LogStageInformation();

            var newCard = this.deck.DealCard();

            this.TableCards.Add(newCard);

            this.logger.Log(String.Format("New card: {0}.", newCard));

            LogTableCards();

            // Evaluate player hands
            this.EvaluatePlayersHands();

            foreach (var player in this.Players)
            {
                player.DecisionTaker.HandleTurnStageContext(this.GetTurnStageContextForPlayer(player), this.GetFullPlayerInformation(player));
            }
        }

        private void AdvanceToRiverStage() //DONE
        {
            this.GameStage = GameStage.River;

            this.LogStageInformation();

            var newCard = this.deck.DealCard();

            this.TableCards.Add(newCard);

            this.logger.Log(String.Format("New card: {0}.", newCard));

            LogTableCards();

            // Evaluate player hands
            this.EvaluatePlayersHands();

            foreach (var player in this.Players)
            {
                player.DecisionTaker.HandleRiverStageContext(this.GetRiverStageContextForPlayer(player), this.GetFullPlayerInformation(player));
            }
        }

        private void AdvanceToShowdownStage()
        {
            this.GameStage = GameStage.Showdown;

            this.LogStageInformation();

            var endGameContext = this.BuildEndGameContext();

            // Add winnings to player accounts
            Player currentWinner;

            foreach (var pot in endGameContext.Pots)
            {
                foreach (var winner in pot.Winners)
                {
                    currentWinner = this.Players.FirstOrDefault(x => x.Name == winner.Name);
                    currentWinner.Money += pot.WinAmount;
                    winner.Money = currentWinner.Money;             
                }
            }

            this.LogEndGameResults(endGameContext);      

            foreach (var player in this.Players)
            {
                player.DecisionTaker.HandleEndGameContext(endGameContext, this.GetFullPlayerInformation(player));
            }
        } //DONE

        private BettingOutcome AdvanceToBetting(int firstToBetIndex, bool blindBetting)
        {
            var playerIndex = firstToBetIndex;
            var currentMaxStake = this.currentPot.CurrentMaxStake;

            var stillBlindBetting = blindBetting;

            bool shouldContinue = true;

            while (true)                
            {
                var currentPlayer = this.currentPot.PotentialWinners[playerIndex];

                if (currentPlayer == this.currentPot.CurrentMaxStakePosition && (!stillBlindBetting && !shouldContinue))
                {
                    break;
                }

                if (!currentPlayer.HasFolded)
                {
                    this.ConcludePlayerDecision(currentPlayer, blindBetting);

                    if (this.Players.Count - 1 == this.playersFoldCount)
                    {
                        return BettingOutcome.WinThroughFold;
                    }

                    if (this.AreAllPlayersAllIn())
                    {
                        return BettingOutcome.AllInShowdown;
                    }
                }                
                
                if (currentMaxStake != this.currentPot.CurrentMaxStake)
                {
                    this.currentPot.CurrentMaxStakePosition = currentPlayer;
                    currentMaxStake = this.currentPot.CurrentMaxStake;

                    stillBlindBetting = false;
                    shouldContinue = false;
                }

                if (currentPlayer == this.currentPot.CurrentMaxStakePosition && (stillBlindBetting || shouldContinue))
                {
                    break;
                }

                playerIndex = (playerIndex + 1) % this.currentPot.PotentialWinners.Count;
            }

            this.SyncPots();

            int previousIndex = this.firstToBetIndex - 1;

            if (previousIndex < 0)
            {
                previousIndex = this.currentPot.PotentialWinners.Count - 1;
            }

            this.currentPot.CurrentMaxStakePosition = this.currentPot.PotentialWinners[previousIndex];

            return BettingOutcome.ContinueBetting;
        }

        private void ConcludePlayerDecision(Player player, bool blindBetting)
        {
            var playerContext = this.GetDecisionContextForPlayer(player);
            DecisionInformation playerDecisionInformation;

            if (blindBetting)
            {
                playerDecisionInformation = player.DecisionTaker.TakeDecision(playerContext, this.GetPlayerInformation(player));
            }
            else
            {
                playerDecisionInformation = player.DecisionTaker.TakeDecision(playerContext, this.GetFullPlayerInformation(player));
            }                        

            var playerDecisionCounter = 0;
            bool isValidDecision = true;

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
                        isValidDecision = this.PlayerRaise(player, playerDecisionInformation.Amount);
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

            this.logger.Log(String.Format("Player \"{0}\"({1}) gives invalid decision for 3rd time.", player, player.Money));

            // Take default action if 3 consecutive decisions aren't valid.
            if (this.PlayerCheck(player))
            {
                return;
            }
            else
            {
                this.PlayerFold(player);
            }
        } //DONE

        private PlayerInformation GetPlayerInformation(Player player)
        {
            var playerInfo = this.drawContext.PlayersInformation.FirstOrDefault(x => x.Name == player.Name);

            return playerInfo;
        } // NO LOGGER

        private FullPlayerInformation GetFullPlayerInformation(Player player)
        {
            var fullPlayerInfo = new FullPlayerInformation(player.Name, player.Money, player.Cards.AsReadOnly(), player.Hand);

            return fullPlayerInfo;
        } // NO LOGGER
        
        private void EvaluatePlayersHands()
        {
            var playersInGame = this.Players.Where(x => !x.HasFolded).ToList();

            Hand playerHand;
            Player currentPlayer;

            var evaluateHandCards = new List<Card>();

            for (int i = 0; i < playersInGame.Count; i++)
            {
                currentPlayer = playersInGame[i];

                evaluateHandCards.AddRange(this.TableCards);
                evaluateHandCards.AddRange(currentPlayer.Cards);

                playerHand = this.handEvaluator.HandEvaluator.EvaluateHand(evaluateHandCards);

                currentPlayer.Hand = playerHand;

                evaluateHandCards.Clear();
            }
        }        

        private void SyncPlayerInformation(Player player)
        {
            var playerToSync = this.drawContext.PlayersInformation.FirstOrDefault(x => x.Name == player.Name);

            playerToSync.Money = player.Money;
        } // NO LOGGER

        private void FilePlayerAction(PlayerAction playerAction)
        {
            this.PlayerActions.Add(playerAction);

            var playerActionInformation = new PlayerActionInformation(this.GetPlayerInformation(playerAction.Player), playerAction.Action, playerAction.Amount, playerAction.IsAllIn);

            this.drawContext.PlayerActions.Add(playerActionInformation);
        } // NO LOGGER

        private void DealPlayerCards()
        {
            var startIndex = this.dealerIndex + 1;
            var finishIndex = startIndex + this.Players.Count;
            int currentPlayerIndex;

            for (int i = startIndex; i < finishIndex; i++)
            {
                currentPlayerIndex = i % this.Players.Count;

                var currentPlayer = this.Players[currentPlayerIndex];
                var cardsDealt = this.deck.DealMultipleCards(2);

                currentPlayer.Cards.AddRange(cardsDealt);

                this.logger.Log(String.Format("Player \"{0}\"({1}) receives cards: {2}.", currentPlayer, currentPlayer.Money, String.Join(", ", currentPlayer.Cards)));
            }
        } //DONE

        private bool InvestToPot(Player player, decimal amountToPay, out decimal amountInvested)
        {
            bool isAllIn = false;

            if (amountToPay < player.Money)
            {
                player.Money -= amountToPay;

                amountInvested = amountToPay;
            }
            else
            {                
                amountInvested = player.Money;

                player.Money = 0;

                player.IsAllIn = true;
                isAllIn = true;

                this.currentPot.PlayerWentAllIn = true;
                this.playersAllInCount++;
            }

            this.SyncPlayerInformation(player);

            this.currentPot.CurrentPotAmount[player] += amountInvested;
            this.currentPot.Amount += amountInvested;

            this.currentDrawAmount[player] += amountInvested;
            this.FullPotAmount += amountInvested;

            var newStake = this.currentPot.CurrentPotAmount[player];

            if (newStake > this.currentPot.CurrentMaxStake)
            {
                this.currentPot.CurrentMaxStake = newStake;
            }

            return isAllIn;
        } // NO LOGGER

        private void PaySmallBlind()
        {
            var playerMoney = this.SmallBlindPosition.Money;

            decimal amountInvested;

            var isAllIn = this.InvestToPot(this.SmallBlindPosition, this.SmallBlindAmount, out amountInvested);
            this.FilePlayerAction(new PlayerAction(this.SmallBlindPosition, Models.Enumerations.Action.PaySmallBlind, amountInvested, isAllIn));

            if (isAllIn)
            {
                if (amountInvested == playerMoney)
                {
                    this.logger.Log(String.Format("Player \"{0}\"({1}) goes all-in with {2}.", this.SmallBlindPosition, this.SmallBlindPosition.Money, amountInvested));
                }
                else
                {
                    this.logger.Log(String.Format("Player \"{0}\"({1}) cannot afford the small blind amount of {2} and goes all-in with {3}.", this.SmallBlindPosition, this.SmallBlindPosition.Money, this.SmallBlindAmount, amountInvested));
                }
            }
            else
            {
                this.logger.Log(String.Format("Player \"{0}\"({1}) pays the small blind amount of {2}.", this.SmallBlindPosition, this.SmallBlindPosition.Money, this.SmallBlindAmount));
            }
        } //DONE

        private void PayBigBlind()
        {
            var playerMoney = this.BigBlindPosition.Money;

            decimal amountInvested;

            var isAllIn = this.InvestToPot(this.BigBlindPosition, this.BigBlindAmount, out amountInvested);
            this.FilePlayerAction(new PlayerAction(this.BigBlindPosition, Models.Enumerations.Action.PayBigBlind, amountInvested, isAllIn));

            if (isAllIn)
            {
                if (amountInvested == playerMoney)
                {
                    this.logger.Log(String.Format("Player \"{0}\"({1}) goes all-in with {2}.", this.BigBlindPosition, this.BigBlindPosition.Money, amountInvested));
                }
                else
                {
                    this.logger.Log(String.Format("Player \"{0}\"({1}) cannot afford the big blind amount of {2} and goes all-in with {3}.", this.BigBlindPosition, this.BigBlindPosition.Money, this.BigBlindAmount, amountInvested));
                }
            }
            else
            {
                this.logger.Log(String.Format("Player \"{0}\"({1}) pays the big blind amount of {2}.", this.BigBlindPosition, this.BigBlindPosition.Money, this.BigBlindAmount));
            }
        } //DONE

        private bool PlayerCall(Player player)
        {
            if (this.currentPot.CurrentPotAmount[player] < this.currentPot.CurrentMaxStake)
            {
                var playerMoney = player.Money;

                decimal amountInvested;

                var callAmount = this.currentPot.CurrentMaxStake - this.currentPot.CurrentPotAmount[player];

                var isAllIn = this.InvestToPot(player, callAmount, out amountInvested);
                this.FilePlayerAction(new PlayerAction(player, Models.Enumerations.Action.Call, amountInvested, isAllIn));

                if (isAllIn)
                {
                    if (amountInvested == playerMoney)
                    {
                        this.logger.Log(String.Format("Player \"{0}\"({1}) goes all-in with {2}.", player, player.Money, amountInvested));
                    }
                    else
                    {
                        this.logger.Log(String.Format("Player \"{0}\"({1}) cannot afford the call amount of {2} and goes all-in with {3}.", player, player.Money, callAmount, amountInvested));
                    }                    
                }
                else
                {
                    this.logger.Log(String.Format("Player \"{0}\"({1}) calls with the amount of {2}.", player, player.Money, amountInvested));
                }

                return true;
            }
            else
            {
                this.logger.Log(String.Format("Player \"{0}\"({1}) tries to call but this is not a valid action.", player, player.Money));

                return false;
            }
        } //DONE

        private bool PlayerRaise(Player player, decimal amountToRaise)
        {
            var potStakeDifference = this.currentPot.CurrentMaxStake - this.currentPot.CurrentPotAmount[player];
            var minimalRaiseAmount = potStakeDifference + this.BigBlindAmount;
            var maximalRaiseAmount = player.Money - potStakeDifference;

            var investAmount = potStakeDifference + amountToRaise;

            if (player.Money <= potStakeDifference)
            {
                return false;
            }

            if (amountToRaise > maximalRaiseAmount)
            {
                this.logger.Log(String.Format("Player \"{0}\"({1}) tries to raise with {2} but he has only {3}.", player, player.Money, amountToRaise, player.Money - potStakeDifference));
                return false;
            }

            if (amountToRaise < this.BigBlindAmount && player.Money > minimalRaiseAmount)
            {
                this.logger.Log(String.Format("Player \"{0}\"({1}) tries to raise with {2} but this amount is less than the minimal required amount for rise of {3}.", player, player.Money, amountToRaise, this.BigBlindAmount));
                return false;
            }

            decimal amountInvested;

            var isAllIn = this.InvestToPot(player, investAmount, out amountInvested);
            this.FilePlayerAction(new PlayerAction(player, Models.Enumerations.Action.Raise, amountInvested, isAllIn));

            var raisedAmount = amountInvested - potStakeDifference;

            if (isAllIn)
            {
                this.logger.Log(String.Format("Player \"{0}\"({1}) goes all-in with the amount of {2}.", player, player.Money, raisedAmount));
            }
            else
            {
                if (potStakeDifference == 0)
                {
                    this.logger.Log(String.Format("Player \"{0}\"({1}) raises with the amount of {2}.", player, player.Money, raisedAmount));
                }
                else
                {
                    this.logger.Log(String.Format("Player \"{0}\"({1}) calls the amount of {2} and raises with the amount of {3}.", player, player.Money, potStakeDifference, raisedAmount));
                }
            }

            return true;
        } //DONE

        private bool PlayerCheck(Player player)
        {
            if (this.currentPot.CurrentPotAmount[player] == this.currentPot.CurrentMaxStake)
            {
                this.FilePlayerAction(new PlayerAction(player, Models.Enumerations.Action.Check, 0, false));
                
                this.logger.Log(String.Format("Player \"{0}\"({1}) checks.", player, player.Money));

                return true;
            }
            else
            {
                this.logger.Log(String.Format("Player \"{0}\"({1}) tries to check but this is not a valid action.", player, player.Money));

                return false;
            }
        } //DONE

        private void PlayerFold(Player player)
        {
            this.FilePlayerAction(new PlayerAction(player, Models.Enumerations.Action.Fold, 0, false));

            this.logger.Log(String.Format("Player \"{0}\"({1}) folds.", player, player.Money));

            player.HasFolded = true;
            this.playersFoldCount++;

            if (player == this.currentPot.PotentialWinners[this.firstToBetIndex])
            {
                if (this.firstToBetIndex == this.currentPot.PotentialWinners.Count - 1)
                {
                    this.firstToBetIndex = 0;
                }

                //var newFirstToBetIndex = this.firstToBetIndex;

                //while (true)
                //{
                //    newFirstToBetIndex = (newFirstToBetIndex + 1) % this.currentPot.PotentialWinners.Count;

                //    if (!this.currentPot.PotentialWinners[newFirstToBetIndex].HasFolded)
                //    {
                //        this.firstToBetIndex = newFirstToBetIndex;
                //        break;
                //    }
                //}
            }
        } //DONE

        private bool AreAllPlayersAllIn()
        {
            var areAllAllIn = false;

            var playersLeftInGame = this.Players.Count - this.playersFoldCount;

            if (playersLeftInGame == this.playersAllInCount || playersLeftInGame == this.playersAllInCount + 1)
            {
                areAllAllIn = true;
            }

            return areAllAllIn;
        }

        private void SyncPots()
        {
            if (this.currentPot.PlayerWentAllIn)
            {
                var newPots = new List<Pot>();

                // Take all who are all in
                // Sort the list descending by pot value
                var allInPlayersPotAmount = this.currentPot.CurrentPotAmount.Where(x => x.Key.IsAllIn).Select(x => x.Value).Distinct().OrderByDescending(x => x).ToList();

                decimal potAmountDifference;
                List<Player> playersFullfillingThePotAmount;
                Pot newPot;

                if (allInPlayersPotAmount[0] != this.currentPot.CurrentMaxStake)
                {
                    var playersAboveMaxAllInStake = this.currentPot.CurrentPotAmount.Where(x => x.Value > allInPlayersPotAmount[0]).ToDictionary(x => x.Key, x => x.Value);

                    newPot = new Pot();
                    newPots.Add(newPot);

                    foreach (var entry in playersAboveMaxAllInStake)
                    {
                        potAmountDifference = this.currentPot.CurrentPotAmount[entry.Key] - allInPlayersPotAmount[0];

                        this.currentPot.CurrentPotAmount[entry.Key] -= potAmountDifference;
                        this.currentPot.Amount -= potAmountDifference;

                        if (!newPot.CurrentPotAmount.ContainsKey(entry.Key))
                        {
                            newPot.CurrentPotAmount[entry.Key] = 0;
                        }

                        newPot.CurrentPotAmount[entry.Key] += potAmountDifference;
                        newPot.Amount += potAmountDifference;

                        newPot.PotentialWinners = playersAboveMaxAllInStake.Where(x => !x.Key.HasFolded).Select(x => x.Key).ToList();
                    }
                }

                // Calculate difference between the first and the second pot
                for (int i = 0; i < allInPlayersPotAmount.Count - 1; i++)
                {
                    potAmountDifference = allInPlayersPotAmount[i] - allInPlayersPotAmount[i + 1];

                    playersFullfillingThePotAmount = this.currentPot.CurrentPotAmount.Where(x => x.Value >= allInPlayersPotAmount[i]).Select(x => x.Key).ToList();

                    var aggregateAmount = potAmountDifference * playersFullfillingThePotAmount.Count;

                    this.currentPot.Amount -= aggregateAmount;

                    newPot = new Pot(aggregateAmount, playersFullfillingThePotAmount.Where(x => !x.HasFolded).ToList());
                    newPots.Add(newPot);
                }

                // Main Pot
                newPot = new Pot(currentPot.Amount, this.currentPot.PotentialWinners.Where(x => !x.HasFolded).ToList());
                newPots.Add(newPot);

                this.pots.Dequeue(); // Remove the current pot.                

                for (int i = newPots.Count - 1; i >= 0; i--)
                {
                    this.pots.Enqueue(newPots[i]);
                }

                this.currentPot = this.pots.Peek();
            }
        }

        private List<FullPlayerInformation> GetWinnersForPot(Pot pot)
        {
            var winners = this.handEvaluator.HandComparer.GetWinners(pot.PotentialWinners.Where(x => !x.HasFolded).ToList());
            var mappedWinners = winners.Select(x => new FullPlayerInformation(x.Name, x.Money, new List<Card>(x.Cards).AsReadOnly(), x.Hand)).ToList();

            return mappedWinners;
        }

        private void LogDrawStartInformation()
        {
            this.logger.Log(String.Format("Draw opens with {0} players.", this.Players.Count));

            foreach (var player in this.Players)
            {
                this.logger.Log(String.Format("Player \"{0}\"({1}) joins the draw.", player.Name, player.Money));
            }

            this.logger.Log(String.Format("Small blind amount: {0}.", this.SmallBlindAmount));
            this.logger.Log(String.Format("Small big amount: {0}.", this.BigBlindAmount));

            this.logger.Log(String.Format("{0}({1}) is the current dealer.", this.DealerPosition, this.DealerPosition.Money));
            this.logger.Log(String.Format("{0}({1}) is the current small blind.", this.SmallBlindPosition, this.SmallBlindPosition.Money));
            this.logger.Log(String.Format("{0}({1}) is the current big blind.", this.BigBlindPosition, this.BigBlindPosition.Money));
        }

        private void LogStageInformation()
        {
            string stage;

            switch (this.GameStage)
            {
                case GameStage.PreFlop:
                    stage = "Pre-Flop";
                    break;
                case GameStage.Flop:
                    stage = "Flop";
                    break;
                case GameStage.Turn:
                    stage = "Turn";
                    break;
                case GameStage.River:
                    stage = "River";
                    break;
                case GameStage.Showdown:
                    stage = "Showdown";
                    break;
                default:
                    stage = null;
                    break;
            }

            var playersInGame = this.Players.Where(x => !x.HasFolded).ToList();

            this.logger.Log(String.Format("{0} stage begins, {1} players in: [{2}].", stage, playersInGame.Count, String.Join(", ", playersInGame)));
        }

        private void LogTableCards()
        {
            var messageToLog = String.Format("Current table cards: {0}.", String.Join(" ", this.TableCards));

            this.logger.Log(messageToLog);
        }

        private void LogEndGameResults(EndGameContext context)
        {
            this.logger.Log("End game results:");

            var currentIndex = context.Pots.Count - 1;

            foreach (var pot in context.Pots)
            {
                this.logger.AddSeparator();

                if (currentIndex > 0)
                {
                    this.logger.Log(String.Format("Side pot {0}:", currentIndex));
                }
                else
                {
                    this.logger.Log(String.Format("Main pot:", currentIndex));
                }
                
                this.logger.Log(String.Format("Pot amount: {0}.", pot.Amount));

                this.logger.Log("Players:");

                foreach (var player in this.currentPot.PotentialWinners.Where(x => !x.HasFolded))
                {
                    this.logger.Log(String.Format("Player \"{0}\"({1}): [{2}]({3})", player.Name, player.Money, player.Hand, player.Hand.HandValue));
                }

                this.logger.Log("Winners:");

                foreach (var winner in pot.Winners)
                {
                    this.logger.Log(String.Format("Player \"{0}\"({1}) wins {2} with the hand [{3}]({4}).", winner.Name, winner.Money, pot.WinAmount, winner.Hand, winner.Hand.HandValue));
                }

                this.logger.AddSeparator();

                currentIndex--;
            }
        }
    }
}
