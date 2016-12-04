using PokerEngine.Models;

namespace PokerEngine.Logic.Contracts
{
    public interface IBlindsEvaluator
    {
        decimal GetSmallBlindAmount(DrawContext context);
    }
}
