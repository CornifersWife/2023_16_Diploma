using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour {
    [SerializeField] private List<CardSet> currentCardSets;

    private static ManageGame Instance = null;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        if (currentCardSets != null) {
            foreach (CardSet cardSet in currentCardSets) {
                InventoryController.Instance.AddItem(cardSet);
            }
        }
    }

}
