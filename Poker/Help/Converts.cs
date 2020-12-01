using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Help
{
    public static class Converts
    {
        public static int ConvertValue(string c)
        {
            switch (c)
            {
                case "K":
                    return (int)EnumCards.ValueCards.K;

                case "A":
                    return (int)EnumCards.ValueCards.A;

                case "Q":
                    return (int)EnumCards.ValueCards.Q;

                case "J":
                    return (int)EnumCards.ValueCards.J;

                case "T":
                    return (int)EnumCards.ValueCards.T;

                default:
                    return Convert.ToInt16(c);

            }
        }

        public static string ConvertValueString(int c)
        {
            switch (c)
            {
                case 13:
                    return EnumCards.ValueCards.K.ToString();

                case 14:
                    return EnumCards.ValueCards.A.ToString();

                case 12:
                    return EnumCards.ValueCards.Q.ToString();

                case 11:
                    return EnumCards.ValueCards.J.ToString();

                case 10:
                    return EnumCards.ValueCards.T.ToString();

                default:
                    return c.ToString();

            }
        }
    }
}
