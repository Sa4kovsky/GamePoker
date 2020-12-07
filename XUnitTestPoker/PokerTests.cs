using Poker.Model;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestPoker
{
    public class PokerTests
    {
        [Fact]
        //Error: Invalid card
        public void ParseGameTestInvalidCard() 
        {
            // Arrange
            var input = "omaha-holdem Kc2s3c6cQd AdTbTs6s 4c7sTs8h"; //error card: Tb
            // Act
            var actual = Poker.Help.Parsing.ParseGame(input);
            // Assert
            Assert.Null(actual);
        }

        [Fact]
        //Error: Wrong table cards
        public void ParseGameTestWrongTableCars()
        {
            // Arrange
            var input = "omaha-holdem Kc2s3c6c AdTbTs6s 4c7sTs8h"; //error table: count table card = 4
            // Act
            var actual = Poker.Help.Parsing.ParseGame(input);
            // Assert
            Assert.Null(actual);
        }

        [Fact]
        //Error: Invalid game type
        public void ParseGameTestInvalidGameType()
        {
            // Arrange
            var input = "omaha Kc2s3c6cQd AdTbTs6s 4c7sTs8h"; //error game type: omaha
            // Act
            var actual = Poker.Help.Parsing.ParseGame(input);
            // Assert
            Assert.Null(actual);
        }

        [Fact]
        //Error: Empty string
        public void ParseGameTestEmptyString()
        {
            // Arrange
            var input = ""; //error string: ""
            // Act
            var actual = Poker.Help.Parsing.ParseGame(input);
            // Assert
            Assert.Null(actual);
        }
    }
}
