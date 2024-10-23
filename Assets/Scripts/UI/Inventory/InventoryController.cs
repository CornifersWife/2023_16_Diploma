using System.Collections.Generic;
using CardBattles.CardScripts.CardDatas;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class InventoryController : MonoBehaviour, ISaveable {
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private ManageCardSetDetails manageCardSetDetails;
    [SerializeField] private InputAction rightClickAction;

    [SerializeField] private List<ItemSlot> itemSlots;
    [SerializeField] private List<ItemSlot> cardSetSlots;
    [SerializeField] private List<ItemSlot> deckSlots;
    
    private PostProcessVolume postProcessVolume;

    private bool isOpen;
    
    public static InventoryController Instance = null; 

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }         
        else if (Instance != this) {
            Destroy(gameObject);
        }          
        //DontDestroyOnLoad(gameObject); 
        if (inventoryUI == null)
            inventoryUI = GameObject.FindWithTag("Inventory UI");
        postProcessVolume = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessVolume>();
    }

    private void OnEnable() {
        rightClickAction.Enable();
        rightClickAction.performed += ShowInventory;
    }

    private void OnDisable() {
        rightClickAction.performed -= ShowInventory;
        rightClickAction.Disable();
    }

    public void ShowInventory(InputAction.CallbackContext context) {
        if (PauseManager.Instance.IsOpen)
            return;
        inventoryUI.SetActive(true);
        postProcessVolume.enabled = true;
        isOpen = true;
        HUDController.Instance.HideHUD();
    }

    public void HideInventory() {
        if (PauseManager.Instance.IsOpen)
            return;
        postProcessVolume.enabled = false;
        manageCardSetDetails.Hide();
        DeselectAllSlots();
        inventoryUI.SetActive(false);
        isOpen = false;
        HUDController.Instance.ShowHUD();
    }

    public void AddItem(Item item) {
        if (item is CardSetItem)
            AddToSlot(item, cardSetSlots);
        else
            AddToSlot(item, itemSlots);
    }

    private void AddToSlot(Item item, List<ItemSlot> itemList) {
        foreach (ItemSlot itemSlot in itemList) {
            if (!itemSlot.IsOccupied()) {
                itemSlot.AddItem(item);
                itemSlot.SetIsOccupied(true);
                return;
            }
        }
    }

    public void DeselectAllSlots() {
        DeselectSlots(itemSlots);
        DeselectSlots(cardSetSlots);
        DeselectSlots(deckSlots);
    }

    private void DeselectSlots(List<ItemSlot> itemList) {
        foreach (ItemSlot itemSlot in itemList) {
            itemSlot.GetSelectedShader().SetActive(false);
        }
    }

    public void ShowCardSetDetails(CardSetData cardSetData) {
        if (!manageCardSetDetails.IsOpen)
            manageCardSetDetails.ReadCardSet(cardSetData);
    }

    public List<ItemSlot> GetDeck() {
        return deckSlots;
    }

    public List<CardSetData> GetCardSets() {
        List<CardSetData> cardSets = new List<CardSetData>();
        foreach(ItemSlot slot in deckSlots)
            cardSets.Add(((CardSetItem)slot.GetItem()).GetCardSetData());
        return cardSets;
    }

    public bool IsOpen() {
        return isOpen;
    }


    public void PopulateSaveData(SaveDataOld saveDataOld) {
        //TODO change how card sets are handled -> Card Data
        for (int i = 0; i < itemSlots.Count; i++) {
            if (!itemSlots[i].IsOccupied()) 
                continue;
            Item item = itemSlots[i].GetItem();
           
            InventorySaveDataOld.ItemData itemData = new InventorySaveDataOld.ItemData {
                index = i,
                name = item.GetName(),
                image = AssetDatabase.GetAssetPath(item.GetSprite())
            };
            ((InventorySaveDataOld)saveDataOld).itemDatas.Add(itemData);
        }
        
        for (int i = 0; i < cardSetSlots.Count; i++) {
            if (!cardSetSlots[i].IsOccupied()) 
                continue;
            Item item = cardSetSlots[i].GetItem();
            
            InventorySaveDataOld.CardSetItemData itemData = new InventorySaveDataOld.CardSetItemData() {
                index = i,
                name = item.GetName(),
                image = AssetDatabase.GetAssetPath(item.GetSprite()),
                cardSetData = AssetDatabase.GetAssetPath(((CardSetItem)item).GetCardSetData())
            };
            ((InventorySaveDataOld)saveDataOld).cardSetDatas.Add(itemData);
        }
        
        for (int i = 0; i < deckSlots.Count; i++) {
            if (!deckSlots[i].IsOccupied()) 
                continue;
            Item item = deckSlots[i].GetItem();
            
            InventorySaveDataOld.CardSetItemData itemData = new InventorySaveDataOld.CardSetItemData() {
                index = i,
                name = item.GetName(),
                image = AssetDatabase.GetAssetPath(item.GetSprite()),
                cardSetData = AssetDatabase.GetAssetPath(((CardSetItem)item).GetCardSetData())
            };
            ((InventorySaveDataOld)saveDataOld).deckDatas.Add(itemData);
        }
    }

    public void LoadSaveData(SaveDataOld saveDataOld) {
        InventorySaveDataOld inventorySaveDataOld = (InventorySaveDataOld)saveDataOld;
        for (int i = 0; i < inventorySaveDataOld.itemDatas.Count; i++) {
            CollectibleItem item = new GameObject().AddComponent<CollectibleItem>();
            item.SetName(inventorySaveDataOld.itemDatas[i].name);
            item.SetSprite(AssetDatabase.LoadAssetAtPath<Sprite>(inventorySaveDataOld.itemDatas[i].image));
            itemSlots[inventorySaveDataOld.itemDatas[i].index].AddItem(item);
        }
        
        for (int i = 0; i < inventorySaveDataOld.cardSetDatas.Count; i++) {
            CardSetItem item = new GameObject().AddComponent<CardSetItem>();
            item.SetName(inventorySaveDataOld.cardSetDatas[i].name);
            item.SetSprite(AssetDatabase.LoadAssetAtPath<Sprite>(inventorySaveDataOld.cardSetDatas[i].image));
            item.SetCardSetData(AssetDatabase.LoadAssetAtPath<CardSetData>(inventorySaveDataOld.cardSetDatas[i].cardSetData));
            
            cardSetSlots[inventorySaveDataOld.cardSetDatas[i].index].AddItem(item);
        }
        
        for (int i = 0; i < inventorySaveDataOld.deckDatas.Count; i++) {
            CardSetItem item = new GameObject().AddComponent<CardSetItem>();
            item.SetName(inventorySaveDataOld.deckDatas[i].name);
            item.SetSprite(AssetDatabase.LoadAssetAtPath<Sprite>(inventorySaveDataOld.deckDatas[i].image));
            item.SetCardSetData(AssetDatabase.LoadAssetAtPath<CardSetData>(inventorySaveDataOld.deckDatas[i].cardSetData));
            
            deckSlots[inventorySaveDataOld.deckDatas[i].index].AddItem(item);
        }
    }
}
