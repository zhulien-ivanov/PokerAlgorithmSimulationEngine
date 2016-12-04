using PokerEngine.Models.Helpers;

namespace PokerEngine.Logic.Contracts
{
    public interface IBlindsEvaluator
    {
        BlindsInformation GetBlindAmounts(BlindsDrawContext context);
    }
}
