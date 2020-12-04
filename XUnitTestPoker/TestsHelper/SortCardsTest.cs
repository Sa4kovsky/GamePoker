using Poker.Model;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestPoker.TestsHelper
{
    public class SortCardsTest
    {
        [Fact]
        // Sorting cards by suit and value 
        public void SortCardsBySuit()
        {
            // Arrange
            List<Card> cards = new List<Card>();
            cards.Add(new Card { Value = 14, Suit = 'c' });
            cards.Add(new Card { Value = 10, Suit = 'd' });
            cards.Add(new Card { Value = 12, Suit = 'd' });
            cards.Add(new Card { Value = 11, Suit = 'd' });
            cards.Add(new Card { Value = 13, Suit = 'c' });

            List<Card> expected = new List<Card>();
            expected.Add(new Card { Value = 14, Suit = 'c' });
            expected.Add(new Card { Value = 13, Suit = 'c' });
            expected.Add(new Card { Value = 12, Suit = 'd' });
            expected.Add(new Card { Value = 11, Suit = 'd' });
            expected.Add(new Card { Value = 10, Suit = 'd' });

            // Act
            var actual = Poker.Help.SortHandCards.SortCardsBySuit(cards);

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Value, actual[i].Value);
                Assert.Equal(expected[i].Suit, actual[i].Suit);
            }
        }

        [Fact]
        // Sorting cards by value 
        public void SortCardsByValue()
        {
            // Arrange
            var cards = new List<Card>();
            cards.Add(new Card { Value = 14, Suit = 'd' });
            cards.Add(new Card { Value = 10, Suit = 'c' });
            cards.Add(new Card { Value = 12, Suit = 'd' });
            cards.Add(new Card { Value = 11, Suit = 'd' });
            cards.Add(new Card { Value = 13, Suit = 'd' });

            var expected = new List<Card>();
            expected.Add(new Card { Value = 14, Suit = 'd' });
            expected.Add(new Card { Value = 13, Suit = 'd' });
            expected.Add(new Card { Value = 12, Suit = 'd' });
            expected.Add(new Card { Value = 11, Suit = 'd' });
            expected.Add(new Card { Value = 10, Suit = 'c' });

            // Act
            var actual = Poker.Help.SortHandCards.SortCardsByValue(cards);

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Value, actual[i].Value);
                Assert.Equal(expected[i].Suit, actual[i].Suit);
            }

        }

        [Fact]
        // Sorting Cards Result
        public void SortCardsResultTest()
        {
            // Arrange
            var player1 = new List<Card>();
            player1.Add(new Card { Value = 3, Suit = 'd' });
            player1.Add(new Card { Value = 12, Suit = 's' });

            var player2 = new List<Card>();
            player2.Add(new Card { Value = 12, Suit = 'c' });
            player2.Add(new Card { Value = 4, Suit = 'd' });

            var resultHand = new List<Card>();
            resultHand.Add(new Card { Value = 14, Suit = 'h' });
            resultHand.Add(new Card { Value = 13, Suit = 'h' });
            resultHand.Add(new Card { Value = 12, Suit = 'h' });
            resultHand.Add(new Card { Value = 11, Suit = 'h' });
            resultHand.Add(new Card { Value = 10, Suit = 'h' });

            var resultGames = new List<ResultGame>();
            resultGames.Add(new ResultGame { HandValue = 10, PlayerCards = player2, ResultHand = resultHand });
            resultGames.Add(new ResultGame { HandValue = 10, PlayerCards = player1, ResultHand = resultHand });

            var expected = new List<ResultGame>();
            expected.Add(new ResultGame { HandValue = 10, PlayerCards = player1, ResultHand = resultHand });
            expected.Add(new ResultGame { HandValue = 10, PlayerCards = player2, ResultHand = resultHand });

            // Act
            var actual = Poker.Help.SortHandCards.SortCardsResult(resultGames);

            // Assert
            for (var i = 0; i < expected.Count; i++)
            {
                for (var j = 0; j < expected[i].PlayerCards.Count; j++)
                {
                    Assert.Equal(expected[i].PlayerCards[j].Value, actual[i].PlayerCards[j].Value);
                    Assert.Equal(expected[i].PlayerCards[j].Suit, actual[i].PlayerCards[j].Suit);
                }
            }

        }

    }
}
