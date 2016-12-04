using PokerEngine.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerEngine.Evaluators.Contracts;
using PokerEngine.Evaluators;

namespace PokerEngine
{
    class PlayerHandEvaluator : IPlayerHandEvaluator
    {
        private IPlayerHandComparer comparer = new PlayerHandComparer();
        private IHandEvaluator handEvaluator = new HandEvaluator();

        public IPlayerHandComparer HandComparer
        {
            get
            {
                return this.comparer;
            }
        }

        public IHandEvaluator HandEvaluator
        {
            get
            {
                return this.handEvaluator;
            }
        }
    }
}
