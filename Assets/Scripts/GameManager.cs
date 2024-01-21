using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public DeckManager deckManager;
    public HandManager handManager;
    public DeckManager opponentDeckManager;
    public HandManager opponentHandManager;

    // Call this method to test drawing a card
    public void TestDrawCard() {
        BaseCardData drawnCard = deckManager.DrawCard();
        if (drawnCard != null) {
            handManager.AddCardToHand(drawnCard);
        }
    }
    
    // Call this method to test opponent drawing a card
    public void TestOpponentDrawCard() {
        BaseCardData drawnCard = opponentDeckManager.DrawCard();
        if (drawnCard != null) {
            opponentHandManager.AddCardToHand(drawnCard);
        }
    }

    // Call this method to test shuffling the deck
    public void TestShuffle()
    {
        string ids = "Before shuffle: [";
        foreach (BaseCardData card in deckManager.deck)
        {
            ids += card.id + ", ";
        }
        ids += "]";
        Debug.Log(ids);
        
        deckManager.Shuffle();
        
        ids = "After shuffle: [";
        foreach (BaseCardData card in deckManager.deck)
        {
            ids += card.id + ", ";
        }
        ids += "]";
        Debug.Log(ids);
    }
}
