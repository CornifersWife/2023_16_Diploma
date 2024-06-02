using UnityEngine;

public class CollectibleItem : Item {
        
    private void OnCollisionExit(Collision other) {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Player")) {
            InventoryController.Instance.AddItem(this);
            gameObject.SetActive(false);
        }
    }
}