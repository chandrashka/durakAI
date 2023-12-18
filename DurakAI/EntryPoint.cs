using System.Collections;

namespace DurakAI;

public static class EntryPoint
{
    private static int _player1WinCounter;
    private static int _player2WinCounter;
    
    public static void Main()
    {
        for (var i = 0; i < 10000; i++)
        {
            PlayGame(new SmartPlayer(), new RealPlayer());
        }
        
        Console.WriteLine($"Player1 wins {_player1WinCounter} times             Player2 wins {_player2WinCounter} times");
    }

    private static void PlayGame(Player player1, Player player2)
    {
        var deck = Deck.CreateDeck();
        Deck.Shuffle(deck);
        Deck.Shuffle(deck);
        
        //Deck.PrintDeck(deck);
       
        player1.PlayerCards = Deck.GetSixCards(deck);
        player2.PlayerCards = Deck.GetSixCards(deck);

        var trump = deck[0].Suit;

        ChoosePlayerAndBeater(player1, player2, trump, out var player, out var beater);
        var isFirstRound = true;

        while (IsGameCanContinue(deck, player1, player2))
        {
            var cardsOnTableQuantity = 0;
            var cardsOnTable = new List<Card>();
            
            var isBeaterBeat = false;
            
            while (cardsOnTableQuantity < 5)
            {
                var cardToBeat = player.PlayACard(trump, cardsOnTable);
                cardsOnTable.Add(cardToBeat);
                cardsOnTableQuantity++;
                
                if (beater.CanBeat(cardToBeat, trump))
                {
                    var beatACard = beater.BeatACard(cardToBeat, trump, cardsOnTable);
                    isBeaterBeat = true;
                    
                    cardsOnTable.Add(beatACard);
                    
                    if (player is RealPlayer)
                    {
                        Console.WriteLine($"Played card: {beatACard.Rank} {beatACard.Suit}");
                    }
                }
                else
                {
                    isBeaterBeat = false;
                }
                
                if(player.PlayerCards.Count == 0 || beater.PlayerCards.Count == 0) break;

                if (!player.CanContinue(cardsOnTable) || !player.DecideToContinue(trump))
                {
                    break;
                }
                
                if(isFirstRound && cardsOnTableQuantity == 4) break;
            }

            if (isBeaterBeat)
            {
                (player, beater) = (beater, player);
            }
            else
            {
                foreach (var card in cardsOnTable)
                {
                    beater.TakeCard(card);
                }
            }
            
            FillPlayersHand(player, deck);
            FillPlayersHand(beater, deck);
        }
    }

    private static void ChoosePlayerAndBeater(Player player1, Player player2, Suit trump, out Player player, out Player beater)
    {
        if (player1.HasTrumps(trump) && player2.HasTrumps(trump))
        {
            var smallestTrumpPlayer1 = player1.FindTheSmallestTrump(trump);
            var smallestTrumpPlayer2 = player2.FindTheSmallestTrump(trump);

            if (smallestTrumpPlayer1 < smallestTrumpPlayer2)
            {
                player = player1;
                beater = player2;
            }
            else
            {
                player = player2;
                beater = player1;
            }
        }
        else if (player1.HasTrumps(trump) && !player2.HasTrumps(trump))
        {
            player = player1;
            beater = player2;
        }
        else
        {
            player = player2;
            beater = player1;
        }
    }

    private static void FillPlayersHand(Player player, List<Card> deck)
    {
        while (player.PlayerCards.Count < 6)
        {
            if (deck.Count > 0)
            {
                player.TakeCard(Deck.GetCard(deck));
            }
            else
            {
                break;
            }
        }
    }

    private static bool IsGameCanContinue(ICollection deck, Player player1, Player player2)
    {
        if (deck.Count != 0 || (player1.PlayerCards.Count != 0 && player2.PlayerCards.Count != 0)) return true;
        _ = player1.PlayerCards.Count == 0
            ? _player1WinCounter++
            : _player2WinCounter++;

        return false;

    }
}