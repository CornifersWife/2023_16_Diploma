using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public List<BaseCardData> hand = new List<BaseCardData>();
    public GameObject cardPrefab; 
    public float cardSpacing = 1.0f; // Space between cards

    public ButtonManager buttonManager;
    
    private void UpdateCardPositionsInHand() {
        for (int i = 0; i < hand.Count; i++) {
            // Calculate the position for each card
            Vector3 cardPos = new Vector3(i * cardSpacing, 0, 0);
            // Find or create the visual representation for each card
            CardDisplay cardDisplay = FindOrCreateCardDisplay(hand[i]);
            cardDisplay.transform.localPosition = cardPos;
        }
        buttonManager.UpdateCardOptions();
    }

    private CardDisplay FindOrCreateCardDisplay(BaseCardData cardData) {
        // Search for an existing CardDisplay that represents this cardData
        foreach (Transform child in transform) {
            CardDisplay cardDisplay = child.GetComponent<CardDisplay>();
            if (cardDisplay != null && cardDisplay.cardData == cardData) {
                return cardDisplay;
            }
        }

        // If not found, create a new one
        GameObject cardObj = Instantiate(cardPrefab, transform);
        CardDisplay newCardDisplay = cardObj.GetComponent<CardDisplay>();
        newCardDisplay.SetupCard(cardData);
        return newCardDisplay;
    }
    
    public void AddCardToHand(BaseCardData cardData) {
        hand.Add(cardData);
        UpdateCardPositionsInHand();
    }
    public void RemoveCardFromHand(int index) {
        if (index < 0 || index >= hand.Count) {
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid card index.");
        }
        BaseCardData cardToRemove = hand[index];
        hand.RemoveAt(index);
        DestroyCardDisplay(cardToRemove);
        UpdateCardPositionsInHand();
    }

    public void RemoveCardFromHand(BaseCardData cardData) {
        if (hand.Remove(cardData)) {
            DestroyCardDisplay(cardData);
            UpdateCardPositionsInHand();
        }
    }
    
    private void DestroyCardDisplay(BaseCardData cardData) {
        foreach (Transform child in transform) {
            CardDisplay cardDisplay = child.GetComponent<CardDisplay>();
            if (cardDisplay != null && cardDisplay.cardData == cardData) {
                Destroy(cardDisplay.gameObject);
                break;
            }
        }
    }
    /*public BaseCardData PlayCard(int index) {
        if (index < 0 || index >= hand.Count) {
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid card index.");
        }
        BaseCardData playedCard = hand[index];
        hand.RemoveAt(index);
        UpdateCardPositionsInHand();
        // Additional logic for playing the card (e.g., moving it to the board)
        return playedCard;
    }*/

    public int GetCardIndex(BaseCardData cardData)
    {
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i] == cardData)
            {
                return i;
            }
        }
        return 0;
    }
}