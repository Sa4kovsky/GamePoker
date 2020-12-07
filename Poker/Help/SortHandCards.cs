using System.Collections.Generic;
using System.Linq;

using Poker.Model;

namespace Poker.Help
{
    public static class SortHandCards
    {
        public static List<Card> SortCardsBySuit(List<Card> cards)
        {
            return cards.OrderBy(x => x.Suit).ThenByDescending(u => u.Value).ToList(); ;
        }

        public static List<Card> SortCardsByValue(List<Card> cards)
        {
            return cards.OrderByDescending(u => u.Value).ToList(); ;
        }

        /// <summary>
        /// Method for sorting game results in ascending and alphabetical order.
        /// Check the weight of the final hand, if the total is the same, 
        /// check the final cards by seniority, if the cards are the same, 
        /// then arrange the players in alphabetical order
        /// </summary>
        /// <param name="resultGame">List result game</param>
        /// <returns>Sort list result game</returns>
        public static List<ResultGame> SortCardsResult(List<ResultGame> resultGame)
        {
            return resultGame.OrderBy(x => x.HandValue).ThenBy(y => y.ResultHand[0].Value)
               .ThenBy(y => y.ResultHand[1].Value).ThenBy(y => y.ResultHand[2].Value)
               .ThenBy(y => y.ResultHand[3].Value).ThenBy(y => y.ResultHand[4].Value)
               .ThenBy(y => Converts.ConvertValueString(y.PlayerCards[0].Value)).ThenBy(y => y.PlayerCards[0].Suit).ToList();
        }
    }
}
