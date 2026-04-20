using System.Collections.Generic;

namespace WarGame.Core
{
    public class PlayerHands
    {
        public Dictionary<string, Hand> Hands { get; } = new();

        public void AddPlayer(string name)
        {
            Hands[name] = new Hand();
        }
    }
}