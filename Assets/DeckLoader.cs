using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckLoader : MonoBehaviour {
    private DeckManager deckManager;

    void Start() {
        deckManager = GetComponent<DeckManager>();
    }


}