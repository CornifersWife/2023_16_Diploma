using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPopup : MonoBehaviour {
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private InputAction mouseClickAction;
    [SerializeField] private GameObject deckPopup;
    
    private Camera mainCamera;
    public GameObject player;
    private Enemy enemy; //TODO IF We want to not use null, use for example Enemy 0 for default value

    private int enemyLayer;
    private PlayerController playerController;
    private Vector3 target;
    
    private void Awake() {
        mainCamera = Camera.main;
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerController = player.GetComponent<PlayerController>();
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
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(enemyLayer) == 0) {
            if (enemy == null) {
                enemy = hit.collider.gameObject.GetComponent<EnemySM>().GetEnemy();
                if (enemy.GetState() == EnemyState.UnbeatenState) {
                    playerController.enabled = false;
                    popupPanel.gameObject.SetActive(true);
                }
            }
        }
    }

    public void YesClicked() {
        popupPanel.gameObject.SetActive(false);
        if (CheckDeck(3)) {
            playerController.enabled = true;
            EnemyStateManager.Instance.SetCurrentEnemy(enemy);
            popupPanel.gameObject.SetActive(false);
            enemy = null;
            SceneSwitcher.Instance.LoadScene("Irys playspace");
        }
        else {
            deckPopup.SetActive(true);
            popupPanel.gameObject.SetActive(false);
        }
    }

    public void NoClicked() {
        popupPanel.gameObject.SetActive(false);
        playerController.enabled = true;
        enemy = null;
    }

    public void ClosePopup() {
        deckPopup.SetActive(false);
        playerController.enabled = true;
        enemy = null;
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
