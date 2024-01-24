using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public List<CardDisplay> hand = new List<CardDisplay>();
    public GameObject cardPrefab; 
    public float cardSpacing = 1.0f; // Space between cards
    
    public void UpdateCardPositionsInHand() {
        for (int i = 0; i < hand.Count; i++) {
            // Calculate the position for each card
            Vector3 cardPos = new Vector3(i * cardSpacing, 0, 0);
            hand[i].transform.localPosition = cardPos;
        }
    }
    
    public void AddCardToHand(BaseCardData cardData) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        cardDisplay.SetupCard(cardData);
        hand.Add(cardDisplay);
        UpdateCardPositionsInHand();
        cardDisplay.DisplayData(cardObj);
    }
    
    public BaseCardData PlayCard(int index) {
        if (index < 0 || index >= hand.Count) return null; // Added bounds check for safety

        BaseCardData cardData = hand[index].cardData;

        // Destroy the GameObject associated with the card
        if (hand[index].gameObject != null) {
            Destroy(hand[index].gameObject);
        }

        // Remove the card from the hand
        hand.RemoveAt(index);

        // Optional: Update the positions of the remaining cards in the hand
        UpdateCardPositionsInHand();

        return cardData;
    }
}
