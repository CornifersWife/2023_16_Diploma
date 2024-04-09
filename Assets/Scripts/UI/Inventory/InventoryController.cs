using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class InventoryController : MonoBehaviour {
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private InputAction rightClickAction;

    [SerializeField] private List<ItemSlot> itemSlots;
    [SerializeField] private List<ItemSlot> cardSetSlots;
    [SerializeField] private List<ItemSlot> deckSlots;
    //add space to show cards
    
    private PostProcessVolume postProcessVolume;
    private PlayerController playerController;
    
    public static InventoryController instance = null; 

    private void Awake() {
        if (instance == null) {
            instance = this;
        }         
        else if (instance != this) {
            Destroy(gameObject);
        }          
        DontDestroyOnLoad(gameObject); 
        postProcessVolume = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessVolume>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnEnable() {
        rightClickAction.Enable();
        rightClickAction.started += ShowInventory;
    }

    private void OnDisable() {
        rightClickAction.Disable();
    }

    private void ShowInventory(InputAction.CallbackContext context) {
        inventoryUI.SetActive(true);
        postProcessVolume.enabled = true;
        Time.timeScale = 0;
        playerController.SetIsUIOpen(true);
    }

    public void HideInventory() {
        postProcessVolume.enabled = false;
        inventoryUI.SetActive(false);
        Time.timeScale = 1;
        playerController.SetIsUIOpen(true);
    }

    public void AddItem(Item item) {
        if(item is CardSet)
            AddToSlot(item, cardSetSlots);
        else
            AddToSlot(item, itemSlots);
    }

    public void AddToSlot(Item item, List<ItemSlot> itemList) {
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
            itemSlot.SetIsActive(false);
        }
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
    
    //TODO add something to load from deckslots into deck
    
}
