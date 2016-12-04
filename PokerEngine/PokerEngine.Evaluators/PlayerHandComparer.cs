using System.Collections.Generic;
using System.Linq;

using PokerEngine.Evaluators.Contracts;
using PokerEngine.Evaluators.Enumerations;

using PokerEngine.Models;
using PokerEngine.Models.Enumerations;
using System;

namespace PokerEngine.Evaluators
{
    public class PlayerHandComparer : IPlayerHandComparer
    {
        public List<Player> GetWinners(List<Player> players)
        {
            var sortedPlayers = players.OrderByDescending(x => (int)x.Hand.HandValue).ToList();

            var possibleWinners = new List<Player>();
            var maxCombination = sortedPlayers[0].Hand.HandValue;

            possibleWinners.Add(sortedPlayers[0]);

            for (int i = 1; i < sortedPlayers.Count; i++)
            {
                if (sortedPlayers[i].Hand.HandValue != maxCombination)
                {
                    break;
                }

                possibleWinners.Add(sortedPlayers[i]);
            }

            var winningPlayers = new List<Player>();

            winningPlayers.Add(possibleWinners[0]);

            for (int i = 1; i < possibleWinners.Count; i++)
            {
                var handResult = this.CompareHands(winningPlayers[0].Hand, possibleWinners[i].Hand, maxCombination);

                if (handResult == HandResult.HandsEqual)
                {
                    winningPlayers.Add(possibleWinners[i]);
                }
                else if (handResult == HandResult.SecondHandWinner)
                {
                    winningPlayers.Clear();
                    winningPlayers.Add(possibleWinners[i]);
                }
            }

            return winningPlayers;
        }

        private HandResult CompareHands(Hand firstHand, Hand secondHand, HandValue handValue)
        {
            HandResult handResult;

            switch (handValue)
            {
                case HandValue.HighCard:
                    handResult = this.CompareHighCardHands(firstHand, secondHand);
                    break;
                case HandValue.OnePair:
                    handResult = this.CompareOnePairHands(firstHand, secondHand);
                    break;
                case HandValue.TwoPairs:
                    handResult = this.CompareTwoPairsHands(firstHand, secondHand);
                    break;
                case HandValue.ThreeOfAKind:
                    handResult = this.CompareThreeOfAKindHands(firstHand, secondHand);
                    break;
                case HandValue.Straight:
                    handResult = this.CompareStraightHands(firstHand, secondHand);
                    break;
                case HandValue.Flush:
                    handResult = this.CompareFlushHands(firstHand, secondHand);
                    break;
                case HandValue.FullHouse:
                    handResult = this.CompareFullHouseHands(firstHand, secondHand);
                    break;
                case HandValue.FourOfAKind:
                    handResult = this.CompareFourOfAKindHands(firstHand, secondHand);
                    break;
                case HandValue.StraightFlush:
                    handResult = this.CompareStraightFlushHands(firstHand, secondHand);
                    break;
                default:
                    throw new ArgumentException("Invalid hand value.");                    
            }

            return handResult;
        }

        private HandResult CompareHighCardHands(Hand firstHand, Hand secondHand)
        {
            for (int i = 0; i < firstHand.Cards.Count; i++)
            {
                if (firstHand.Cards[i].CardFace > secondHand.Cards[i].CardFace)
                {
                    return HandResult.FirstHandWinner;
                }
                else if (firstHand.Cards[i].CardFace < secondHand.Cards[i].CardFace)
                {
                    return HandResult.SecondHandWinner;
                }
                else
                {
                    continue;
                }
            }

            return HandResult.HandsEqual;
        }

        private HandResult CompareOnePairHands(Hand firstHand, Hand secondHand)
        {
            if (firstHand.Cards[0].CardFace > secondHand.Cards[0].CardFace)
            {
                return HandResult.FirstHandWinner;
            }
            else if (firstHand.Cards[0].CardFace < secondHand.Cards[0].CardFace)
            {
                return HandResult.SecondHandWinner;
            }
            else
            {
                for (int i = 2; i < firstHand.Cards.Count; i++)
                {
                    if (firstHand.Cards[i].CardFace > secondHand.Cards[i].CardFace)
                    {
                        return HandResult.FirstHandWinner;
                    }
                    else if (firstHand.Cards[i].CardFace < secondHand.Cards[i].CardFace)
                    {
                        return HandResult.SecondHandWinner;
                    }
                    else
                    {
                        continue;
                    }
                }

                return HandResult.HandsEqual;
            }
        }

        private HandResult CompareTwoPairsHands(Hand firstHand, Hand secondHand)
        {
            if (firstHand.Cards[0].CardFace > secondHand.Cards[0].CardFace)
            {
                return HandResult.FirstHandWinner;
            }
            else if (firstHand.Cards[0].CardFace < secondHand.Cards[0].CardFace)
            {
                return HandResult.SecondHandWinner;
            }
            else
            {
                if (firstHand.Cards[2].CardFace > secondHand.Cards[2].CardFace)
                {
                    return HandResult.FirstHandWinner;
                }
                else if (firstHand.Cards[2].CardFace < secondHand.Cards[2].CardFace)
                {
                    return HandResult.SecondHandWinner;
                }
                else
                {
                    if (firstHand.Cards[4].CardFace > secondHand.Cards[4].CardFace)
                    {
                        return HandResult.FirstHandWinner;
                    }
                    else if (firstHand.Cards[4].CardFace < secondHand.Cards[4].CardFace)
                    {
                        return HandResult.SecondHandWinner;
                    }
                    else
                    {
                        return HandResult.HandsEqual;
                    }
                }
            }
        }

        private HandResult CompareThreeOfAKindHands(Hand firstHand, Hand secondHand)
        {
            if (firstHand.Cards[0].CardFace > secondHand.Cards[0].CardFace)
            {
                return HandResult.FirstHandWinner;
            }
            else if (firstHand.Cards[0].CardFace < secondHand.Cards[0].CardFace)
            {
                return HandResult.SecondHandWinner;
            }
            else
            {
                for (int i = 3; i < firstHand.Cards.Count; i++)
                {
                    if (firstHand.Cards[i].CardFace > secondHand.Cards[i].CardFace)
                    {
                        return HandResult.FirstHandWinner;
                    }
                    else if (firstHand.Cards[i].CardFace < secondHand.Cards[i].CardFace)
                    {
                        return HandResult.SecondHandWinner;
                    }
                    else
                    {
                        continue;
                    }
                }

                return HandResult.HandsEqual;
            }
        }

        private HandResult CompareStraightHands(Hand firstHand, Hand secondHand)
        {
            if (firstHand.Cards[0].CardFace > secondHand.Cards[0].CardFace)
            {
                return HandResult.FirstHandWinner;
            }
            else if (firstHand.Cards[0].CardFace < secondHand.Cards[0].CardFace)
            {
                return HandResult.SecondHandWinner;
            }
            else
            {
                return HandResult.HandsEqual;
            }
        }

        private HandResult CompareFlushHands(Hand firstHand, Hand secondHand)
        {
            return this.CompareHighCardHands(firstHand, secondHand);
        }

        private HandResult CompareFullHouseHands(Hand firstHand, Hand secondHand)
        {
            if (firstHand.Cards[0].CardFace > secondHand.Cards[0].CardFace)
            {
                return HandResult.FirstHandWinner;
            }
            else if (firstHand.Cards[0].CardFace < secondHand.Cards[0].CardFace)
            {
                return HandResult.SecondHandWinner;
            }
            else
            {
                if (firstHand.Cards[3].CardFace > secondHand.Cards[3].CardFace)
                {
                    return HandResult.FirstHandWinner;
                }
                else if (firstHand.Cards[3].CardFace < secondHand.Cards[3].CardFace)
                {
                    return HandResult.SecondHandWinner;
                }
                else
                {
                    return HandResult.HandsEqual;
                }
            }
        }

        private HandResult CompareFourOfAKindHands(Hand firstHand, Hand secondHand)
        {
            if (firstHand.Cards[0].CardFace > secondHand.Cards[0].CardFace)
            {
                return HandResult.FirstHandWinner;
            }
            else if (firstHand.Cards[0].CardFace < secondHand.Cards[0].CardFace)
            {
                return HandResult.SecondHandWinner;
            }
            else
            {
                if (firstHand.Cards[4].CardFace > secondHand.Cards[4].CardFace)
                {
                    return HandResult.FirstHandWinner;
                }
                else if (firstHand.Cards[4].CardFace < secondHand.Cards[4].CardFace)
                {
                    return HandResult.SecondHandWinner;
                }
                else
                {
                    return HandResult.HandsEqual;
                }
            }
        }

        private HandResult CompareStraightFlushHands(Hand firstHand, Hand secondHand)
        {
            return this.CompareStraightHands(firstHand, secondHand);
        }
    }
}
