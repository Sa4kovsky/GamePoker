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
        static void Main(string[] args)
        {
            string[] arStr = File.ReadAllLines(@"C:\Users\sachkovski.ev\Desktop\poker.txt");

            Parsing.ParsingFile(File.ReadAllLines(args[0]), out List<Game> games);

            foreach (Game game in games)
            {
                Console.Write(game.Title + " ");
                List<ResultGame> resultGame = new List<ResultGame>();
                foreach (Player player in game.Players)
                {
                    Сombinations.Flush(game.Title, game.Board, player.Cards, resultGame);
                }

                var result = resultGame.OrderBy(x => x.HandValue).ThenBy(y => y.ResultHand[0].Value).ThenBy(y => y.ResultHand[1].Value).ThenBy(y => y.ResultHand[2].Value).ThenBy(y => y.ResultHand[3].Value).ThenBy(y => y.ResultHand[4].Value).ThenBy(y => y.PlayerCards[0].Suits).ThenBy(y => y.PlayerCards[1].Suits).ToList();
                for (int i = 0; i < result.Count(); i++)
                {
                    string s = "";
                    foreach (Card playerCard in result[i].PlayerCards)
                    {
                        s += Converts.ConvertValueString(playerCard.Value) + playerCard.Suits;
                    }

                    if (i < result.Count()-1 && result[i].HandValue == result[i + 1].HandValue && result[i].ResultHand[1].Value == result[i + 1].ResultHand[1].Value && result[i].ResultHand[2].Value == result[i + 1].ResultHand[2].Value && result[i].ResultHand[3].Value == result[i + 1].ResultHand[3].Value && result[i].ResultHand[4].Value == result[i + 1].ResultHand[4].Value)
                    {
                        Console.Write(s + "=");
                    }
                    else
                    {
                        Console.Write(s + " ");
                    } 
                }
                Console.WriteLine();

              /*  foreach (ResultGame e in resultGame.OrderBy(x => x.HandValue).ThenBy(y => y.ResultHand[0].Value).ThenBy(y => y.ResultHand[1].Value).ThenBy(y => y.ResultHand[2].Value).ThenBy(y => y.ResultHand[3].Value).ThenBy(y => y.ResultHand[4].Value).ThenBy(y => y.PlayerCards[1].Suits))
                                {
                                    string s = "", h = "";
                                    foreach (Card playerCard in e.PlayerCards)
                                    {
                                        s += playerCard.Value + playerCard.Suits;
                                    }
                                    foreach (Card handCard in e.ResultHand)
                                    {
                                        h += handCard.Value + handCard.Suits;
                                    }
                                    Console.WriteLine(s + " " + h + " " + e.HandValue);
                                }*/
            }
            Console.ReadLine();
        }
    }
}
