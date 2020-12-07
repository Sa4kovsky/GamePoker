using Poker.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestPoker.TestsHelper
{
    public class HelpCombinationsTests
    {
        [Fact]
        //search for all possible variants of 2 cards from your hand
        public void GetHandCombinationsTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 1, Suit = 'd' });
            playerCards.Add(new Card { Value = 2, Suit = 'd' });
            playerCards.Add(new Card { Value = 3, Suit = 'd' });
            playerCards.Add(new Card { Value = 4, Suit = 'd' });

            var expected = new List<List<Card>>();
            var card = new List<Card>();
            card.Add(new Card { Value = 1, Suit = 'd' });
            card.Add(new Card { Value = 2, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 1, Suit = 'd' });
            card.Add(new Card { Value = 3, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 1, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 2, Suit = 'd' });
            card.Add(new Card { Value = 3, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 2, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 3, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'd' });
            expected.Add(card);

            // Act
            var actual = Poker.Help.HelpCombinations.GetHandCombinations(playerCards);

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                for (var j = 0; j < expected[i].Count; j++)
                {
                    Assert.Equal(expected[i][j].Value, actual[i][j].Value);
                    Assert.Equal(expected[i][j].Suit, actual[i][j].Suit);
                }
            }
        }

        [Fact]
        //search for all possible variants of 2 cards from your hand
        public void GetBoardCombinationsTest()
        {
            // Arrange
            var board = new List<Card>();
            board.Add(new Card { Value = 1, Suit = 'd' });
            board.Add(new Card { Value = 2, Suit = 'd' });
            board.Add(new Card { Value = 3, Suit = 'd' });
            board.Add(new Card { Value = 4, Suit = 'd' });
            board.Add(new Card { Value = 5, Suit = 'd' });

            var expected = new List<List<Card>>();
            var card = new List<Card>();
            card.Add(new Card { Value = 3, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'd' });
            card.Add(new Card { Value = 5, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 2, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'd' });
            card.Add(new Card { Value = 5, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 2, Suit = 'd' });
            card.Add(new Card { Value = 3, Suit = 'd' });
            card.Add(new Card { Value = 5, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 2, Suit = 'd' });
            card.Add(new Card { Value = 3, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 1, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'd' });
            card.Add(new Card { Value = 5, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 1, Suit = 'd' });
            card.Add(new Card { Value = 3, Suit = 'd' });
            card.Add(new Card { Value = 5, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 1, Suit = 'd' });
            card.Add(new Card { Value = 3, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 1, Suit = 'd' });
            card.Add(new Card { Value = 2, Suit = 'd' });
            card.Add(new Card { Value = 5, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 1, Suit = 'd' });
            card.Add(new Card { Value = 2, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'd' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 1, Suit = 'd' });
            card.Add(new Card { Value = 2, Suit = 'd' });
            card.Add(new Card { Value = 3, Suit = 'd' });
            expected.Add(card);

            // Act
            var actual = Poker.Help.HelpCombinations.GetBoardCombinations(board);

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                for (var j = 0; j < expected[i].Count; j++)
                {
                    Assert.Equal(expected[i][j].Value, actual[i][j].Value);
                    Assert.Equal(expected[i][j].Suit, actual[i][j].Suit);
                }
            }
        }

        [Fact]
        //search for all possible variants of the same cards
        public void SearchMatchTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 4, Suit = 'd' });
            playerCards.Add(new Card { Value = 2, Suit = 'd' });
            
            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 4, Suit = 'd' });
            handCards.Add(new Card { Value = 4, Suit = 'c' });
            handCards.Add(new Card { Value = 4, Suit = 's' });
            handCards.Add(new Card { Value = 3, Suit = 'd' });
            handCards.Add(new Card { Value = 3, Suit = 'c' });
            handCards.Add(new Card { Value = 2, Suit = 'd' });
            handCards.Add(new Card { Value = 2, Suit = 'c' });

            var expected = new List<List<Card>>();
            var card = new List<Card>();
            card.Add(new Card { Value = 4, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'c' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 4, Suit = 'd' });
            card.Add(new Card { Value = 4, Suit = 'c' });
            card.Add(new Card { Value = 4, Suit = 's' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 3, Suit = 'd' });
            card.Add(new Card { Value = 3, Suit = 'c' });
            expected.Add(card);
            card = new List<Card>();
            card.Add(new Card { Value = 2, Suit = 'd' });
            card.Add(new Card { Value = 2, Suit = 'c' });
            expected.Add(card);

            // Act
            var actual = Poker.Help.HelpCombinations.SearchMatch(playerCards, handCards);

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                for (var j = 0; j < expected[i].Count; j++)
                {
                    Assert.Equal(expected[i][j].Value, actual[i].ResultHand[j].Value);
                    Assert.Equal(expected[i][j].Suit, actual[i].ResultHand[j].Suit);
                }
            }
        }

        [Fact]
        //method compares maps, if the recorded result is greater, than the input one it returns true
        public static void ChoosingBestHandTestTrue()
        {
            // Arrange
            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 13, Suit = 'd' });
            handCards.Add(new Card { Value = 13, Suit = 's' });
            handCards.Add(new Card { Value = 12, Suit = 'c' });
            handCards.Add(new Card { Value = 12, Suit = 's' });
            handCards.Add(new Card { Value = 10, Suit = 'd' });

            var resultGame = new List<ResultGame>();
            var card = new List<Card>();
            card.Add(new Card { Value = 13, Suit = 'd' });
            card.Add(new Card { Value = 13, Suit = 's' });
            card.Add(new Card { Value = 12, Suit = 'c' });
            card.Add(new Card { Value = 12, Suit = 's' });
            card.Add(new Card { Value = 11, Suit = 'd' });

            resultGame.Add(new ResultGame { ResultHand = card });

            // Act
            var actual = Poker.Help.HelpCombinations.ChoosingBestHand(handCards, resultGame);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        //method compares maps, if the recorded result is less than the input, it returns false
        public static void ChoosingBestHandTestFalse()
        {
            // Arrange
            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 13, Suit = 'd' });
            handCards.Add(new Card { Value = 13, Suit = 's' });
            handCards.Add(new Card { Value = 12, Suit = 'c' });
            handCards.Add(new Card { Value = 12, Suit = 's' });
            handCards.Add(new Card { Value = 10, Suit = 'd' });

            var resultGame = new List<ResultGame>();
            var card = new List<Card>();
            card.Add(new Card { Value = 13, Suit = 'd' });
            card.Add(new Card { Value = 13, Suit = 's' });
            card.Add(new Card { Value = 12, Suit = 'c' });
            card.Add(new Card { Value = 12, Suit = 's' });
            card.Add(new Card { Value = 9, Suit = 'd' });

            resultGame.Add(new ResultGame { ResultHand = card });

            // Act
            var actual = Poker.Help.HelpCombinations.ChoosingBestHand(handCards, resultGame);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        //method writes the result to a List<ResultGame>
        public static void RememberResultCardTest()
        {
            // Arrange
            var playerCards = new List<Card>();
            playerCards.Add(new Card { Value = 13, Suit = 'd' });
            playerCards.Add(new Card { Value = 12, Suit = 's' });

            var handCards = new List<Card>();
            handCards.Add(new Card { Value = 13, Suit = 'd' });
            handCards.Add(new Card { Value = 13, Suit = 's' });
            handCards.Add(new Card { Value = 12, Suit = 'c' });
            handCards.Add(new Card { Value = 12, Suit = 's' });
            handCards.Add(new Card { Value = 9, Suit = 'd' });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 3, PlayerCards = playerCards, ResultHand = handCards });


            // Act
            var actual = Poker.Help.HelpCombinations.RememberResultCard(resultHandValue: 3, i: 0, playerCards, handCards, new List<ResultGame>());

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
