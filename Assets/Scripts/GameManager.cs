using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
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

    

   
}