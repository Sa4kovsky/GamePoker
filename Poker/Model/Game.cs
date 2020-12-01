using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Model
{
    public class Game
    {
        public string Title { get; set; }
        public List<Card> Board { get; set; }
        public List<Player> Players { get; set; }


    }
}
