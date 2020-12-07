using Poker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Help
{
    public static class HelpCombinations
    {
        /// <summary>
        /// search for all possible variants of 2 cards from your hand
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <returns>all possible variants of 2 cards from your hand</returns>
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

        /// <summary>
        /// search for all possible variants of 3 cards from board
        /// </summary>
        /// <param name="board">Table cards</param>
        /// <returns>all possible variants of 3 cards from board</returns>
        public static List<List<Card>> GetBoardCombinations(List<Card> board)
        {
            var boardCombinations = new List<List<Card>>();
            var combinationCard = new List<Card>();
            for (var i = 0; i < board.Count; i++)
            {
                var card = new List<Card>(board);
                for (var j = i; j < board.Count - 1; j++)
                {
                    card.Remove(card[i]);
                    card.Remove(card[j]);
                    combinationCard.AddRange(card);
                    card.Clear();
                    card.AddRange(board);
                }
            }

            for (var i = 0; i < combinationCard.Count; i = i + 3)
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

        /// <summary>
        /// search for all possible variants of the same cards
        /// </summary>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Five hand cards player</param>
        /// <returns>all possible variants</returns>
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

        public static List<ResultGame> ResultConverterCards(int resultHandValue, int i, List<Card> playerCards,
        List<Card> handCards, List<ResultGame> resultGames)
        {
            if (resultGames.Count(p => p.PlayerCards == playerCards) != 0 && resultHandValue <
                resultGames.Where(p => p.PlayerCards == playerCards).Max(e => e.HandValue))
            {
                return resultGames;
            }
            else if (resultGames.Count(p => p.PlayerCards == playerCards) != 0 && resultHandValue ==
                     resultGames.Where(p => p.PlayerCards == playerCards).Max(e => e.HandValue))
            {
                if (ChoosingBestHand(handCards, resultGames.Where(p => p.PlayerCards == playerCards).ToList()))
                {
                    return resultGames;
                }
                else
                {
                    return RememberResultCard(resultHandValue, i, playerCards, handCards, resultGames);
                }

            }
            else
            {
                return RememberResultCard(resultHandValue, i, playerCards, handCards, resultGames);
            }
        }

        /// <summary>
        /// method writes the result to a List<ResultGame>
        /// </summary>
        /// <param name="resultHandValue">value combinations</param>
        /// <param name="i"></param>
        /// <param name="playerCards">Hand cards player</param>
        /// <param name="handCards">Five hand cards player</param>
        /// <param name="resultGames">The best result five hand cards player</param>
        /// <returns>The best result five hand cards player</returns>
        public static List<ResultGame> RememberResultCard(int resultHandValue, int i, List<Card> playerCards,
         List<Card> handCards, List<ResultGame> resultGames)
        {
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

        /// <summary>
        /// method compares maps, if the recorded result is greater, than the input one it returns true, else false
        /// </summary>
        /// <param name="handCards">Five hand cards player</param>
        /// <returns>The best result five hand cards player</returns>
        public static bool ChoosingBestHand(List<Card> handCards, List<ResultGame> resultGames)
        {
            for (var i = 0; i < resultGames[0].ResultHand.Count; i++)
            {
                if (resultGames[0].ResultHand[i].Value > handCards[i].Value)
                {
                    return true;
                }
                else if (resultGames[0].ResultHand[i].Value < handCards[i].Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
