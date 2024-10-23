using Events;
using UnityEngine.EventSystems;

public class CollectibleItem : Item, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        InventoryController.Instance.AddItem(this);
        gameObject.SetActive(false);
        GameEventsManager.Instance.ItemEvents.ItemCollected();
    }
}