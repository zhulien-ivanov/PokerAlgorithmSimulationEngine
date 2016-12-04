using PokerEngine.Evaluators.Contracts;

namespace PokerEngine.Logic.Contracts
{
    public interface IPlayerHandEvaluator
    {
        IPlayerHandComparer HandComparer { get; }
        IHandEvaluator HandEvaluator { get; }
    }
}
