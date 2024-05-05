using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPopup : MonoBehaviour, PlayerControls.IEnemyClickMapActions {
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private GameObject deckPopup;

    private PlayerControls playerControls;
    
    private Camera mainCamera;
    private Enemy enemy; //TODO IF We dont want to use null, use for example Enemy 0 for default value

    private int enemyLayer;
    private Vector3 target;
    
    private void Awake() {
        mainCamera = Camera.main;
        
        enemyLayer = LayerMask.NameToLayer("Enemy");
        popupPanel.gameObject.SetActive(false);
    }

    private void Start() {
        playerControls = InputManager.Instance.playerControls;
        playerControls.EnemyClickMap.SetCallbacks(this);
        playerControls.EnemyClickMap.Enable();
    }
    
    public void OnEnemyClick(InputAction.CallbackContext context) {
        if(context.started)
            Clicked();
    }

    private void Clicked() {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(enemyLayer) == 0) {
            if (enemy != null)
                return;
            enemy = hit.collider.gameObject.GetComponent<EnemySM>().GetEnemy();
            if (enemy.GetState() == EnemyState.Undefeated) {
                popupPanel.gameObject.SetActive(true);
                UIManager.Instance.SetIsOpen(true);
                ObjectClickHandler.Instance.SetObject(hit.collider.gameObject);
                playerControls.ObjectClickMap.Disable();
                playerControls.PlayerActionMap.Disable();
            }
            else {
                enemy = null;
            }
        }
    }

    public void YesClicked() {
        popupPanel.gameObject.SetActive(false);
        
        if (CheckDeck(3)) {
            Close();
            EnemyStateManager.Instance.SetCurrentEnemy(enemy);
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
    }

    private void Close() {
        enemy = null;
        UIManager.Instance.SetIsOpen(false);
        playerControls.ObjectClickMap.Enable();
        playerControls.PlayerActionMap.Enable();
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
