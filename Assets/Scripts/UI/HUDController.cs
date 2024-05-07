using UnityEngine;
using UnityEngine.InputSystem;

public class HUDController : MonoBehaviour {
    [SerializeField] private GameObject overlay;

    private void Update() {
        overlay.SetActive(!UIManager.Instance.IsOpen());
    }
    
    public void PauseGame() {
        PauseManager.Instance.Open();
    }

    public void OpenInventory() {
        InventoryController.Instance.ShowInventory(new InputAction.CallbackContext());
    }
}