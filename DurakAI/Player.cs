namespace DurakAI;

public abstract class Player
{
    public List<Card> PlayerCards { get; set; }

    protected List<Card> FindPossibleMoves(Card cardToBeat, Suit trump)
    {
        var possibleMoves = new List<Card>();
        
        foreach (var card in PlayerCards)
        {
            if (card.Suit == cardToBeat.Suit)
            {
                if (card.Rank > cardToBeat.Rank)
                {
                    possibleMoves.Add(card);
                }
            }
            else if (card.Suit != cardToBeat.Suit)
            {
                if (cardToBeat.Suit != trump && card.Suit == trump)
                {
                    possibleMoves.Add(card);
                }
            }
        }

        return possibleMoves;
    }

    protected List<Card> FindPossibleMoves(List<Card> cardsOnTable)
    {
        var possibleMoves = new List<Card>();
        foreach (var card in PlayerCards)
        {
            possibleMoves.AddRange(from cardOnTable in cardsOnTable where cardOnTable.Suit == card.Suit select card);
        }

        return possibleMoves;
    }

    public bool CanBeat(Card cardToBeat, Suit trump)
    {
        return FindPossibleMoves(cardToBeat, trump).Count > 0;
    }
    
    public abstract Card PlayACard(Suit trump, List<Card> cardsOnTable);
    
    public void TakeCard(Card card)
    {
        PlayerCards.Add(card);
    }

    public Rank FindTheSmallestTrump(Suit trump)
    {
        var trumps = GetTrumps(trump);

        var rank = trumps[0].Rank;

        foreach (var card in trumps.Where(card => card.Rank < rank))
        {
            rank = card.Rank;
        }

        return rank;
    }

    private List<Card> GetTrumps(Suit trump)
    {
        return PlayerCards.Where(card => card.Suit == trump).ToList();
    }

    public bool HasTrumps(Suit trump)
    {
        return GetTrumps(trump).Count > 0;
    }

    public abstract Card BeatACard(Card cardToBeat, Suit trump, List<Card> cardsOnTable);

    public abstract bool DecideToContinue(Suit trump);

    public bool CanContinue(List<Card> cardsOnTable)
    {
        var possibleMoves = new List<Card>();
        foreach (var card in PlayerCards)
        {
            possibleMoves.AddRange(from cardOnTable in cardsOnTable where cardOnTable.Suit == card.Suit select card);
        }

        return possibleMoves.Count > 0;
    }
}