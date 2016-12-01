using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.Contracts
{
    public interface IDecisionHandler
    {
        DecisionInformation TakeDecision(TurnContext context);

        void HandleStartGameContext(StartGameContext context);

        void HandleEndGameContext(EndGameContext context);
    }
}
