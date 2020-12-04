using Poker.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Help
{
    public static class Parsing
    {
        private const int IndexTypeGame = 0;
        private const int IndexBoard = 1;

        public static Game ParseGame(string input)
        {
            var splitText = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var players = new List<Player>();
            Game game = null;
            bool error = false;

            if (splitText.Length != 0)
            {
                GameType gameType;
                List<Card> tableCards;
                switch (splitText[IndexTypeGame])
                {
                    case "texas-holdem":
                        gameType = GameType.Holdem;
                        tableCards = ParseCard(GameType.FiveCard, splitText, IndexBoard);
                        if (tableCards != null)
                        {
                            for (var i = 2; i < splitText.Length; i++)
                            {
                                var cards = ParseCard(gameType, splitText, i);
                                if (cards != null)
                                {
                                    players.Add(new Player { Cards = cards });
                                }
                                else 
                                {
                                    Console.WriteLine("Error: Invalid card");
                                    error = true;
                                    break;
                                }
                            }
                            if (error)
                            {
                                game = null;
                            }
                            else 
                            { 

                                game = new Game
                                {
                                    Type = gameType,
                                    Board = tableCards,
                                    Players = players
                                };
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Wrong table cards");
                        }

                        break;
                    case "omaha-holdem":
                        {
                            gameType = GameType.Omaha;
                            tableCards = ParseCard(GameType.FiveCard, splitText, IndexBoard);
                            if (tableCards != null)
                            {
                                for (var i = 2; i < splitText.Length; i++)
                                {
                                    var cards = ParseCard(gameType, splitText, i);
                                    if (cards != null)
                                    {
                                        players.Add(new Player { Cards = cards });
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: Invalid card");
                                        error = true;
                                        break;
                                    }
                                }
                                if (error)
                                {
                                    game = null;
                                }
                                else
                                {
                                    game = new Game
                                    {
                                        Type = gameType,
                                        Board = tableCards,
                                        Players = players
                                    };
                                } 
                            }
                            else
                            {
                                Console.WriteLine("Error: Wrong table cards");
                            }

                            break;
                        }
                    case "five-card-draw":
                        {
                            gameType = GameType.FiveCard;
                            for (int i = 1; i < splitText.Length; i++)
                            {
                                var cards = ParseCard(gameType, splitText, i);
                                if (cards != null)
                                {
                                    players.Add(new Player { Cards = cards });
                                }
                                else
                                {
                                    Console.WriteLine("Error: Invalid card");
                                    error = true;
                                    break;
                                }
                            }
                            if (error)
                            {
                                game = null;
                            }
                            {
                                game = new Game
                                {
                                    Type = gameType,
                                    Players = players
                                };
                            }
                            break;
                        }
                    default:
                        Console.WriteLine("Error: Invalid game type");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Error: Empty string");
            }

            return game;
        }

        private static List<Card> ParseCard(GameType type, string[] splitText, int i)
        {
            var cardCount = type switch
            {
                GameType.Holdem => 2,
                GameType.Omaha => 4,
                GameType.FiveCard => 5,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var chars = splitText[i].ToCharArray();

            if (cardCount * 2 != chars.Length)
            {
                return null;
            }

            var card = new List<Card>();
            for (var j = 0; j < cardCount * 2; j = j + 2)
            {
                if (chars[j + 1] == 'h' || chars[j + 1] == 'd' || chars[j + 1] == 'c' || chars[j + 1] == 's')
                {
                    if (Converts.ConvertValue(chars[j].ToString()) != 0)
                    {
                        card.Add(new Card
                        { Value = Converts.ConvertValue(chars[j].ToString()), Suit = chars[j + 1] });
                    }
                    else 
                    {
                        return null;
                    }
                }
                else 
                {
                    return null;
                }
            }
            return card;
        }
    }
}