using System;

using PokerEngine.Models.Contracts;
using PokerEngine.Models.Enumerations;
using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Strategies
{
    public class RandomPlayer : IStrategy
    {
        private Random randomGenerator;

        public RandomPlayer()
        {
            this.randomGenerator = new Random();
        }

        public void HandleAllFoldContext(AllFoldContext context, FullPlayerInformation me)
        {
            return;
        }

        public void HandleEndGameContext(EndGameContext context, FullPlayerInformation me)
        {
            return;
        }

        public void HandleFlopStageContext(FlopStageContext context, FullPlayerInformation me)
        {
            return;
        }

        public void HandleRiverStageContext(RiverStageContext context, FullPlayerInformation me)
        {
            return;
        }

        public void HandleStartGameContext(StartGameContext context, PlayerInformation me)
        {
            return;
        }

        public void HandleTurnStageContext(TurnStageContext context, FullPlayerInformation me)
        {
            return;
        }

        public DecisionInformation TakeDecision(DecisionContext context, FullPlayerInformation me)
        {
            var decisionNames = Enum.GetNames(typeof(Decision));

            var decision = (Decision)randomGenerator.Next(0, decisionNames.Length);

            decimal amount = 0;

            if (decision == Decision.Raise)
            {
                amount = 20;
            }

            var decisionInformation = new DecisionInformation(decision, amount);

            return decisionInformation;
        }

        public DecisionInformation TakeDecision(DecisionContext context, PlayerInformation me)
        {
            var decisionNames = Enum.GetNames(typeof(Decision));

            var decision = (Decision)randomGenerator.Next(0, decisionNames.Length);

            decimal amount = 0;

            if (decision == Decision.Raise)
            {
                amount = 20;
            }

            var decisionInformation = new DecisionInformation(decision, amount);

            return decisionInformation;
        }
    }
}
