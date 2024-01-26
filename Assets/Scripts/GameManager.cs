using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public DeckManager deckManager;
    public HandManager handManager;
    public BoardSideManager boardSideManager;
    public Enemy enemy;

    public DeckManager playerDeck;
    public HandManager playerHand;
    
    public DeckManager enemyDeck;
    public HandManager enemyHand;
    
    public Board board;
    public int cardtoplay = -1;
    public int boardspacechosen = -1;


    // Call this method to test drawing a card
    public void TestPlayPlayerCard() {
        int handIndex = 0; // For testing, we'll play the first card in hand
        if (playerHand.hand.Count > handIndex) {
            BaseCardData playedCard = playerHand.hand[handIndex];
            if (playedCard is MinionCardData) {
                board.AddMinionToBoard((MinionCardData)playedCard, true);
                playerHand.RemoveCardFromHand(playedCard);
            }
            // Additional logic for other types of cards (like spells)
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

    public void TestDrawPlayerCard() {
        BaseCardData drawnCard = playerDeck.DrawCard();
        if (drawnCard != null) {
            playerHand.AddCardToHand(drawnCard);
        }
    }
    
    public void TestPlayEnemyCard() {
        int handIndex = 0; // For testing, we'll play the first card in hand
        if (enemyHand.hand.Count > handIndex) {
            BaseCardData playedCard = enemyHand.hand[handIndex];
            if (playedCard is MinionCardData) {
                board.AddMinionToBoard((MinionCardData)playedCard, false);
                enemyHand.RemoveCardFromHand(playedCard);
            }
            // Additional logic for other types of cards (like spells)
        }
    }
    public void TestDrawEnemyCard() {
        BaseCardData drawnCard = enemyDeck.DrawCard();
        if (drawnCard != null) {
            enemyHand.AddCardToHand(drawnCard);
        }
    }
    


    // Call this method to test shuffling the deck
    public void TestShuffle() {
        string ids = "Before shuffle: [";
        foreach (BaseCardData card in playerDeck.deck) {
            ids += card.id + ", ";
        }

        ids += "]";
        Debug.Log(ids);

        playerDeck.Shuffle();

        ids = "After shuffle: [";
        foreach (BaseCardData card in playerDeck.deck) {
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