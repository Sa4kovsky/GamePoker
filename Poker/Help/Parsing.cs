using Poker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Help
{
    public static class Parsing
    {
        const int INDEXTITLEGAME = 0;
        const int INDEXBOARD = 1;

        public static void ParsingFile(string[] arStr, out List<Game> games)
        {
            string[] splitText;
            games = new List<Game>();

            foreach (string line in arStr)
            {
                splitText = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<Player> players = new List<Player>();

                if (splitText.Length != 0)
                {
                    if (splitText[INDEXTITLEGAME].ToString() == "texas-holdem" || splitText[INDEXTITLEGAME].ToString() == "omaha-holdem")
                    {
                        for (int i = 2; i < splitText.Length; i++)
                        {
                            ParsingCard(splitText, i, out List<Card> cards);
                            players.Add(new Player { Cards = cards });
                        }

                        ParsingCard(splitText, INDEXBOARD, out List<Card> card);

                        games.Add(new Game
                        {
                            Title = splitText[INDEXTITLEGAME].ToString(),
                            Board = card,
                            Players = players
                        });
                    }
                    else if (splitText[INDEXTITLEGAME].ToString() == "five-card-draw")
                    {
                        for (int i = 1; i < splitText.Length; i++)
                        {
                            ParsingCard(splitText, i, out List<Card> cards);
                            players.Add(new Player { Cards = cards });
                        }

                        games.Add(new Game
                        {
                            Title = splitText[INDEXTITLEGAME].ToString(),
                            Players = players
                        });
                    }
                }
                else 
                {
                    Console.WriteLine("Error: Null string");
                }
                
            }
        }

        public static void ParsingCard(string[] splitText, int i, out List<Card> card)
        {
            char[] chars = splitText[i].ToCharArray();
            card = new List<Card>();
            int kBoard;
            for (int j = 0; j < splitText[i].Length; j = j + 2)
            {
                kBoard = j + 1;
                card.Add(new Card { Value = Converts.ConvertValue(chars[j].ToString()), Suits = chars[kBoard].ToString() });
            }
        }
    }
}
