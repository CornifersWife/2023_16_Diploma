using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public DeckManager deckManager;
    public HandManager handManager;
    public DeckManager enemyDeckManager;
    public HandManager enemyHandManager;

    // Call this method to test drawing a card
    public void TestDrawCard() {
        BaseCardData drawnCard = deckManager.DrawCard();
        if (drawnCard != null) {
            handManager.AddCardToHand(drawnCard);
        }
    }
    
    // Call this method to test enemy drawing a card
    public void TestOpponentDrawCard() {
        BaseCardData drawnCard = enemyDeckManager.DrawCard();
        if (drawnCard != null) {
            enemyHandManager.AddCardToHand(drawnCard);
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
