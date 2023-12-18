namespace DurakAI;

public class RealPlayer : Player
{
    public override Card PlayACard(Suit trump, List<Card> cardsOnTable)
    {
        Console.Clear();
        Console.WriteLine($"Trump: {trump}");
        
        Console.WriteLine("Cards on table:"); 
        foreach (var card in cardsOnTable) 
        { 
            Console.WriteLine(card);
        }
        
        Console.WriteLine("Your cards:"); 
        foreach (var card in PlayerCards) 
        { 
            Console.WriteLine($"{card} {PlayerCards.IndexOf(card)}");
        }
        
        Console.WriteLine("Your turn to play a card");
        Console.WriteLine("Write index of card to play");
        var input = Console.ReadLine();
        
        return int.TryParse(input, out int index) ? PlayerCards[index] : null;
    }

    public override Card BeatACard(Card cardToBeat, Suit trump, List<Card> cardsOnTable)
    {
        Console.Clear();
        Console.WriteLine($"Trump: {trump}");
        
        Console.WriteLine("Cards on table:"); 
        foreach (var card in cardsOnTable) 
        { 
            Console.WriteLine(card);
        }
        
        Console.WriteLine("Your cards:"); 
        foreach (var card in PlayerCards) 
        { 
            Console.WriteLine($"{card} {PlayerCards.IndexOf(card)}");
        }
        
        Console.WriteLine($"You need to beat a card: {cardToBeat.Rank} {cardToBeat.Suit}");
        
        Console.WriteLine("Your turn to beat a card");
        Console.WriteLine("Write index of card to play");
        var input = Console.ReadLine();
        
        return int.TryParse(input, out int index) ? PlayerCards[index] : null;
    }

    public override bool DecideToContinue(Suit trump)
    {
        Console.WriteLine("Do you want to continue or end round? Write 0 to end and 1 to continue");
        var input = Console.ReadLine();
        int.TryParse(input, out int index);
        
        return  index == 1;
    }
}