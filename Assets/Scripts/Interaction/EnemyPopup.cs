using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPopup : MonoBehaviour {
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private InputAction mouseClickAction;
    [SerializeField] private EnemySM enemySM;
    
    private Camera mainCamera;
    public GameObject player;

    private int clickableLayer;
    private PlayerController playerController;
    private Vector3 target;
    
    private void Awake() {
        mainCamera = Camera.main;
        clickableLayer = LayerMask.NameToLayer("Clickable");
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
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(clickableLayer) == 0) {
            popupPanel.gameObject.SetActive(true);
            playerController.enabled = false;
        }
    }

    public void YesClicked() {
        popupPanel.gameObject.SetActive(false);
        enemySM.SetBeaten(true);
    }

    public void NoClicked() {
        popupPanel.gameObject.SetActive(false);
        enemySM.SetBeaten(false);
        playerController.enabled = true;
    }
}
