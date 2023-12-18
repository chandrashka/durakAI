namespace DurakAI;

public class RandomPlayer : Player
{
    public override Card PlayACard(Suit trump, List<Card> cardsOnTable)
    {
        var random = new Random();

        if (cardsOnTable.Count == 0)
        {
            var card = PlayerCards[random.Next(0, PlayerCards.Count)];
            
            PlayerCards.Remove(card); 
            return card;
        }
        else
        {
            var possibleMoves = FindPossibleMoves(cardsOnTable);
            var card = possibleMoves[random.Next(0, possibleMoves.Count)];
            
            PlayerCards.Remove(card); 
            return card;
        }
    }

    public override Card BeatACard(Card cardToBeat, Suit trump, List<Card> cardsOnTable)
    {
        var random = new Random();
        var possibleMoves = FindPossibleMoves(cardToBeat, trump);
        var card = possibleMoves[random.Next(0, possibleMoves.Count)];

        PlayerCards.Remove(card);

        return card;
    }
    
    public override bool DecideToContinue(Suit trump)
    {
        var random = new Random();
        return random.Next(0, 2) != 0;
    }
}