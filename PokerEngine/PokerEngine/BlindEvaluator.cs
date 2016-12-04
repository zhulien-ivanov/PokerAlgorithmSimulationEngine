using PokerEngine.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerEngine.Models.Helpers;

namespace PokerEngine
{
    class BlindEvaluator : IBlindsEvaluator
    {
        public BlindsInformation GetBlindAmounts(BlindsDrawContext context)
        {
            return new BlindsInformation(5, 10);
        }
    }
}
