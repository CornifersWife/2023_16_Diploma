using UnityEngine;
using UnityEngine.InputSystem;

//TODO attach this to some manager, not every enemy object
public class EnemyPopup : MonoBehaviour {
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private InputAction mouseClickAction;
    [SerializeField] private EnemySM enemySM;
    [SerializeField] private GameObject deckPopup;
    
    private Camera mainCamera;
    public GameObject player;

    private int enemyLayer;
    private PlayerController playerController;
    private InventoryController inventoryController;
    private Vector3 target;
    
    private void Awake() {
        mainCamera = Camera.main;
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerController = player.GetComponent<PlayerController>();
        inventoryController = InventoryController.Instance;
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
            popupPanel.gameObject.SetActive(true);
            playerController.enabled = false;
        }
    }

    public void YesClicked() {
        popupPanel.gameObject.SetActive(false);
        if (CheckDeck(3)) {
            playerController.enabled = true;
            SceneSwitcher.Instance.LoadScene("Irys playspace");
        }
        else {
            deckPopup.SetActive(true);
            NoClicked();
        }
    }

    public void NoClicked() {
        popupPanel.gameObject.SetActive(false);
        playerController.enabled = true;
    }

    public void ClosePopup() {
        deckPopup.SetActive(false);
    }

    private bool CheckDeck(int count) {
        int occupiedCount = 0;
        foreach (ItemSlot slot in inventoryController.GetDeck()) {
            if (slot.IsOccupied())
                occupiedCount++;
        }
        return occupiedCount == count;
    }
}
