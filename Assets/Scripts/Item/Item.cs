using UnityEngine;
using UnityEngine.EventSystems;

public class Item: MonoBehaviour, IPointerClickHandler{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;

    public string GetName() {
        return itemName;
    }

    public Sprite GetSprite() {
        return sprite;
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        InventoryController.Instance.AddItem(this);
        gameObject.SetActive(false);
    }
}
