using System;
using PokerEngine.Models.Contracts;
using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;

namespace PokerEngine
{
    public class AggressivePlayer : IStrategy
    {
        public void HandleAllFoldContext(AllFoldContext context, FullPlayerInformation me)
        {
            throw new NotImplementedException();
        }

        public void HandleEndGameContext(EndGameContext context, FullPlayerInformation me)
        {
            throw new NotImplementedException();
        }

        public void HandleFlopStageContext(FlopStageContext context, FullPlayerInformation me)
        {
            throw new NotImplementedException();
        }

        public void HandleRiverStageContext(RiverStageContext context, FullPlayerInformation me)
        {
            throw new NotImplementedException();
        }

        public void HandleStartGameContext(StartGameContext context, PlayerInformation me)
        {
            throw new NotImplementedException();
        }

        public void HandleTurnStageContext(TurnStageContext context, FullPlayerInformation me)
        {
            throw new NotImplementedException();
        }

        public DecisionInformation TakeDecision(DecisionContext context, FullPlayerInformation me)
        {
            return new DecisionInformation(Models.Enumerations.Decision.Raise, 60);
        }

        public DecisionInformation TakeDecision(DecisionContext context, PlayerInformation me)
        {
            return new DecisionInformation(Models.Enumerations.Decision.Raise, 60);
        }
    }
}
