using System;
using PokerEngine.Models.Contracts;
using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;

namespace PokerEngine
{
    public class PassivePlayer : IStrategy
    {
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
            return new DecisionInformation(Models.Enumerations.Decision.Call, 0);
        }

        public DecisionInformation TakeDecision(DecisionContext context, PlayerInformation me)
        {
            return new DecisionInformation(Models.Enumerations.Decision.Call, 0);
        }
    }
}
