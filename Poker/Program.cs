using System;
using System.Collections.Generic;
using Poker.Model;
using Poker.Help;
using System.Linq;
using static Poker.Help.SortHandCards;

namespace Poker
{
    class Program
    {
        static void Main()
        {
            string input;
            while ((input = Console.ReadLine()) != null)
            {
                var game = Parsing.ParseGame(input);
                var resultGame = new List<ResultGame>();

                if (game != null)
                {
                    foreach (var player in game.Players)
                    {
                        resultGame.AddRange(Сombinations.DefineResult(game.Type, game.Board, player.Cards));
                    }

                    var result = SortCardsResult(resultGame);
                    for (var i = 0; i < result.Count; i++)
                    {
                        var s = result[i].PlayerCards.Aggregate("", (current, playerCard) => current + Converts.ConvertValueString(playerCard.Value) + playerCard.Suit);

                        if (i < result.Count - 1 && result[i].HandValue == result[i + 1].HandValue &&
                            result[i].ResultHand[0].Value == result[i + 1].ResultHand[0].Value &&
                            result[i].ResultHand[1].Value == result[i + 1].ResultHand[1].Value &&
                            result[i].ResultHand[2].Value == result[i + 1].ResultHand[2].Value &&
                            result[i].ResultHand[3].Value == result[i + 1].ResultHand[3].Value &&
                            result[i].ResultHand[4].Value == result[i + 1].ResultHand[4].Value
                        )
                        {
                            Console.Write(s + "=");
                        }
                        else if (i < result.Count() - 1)
                        {
                            Console.Write(s + " ");
                        }
                        else
                        {
                            Console.Write(s);
                        }
                    }
                    Console.WriteLine();

                }
            }
        }
    }
}
