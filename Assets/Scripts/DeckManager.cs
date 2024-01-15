using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {
    public List<BaseCardData> deck = new List<BaseCardData>();

    // Methods to shuffle, draw, and manage the deck
    
    public BaseCardData DrawCard() {
        if (deck.Count == 0) return null;
    
        BaseCardData cardData = deck[0];
        deck.RemoveAt(0);
        return cardData;
    }
}
