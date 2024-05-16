using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EnemyPopup : MonoBehaviour{
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private GameObject deckPopup;
    [SerializeField] private InputAction mouseClickAction;
    
    private Camera mainCamera;
    private Enemy enemy; //TODO IF We dont want to use null, use for example Enemy 0 for default value

    private int enemyLayer;
    private Vector3 target;
    
    private void Awake() {
        mainCamera = Camera.main;
        
        enemyLayer = LayerMask.NameToLayer("Enemy");
        popupPanel.gameObject.SetActive(false);
    }

    private void OnEnable() {
        mouseClickAction.Enable();
        mouseClickAction.performed += Clicked;
    }

    private void OnDisable() {
        mouseClickAction.performed -= Clicked;
        mouseClickAction.Disable();
    }

    private void Clicked(InputAction.CallbackContext context) {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Irys playspace")) {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider &&
                hit.collider.gameObject.layer.CompareTo(enemyLayer) == 0) {
                if (enemy != null)
                    return;
                enemy = hit.collider.gameObject.GetComponent<EnemySM>().GetEnemy();
                if (enemy.GetState() == EnemyState.Undefeated) {
                    popupPanel.gameObject.SetActive(true);
                    UIManager.Instance.SetIsOpen(true);
                    ObjectClickHandler.Instance.SetObject(hit.collider.gameObject);
                }
                else {
                    enemy = null;
                }
            }
        }
    }

    public void YesClicked() {
        popupPanel.gameObject.SetActive(false);
        if (CheckDeck(3)) {
            EnemyStateManager.Instance.SetCurrentEnemy(enemy);
            Close();
            SceneSwitcher.Instance.LoadScene("Irys playspace");
        }
        else {
            deckPopup.SetActive(true);
        }
    }

    public void NoClicked() {
        popupPanel.gameObject.SetActive(false);
        Close();
    }

    public void ClosePopup() {
        deckPopup.SetActive(false);
        Close();
        InventoryController.Instance.ShowInventory(new InputAction.CallbackContext());
    }

    private void Close() {
        enemy = null;
        UIManager.Instance.SetIsOpen(false);
    }

    private bool CheckDeck(int count) {
        int occupiedCount = 0;
        foreach (ItemSlot slot in InventoryController.Instance.GetDeck()) {
            if (slot.IsOccupied())
                occupiedCount++;
        }
        return occupiedCount == count;
    }
}
