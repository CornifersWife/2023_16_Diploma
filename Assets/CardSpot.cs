using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpot : MonoBehaviour {
    [SerializeField]
    private GameObject cardDisplay;

    public bool isPlayers = true;
    public event Action<CardSpot,GameObject> Play;

    public GameObject CardDisplay {
        get { return cardDisplay; }
        set {
            cardDisplay = value;
            Play?.Invoke(this,cardDisplay);
            Debug.Log("xd");
        }
    }

    public bool IsValid () {
        if (!isPlayers)
            return false;
        if (cardDisplay is not null)
            return false;
        return true;
    }
    
}
