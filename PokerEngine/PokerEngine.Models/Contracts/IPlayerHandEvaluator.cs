namespace PokerEngine.Models.Contracts
{
    internal interface IPlayerHandEvaluator
    {
        IHandComparer HandComparer { get; }
        IHandEvaluator HandEvaluator { get; }
    }
}
