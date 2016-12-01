using PokerEngine.Models.GameContexts;
using PokerEngine.Models.Helpers;

namespace PokerEngine.Models.Contracts
{
    public interface IDecisionTaker
    {
        DecisionInformation TakeDecision(TurnContext context);
    }
}
