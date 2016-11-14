namespace PokerEngine.Models.Contracts
{
    public interface IBlindsEvaluator
    {
        decimal GetSmallBlindAmount(DrawContext context);
    }
}
