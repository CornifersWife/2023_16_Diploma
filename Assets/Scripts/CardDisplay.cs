using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour {
    public BaseCardData cardData;

    public void SetupCard(BaseCardData data) {
        cardData = data;
        // Update the card's visuals here
        // For example, set the card's sprite to cardData.cardImage
    }
}
