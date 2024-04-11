using UnityEngine;

public class CollectibleItem : Item {
        
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            InventoryController.instance.AddItem(this);
            Destroy(gameObject);
        }
    }
}