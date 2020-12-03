using System;
using static Poker.Help.EnumCards;

namespace Poker.Help
{
    public static class Converts
    {
        public static int ConvertValue(string c)
        {
            return c switch
            {
                "A" => (int)ValueCards.A,
                "K" => (int)ValueCards.K,
                "Q" => (int)ValueCards.Q,
                "J" => (int)ValueCards.J,
                "T" => (int)ValueCards.T,
                _ => Convert.ToInt16(c)
            };
        }

        public static string ConvertValueString(int c)
        {
            return c switch
            {
                14 => ValueCards.A.ToString(),
                13 => ValueCards.K.ToString(),
                12 => ValueCards.Q.ToString(),
                11 => ValueCards.J.ToString(),
                10 => ValueCards.T.ToString(),
                _ => c.ToString()
            };
        }
    }
}
