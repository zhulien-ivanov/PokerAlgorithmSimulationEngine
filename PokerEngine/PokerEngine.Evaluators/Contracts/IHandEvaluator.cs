using System.Collections.Generic;

using PokerEngine.Models;

namespace PokerEngine.Evaluators.Contracts
{
    public interface IHandEvaluator
    {
        Hand EvaluateHand(List<Card> cards);
    }
}