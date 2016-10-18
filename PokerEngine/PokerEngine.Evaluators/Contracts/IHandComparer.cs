using System.Collections.Generic;

using PokerEngine.Models;

namespace PokerEngine.Evaluators.Contracts
{
    public interface IHandComparer
    {
        List<Hand> GetWinningHands(List<Hand> hands);
    }
}
