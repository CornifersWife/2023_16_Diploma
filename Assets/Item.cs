using System;
using UnityEngine;

public class Item: MonoBehaviour {
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;

    private InventoryController inventoryController;

    private void Awake() {
        inventoryController = GameObject.Find("Inventory Controller").GetComponent<InventoryController>();
    }

    public string GetName() {
        return itemName;
    }

    public Sprite GetSprite() {
        return sprite;
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            inventoryController.AddItem(this);
            Destroy(gameObject);
        }
    }
}
