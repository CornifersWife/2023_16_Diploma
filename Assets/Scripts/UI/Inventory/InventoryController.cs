using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My;
using ScriptableObjects;
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
        InputManager.Instance.DisableAllMovement();
        InputManager.Instance.DisablePause();
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
        InputManager.Instance.EnableAllMovement();
        InputManager.Instance.EnablePause();
    }

    public void AddItem(Item item) {
        if (item is CardSet)
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
            cardSets.Add(((CardSet)slot.GetItem()).GetCardSetData());
        return cardSets;
    }

    public bool IsOpen() {
        return isOpen;
    }


    public void PopulateSaveData(SaveData saveData) {
        //TODO change how card sets are handled -> Card Data
        for (int i = 0; i < itemSlots.Count; i++) {
            if (!itemSlots[i].IsOccupied()) 
                continue;
            Item item = itemSlots[i].GetItem();
           
            InventorySaveData.ItemData itemData = new InventorySaveData.ItemData {
                index = i,
                name = item.GetName(),
                image = AssetDatabase.GetAssetPath(item.GetSprite())
            };
            ((InventorySaveData)saveData).itemDatas.Add(itemData);
        }
        
        for (int i = 0; i < cardSetSlots.Count; i++) {
            if (!cardSetSlots[i].IsOccupied()) 
                continue;
            Item item = cardSetSlots[i].GetItem();
            
            InventorySaveData.CardSetItemData itemData = new InventorySaveData.CardSetItemData() {
                index = i,
                name = item.GetName(),
                image = AssetDatabase.GetAssetPath(item.GetSprite()),
                cardSetData = AssetDatabase.GetAssetPath(((CardSet)item).GetCardSetData())
            };
            ((InventorySaveData)saveData).cardSetDatas.Add(itemData);
        }
        
        for (int i = 0; i < deckSlots.Count; i++) {
            if (!deckSlots[i].IsOccupied()) 
                continue;
            Item item = deckSlots[i].GetItem();
            
            InventorySaveData.CardSetItemData itemData = new InventorySaveData.CardSetItemData() {
                index = i,
                name = item.GetName(),
                image = AssetDatabase.GetAssetPath(item.GetSprite()),
                cardSetData = AssetDatabase.GetAssetPath(((CardSet)item).GetCardSetData())
            };
            ((InventorySaveData)saveData).deckDatas.Add(itemData);
        }
    }

    public void LoadSaveData(SaveData saveData) {
        InventorySaveData inventorySaveData = (InventorySaveData)saveData;
        for (int i = 0; i < inventorySaveData.itemDatas.Count; i++) {
            CollectibleItem item = new GameObject().AddComponent<CollectibleItem>();
            item.SetName(inventorySaveData.itemDatas[i].name);
            item.SetSprite(AssetDatabase.LoadAssetAtPath<Sprite>(inventorySaveData.itemDatas[i].image));
            itemSlots[inventorySaveData.itemDatas[i].index].AddItem(item);
        }
        
        for (int i = 0; i < inventorySaveData.cardSetDatas.Count; i++) {
            CardSet item = new GameObject().AddComponent<CardSet>();
            item.SetName(inventorySaveData.cardSetDatas[i].name);
            item.SetSprite(AssetDatabase.LoadAssetAtPath<Sprite>(inventorySaveData.cardSetDatas[i].image));
            item.SetCardSetData(AssetDatabase.LoadAssetAtPath<CardSetData>(inventorySaveData.cardSetDatas[i].cardSetData));
            
            cardSetSlots[inventorySaveData.cardSetDatas[i].index].AddItem(item);
        }
        
        for (int i = 0; i < inventorySaveData.deckDatas.Count; i++) {
            CardSet item = new GameObject().AddComponent<CardSet>();
            item.SetName(inventorySaveData.deckDatas[i].name);
            item.SetSprite(AssetDatabase.LoadAssetAtPath<Sprite>(inventorySaveData.deckDatas[i].image));
            item.SetCardSetData(AssetDatabase.LoadAssetAtPath<CardSetData>(inventorySaveData.deckDatas[i].cardSetData));
            
            deckSlots[inventorySaveData.deckDatas[i].index].AddItem(item);
        }
    }
}
