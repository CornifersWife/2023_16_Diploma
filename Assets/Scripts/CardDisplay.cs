using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour {
    public BaseCardData cardData;

    public void SetupCard(BaseCardData data) {
        cardData = data;
    }
}
