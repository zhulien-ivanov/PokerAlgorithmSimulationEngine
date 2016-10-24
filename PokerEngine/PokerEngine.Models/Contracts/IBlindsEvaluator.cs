namespace PokerEngine.Models.Contracts
{
    public interface IBlindsEvaluator
    {
        decimal GetSmallBlindAmount(GameTableContext context);
    }
}
