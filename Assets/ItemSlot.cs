using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    [SerializeField] private Image itemImage;
    
    private string itemName;
    private Sprite itemSprite;
    private bool isOccupied;

    public void AddItem(Item item) {
        itemName = item.GetName();
        itemSprite = item.GetSprite();
        isOccupied = true;
        itemImage.sprite = itemSprite;
    }

    public bool IsOccupied() {
        return isOccupied;
    }
}
