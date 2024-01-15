using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public DeckManager deckManager;
    public HandManager handManager;

    // Call this method to test drawing a card
    public void TestDrawCard() {
        BaseCardData drawnCard = deckManager.DrawCard();
        if (drawnCard != null) {
            handManager.AddCardToHand(drawnCard);
        }
    }
}
