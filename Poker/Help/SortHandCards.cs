using System.Collections.Generic;
using System.Linq;

using Poker.Model;

namespace Poker.Help
{
    public static class SortHandCards
    {
        public static List<Card> SortCardsBySuit(List<Card> cards)
        {
            cards = cards.OrderBy(x => x.Suit).ThenByDescending(u => u.Value).ToList();
            return cards;
        }

        public static List<Card> SortCardsByValue(List<Card> cards)
        {
            cards = cards.OrderByDescending(u => u.Value).ToList();
            return cards;
        }
    }
}
