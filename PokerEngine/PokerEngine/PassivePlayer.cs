using System;
using PokerEngine.Models.Contracts;
using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;

namespace PokerEngine
{
    public class PassivePlayer : IDecisionHandler
    {
        public void HandleAllFoldContext(AllFoldContext context)
        {
            return;
        }

        public void HandleEndGameContext(EndGameContext context)
        {
            return;
        }

        public void HandleFlopStageContext(FlopStageContext context)
        {
            return;
        }

        public void HandleRiverStageContext(RiverStageContext context)
        {
            return;
        }

        public void HandleStartGameContext(StartGameContext context)
        {
            return;
        }

        public void HandleTurnStageContext(TurnStageContext context)
        {
            return;
        }

        public DecisionInformation TakeDecision(DecisionContext context)
        {
            return new DecisionInformation(Models.Enumerations.Decision.Call, 0);
        }
    }
}
