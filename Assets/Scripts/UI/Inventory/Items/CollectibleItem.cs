using Events;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectibleItem : Item, IPointerClickHandler {
    [SerializeField] private CollectibleItemData itemData;
    
    public void OnPointerClick(PointerEventData eventData) {
        InventoryController.Instance.AddItem(this);
        gameObject.SetActive(false);
        GameEventsManager.Instance.ItemEvents.ItemCollected();
    }

    public CollectibleItemData GetItemData() {
        return itemData;
    }
    
    public void SetItemData(CollectibleItemData itemData) {
        this.itemData = itemData;
    }
}