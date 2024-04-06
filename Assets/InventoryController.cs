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

    private PostProcessVolume postProcessVolume;
    private PlayerController playerController;

    private void Awake() {
        postProcessVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
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
        playerController.enabled = false;
    }

    public void HideInventory() {
        postProcessVolume.enabled = false;
        playerController.enabled = true;
        inventoryUI.SetActive(false);
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
                return;
            }
        }
    }
}
