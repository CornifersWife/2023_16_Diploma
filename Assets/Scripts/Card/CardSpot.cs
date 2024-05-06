using System;
using UnityEngine;

public class CardSpot : MonoBehaviour {
    private Card card;
    public bool isPlayers = true;
    public event Action<CardSpot,Card> Play;
    
    public Material defaultMaterial;
    public Material outlineMaterial; // Assign the shimmering outline material here
    private Renderer objectRenderer;
    public Card Card {
        get => card;
        set => Play?.Invoke(this,value);
    }

    void Start() {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = defaultMaterial;
    }
    public void SetCardDisplay(Card newCard) {
        card = newCard;
        //card.cardData.belongsToPlayer = isPlayers;
        card.cardData.Play();
        card.GetComponent<DragAndDrop>().enabled = false;
        card.OnDestroyed += HandleCardDestroyed;
        card.transform.localPosition = new Vector3(0, 0, 0);
    }
    private void HandleCardDestroyed() {
        card.OnDestroyed -= HandleCardDestroyed;
        card = null;
    }
    

    public bool IsEmpty () {
        if (card is null) {
            return true;
        }
        return false;
    }
    public bool IsValid () {
        if (isPlayers && IsEmpty()) {
            return true;
        }
        return false;
    }

    public void Highlight() {
        objectRenderer.material = outlineMaterial;
    }

    public void ClearHighlight() {
        objectRenderer.material = defaultMaterial;
    }
}
