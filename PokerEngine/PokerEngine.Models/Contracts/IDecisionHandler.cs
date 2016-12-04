using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.Contracts
{
    public interface IDecisionHandler
    {
        DecisionInformation TakeDecision(DecisionContext context);

        void HandleStartGameContext(StartGameContext context);

        void HandleFlopStageContext(FlopStageContext context);

        void HandleTurnStageContext(TurnStageContext context);

        void HandleRiverStageContext(RiverStageContext context);

        void HandleEndGameContext(EndGameContext context);
    }
}
