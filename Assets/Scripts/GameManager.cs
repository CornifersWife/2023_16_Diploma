using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public BoardManager boardManager;
    public BaseCardData someCardData; // Assign this in the Inspector

    void Start() {
        boardManager.AddCard(someCardData, 0); // Add the card to the first position
    }
}

