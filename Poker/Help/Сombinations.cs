using System;
using System.Collections.Generic;
using System.Linq;

using Poker.Model;
using static Poker.Help.SortHandCards;
using static Poker.Help.HelpCombinations;

namespace Poker.Help
{
    public static class Сombinations
    {
        /// <summary>
        /// Define Result: 
        /// 1. Сheck the type of the game. According to the game type, we add the player's cards to the table cards.
        /// 2. Check combinations => calling method DefinitionCombinations
        /// 3. Returning the best result
        /// </summary>
        /// <param name="type">Type game</param>
        /// <param name="board">Table cards</param>
        /// <param name="playerCards">Hand cards player</param>
        /// <returns>The best result</returns>
        public static List<ResultGame> DefineResult(GameType type, List<Card> board, List<Card> playerCards)
        {
            var resultGame = new List<ResultGame>();
            List<Card> handCards;

            switch (type)
            {
                case GameType.Holdem:
                    handCards = new List<Card>(board);
                    handCards.AddRange(playerCards);
                    resultGame = DefineCombinations(playerCards, handCards);
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
                            resultGame = DefineCombinations(playerCards, handCards);
                        }
                    }
                    break;
                case GameType.FiveCard:
                    handCards = new List<Card>();
                    handCards.AddRange(playerCards);
                    resultGame = DefineCombinations(playerCards, handCards);
                    break;
            }
            return resultGame;
        }

        /// <summary>
        /// Find best combinations
        /// Check Flush. If Flush then check StraightFlush, if StraightFlush then return resultGame
        /// else check the rest combinations
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Table cards + player cards</param>
        /// <returns>The best result</returns>
        public static List<ResultGame> DefineCombinations(List<Card> playerCards, List<Card> handCards)
        {
            List<ResultGame> combinations;
            List<ResultGame> max;
            var preliminaryResult = new List<ResultGame>();

            preliminaryResult = Flush(playerCards, SortCardsBySuit(handCards), preliminaryResult);

            if (preliminaryResult.Count != 0 && preliminaryResult[0].HandValue == 6)
            {
                preliminaryResult = StraightFlush(playerCards, SortCardsByValue(handCards), preliminaryResult);
            }

            if(preliminaryResult.Count != 0 && preliminaryResult[0].HandValue > 9)
            {
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
            }
          
            return preliminaryResult;
        }

        /// <summary>
        /// Define Flush
        /// Sorting cards by suit, if 5 consecutive cards have the same suit, then Flush
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Table cards + player cards</param>
        /// <param name="resultGame">Result cards player</param>
        /// <returns>List result player in game</returns>
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

        /// <summary>
        /// Define StraightFlush
        /// Sorting cards by value and suite, if we find 5 consecutive cards have the same suit 
        /// and difference between each subsequent card is 1, then StraightFlush
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Table cards + player cards</param>
        /// <param name="resultGame">Result cards player</param>
        /// <returns>List result player in game</returns>
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

        /// <summary>
        /// Define StraightFlush
        /// Sorting cards by value and suite, if we find 5 consecutive cards difference between each subsequent card is 1, then Straight
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Table cards + player cards</param>
        /// <param name="resultGame">Result cards player</param>
        /// <returns>List result player in game</returns>
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

        /// <summary>
        /// Define FourKind
        /// Sorting card by value, if we find 4 consecutive cards and add one best card, then FourKind
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Table cards + player cards</param>
        /// <param name="max">The best consecutive cards</param>
        /// <param name="resultGame">Result cards player</param>
        /// <returns>List result player in game</returns>
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

        /// <summary>
        /// Define FullHouseORThreeKind
        /// Sorting card by value, if we find 3 consecutive cards and add find 2 consecutive cards, then FullHouse, 
        /// else if we find 3 consecutive cards and we don't find 2 consecutive cards, then ThreeKind
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Table cards + player cards</param>
        /// <param name="max">The best consecutive cards</param>
        /// <param name="resultGame">Result cards player</param>
        /// <returns>List result player in game</returns>
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

        /// <summary>
        /// Define TwoPairsORPairs
        /// Sorting card by value, if we find 2 consecutive cards and add find 2 consecutive cards, then TwoPairs, 
        /// else if we find 2 consecutive cards and we don't find 2 consecutive cards, then Pairs
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Table cards + player cards</param>
        /// <param name="max">The best consecutive cards</param>
        /// <param name="resultGame">Result cards player</param>
        /// <returns>List result player in game</returns>
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

        /// <summary>
        /// Define SearchOlder
        /// Sorting card by value, we choose five the best card
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Table cards + player cards</param>
        /// <param name="resultGame">Result cards player</param>
        /// <returns>List result player in game</returns>
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
       
    }
}