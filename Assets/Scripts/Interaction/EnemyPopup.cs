using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPopup : MonoBehaviour, PlayerControls.IEnemyClickMapActions {
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private GameObject deckPopup;

    private PlayerControls playerControls;
    
    private Camera mainCamera;
    public GameObject player;
    private Enemy enemy; //TODO IF We dont want to use null, use for example Enemy 0 for default value

    private int enemyLayer;
    private PlayerController playerController;
    private Vector3 target;
    
    private void Awake() {
        mainCamera = Camera.main;
        
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerController = player.GetComponent<PlayerController>();
        popupPanel.gameObject.SetActive(false);
    }

    private void Start() {
        playerControls = InputManager.Instance.playerControls;
        playerControls.EnemyClickMap.SetCallbacks(this);
        playerControls.EnemyClickMap.Enable();
    }
    
    public void OnEnemyClick(InputAction.CallbackContext context) {
        Clicked(context);
    }

    private void Clicked(InputAction.CallbackContext context) {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(enemyLayer) == 0) {
            if (enemy == null) {
                enemy = hit.collider.gameObject.GetComponent<EnemySM>().GetEnemy();
                if (enemy.GetState() == EnemyState.Undefeated) {
                    playerController.enabled = false;
                    popupPanel.gameObject.SetActive(true);
                    UIManager.Instance.SetIsOpen(true);
                    playerControls.ObjectClickMap.ObjectClick.Disable();
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
        playerController.enabled = true;
        enemy = null;
        UIManager.Instance.SetIsOpen(false);
        playerControls.ObjectClickMap.ObjectClick.Enable();
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
