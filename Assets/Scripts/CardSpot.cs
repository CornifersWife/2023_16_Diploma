using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CardSpot : MonoBehaviour {
    public CardDisplay cardDisplay;

    public bool isPlayers = true;
    public event Action<CardSpot,CardDisplay> Play;

    public CardDisplay CardDisplay {
        get { return cardDisplay; }
        set {
            Play?.Invoke(this,value);
            cardDisplay.OnDestroyed += HandleCardDisplayDestroyed;
        }
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
    
    
}
