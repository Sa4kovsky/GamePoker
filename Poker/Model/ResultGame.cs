using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Model
{
    public class ResultGame
    {
        public List<Card> PlayerCards { get; set; }
        public List<Card> ResultHand { get; set; }
        public int HandValue { get; set; }
    }
}
