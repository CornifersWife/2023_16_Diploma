using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpot : MonoBehaviour {
    [SerializeField]
    private GameObject cardDisplay;

    private bool isPlayers = true;
    
    public GameObject CardDisplay {
        get { return cardDisplay; }
        set { cardDisplay = value; }
    }

    public bool IsValid () {
        if (!isPlayers)
            return false;
        if (cardDisplay is not null)
            return false;
        return true;
    }
}
