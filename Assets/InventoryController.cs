using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class InventoryController : MonoBehaviour {
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private InputAction rightClickAction;

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
}
