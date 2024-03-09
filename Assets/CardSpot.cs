using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpot : MonoBehaviour {
    [SerializeField]
    private GameObject cardDisplay;

    public GameObject CardDisplay {
        get { return cardDisplay; }
        set { cardDisplay = value; }
    }
}
