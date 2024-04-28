using System;
using UnityEngine;

public class CardSpot : MonoBehaviour {
    private CardDisplay cardDisplay;
    public bool isPlayers = true;
    public event Action<CardSpot,CardDisplay> Play;
    
    public Material defaultMaterial;
    public Material outlineMaterial; // Assign the shimmering outline material here
    private Renderer objectRenderer;
    public CardDisplay CardDisplay {
        get => cardDisplay;
        set => Play?.Invoke(this,value);
    }

    void Start() {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = defaultMaterial;
    }
    public void SetCardDisplay(CardDisplay newCardDisplay) {
        cardDisplay = newCardDisplay;
        //cardDisplay.cardData.belongsToPlayer = isPlayers;
        cardDisplay.cardData.Play();
        cardDisplay.GetComponent<DragAndDrop>().enabled = false;
        cardDisplay.OnDestroyed += HandleCardDisplayDestroyed;
        cardDisplay.transform.localPosition = new Vector3(0, 0, 0);
    }
    private void HandleCardDisplayDestroyed() {
        cardDisplay.OnDestroyed -= HandleCardDisplayDestroyed;
        cardDisplay = null;
    }
    

    public bool IsEmpty () {
        if (cardDisplay is null) {
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
