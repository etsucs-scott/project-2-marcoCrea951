using System;
using System.Collections.Generic;
using System.Linq;

namespace WarGame.Core
{
    public class Deck
    {
        // Stack LIFO 
        private Stack<Card> _cards;
        private static Random _rng = new Random();

        public Deck()
        {
            var cards = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }

            _cards = new Stack<Card>(Shuffle(cards));
        }

        private List<Card> Shuffle(List<Card> cards)
        {
            return cards.OrderBy(x => _rng.Next()).ToList();
        }

        public bool HasCards => _cards.Count > 0;

        // Draws top card from the deck
        public Card Draw()
        {
            return _cards.Pop();
        }
    }
}