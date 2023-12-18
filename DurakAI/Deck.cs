namespace DurakAI;

public static class Deck
{
    public static List<Card> CreateDeck()
    {
        var cardDeck = new List<Card>();
        
        foreach (var suit in Enum.GetValues(typeof(Suit)))
        {
            cardDeck.AddRange(from object? rank in Enum.GetValues(typeof(Rank)) select new Card(suit, rank));
        }

        return cardDeck;
    }
    
    public static void Shuffle(List<Card> deck)
    {
        var random = new Random();
        
        for (var n = deck.Count - 1; n > 0; --n)
        {
            var k = random.Next(n+1);
            (deck[n], deck[k]) = (deck[k], deck[n]);
        }
    }

    public static List<Card> GetSixCards(List<Card> deck)
    {
        var cards = new List<Card>();

        for (var i = 0; i < 6; i++)
        {
            cards.Add(GetCard(deck));
        }

        return cards;
    }

    public static Card GetCard(List<Card> deck)
    {
        var card = deck[^1];
        deck.Remove(deck[^1]);

        return card;
    }

    public static void PrintDeck(List<Card> deck)
    {
        foreach (var card in deck)
        {
            Console.WriteLine(card);
        }
    }
}