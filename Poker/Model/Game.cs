using System.Collections.Generic;

namespace Poker.Model
{
    public class Game
    {
        public GameType Type { get; set; }
        public List<Card> Board { get; set; }
        public List<Player> Players { get; set; }
    }

    public enum GameType
    {
        Omaha,
        Holdem,
        FiveCard
    }
}
