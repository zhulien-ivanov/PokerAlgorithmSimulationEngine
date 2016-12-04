using PokerEngine.Evaluators.Contracts;

namespace PokerEngine.Logic.Contracts
{
    internal interface IPlayerHandEvaluator
    {
        IHandComparer HandComparer { get; }
        IHandEvaluator HandEvaluator { get; }
    }
}
