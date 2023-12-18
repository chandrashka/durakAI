namespace DurakAI;

public class SmartPlayer : Player
{
    private static int GetCardStrength(Card card, Suit trump)
    {
        var rankStrength = GetRankStrength(card.Rank, card.Suit == trump);

        var suitStrength = card.Suit == trump ? 2 : 1;

        return rankStrength * suitStrength;
    }

    private static int GetRankStrength(Rank cardRank, bool isTrump)
    {
        int value;
        if (!isTrump)
        {
            value = cardRank switch
            {
                Rank.Six => -2,
                Rank.Seven => -1,
                Rank.Eight => 0,
                Rank.Nine => 1,
                Rank.Ten => 2,
                Rank.Jack => 3,
                Rank.Queen => 4,
                Rank.King => 5,
                Rank.Ace => 6,
                _ => 0
            };
        }
        else
        {
            value = cardRank switch
            {
                Rank.Six => 4,
                Rank.Seven => 5,
                Rank.Eight => 6,
                Rank.Nine => 7,
                Rank.Ten => 8,
                Rank.Jack => 9,
                Rank.Queen => 10,
                Rank.King => 11,
                Rank.Ace => 12,
                _ => 0
            };
        }

        return value;
    }

    public override Card PlayACard(Suit trump, List<Card> cardsOnTable)
    {
        if (cardsOnTable.Count == 0)
        {
            var sortedHand = PlayerCards.OrderBy(card => GetCardStrength(card, trump)).ToList();

            var cardToMove = sortedHand.First();

            PlayerCards.Remove(cardToMove);

            return cardToMove; 
        }
        else
        {
            var possibleMoves = FindPossibleMoves(cardsOnTable);
            var sortedHand = possibleMoves.OrderBy(card => GetCardStrength(card, trump)).ToList();

            var cardToMove = sortedHand.First();

            PlayerCards.Remove(cardToMove);

            return cardToMove; 
        }
    }

    public override Card BeatACard(Card cardToBeat, Suit trump, List<Card> cardsOnTable)
    {
        var playableCards = PlayerCards.Where(card => CanBeatCard(card, cardToBeat, trump)).ToList();

        var sortedPlayableCards = playableCards.OrderBy(card => GetCardStrength(card, trump)).ToList();

        var card = sortedPlayableCards[0];
        PlayerCards.Remove(card);

        return card;
    }

    public override bool DecideToContinue(Suit trump)
    {
        var sortedHand = PlayerCards.OrderBy(card => GetCardStrength(card, trump)).ToList();
        return GetCardStrength(sortedHand.First(), trump) <= 0;
    }

    static bool CanBeatCard(Card candidate, Card tableCard, Suit trump)
    {
        return (candidate.Rank > tableCard.Rank && candidate.Suit == tableCard.Suit) || (tableCard.Suit != trump && candidate.Suit == trump);
        
    }
}