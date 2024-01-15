using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public List<CardDisplay> hand = new List<CardDisplay>();
    public GameObject cardPrefab; // Assign your card prefab here

    public float cardSpacing = 1.0f; // Space between cards
    // Call this method whenever you need to update the card positions in hand
    public void UpdateCardPositionsInHand() {
        for (int i = 0; i < hand.Count; i++) {
            // Calculate the position for each card
            Vector3 cardPos = new Vector3(i * cardSpacing, 0, 0);
            hand[i].transform.localPosition = cardPos;
        }
    }

    // Method to add a card to the hand
    public void AddCardToHand(BaseCardData cardData) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        cardDisplay.SetupCard(cardData);

        hand.Add(cardDisplay);
        UpdateCardPositionsInHand();
    }

    // Rest of the HandManager code...
}
