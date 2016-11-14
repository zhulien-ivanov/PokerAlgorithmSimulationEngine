using System;
using System.Collections.Generic;
using System.Linq;

using PokerEngine.Evaluators.Contracts;
using PokerEngine.Models;
using PokerEngine.Models.Enumerations;

namespace PokerEngine.Evaluators
{
    public class HandEvaluator : IHandEvaluator
    {
        private Dictionary<CardSuit, List<Card>> suitDictionary;
        private Dictionary<CardFace, List<Card>> faceDictionary;

        public Hand EvaluateHand(List<Card> cards)
        {
            if (cards.Count != 7)
            {
                throw new ArgumentException("Invalid number of cards.");
            }

            var sortedCards = cards.OrderByDescending(x => (int)x.CardFace).ToList();

            // SETUP SUIT DICTIONARY - START
            this.suitDictionary = new Dictionary<CardSuit, List<Card>>();

            foreach (var card in sortedCards)
            {
                if (!this.suitDictionary.ContainsKey(card.CardSuit))
                {
                    this.suitDictionary[card.CardSuit] = new List<Card>();
                }

                this.suitDictionary[card.CardSuit].Add(card);
            }

            this.suitDictionary = this.suitDictionary.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
            // SETUP SUIT DICTIONARY - END

            // SETUP FACE DICTIONARY - START
            this.faceDictionary = new Dictionary<CardFace, List<Card>>();
        
            foreach (var card in sortedCards)
            {
                if (!this.faceDictionary.ContainsKey(card.CardFace))
                {
                    this.faceDictionary[card.CardFace] = new List<Card>();
                }

                this.faceDictionary[card.CardFace].Add(card);
            }

            this.faceDictionary = this.faceDictionary.OrderByDescending(x => x.Value.Count).ThenByDescending(x => (int)x.Key).ToDictionary(x => x.Key, x => x.Value);
            // SETUP FACE DICTIONARY - END

            var hand = new List<Card>();

            Hand finalHand;

            if (this.IsStraightFlush(new List<Card>(sortedCards), out hand))
            {
                finalHand = new Hand(hand, HandValue.StraightFlush);
            }
            else if (this.IsFourOfAKind(new List<Card>(sortedCards), out hand))
            {
                finalHand = new Hand(hand, HandValue.FourOfAKind);
            }
            else if (this.IsFullHouse(new List<Card>(sortedCards), out hand))
            {
                finalHand = new Hand(hand, HandValue.FullHouse);
            }
            else if (this.IsFlush(new List<Card>(sortedCards), out hand))
            {
                finalHand = new Hand(hand, HandValue.Flush);
            }
            else if (this.IsStraight(new List<Card>(sortedCards), out hand))
            {
                finalHand = new Hand(hand, HandValue.Straight);
            }
            else if (this.IsThreeOfAKind(new List<Card>(sortedCards), out hand))
            {
                finalHand = new Hand(hand, HandValue.ThreeOfAKind);
            }
            else if (this.IsTwoPairs(new List<Card>(sortedCards), out hand))
            {
                finalHand = new Hand(hand, HandValue.TwoPairs);
            }
            else if (this.IsOnePair(new List<Card>(sortedCards), out hand))
            {
                finalHand = new Hand(hand, HandValue.OnePair);
            }
            else
            {
                hand = new List<Card>(sortedCards.Take(5));

                finalHand = new Hand(hand, HandValue.HighCard);
            }

            return finalHand;
        }

        private bool IsStraightFlush(List<Card> cards, out List<Card> hand)
        {
            hand = new List<Card>();

            var firstList = new List<Card>(this.suitDictionary.First().Value);

            if (firstList.Count >= 5)
            {
                if (this.CanFormStraight(firstList, out hand))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool IsFourOfAKind(List<Card> cards, out List<Card> hand)
        {
            hand = new List<Card>();

            var firstList = this.faceDictionary.First().Value;

            if (firstList.Count == 4)
            {
                hand.AddRange(firstList);

                hand.Add(cards.First(x => x.CardFace != firstList[0].CardFace));

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsFullHouse(List<Card> cards, out List<Card> hand)
        {
            hand = new List<Card>();

            var firstList = this.faceDictionary.First().Value;

            if (firstList.Count == 3)
            {
                var secondList = this.faceDictionary.Skip(1).First().Value;

                if (secondList.Count == 3 || secondList.Count == 2)
                {
                    hand.AddRange(firstList);

                    if (secondList.Count == 3)
                    {
                        hand.AddRange(secondList.Take(2));
                    }
                    else
                    {
                        hand.AddRange(secondList);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool IsFlush(List<Card> cards, out List<Card> hand)
        {
            hand = new List<Card>();

            var firstList = new List<Card>(this.suitDictionary.First().Value);

            if (firstList.Count >= 5)
            {
                firstList = firstList.OrderByDescending(x => (int)x.CardFace).ToList();

                hand = firstList.Take(5).ToList();

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsStraight(List<Card> cards, out List<Card> hand)
        {
            hand = new List<Card>();

            if (this.CanFormStraight(cards, out hand))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsThreeOfAKind(List<Card> cards, out List<Card> hand)
        {
            hand = new List<Card>();

            var firstList = this.faceDictionary.First().Value;

            if (firstList.Count == 3)
            {
                hand.AddRange(firstList);
                hand.AddRange(cards.Where(x => x.CardFace != firstList[0].CardFace).Take(2));

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsTwoPairs(List<Card> cards, out List<Card> hand)
        {
            hand = new List<Card>();

            var firstList = this.faceDictionary.First().Value;

            if (firstList.Count == 2)
            {
                var secondList = this.faceDictionary.Skip(1).First().Value;

                if (secondList.Count == 2)
                {
                    hand.AddRange(firstList);
                    hand.AddRange(secondList);

                    hand.Add(cards.First(x => x.CardFace != firstList[0].CardFace && x.CardFace != secondList[0].CardFace));

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool IsOnePair(List<Card> cards, out List<Card> hand)
        {
            hand = new List<Card>();

            var firstList = this.faceDictionary.First().Value;

            if (firstList.Count == 2)
            {
                hand.AddRange(firstList);

                hand.AddRange(cards.Where(x => x.CardFace != firstList[0].CardFace).Take(3));

                return true;
            }
            else
            {
                return false;
            }
        }        

        private bool CanFormStraight(List<Card> cards, out List<Card> hand)
        {
            var sequenceCounter = 1;
            hand = new List<Card>();

            hand.Add(cards[0]);

            if (cards[0].CardFace == CardFace.Ace)
            {
                cards.Add(cards[0]);
            }

            for (int i = 1; i < cards.Count; i++)
            {
                var firstCard = (int)cards[i - 1].CardFace;
                var secondCard = (int)cards[i].CardFace;

                if (i == cards.Count - 1 && cards[i].CardFace == CardFace.Ace)
                {
                    secondCard = 1;
                }

                if (firstCard - secondCard == 0)
                {
                    continue;
                }
                else
                {
                    if (firstCard - secondCard == 1)
                    {
                        sequenceCounter++;
                    }
                    else
                    {
                        sequenceCounter = 1;

                        hand.Clear();
                    }

                    hand.Add(cards[i]);

                    if (sequenceCounter == 5)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
