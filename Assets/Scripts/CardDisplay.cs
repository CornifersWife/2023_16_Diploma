using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour {
    public BaseCardData cardData; // The data of the card
    public Renderer cardRenderer; // Renderer to display the #card image

    void Start() {
        if (cardData != null) {
            UpdateCardDisplay();
        }
    }

    public void SetCardData(BaseCardData newCardData) {
        cardData = newCardData;
        UpdateCardDisplay();
    }

    private void UpdateCardDisplay() {
        // Assign the card image to the card's material (assuming it's a texture)
        Texture2D cardTexture = cardData.cardImage.texture;
        cardRenderer.material.mainTexture = cardTexture;
        // Set other card properties as needed
    }
}
