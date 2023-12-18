namespace DurakAI;

public class SmartPlayerPlaysTheSmallestCard : Player
{
    public override Card PlayACard(Suit trump, List<Card> cardsOnTable)
    {
        Card move;
        if (cardsOnTable.Count == 0)
        {
            move = PlayerCards[0];

            foreach (var card in PlayerCards.Where(card => card.Rank < move.Rank && card.Suit == move.Suit
                                                           || card.Suit != trump && move.Suit == trump))
            {
                move = card;
            }
        }
        else
        {
            var possibleMoves = FindPossibleMoves(cardsOnTable);
            move = possibleMoves[0];
            foreach (var card in possibleMoves)
            {
                if (card.Rank < move.Rank && card.Suit == move.Suit
                    || card.Suit != trump && move.Suit == trump)
                {
                    move = card;
                }
            }
        }

        PlayerCards.Remove(move);
        
        return move;
    }

    public override Card BeatACard(Card cardToBeat, Suit trump, List<Card> cardsOnTable)
    {
        var possibleMoves = FindPossibleMoves(cardToBeat, trump);

        var move = possibleMoves[0];

        foreach (var possibleMove in possibleMoves.Where(possibleMove => possibleMove.Rank < move.Rank))
        {
            move = possibleMove;
        }

        PlayerCards.Remove(move);
        
        return move;
    }
    
    public override bool DecideToContinue(Suit trump)
    {
        var random = new Random();
        return random.Next(0, 2) != 0;
    }
}