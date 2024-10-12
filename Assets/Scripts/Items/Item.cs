using Events;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Item: MonoBehaviour, IPointerClickHandler{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;

    public string GetName() {
        return itemName;
    }

    public Sprite GetSprite() {
        return sprite;
    }
    
    public void SetName(string name) {
        itemName = name;
    }

    public void SetSprite(Sprite sprite) {
        this.sprite = sprite;
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        InventoryController.Instance.AddItem(this);
        gameObject.SetActive(false);
        GameEventsManager.Instance.ItemEvents.ItemCollected();
    }
}
