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

    public ButtonManager buttonManager;

    // Call this method to test drawing a card
    public void PlayerPlayCard() {
        int handIndex = buttonManager.GetCardIndex(); // Get selected card index in hand
        if (playerHand.hand.Count > handIndex)
        {
            BaseCardData playedCard = playerHand.hand[handIndex];
            if (GameObject.FindWithTag("Player").GetComponent<ManaManager>().UseMana(playedCard)) {//check if player has enough mana
                if (playedCard is MinionCardData) {
                    bool success = board.AddMinionToBoard((MinionCardData)playedCard, buttonManager.GetSpotIndex(), buttonManager.GetToggle().transform);
                    if(success)
                        playerHand.RemoveCardFromHand(playedCard);
                }
                // Additional logic for other types of cards (like spells)
            }
        }
    }
    public void PlayerDrawCard() {
        BaseCardData drawnCard = playerDeck.DrawCard();
        if (drawnCard != null) {
            playerHand.AddCardToHand(drawnCard);
        }
    }
    
    public void EnemyPlayCard() {
        int handIndex = 0; // For testing, we'll play the first card in hand
        if (enemyHand.hand.Count > handIndex) {
            BaseCardData playedCard = enemyHand.hand[handIndex];
            if (GameObject.FindWithTag("Enemy").GetComponent<ManaManager>().UseMana(playedCard)) { //check if enemy has enough mana, but he don't use this function
                if (playedCard is MinionCardData) {
                    board.AddMinionToBoard((MinionCardData)playedCard, false);
                    enemyHand.RemoveCardFromHand(playedCard);
                }
                // Additional logic for other types of cards (like spells)
            }
        }
    }
    public void EnemyDrawCard() {
        BaseCardData drawnCard = enemyDeck.DrawCard();
        if (drawnCard != null) {
            enemyHand.AddCardToHand(drawnCard);
        }
    }


    public void PlayerAttack() {
        board.MinionsAttack(true);
    }
    
    public void EnemyAttack() {
        board.MinionsAttack(false);
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