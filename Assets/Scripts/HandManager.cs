using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public List<BaseCardData> hand = new List<BaseCardData>();
    public DeckManager deck;
    public GameObject cardPrefab; 
    public float cardSpacing = 1.0f; // Space between cards

    public ButtonManager buttonManager;
    
    private Dictionary<BaseCardData, CardDisplay> cardDisplays = new Dictionary<BaseCardData, CardDisplay>();

    
    private void UpdateCardPositionsInHand() {
        float totalWidth = (hand.Count - 1) * cardSpacing;
        Vector3 startPos = -Vector3.right * totalWidth / 2;

        for (int i = 0; i < hand.Count; i++) {
            Vector3 cardPos = startPos + Vector3.right * (i * cardSpacing);
            if (cardDisplays.TryGetValue(hand[i], out CardDisplay cardDisplay)) {
                cardDisplay.transform.localPosition = cardPos;
            }
        }
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

    public void DrawACard() {
        BaseCardData drawn = deck.DrawCard();
        AddCardToHand(drawn);
    }
    
    public void AddCardToHand(BaseCardData cardData) {
        if (!hand.Contains(cardData)) {
            hand.Add(cardData);
            // Instantiate the card display prefab and set up its data
            GameObject cardObj = Instantiate(cardPrefab, transform);
            CardDisplay newCardDisplay = cardObj.GetComponent<CardDisplay>();
            newCardDisplay.SetupCard(cardData);
            // Add the display to the dictionary
            cardDisplays.Add(cardData, newCardDisplay);
        }

        UpdateCardPositionsInHand();
    }

    

    public void RemoveCardFromHand(BaseCardData cardData) {
        if (hand.Remove(cardData)) {
            // Remove and destroy the card's GameObject
            if (cardDisplays.TryGetValue(cardData, out CardDisplay cardObj)) {
                Destroy(cardObj); // Remove the GameObject from the scene
                cardDisplays.Remove(cardData); // Remove the reference
            }
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