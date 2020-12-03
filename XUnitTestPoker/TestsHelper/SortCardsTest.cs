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


    }
}
