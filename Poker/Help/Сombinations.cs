using System;
using System.Collections.Generic;
using System.Linq;

using Poker.Model;
using static Poker.Help.SortHandCards;

namespace Poker.Help
{
    public static class Сombinations
    {
        public static List<ResultGame> DefinitionCombinations(GameType type, List<Card> board, List<Card> playerCards)
        {
            var resultGame = new List<ResultGame>();
            var preliminaryResult = new List<ResultGame>();
            List<ResultGame> combinations;
            List<ResultGame> max;
            List<Card> handCards;

            switch (type)
            {
                case GameType.Holdem:
                    handCards = new List<Card>(board);
                    handCards.AddRange(playerCards);

                    preliminaryResult = Flush(playerCards, SortCardsBySuit(handCards), preliminaryResult);

                    if (preliminaryResult.Count != 0 && preliminaryResult[0].HandValue == 6)
                    {
                        preliminaryResult = StraightFlush(playerCards, SortCardsByValue(handCards), preliminaryResult);
                    }

                    preliminaryResult = Straight(playerCards, SortCardsByValue(handCards), preliminaryResult);

                    combinations = SearchMatch(playerCards, SortCardsByValue(handCards));
                    max = combinations.Where(c => c.HandValue == Convert.ToInt32(combinations.Max(e => e.HandValue)))
                        .ToList();

                    if (max.Count != 0)
                    {
                        switch (5 - max[0].ResultHand.Count)
                        {
                            case 1:
                                {
                                    preliminaryResult = FourKind(playerCards, SortCardsByValue(handCards), max, preliminaryResult);
                                    break;
                                }

                            case 2:
                                {
                                    preliminaryResult = FullHouseORThreeKind(playerCards, SortCardsByValue(handCards), combinations, max, preliminaryResult);
                                    break;
                                }

                            case 3:
                                {
                                    preliminaryResult = TwoPairsORPairs(playerCards, SortCardsByValue(handCards), combinations, max, preliminaryResult);
                                    break;
                                }
                        }
                    }
                    else 
                    {
                        preliminaryResult = SearchOlder(playerCards, SortCardsByValue(handCards), preliminaryResult);
                    }

                    resultGame = preliminaryResult;

                    break;
                case GameType.Omaha:
                    var playerPairs = GetHandCombinations(playerCards);
                    var boardPairs = GetBoardCombinations(board);

                    foreach (var e in playerPairs)
                    {
                        foreach (var p in boardPairs)
                        {
                            handCards = new List<Card>(p);

                            handCards.AddRange(e);

                            preliminaryResult = Flush(playerCards, SortCardsBySuit(handCards), preliminaryResult);

                            if (preliminaryResult.Count != 0 && preliminaryResult[0].HandValue == 6)
                            {
                                preliminaryResult = StraightFlush(playerCards, SortCardsByValue(handCards), preliminaryResult);
                            }

                            preliminaryResult = Straight(playerCards, SortCardsByValue(handCards), preliminaryResult);

                            combinations = SearchMatch(playerCards, SortCardsByValue(handCards));
                            max = combinations.Where(c => c.HandValue == Convert.ToInt32(combinations.Max(e => e.HandValue)))
                                .ToList();

                            if (max.Count != 0)
                            {
                                switch (5 - max[0].ResultHand.Count)
                                {
                                    case 1:
                                        {
                                            preliminaryResult = FourKind(playerCards, SortCardsByValue(handCards), max, preliminaryResult);
                                            break;
                                        }

                                    case 2:
                                        {
                                            preliminaryResult = FullHouseORThreeKind(playerCards, SortCardsByValue(handCards), combinations, max, preliminaryResult);
                                            break;
                                        }

                                    case 3:
                                        {
                                            preliminaryResult = TwoPairsORPairs(playerCards, SortCardsByValue(handCards), combinations, max, preliminaryResult);
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                preliminaryResult = SearchOlder(playerCards, SortCardsByValue(handCards), preliminaryResult);
                            }
                            resultGame = preliminaryResult;
                        }
                    }
                 
                    break;
                case GameType.FiveCard:
                    handCards = new List<Card>();
                    handCards.AddRange(playerCards);

                    preliminaryResult = Flush(playerCards, SortCardsBySuit(handCards), preliminaryResult);

                    if (preliminaryResult.Count != 0 && preliminaryResult[0].HandValue == 6)
                    {
                        preliminaryResult = StraightFlush(playerCards, SortCardsByValue(handCards), preliminaryResult);
                    }

                    preliminaryResult = Straight(playerCards, SortCardsByValue(handCards), preliminaryResult);

                    combinations = SearchMatch(playerCards, SortCardsByValue(handCards));
                    max = combinations.Where(c => c.HandValue == Convert.ToInt32(combinations.Max(e => e.HandValue)))
                        .ToList();

                    if (max.Count != 0)
                    {
                        switch (5 - max[0].ResultHand.Count)
                        {
                            case 1:
                                {
                                    preliminaryResult = FourKind(playerCards, SortCardsByValue(handCards), max, preliminaryResult);
                                    break;
                                }

                            case 2:
                                {
                                    preliminaryResult = FullHouseORThreeKind(playerCards, SortCardsByValue(handCards), combinations, max, preliminaryResult);
                                    break;
                                }

                            case 3:
                                {
                                    preliminaryResult = TwoPairsORPairs(playerCards, SortCardsByValue(handCards), combinations, max, preliminaryResult);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        preliminaryResult = SearchOlder(playerCards, SortCardsByValue(handCards), preliminaryResult);
                    }

                    resultGame = preliminaryResult;
                    break;
            }

            return resultGame;
        }

        public static List<List<Card>> GetHandCombinations(List<Card> playerCards)
        {
            var cards = new List<List<Card>>();
            for (var i = 0; i < playerCards.Count; i++)
            {
                for (var j = i + 1; j < playerCards.Count; j++)
                {
                    var combinationCard = new List<Card>
                    {
                        new Card {Suit = playerCards[i].Suit, Value = playerCards[i].Value},
                        new Card {Suit = playerCards[j].Suit, Value = playerCards[j].Value}
                    };
                    cards.Add(combinationCard);
                }
            }
            return cards;
        }

        public static List<List<Card>> GetBoardCombinations(List<Card> board)
        {
            var boardCombinations = new List<List<Card>>();
            var combinationCard = new List<Card>();
            for (var i = 0; i < board.Count; i++)
            {
                var card = new List<Card>(board);
                for (var j = i; j < board.Count-1; j++)
                {
                    card.Remove(card[i]);
                    card.Remove(card[j]);
                    combinationCard.AddRange(card);
                    card.Clear();
                    card.AddRange(board);
                }
            }
            
            for (var i = 0; i < combinationCard.Count; i=i+3)
            {
                var combination = new List<Card>();
                for (var j = 0 + i; j < 3 + i; j++)
                {
                    combination.Add(new Card { Value = combinationCard[j].Value, Suit = combinationCard[j].Suit });
                }
                boardCombinations.Add(combination);
            }

            return boardCombinations;
        }

        public static List<ResultGame> Flush (List<Card> playerCards, List<Card> handCards, List<ResultGame> resultGame)
        {
            for (int i = 0; i < handCards.Count - 4; i++)
            {
                if (handCards[i].Suit == handCards[i + 4].Suit)
                {
                    resultGame = ResultConverterCards(resultHandValue: 6, i, playerCards, handCards, resultGame);
                    break;
                }
            }

            return resultGame;
        }

        public static List<ResultGame> StraightFlush(List<Card> playerCards, List<Card> handCards, List<ResultGame> resultGame)
        {
            var countFlush = 1;

            for (var i = 0; i < handCards.Count - 1; i++)
            {
                if (handCards[i].Value - handCards[i + 1].Value == 1 &&
                    handCards[i].Suit == handCards[i + 1].Suit)
                {
                    countFlush++;
                    if (countFlush == 5)
                    {
                        if (handCards.Where(a => a.Value == 14).Count(s => s.Suit == handCards[i].Suit) == 1)
                        {
                            resultGame = ResultConverterCards(resultHandValue: 10, i - 3, playerCards, handCards, resultGame);
                            break;
                        }
                        else
                        {
                            resultGame = ResultConverterCards(resultHandValue: 9, i - 3, playerCards, handCards, resultGame);
                            break;
                        }
                    }
                    else if (countFlush == 4
                             && handCards[i].Value == 3
                             && handCards[i].Suit == handCards[i - 1].Suit
                             && handCards.Where(a => a.Value == 14).Count(s => s.Suit == handCards[i].Suit) == 1
                             && handCards.Where(a => a.Value == 2).Count(s => s.Suit == handCards[i].Suit) == 1)
                    {
                        handCards.Insert(i + 2, new Card { Value = 14, Suit = handCards[i].Suit });
                        resultGame = ResultConverterCards(resultHandValue: 9, i - 2, playerCards, handCards, resultGame);
                        break;
                    }
                }
                else if (handCards[i].Value - handCards[i + 1].Value > 1 || handCards[i].Value == handCards[i + 1].Value || handCards[i].Suit != handCards[i + 1].Suit)
                {
                    countFlush = 1;
                }
            }

            return resultGame;
        }

        public static List<ResultGame> Straight (List<Card> playerCards, List<Card> handCards, List<ResultGame> resultGame)
        {
            var cardsPlayer = new List<Card>(handCards);
            var countStraight = 1;

            for (var i = 0; i < cardsPlayer.Count - 1; i++)
            {
                if (cardsPlayer[i].Value - cardsPlayer[i + 1].Value == 1)
                {
                    countStraight++;
                    if (countStraight == 5)
                    {
                        resultGame = ResultConverterCards(resultHandValue: 5, i - 3, playerCards, cardsPlayer, resultGame);
                        break;
                    }
                    else if (countStraight == 4
                             && cardsPlayer[i].Value == 3
                             && cardsPlayer[i].Suit == cardsPlayer[i - 1].Suit
                             && cardsPlayer.Count(a => a.Value == 14) == 1
                             && cardsPlayer.Count(a => a.Value == 2) == 1)
                    {
                        cardsPlayer.Insert(i + 2, new Card { Value = 14, Suit = cardsPlayer[i].Suit });
                        resultGame = ResultConverterCards(resultHandValue: 5, i - 2, playerCards, cardsPlayer, resultGame);
                        break;
                    }
                }
                else if (cardsPlayer[i].Value == cardsPlayer[i + 1].Value)
                {
                    cardsPlayer.Remove(cardsPlayer[i + 1]);
                    i -= 1;
                }
                else if (cardsPlayer[i].Value - cardsPlayer[i + 1].Value > 1)
                {
                    countStraight = 1;
                }
            }

            return resultGame;
        }

        public static List<ResultGame> SearchMatch(List<Card> playerCards, List<Card> handCards)
        {
            var combinations = new List<ResultGame>();

            var count = 1;
            for (var i = 0; i < handCards.Count - 1; i++)
            {
                if (handCards[i].Value - handCards[i + 1].Value == 0)
                {
                    count++;
                }
                else
                {
                    count = 1;
                }

                switch (count)
                {
                    case 4:
                        {
                            var resultHandCarts = new List<Card>();
                            for (var j = i - 2; j <= i + 1; j++)
                            {
                                resultHandCarts.Add(new Card { Suit = handCards[j].Suit, Value = handCards[j].Value });
                            }

                            combinations.Add(new ResultGame
                            { PlayerCards = playerCards, HandValue = 8, ResultHand = resultHandCarts });
                            break;
                        }

                    case 3:
                        {
                            var resultHandCarts = new List<Card>();
                            for (var j = i - 1; j <= i + 1; j++)
                            {
                                resultHandCarts.Add(new Card { Suit = handCards[j].Suit, Value = handCards[j].Value });
                            }

                            combinations.Add(new ResultGame
                            { PlayerCards = playerCards, HandValue = 4, ResultHand = resultHandCarts });
                            break;
                        }

                    case 2:
                        {
                            var resultHandCarts = new List<Card>();
                            for (var j = i; j <= i + 1; j++)
                            {
                                resultHandCarts.Add(new Card { Suit = handCards[j].Suit, Value = handCards[j].Value });
                            }

                            combinations.Add(new ResultGame
                            { PlayerCards = playerCards, HandValue = 2, ResultHand = resultHandCarts });
                            break;
                        }
                }
            }

            return combinations;
        }

        public static List<ResultGame> FourKind(List<Card> playerCards, List<Card> handCards, List<ResultGame> max, List<ResultGame> resultGame)
        {
            if (max.Count != 0)
            {
                if (5 - max[0].ResultHand.Count == 1)
                {
                    handCards.RemoveAll(p => p.Value == max[0].ResultHand[0].Value);
                    max[0].ResultHand.Add(handCards[0]);
                    resultGame = ResultConverterCards(resultHandValue: 8, 0, playerCards, max[0].ResultHand, resultGame);
                }
            }
            return resultGame;
        }

        public static List<ResultGame> FullHouseORThreeKind(List<Card> playerCards, List<Card> handCards, List<ResultGame> combinations, List<ResultGame> max, List<ResultGame> resultGame)
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
                    handCards.RemoveAll(p => p.Value == max[0].ResultHand[0].Value);
                    max[0].ResultHand.Add(card);
                }

                resultGame = ResultConverterCards(resultHandValue: 7, i: 0, playerCards, max[0].ResultHand, resultGame);
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    handCards.RemoveAll(p => p.Value == max[0].ResultHand[0].Value);
                    max[0].ResultHand.Add(handCards[i]);
                }

                resultGame = ResultConverterCards(resultHandValue: 4, i: 0, playerCards, max[0].ResultHand, resultGame);
            }

            return resultGame;
        }

        public static List<ResultGame> TwoPairsORPairs(List<Card> playerCards, List<Card> handCards, List<ResultGame> combinations, List<ResultGame> max, List<ResultGame> resultGame)
        {
            if (combinations.Count > 1)
            {
                var pair = combinations.Where(e => e.HandValue == 2).ToList();
                if (pair.Count > 1)
                {
                    pair.RemoveAll(p => p.ResultHand[0].Value == max[0].ResultHand[0].Value);
                }

                foreach (var card in pair[0].ResultHand)
                {
                    handCards.RemoveAll(p => p.Value == max[0].ResultHand[0].Value);
                    max[0].ResultHand.Add(card);
                }

                handCards.RemoveAll(p => p.Value == max[0].ResultHand[3].Value);
                max[0].ResultHand.Add(handCards[0]);
                resultGame = ResultConverterCards(resultHandValue: 3, i: 0, playerCards, max[0].ResultHand, resultGame);
            }
            else
            {
                for (var i = 0; i < 3; i++)
                {
                    handCards.RemoveAll(p => p.Value == max[0].ResultHand[0].Value);
                    max[0].ResultHand.Add(handCards[i]);
                }

                resultGame = ResultConverterCards(resultHandValue: 2, i: 0, playerCards, max[0].ResultHand, resultGame);
            }

            return resultGame;
        } 

        public static List<ResultGame> SearchOlder(List<Card> playerCards, List<Card> handCards, List<ResultGame> resultGame)
        {
            var older = new List<Card>();
            for (var i = 0; i < 5; i++)
            {
                older.Add(handCards[i]);
            }

            resultGame = ResultConverterCards(1, 0, playerCards, handCards, resultGame);

            return resultGame;
        }

        public static List<ResultGame> ResultConverterCards(int resultHandValue, int i, List<Card> playerCards,
            List<Card> handCards, List<ResultGame> resultGames)
        {
            if (resultGames.Count(p => p.PlayerCards == playerCards) != 0 && resultHandValue <=
                resultGames.Where(p => p.PlayerCards == playerCards).Max(e => e.HandValue))
            {
                return resultGames;
            }
            resultGames.RemoveAll(p => p.PlayerCards == playerCards);
            var resultHandCarts = new List<Card>();
            for (var j = i; j < i + 5; j++)
            {
                resultHandCarts.Add(new Card { Suit = handCards[j].Suit, Value = handCards[j].Value });
            }

            resultGames.Add(new ResultGame
            { HandValue = resultHandValue, PlayerCards = playerCards, ResultHand = resultHandCarts });
            return resultGames;
        }
    }
}