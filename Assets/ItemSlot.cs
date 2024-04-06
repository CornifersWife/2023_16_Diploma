using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject selectedShader;
    
    private string itemName;
    private Sprite itemSprite;
    private bool isOccupied;
    private bool isActive;
    private InventoryController inventoryController;
    private GameObject parentPanel;

    private void Awake() {
        inventoryController = GameObject.Find("Inventory Controller").GetComponent<InventoryController>();
        parentPanel = transform.parent.gameObject;
    }
    
    public void AddItem(Item item) {
        itemName = item.GetName();
        itemSprite = item.GetSprite();
        isOccupied = true;
        itemImage.sprite = itemSprite;
    }

    public bool IsOccupied() {
        return isOccupied;
    }

    public void SetIsActive(bool value) {
        isActive = value;
    }

    public GameObject GetSelectedShader() {
        return selectedShader;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left)
            OnLeftClick();
    }

    private void OnLeftClick() {
        inventoryController.DeselectAllSlots();
        selectedShader.SetActive(true);
        SetIsActive(true);
    }
}
