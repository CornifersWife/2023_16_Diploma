using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpot : MonoBehaviour {
    public GameObject cardDisplay;

    public bool isPlayers = true;
    public event Action<CardSpot,GameObject> Play;

    public GameObject CardDisplay {
        get { return cardDisplay; }
        set {
            Play?.Invoke(this,value);
        }
    }

    public bool IsValid () {
        if (isPlayers && CardDisplay == null) {
            return true;
        }
        return false;
    }
    
}
