using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IDropHandler {
    [SerializeField] private GameObject selectedShader;

    private Item item;
    private bool isOccupied = false;
    private bool isActive;
    private InventoryController inventoryController;

    private void Awake() {
        inventoryController = GameObject.Find("Inventory Controller").GetComponent<InventoryController>();
    }
    
    public void AddItem(Item item) {
        this.item = item;
        isOccupied = true;
        CreateItemObject();
    }
    private void CreateItemObject() {
        GameObject itemObject = new GameObject();
        itemObject.transform.SetParent(transform);
        
        RectTransform itemRectTransform = itemObject.AddComponent<RectTransform>();
        itemRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        itemRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        itemRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        itemRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
        itemRectTransform.anchorMin = new Vector2(0, 0);
        itemRectTransform.anchorMax = new Vector2(1, 1);
        
        Image objectImage = itemObject.AddComponent<Image>();
        objectImage.sprite = item.GetSprite();

        itemObject.AddComponent<DraggableItem>().SetItemData(item);
        itemObject.AddComponent<LayoutElement>();
    }

    public bool IsOccupied() {
        return isOccupied;
    }

    public void SetIsOccupied(bool value) {
        isOccupied = value;
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

    public void OnDrop(PointerEventData eventData) {
        if (!isOccupied) {
            GameObject itemObject = eventData.pointerDrag;
            itemObject.GetComponent<DraggableItem>().SetParentAfterDrag(transform);
            SetIsOccupied(true);
        }
    }

    public Item GetItem() {
        return item;
    }
}
