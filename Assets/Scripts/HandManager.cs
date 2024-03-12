using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public List<CardDisplay> hand = new List<CardDisplay>();
    public DeckManager deck;
    public GameObject cardPrefab;
    public float cardSpacing = 1.0f; // Space between cards
    

    private void UpdateCardPositionsInHand() {
        float totalWidth = (hand.Count - 1) * cardSpacing;
        Vector3 startPos = -Vector3.right * totalWidth / 2;

        for (int i = 0; i < hand.Count; i++) {
            Vector3 cardPos = startPos + Vector3.right * (i * cardSpacing);
            hand[i].transform.localPosition = cardPos;
        }
    }

    public void DrawACard() {
        BaseCardData drawn = deck.DrawCard();
        AddCardToHand(drawn);
    }

    public void AddCardToHand(BaseCardData cardData) {
        GameObject cardObj = Instantiate(cardPrefab, transform);
        CardDisplay newCardDisplay = cardObj.GetComponent<CardDisplay>();
       
        if(cardData is MinionCardData)
            newCardDisplay.SetupCard(Instantiate((MinionCardData)cardData));
        else {
        newCardDisplay.SetupCard(Instantiate(cardData));
        }
        hand.Add(newCardDisplay);
        UpdateCardPositionsInHand();
    }
   



    public void RemoveCardFromHand(CardDisplay card) {
        if (hand.Remove(card))
            UpdateCardPositionsInHand();
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

    public int GetCardIndex(BaseCardData cardData) {
        for (int i = 0; i < hand.Count; i++) {
            if (hand[i] == cardData) {
                return i;
            }
        }
        return 0;
    }
}