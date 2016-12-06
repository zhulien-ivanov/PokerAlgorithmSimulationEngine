using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.Contracts
{
    public interface IStrategy
    {
        DecisionInformation TakeDecision(DecisionContext context, PlayerInformation me);

        DecisionInformation TakeDecision(DecisionContext context, FullPlayerInformation me);
        
        void HandleAllFoldContext(AllFoldContext context, FullPlayerInformation me);

        void HandleStartGameContext(StartGameContext context, PlayerInformation me);

        void HandleFlopStageContext(FlopStageContext context, FullPlayerInformation me);

        void HandleTurnStageContext(TurnStageContext context, FullPlayerInformation me);

        void HandleRiverStageContext(RiverStageContext context, FullPlayerInformation me);

        void HandleEndGameContext(EndGameContext context, FullPlayerInformation me);
    }
}
