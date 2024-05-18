using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class InventoryController : MonoBehaviour {
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
        DontDestroyOnLoad(gameObject); 
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
        InputManager.Instance.DisableAllInput();
    }

    public void HideInventory() {
        if (PauseManager.Instance.IsOpen)
            return;
        postProcessVolume.enabled = false;
        manageCardSetDetails.Hide();
        DeselectAllSlots();
        inventoryUI.SetActive(false);
        isOpen = false;
        InputManager.Instance.EnableAllInput();
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
}
