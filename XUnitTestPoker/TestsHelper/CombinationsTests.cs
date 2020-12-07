using Poker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestPoker.TestsHelper
{
    public class CombinationsTests
    {
       [Fact]
        //Defining Flush
        public void FlushTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 6, Suit = 'd' });
            playerCards.Add(new Card { Value = 5, Suit = 'd' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 14, Suit = 'd' });
            handCards.Add(new Card { Value = 10, Suit = 'd' });
            handCards.Add(new Card { Value = 12, Suit = 'd' });
            handCards.Add(new Card { Value = 8, Suit = 'c' });
            handCards.Add(new Card { Value = 9, Suit = 'c' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 14, Suit = 'd' });
            resultHand.Add(new Card { Value = 12, Suit = 'd' });
            resultHand.Add(new Card { Value = 10, Suit = 'd' });
            resultHand.Add(new Card { Value = 6, Suit = 'd' });
            resultHand.Add(new Card { Value = 5, Suit = 'd' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 6, PlayerCards = playerCards, ResultHand = resultHand });

            // Act
            var actual = Poker.Help.Сombinations.Flush(playerCards, Poker.Help.SortHandCards.SortCardsBySuit(handCards), new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }

        }

        [Fact]
        //Defining Royal Flush
        public void RoyalFlushTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 11, Suit = 'd' });
            playerCards.Add(new Card { Value = 13, Suit = 'd' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 14, Suit = 'd' });
            handCards.Add(new Card { Value = 10, Suit = 'd' });
            handCards.Add(new Card { Value = 12, Suit = 'd' });
            handCards.Add(new Card { Value = 8, Suit = 'c' });
            handCards.Add(new Card { Value = 9, Suit = 'c' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 14, Suit = 'd' });
            resultHand.Add(new Card { Value = 13, Suit = 'd' });
            resultHand.Add(new Card { Value = 12, Suit = 'd' });
            resultHand.Add(new Card { Value = 11, Suit = 'd' });
            resultHand.Add(new Card { Value = 10, Suit = 'd' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 10, PlayerCards = playerCards, ResultHand = resultHand });

            // Act
            var actual = Poker.Help.Сombinations.StraightFlush(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }

        [Fact]
        //Defining Straight Flush up to five
        public void StraightFlushTestUpToFive()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 2, Suit = 'd' });
            playerCards.Add(new Card { Value = 14, Suit = 'd' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 3, Suit = 'd' });
            handCards.Add(new Card { Value = 4, Suit = 'd' });
            handCards.Add(new Card { Value = 5, Suit = 'd' });
            handCards.Add(new Card { Value = 8, Suit = 'c' });
            handCards.Add(new Card { Value = 9, Suit = 'c' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 5, Suit = 'd' });
            resultHand.Add(new Card { Value = 4, Suit = 'd' });
            resultHand.Add(new Card { Value = 3, Suit = 'd' });
            resultHand.Add(new Card { Value = 2, Suit = 'd' });
            resultHand.Add(new Card { Value = 14, Suit = 'd' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 9, PlayerCards = playerCards, ResultHand = resultHand });

            // Act
            var actual = Poker.Help.Сombinations.StraightFlush(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }

        [Fact]
        //Defining Straight Flush
        public void StraightFlushTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 7, Suit = 'd' });
            playerCards.Add(new Card { Value = 5, Suit = 'd' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 3, Suit = 'd' });
            handCards.Add(new Card { Value = 4, Suit = 'd' });
            handCards.Add(new Card { Value = 6, Suit = 'd' });
            handCards.Add(new Card { Value = 8, Suit = 'c' });
            handCards.Add(new Card { Value = 9, Suit = 'c' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 7, Suit = 'd' });
            resultHand.Add(new Card { Value = 6, Suit = 'd' });
            resultHand.Add(new Card { Value = 5, Suit = 'd' });
            resultHand.Add(new Card { Value = 4, Suit = 'd' });
            resultHand.Add(new Card { Value = 3, Suit = 'd' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 9, PlayerCards = playerCards, ResultHand = resultHand });

            // Act
            var actual = Poker.Help.Сombinations.StraightFlush(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }

        [Fact]
        //Defining Straight
        public void StraightTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 7, Suit = 'c' });
            playerCards.Add(new Card { Value = 5, Suit = 'd' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 3, Suit = 'd' });
            handCards.Add(new Card { Value = 6, Suit = 'h' });
            handCards.Add(new Card { Value = 9, Suit = 'd' });
            handCards.Add(new Card { Value = 8, Suit = 'h' });
            handCards.Add(new Card { Value = 9, Suit = 'c' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 9, Suit = 'd' });
            resultHand.Add(new Card { Value = 8, Suit = 'h' });
            resultHand.Add(new Card { Value = 7, Suit = 'c' });
            resultHand.Add(new Card { Value = 6, Suit = 'h' });
            resultHand.Add(new Card { Value = 5, Suit = 'd' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 5, PlayerCards = playerCards, ResultHand = resultHand });

            // Act
            var actual = Poker.Help.Сombinations.Straight(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }

        [Fact]
        //Defining FourKind
        public void FourKindTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 4, Suit = 'd' });
            playerCards.Add(new Card { Value = 2, Suit = 's' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 2, Suit = 'd' });
            handCards.Add(new Card { Value = 2, Suit = 'c' });
            handCards.Add(new Card { Value = 2, Suit = 'h' });
            handCards.Add(new Card { Value = 3, Suit = 'd' });
            handCards.Add(new Card { Value = 5, Suit = 'h' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 2, Suit = 'd' });
            resultHand.Add(new Card { Value = 2, Suit = 'c' });
            resultHand.Add(new Card { Value = 2, Suit = 'h' });
            resultHand.Add(new Card { Value = 2, Suit = 's' });
            resultHand.Add(new Card { Value = 5, Suit = 'h' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 8, PlayerCards = playerCards, ResultHand = resultHand });

            var combinations = Poker.Help.HelpCombinations.SearchMatch(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards));
            var max = combinations.Where(c => c.HandValue == Convert.ToInt32(combinations.Max(e => e.HandValue)))
                .ToList();

            // Act
            var actual = Poker.Help.Сombinations.FourKind(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), max, new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }

        [Fact]
        //Defining FullHouse
        public void FullHouseTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 3, Suit = 'd' });
            playerCards.Add(new Card { Value = 2, Suit = 'h' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 2, Suit = 'd' });
            handCards.Add(new Card { Value = 2, Suit = 'c' });
            handCards.Add(new Card { Value = 4, Suit = 'h' });
            handCards.Add(new Card { Value = 3, Suit = 'c' });
            handCards.Add(new Card { Value = 5, Suit = 'h' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 2, Suit = 'd' });
            resultHand.Add(new Card { Value = 2, Suit = 'c' });
            resultHand.Add(new Card { Value = 2, Suit = 'h' });
            resultHand.Add(new Card { Value = 3, Suit = 'c' });
            resultHand.Add(new Card { Value = 3, Suit = 'd' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 7, PlayerCards = playerCards, ResultHand = resultHand });

            var combinations = Poker.Help.HelpCombinations.SearchMatch(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards));
            var max = combinations.Where(c => c.HandValue == Convert.ToInt32(combinations.Max(e => e.HandValue)))
                .ToList();

            // Act
            var actual = Poker.Help.Сombinations.FullHouseORThreeKind(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), combinations, max, new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }

        [Fact]
        //Defining Three Kind
        public void ThreeKindTest() 
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 8, Suit = 'd' });
            playerCards.Add(new Card { Value = 2, Suit = 'h' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 2, Suit = 'd' });
            handCards.Add(new Card { Value = 2, Suit = 'c' });
            handCards.Add(new Card { Value = 4, Suit = 'h' });
            handCards.Add(new Card { Value = 3, Suit = 'c' });
            handCards.Add(new Card { Value = 5, Suit = 'h' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 2, Suit = 'd' });
            resultHand.Add(new Card { Value = 2, Suit = 'c' });
            resultHand.Add(new Card { Value = 2, Suit = 'h' });
            resultHand.Add(new Card { Value = 8, Suit = 'd' });
            resultHand.Add(new Card { Value = 5, Suit = 'h' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 4, PlayerCards = playerCards, ResultHand = resultHand });

            var combinations = Poker.Help.HelpCombinations.SearchMatch(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards));
            var max = combinations.Where(c => c.HandValue == Convert.ToInt32(combinations.Max(e => e.HandValue)))
                .ToList();

            // Act
            var actual = Poker.Help.Сombinations.FullHouseORThreeKind(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), combinations, max, new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }

        [Fact]
        //Defining Two Pairs
        public void TwoPairsTest()
        { 
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 8, Suit = 'd' });
            playerCards.Add(new Card { Value = 2, Suit = 'h' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 2, Suit = 'd' });
            handCards.Add(new Card { Value = 6, Suit = 'c' });
            handCards.Add(new Card { Value = 4, Suit = 'h' });
            handCards.Add(new Card { Value = 3, Suit = 'c' });
            handCards.Add(new Card { Value = 8, Suit = 'h' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 8, Suit = 'h' });
            resultHand.Add(new Card { Value = 8, Suit = 'd' });
            resultHand.Add(new Card { Value = 2, Suit = 'd' });
            resultHand.Add(new Card { Value = 2, Suit = 'h' });
            resultHand.Add(new Card { Value = 6, Suit = 'c' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 3, PlayerCards = playerCards, ResultHand = resultHand });

            var combinations = Poker.Help.HelpCombinations.SearchMatch(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards));
            var max = combinations.Where(c => c.HandValue == Convert.ToInt32(combinations.Max(e => e.HandValue)))
                .ToList();

            // Act
            var actual = Poker.Help.Сombinations.TwoPairsORPairs(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), combinations, max, new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }

        [Fact]
        //Defining Pairs
        public void PairsTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 8, Suit = 'd' });
            playerCards.Add(new Card { Value = 9, Suit = 'h' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 2, Suit = 'd' });
            handCards.Add(new Card { Value = 6, Suit = 'c' });
            handCards.Add(new Card { Value = 10, Suit = 'h' });
            handCards.Add(new Card { Value = 3, Suit = 'c' });
            handCards.Add(new Card { Value = 8, Suit = 'h' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 8, Suit = 'h' });
            resultHand.Add(new Card { Value = 8, Suit = 'd' });
            resultHand.Add(new Card { Value = 10, Suit = 'h' });
            resultHand.Add(new Card { Value = 9, Suit = 'h' });
            resultHand.Add(new Card { Value = 6, Suit = 'c' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 2, PlayerCards = playerCards, ResultHand = resultHand });

            var combinations = Poker.Help.HelpCombinations.SearchMatch(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards));
            var max = combinations.Where(c => c.HandValue == Convert.ToInt32(combinations.Max(e => e.HandValue)))
                .ToList();

            // Act
            var actual = Poker.Help.Сombinations.TwoPairsORPairs(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), combinations, max, new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }

        [Fact]
        //Defining older hand
        public void SearchOlderTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 8, Suit = 'd' });
            playerCards.Add(new Card { Value = 9, Suit = 'h' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 2, Suit = 'd' });
            handCards.Add(new Card { Value = 6, Suit = 'c' });
            handCards.Add(new Card { Value = 10, Suit = 'h' });
            handCards.Add(new Card { Value = 3, Suit = 'c' });
            handCards.Add(new Card { Value = 13, Suit = 'h' });
            handCards.AddRange(playerCards);

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 13, Suit = 'h' });
            resultHand.Add(new Card { Value = 10, Suit = 'h' });
            resultHand.Add(new Card { Value = 9, Suit = 'h' });
            resultHand.Add(new Card { Value = 8, Suit = 'd' });
            resultHand.Add(new Card { Value = 6, Suit = 'c' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 1, PlayerCards = playerCards, ResultHand = resultHand });


            // Act
            var actual = Poker.Help.Сombinations.SearchOlder(playerCards, Poker.Help.SortHandCards.SortCardsByValue(handCards), new List<ResultGame>());

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].HandValue, actual[i].HandValue);
                for (var j = 0; j < expected[i].ResultHand.Count; j++)
                {
                    Assert.Equal(expected[i].ResultHand[j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i].ResultHand[j].Suit, actual[i].ResultHand[j].Suit);
                }
                for (var k = 0; k < expected[i].PlayerCards.Count; k++)
                {
                    Assert.Equal(expected[i].PlayerCards[k].Value, actual[i].PlayerCards[k].Value);
                    Assert.Equal(expected[i].PlayerCards[k].Suit, actual[i].PlayerCards[k].Suit);
                }
            }
        }
    }
}
