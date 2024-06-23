using System;
using Card;
using UnityEngine;
[Obsolete]
public class CardSpotOld : MonoBehaviour {
    private CardOld cardOld;
    public bool isPlayers = true;
    public event Action<CardSpotOld,CardOld> Play;
    
    public Material defaultMaterial;
    public Material outlineMaterial; // Assign the shimmering outline material here
    private Renderer objectRenderer;
    public CardOld CardOld {
        get => cardOld;
        set => Play?.Invoke(this,value);
    }

    void Start() {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = defaultMaterial;
    }
    public void SetCardDisplay(CardOld newCardOld) {
        cardOld = newCardOld;
        //cardOld.cardData.belongsToPlayer = isPlayers;
        cardOld.cardData.Play();
        cardOld.GetComponent<DragAndDrop>().enabled = false;
        cardOld.OnDestroyed += HandleCardOldDestroyed;
        cardOld.transform.localPosition = new Vector3(0, 0, 0);
    }
    private void HandleCardOldDestroyed() {
        cardOld.OnDestroyed -= HandleCardOldDestroyed;
        cardOld = null;
    }
    

    public bool IsEmpty () {
        if (cardOld is null) {
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
