using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IDropHandler {
    [SerializeField] private GameObject selectedShader;
    [SerializeField] private GameObject itemList;
    [SerializeField] private GameObject deckList;
    [SerializeField] private GameObject cardSetList;

    private Item item;
    private bool isOccupied = false;
    private bool isActive;
    
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
        itemRectTransform.localScale = new Vector3(1, 1, 1);
        
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
        InventoryController.instance.DeselectAllSlots();
        selectedShader.SetActive(true);
        SetIsActive(true);
    }

    public void OnDrop(PointerEventData eventData) {
        if (!isOccupied) {
            GameObject itemObject = eventData.pointerDrag;
            DraggableItem draggableItem = itemObject.GetComponent<DraggableItem>();

            if (transform.parent.name == itemList.name && draggableItem.GetItemData() is CardSet)
                return;
            if ((transform.parent.name == cardSetList.name || transform.parent.name == deckList.name) && draggableItem.GetItemData() is CollectibleItem)
                return;
            draggableItem.SetParentAfterDrag(transform);
            SetIsOccupied(true);
            SetItem(draggableItem.GetItemData());
        }
    }

    public Item GetItem() {
        return item;
    }

    public void SetItem(Item newItem) {
        this.item = newItem;
    }
    
    public void ClearItem() {
        this.item = null;
    }
}
