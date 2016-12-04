using System.Collections.Generic;

using PokerEngine.Models;

namespace PokerEngine.Evaluators.Contracts
{
    public interface IPlayerHandComparer
    {
        List<Player> GetWinners(List<Player> players);
    }
}
