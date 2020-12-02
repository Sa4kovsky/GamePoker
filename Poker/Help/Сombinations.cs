using Poker.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Help
{
    public static class Сombinations
    {
        public static void Flush(string title, in List<Card> board, List<Card> playerCards, List<ResultGame> resultGame)
        {
            if (title == "omaha-holdem")
            {
                List<Card> combinationCard;
                List<List<Card>> cards1 = new List<List<Card>>();
                for (int i = 0; i < playerCards.Count; i++)
                {
                    for (int j = i + 1; j < playerCards.Count; j++)
                    {
                        combinationCard = new List<Card>() { new Card { Suits = playerCards[i].Suits, Value = playerCards[i].Value } };
                        combinationCard.Add(new Card { Suits = playerCards[j].Suits, Value = playerCards[j].Value });
                        cards1.Add(combinationCard);
                    }
                }

                foreach (List<Card> e in cards1)
                {
                    List<Card> handCards;

                    if (board != null)
                    {
                        handCards = new List<Card>(board);
                    }
                    else
                    {
                        handCards = new List<Card>();
                    }

                    foreach (Card c in e)
                    {
                        handCards.Add(new Card { Suits = c.Suits, Value = c.Value });
                    }
                    var cards = handCards.OrderBy(x => x.Suits).ThenByDescending(u => u.Value).ToList();

                    int handValue = 0;
                    for (int i = 0; i < cards.Count - 4; i++)
                    {
                        if (cards[i].Suits == cards[i + 4].Suits)
                        {
                            handValue = 6;
                            ResultConverterCards(handValue, i, playerCards, cards, resultGame);
                            Straight(handValue, playerCards, cards, resultGame);
                            if (handValue < 9)
                            {
                                cards = handCards.OrderByDescending(u => u.Value).ToList();
                                SearchMatch(playerCards, cards, resultGame);
                            }
                            break;
                        }
                        else if (i == cards.Count - 5)
                        {
                            cards = handCards.OrderByDescending(u => u.Value).ToList();
                            Straight(0, playerCards, cards, resultGame);
                            SearchMatch(playerCards, cards, resultGame);
                        }
                    }
                }

            }
            else
            {
                List<Card> handCards;

                if (board != null)
                {
                    handCards = new List<Card>(board);
                }
                else
                {
                    handCards = new List<Card>();
                }
             
                foreach (Card c in playerCards)
                {
                    handCards.Add(new Card { Suits = c.Suits, Value = c.Value });
                }
                var cards = handCards.OrderBy(x => x.Suits).ThenByDescending(u => u.Value).ToList();

                int handValue = 0;
                for (int i = 0; i < cards.Count - 4; i++)
                {
                    if (cards[i].Suits == cards[i + 4].Suits)
                    {
                        handValue = 6;
                        ResultConverterCards(handValue, i, playerCards, cards, resultGame);
                        Straight(handValue, playerCards, cards, resultGame);
                        if (handValue < 9)
                        {
                            cards = handCards.OrderByDescending(u => u.Value).ToList();
                            SearchMatch(playerCards, cards, resultGame);
                        }
                        break;
                    }
                    else if (i == cards.Count - 5)
                    {
                        cards = handCards.OrderByDescending(u => u.Value).ToList();
                        Straight(0, playerCards, cards, resultGame);
                        SearchMatch(playerCards, cards, resultGame);
                    }
                }
            }

        }

        public static void Straight(int handValue, List<Card> playerCards, List<Card> cards, List<ResultGame> resultGame)
        {
            int countFlush = 1;
            int countStraight = 1;
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].Value - cards[i + 1].Value == 1 && cards[i].Suits == cards[i + 1].Suits)
                {
                    countFlush++;
                    if (countFlush == 5)
                    {
                        if (cards.Where(a => a.Value == 14).Where(s => s.Suits == cards[i].Suits).Count() == 1)
                        {
                            ResultConverterCards(10, i - 3, playerCards, cards, resultGame);
                            break;
                        }
                        else
                        {
                            ResultConverterCards(9, i - 3, playerCards, cards, resultGame);
                            break;
                        }

                    }
                    else if (countFlush == 4
                                && cards[i].Value == 3
                                && cards[i].Suits == cards[i - 1].Suits
                                && cards.Where(a => a.Value == 14).Where(s => s.Suits == cards[i].Suits).Count() == 1
                                && cards.Where(a => a.Value == 2).Where(s => s.Suits == cards[i].Suits).Count() == 1)
                    {
                        cards.Insert(i + 2, new Card { Value = 14, Suits = cards[i].Suits });
                        ResultConverterCards(9, i - 2, playerCards, cards, resultGame);
                        break;
                    }
                }
                else if (cards[i].Value - cards[i + 1].Value > 1)
                {
                    countFlush = 1;
                }

                if (cards[i].Value - cards[i + 1].Value == 1)
                {
                    countStraight++;
                    if (countStraight == 5)
                    {
                        ResultConverterCards(5, i - 3, playerCards, cards, resultGame);
                        break;
                    }
                    else if (countStraight == 4
                                && cards[i].Value == 3
                                && cards[i].Suits == cards[i - 1].Suits
                                && cards.Where(a => a.Value == 14).Count() == 1
                                && cards.Where(a => a.Value == 2).Count() == 1)
                    {
                        cards.Insert(i + 2, new Card { Value = 14, Suits = cards[i].Suits });
                        ResultConverterCards(5, i - 2, playerCards, cards, resultGame);
                        break;
                    }
                }
                else if (cards[i].Value - cards[i + 1].Value > 1)
                {
                    countStraight = 1;
                }
            }
        }

        public static void SearchMatch(List<Card> playerCards, List<Card> cards, List<ResultGame> resultGame)
        {
            List<ResultGame> combinations = new List<ResultGame>();
            int count = 1;
            for (int i = 0; i < cards.Count-1; i++)
            {
                if (cards[i].Value - cards[i + 1].Value == 0)
                {
                    count++;
                }
                else 
                { 
                    count = 1; 
                }

                if (count == 4)
                {
                    List<Card> resultHandCarts = new List<Card>();
                    for (int j = i-2; j <= i+1; j++)
                    {
                        resultHandCarts.Add(new Card { Suits = cards[j].Suits, Value = cards[j].Value });
                    }
                    combinations.Add(new ResultGame { PlayerCards = playerCards, HandValue = 8, ResultHand = resultHandCarts });
                }
                else if (count == 3)
                {
                    List<Card> resultHandCarts = new List<Card>();
                    for (int j = i - 1; j <= i+1; j++)
                    {
                        resultHandCarts.Add(new Card { Suits = cards[j].Suits, Value = cards[j].Value });
                    }
                    combinations.Add(new ResultGame { PlayerCards = playerCards, HandValue = 4, ResultHand = resultHandCarts });
                }
                else if (count == 2)
                {
                    List<Card> resultHandCarts = new List<Card>();
                    for (int j = i; j <= i+1; j++)
                    {
                        resultHandCarts.Add(new Card { Suits = cards[j].Suits, Value = cards[j].Value });
                    }
                    combinations.Add(new ResultGame { PlayerCards = playerCards, HandValue = 2, ResultHand = resultHandCarts });
                }
            }
            FullHouse(combinations, playerCards, cards, resultGame);
        }

        public static void FullHouse(List<ResultGame> combinations, List<Card> playerCards,  in List<Card> cards, List<ResultGame> resultGame)
        {
            var max = combinations.Where(c => c.HandValue == Convert.ToInt32(combinations.Max(e => e.HandValue))).ToList();
            if (max.Count != 0)
            {
                if (5 - max[0].ResultHand.Count == 1)
                {
                    cards.RemoveAll(p=>p.Value == max[0].ResultHand[0].Value);
                    max[0].ResultHand.Add(cards[0]);
                    ResultConverterCards(8, 0, playerCards, max[0].ResultHand, resultGame);
                }
                else if (5 - max[0].ResultHand.Count == 2)
                {
                    if (combinations.Count > 2)
                    {
                        var pair = combinations.Where(e => e.HandValue == 2).ToList();
                        if (pair.Count > 1)
                        {
                            pair.RemoveAll(p => p.ResultHand[0].Value == max[0].ResultHand[0].Value);
                        }
                        foreach (Card card in pair[0].ResultHand)
                        {
                            cards.RemoveAll(p => p.Value == max[0].ResultHand[0].Value);
                            max[0].ResultHand.Add(card);
                        }
                        ResultConverterCards(7, 0, playerCards, max[0].ResultHand, resultGame);
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            cards.RemoveAll(p => p.Value == max[0].ResultHand[0].Value);
                            max[0].ResultHand.Add(cards[i]);
                        }
                        ResultConverterCards(4, 0, playerCards, max[0].ResultHand, resultGame);
                    }
                }
                else if (5 - max[0].ResultHand.Count == 3)
                {
                    if (combinations.Count > 1)
                    {
                        var pair = combinations.Where(e => e.HandValue == 2).ToList();
                        if (pair.Count > 1)
                        {
                            pair.RemoveAll(p => p.ResultHand[0].Value == max[0].ResultHand[0].Value);
                            //pair.Remove(pair[0]);
                        }
                        foreach (Card card in pair[0].ResultHand)
                        {
                           cards.RemoveAll(p => p.Value == max[0].ResultHand[0].Value);
                            max[0].ResultHand.Add(card);
                        }
                        max[0].ResultHand.Add(cards[0]);
                        ResultConverterCards(3, 0, playerCards, max[0].ResultHand, resultGame);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            cards.RemoveAll(p => p.Value == max[0].ResultHand[0].Value);
                            max[0].ResultHand.Add(cards[i]);
                        }
                        ResultConverterCards(2, 0, playerCards, max[0].ResultHand, resultGame);

                    }
                }
            }
            else
            {
                SearchOlder(playerCards, cards, resultGame);
            }

        }

        public static void SearchOlder(List<Card> playerCards, List<Card> cards, List<ResultGame> resultGame)
        {
            List<Card> older = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                older.Add(cards[i]);
            }
            ResultConverterCards(1, 0, playerCards, cards, resultGame);
        }

        public static void ResultConverterCards(int resultHandValue, int i, List<Card> playerCards, List<Card> handCards, List<ResultGame> resultGames)
        {
            if (resultGames.Where(p => p.PlayerCards == playerCards).Count() == 0 || resultHandValue > resultGames.Where(p => p.PlayerCards == playerCards).Max(e => e.HandValue) )
            {
                resultGames.RemoveAll(p => p.PlayerCards == playerCards);
                List<Card> resultHandCarts = new List<Card>();
                for (int j = i; j < i + 5; j++)
                {
                    resultHandCarts.Add(new Card { Suits = handCards[j].Suits, Value = handCards[j].Value });
                }
                resultGames.Add(new ResultGame {HandValue = resultHandValue, PlayerCards = playerCards, ResultHand = resultHandCarts });
            }
        }
    }
}
