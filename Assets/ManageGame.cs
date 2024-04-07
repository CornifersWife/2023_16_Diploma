using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour {
    [SerializeField] private List<CardSet> currentDeck;

    private InventoryController inventoryController;
    
    private static ManageGame Instance { get; set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        inventoryController = GameObject.Find("Inventory Controller").GetComponent<InventoryController>();
    }

    private void Start() {
        if (currentDeck != null) {
            foreach (CardSet cardSet in currentDeck) {
                inventoryController.AddItem(cardSet);
            }
        }
    }

}
