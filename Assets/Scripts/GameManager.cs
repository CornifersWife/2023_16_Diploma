using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public DeckManager deckManager;
    public HandManager handManager;
    public BoardSideManager boardSideManager;
    public Enemy enemy;

    // Call this method to test drawing a card
    public void TestDrawCard() {
        BaseCardData drawnCard = deckManager.DrawCard();
        if (drawnCard != null) {
            handManager.AddCardToHand(drawnCard);
        }
    }
    
    // Call this method to test enemy drawing a card
    public void TestEnemyDrawCard() {
        BaseCardData drawnCard = enemy.enemyDeckManager.DrawCard();
        if (drawnCard != null) {
            enemy.enemyHandManager.AddCardToHand(drawnCard);
        }
    }
    
    // Call this method to test enemy playing a card
    public void TestEnemyPlayCard() {
        enemy.PlayCard();
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
    
    // Call this method to test enemy shuffling the deck
    public void TestEnemyShuffle()
    {
        string ids = "Before shuffle: [";
        foreach (BaseCardData card in enemy.enemyDeckManager.deck)
        {
            ids += card.id + ", ";
        }
        ids += "]";
        Debug.Log(ids);
        
        enemy.enemyDeckManager.Shuffle();
        
        ids = "After shuffle: [";
        foreach (BaseCardData card in enemy.enemyDeckManager.deck)
        {
            ids += card.id + ", ";
        }
        ids += "]";
        Debug.Log(ids);
    }
}
