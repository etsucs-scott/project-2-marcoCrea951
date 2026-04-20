using System.Security.Cryptography.X509Certificates;

namespace WarGame.Core;

public enum Suit
{
    Hearts, Diamonds, Clubs, Spades
}
public enum Rank
{
    Two = 2, ThreeFour, Five, Six, Seven,
        Eight, Nine, Ten, Jack, Queen, King, Ace

}
// icomparable to compare cards with eachother
public class Card : IComparable<Card>
{
    public Suit Suit {get; }
    public Rank Rank {get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }
    public int CompareTo(Card other)
    {
        return Rank.CompareTo(other.Rank);
    }
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

