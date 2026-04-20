using System.Collections.Generic;

namespace WarGame.Core
{
    public class Hand
    {
        // queue FIFO
        private Queue<Card> _cards = new Queue<Card>();

        public int Count => _cards.Count;

        public void AddCard(Card card)
        {
            // Add to the end of queue
            _cards.Enqueue(card);
        }

        public void AddCards(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
                _cards.Enqueue(card);
        }

        public Card PlayCard()
        {
            return _cards.Dequeue();
        }
    }
}