using PokerEngine.Evaluators.Contracts;

namespace PokerEngine.Logic.Contracts
{
    internal interface IPlayerHandEvaluator
    {
        IPlayerHandComparer HandComparer { get; }
        IHandEvaluator HandEvaluator { get; }
    }
}
