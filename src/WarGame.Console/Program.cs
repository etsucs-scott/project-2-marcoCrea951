using System;
using System.Collections.Generic;
using WarGame.Core;

namespace WarGame.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int playerCount = 2;
            // Check if an argument exists and if it can be parsed as an integer
            if (args.Length > 0 && int.TryParse(args[0], out int parsed))
                playerCount = Math.Clamp(parsed, 2, 4);
            else
            {
                Console.Write("Enter number of players (2–4): ");
                playerCount = Math.Clamp(int.Parse(Console.ReadLine()), 2, 4);
            }
            // Initialize player list and generate names (e.g., "Player 1", "Player 2")
            var players = new List<string>();
            for (int i = 1; i <= playerCount; i++)
                players.Add($"Player {i}");
            // create game engine with generated player list
            var engine = new WarEngine(players);

            engine.PlayGame(Console.WriteLine);

            Console.WriteLine("Game Over.");
        }
    }
}