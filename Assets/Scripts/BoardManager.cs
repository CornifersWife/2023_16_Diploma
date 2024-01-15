using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public CardDisplay cardPrefab; // Reference to the card prefab
    public List<Transform> cardPositions; // Positions to place cards

    public void AddCard(BaseCardData cardData, int positionIndex) {
        if (positionIndex < 0 || positionIndex >= cardPositions.Count) return;

        CardDisplay newCard = Instantiate(cardPrefab, cardPositions[positionIndex].position, Quaternion.identity);
        newCard.SetCardData(cardData);
        // Parent the new card under the board or a relevant parent object
        newCard.transform.SetParent(transform);
    }
}

