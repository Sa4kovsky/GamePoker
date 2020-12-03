using System;
using System.Collections.Generic;
using System.IO;
using Poker.Model;
using Poker.Help;
using System.Linq;

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
                foreach (var player in game.Players)
                {
                    resultGame.AddRange(Сombinations.DefinitionCombinations(game.Type, game.Board, player.Cards));
                }

                var result = resultGame.OrderBy(x => x.HandValue).ThenBy(y => y.ResultHand[0].Value)
                    .ThenBy(y => y.ResultHand[1].Value).ThenBy(y => y.ResultHand[2].Value)
                    .ThenBy(y => y.ResultHand[3].Value).ThenBy(y => y.ResultHand[4].Value)
                    .ThenBy(y => y.PlayerCards[0].Suit).ThenBy(y => y.PlayerCards[1].Suit).ToList();
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
