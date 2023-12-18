namespace DurakAI;

public class Card
{
    public Card(object suit, object rank)
    {
        Suit = (Suit)suit;
        Rank = (Rank)rank;
    }

    public Suit Suit { get; }
    public Rank Rank { get; }
    
    public override string ToString()
    {
        var suit = Suit.ToString();
        suit += Suit switch
        {
            Suit.Spades => "♠ ",
            Suit.Clubs => "♣ ",
            Suit.Hearts => "♥ ",
            Suit.Diamonds => "♦ ",
            _ => throw new ArgumentOutOfRangeException()
        };

        return $"{Rank} of " + suit;
    }
}