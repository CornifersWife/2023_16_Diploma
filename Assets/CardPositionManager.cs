using System;
using System.Collections.Generic;
using UnityEngine;

public class CardPositionManager : MonoBehaviour {
    public Transform startPosition;
    public Transform endPosition;
    [SerializeField]private int numberOfSpots = 5;

    private List<Vector3> cardPositions;

    void Awake() {
        
    }

    private void Start() {
            
    }

    private void Update() {
        GenerateCardPositions();
    }

    void GenerateCardPositions() {
       // if (numberOfSpots <= 0) return;
       Debug.Log(numberOfSpots);
       cardPositions = new List<Vector3>();
        for (int i = 0; i < numberOfSpots; i++) {
            float t = (float)i / (numberOfSpots - 1);
            cardPositions.Add(Vector3.Lerp(startPosition.position, endPosition.position, t));
            
        }
    }

    public Vector3 GetCardPosition(int index) {
        if (index >= 0 && index < cardPositions.Count) {
            return cardPositions[index];
        }
        return Vector3.zero; // Return zero if the index is out of range
    }

    // This method is called every frame by Unity when the object is selected in the Editor
    void OnDrawGizmos() {
       // if (cardPositions == null || cardPositions.Count == 0) return;
      //  Debug.Log(cardPositions);

        Gizmos.color = Color.red;
        foreach (var pos in cardPositions) {
            Gizmos.DrawSphere(pos, 0.1f); // Draw small spheres at each card position
        }
    }
}