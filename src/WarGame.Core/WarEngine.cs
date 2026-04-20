using System;
using System.Collections.Generic;
using System.Linq;

namespace WarGame.Core
{
    public class WarEngine
    {
        private PlayerHands _players;
        private List<Card> _pot = new();
        private int _roundLimit = 10000;

        public WarEngine(List<string> playerNames)
        {
            _players = new PlayerHands();

            foreach (var name in playerNames)
                _players.AddPlayer(name);

            DealCards();
        }

        private void DealCards()
        {
            // Deals cards to all players in the game 
            var deck = new Deck();
            var names = _players.Hands.Keys.ToList();
            int index = 0;

            while (deck.HasCards)
            {
                var player = names[index % names.Count];
                _players.Hands[player].AddCard(deck.Draw());
                index++;
            }
        }

        public void PlayGame(Action<string> log)
        {
            int round = 0;

            while (round < _roundLimit)
            {
                round++;

                var activePlayers = _players.Hands
                    .Where(p => p.Value.Count > 0)
                    .ToDictionary(p => p.Key, p => p.Value);

                if (activePlayers.Count <= 1)
                    break;

                var played = new Dictionary<string, Card>();

                foreach (var player in activePlayers)
                {
                    played[player.Key] = player.Value.PlayCard();
                    _pot.Add(played[player.Key]);
                }

                var maxRank = played.Max(p => p.Value.Rank);
                var winners = played
                    .Where(p => p.Value.Rank == maxRank)
                    .Select(p => p.Key)
                    .ToList();

                log($"Round {round}: {string.Join(", ", played.Select(p => $"{p.Key} played {p.Value}"))}");

                if (winners.Count == 1)
                {
                    AwardPot(winners[0], log);
                }
                else
                {
                    HandleTie(winners, log);
                }
            }

            DeclareWinner(log);
        }

        private void HandleTie(List<string> tiedPlayers, Action<string> log)
        {
            log($"TIE between: {string.Join(", ", tiedPlayers)}");

            while (true)
            {
                var nextRound = new Dictionary<string, Card>();

                foreach (var player in tiedPlayers.ToList())
                {
                    if (_players.Hands[player].Count == 0)
                    {
                        log($"{player} eliminated during tie!");
                        tiedPlayers.Remove(player);
                        continue;
                    }

                    var card = _players.Hands[player].PlayCard();
                    nextRound[player] = card;
                    _pot.Add(card);
                }

                if (tiedPlayers.Count == 1)
                {
                    AwardPot(tiedPlayers[0], log);
                    return;
                }

                var maxRank = nextRound.Max(p => p.Value.Rank);
                tiedPlayers = nextRound
                    .Where(p => p.Value.Rank == maxRank)
                    .Select(p => p.Key)
                    .ToList();

                if (tiedPlayers.Count == 1)
                {
                    AwardPot(tiedPlayers[0], log);
                    return;
                }

                log($"Tie continues...");
            }
        }

        private void AwardPot(string winner, Action<string> log)
        {
            // Log who won and how many cards they are taking
            log($"{winner} wins the round and takes {_pot.Count} cards.");

            _players.Hands[winner].AddCards(_pot);
            _pot.Clear();

            LogCounts(log);
        }

        // logs how many cards each player currently has
        private void LogCounts(Action<string> log)
        {
            // creates a list  of strings like "player: name"
            var counts = _players.Hands
                .Select(p => $"{p.Key}: {p.Value.Count}");

            log("Card Counts → " + string.Join(", ", counts));
        }

        private void DeclareWinner(Action<string> log)
        {
            // Sorts players by number of cards (higest-lowest)
            var standings = _players.Hands
                .OrderByDescending(p => p.Value.Count)
                .ToList();

            if (standings[0].Value.Count == 52)
            {
                log($"WINNER: {standings[0].Key}");
            }
            else if (standings.Count > 1 &&
                     standings[0].Value.Count == standings[1].Value.Count)
            {
                log("DRAW (round limit reached)");
            }
            else
            {
                log($"WINNER by count: {standings[0].Key}");
            }
        }
    }
}