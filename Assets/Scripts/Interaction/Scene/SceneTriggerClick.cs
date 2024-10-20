using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneTriggerClick : MonoBehaviour {
    /*
    [SerializeField] private string loadName;
    [SerializeField] private string unloadName;
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private InputAction mouseClickAction;
    private Camera mainCamera;
    public GameObject player;

    private int doorLayer;
    private PlayerController playerController;
    private Vector3 target;
    private void Awake() {
        mainCamera = Camera.main;
        doorLayer = LayerMask.NameToLayer("Door");
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
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            if (loadName != "")
                SceneSwitcher.Instance.LoadScene(loadName);
            if(unloadName != "")
                SceneSwitcher.Instance.UnloadScene(unloadName);
        }
    }

    private void Clicked(InputAction.CallbackContext context) {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(doorLayer) == 0) {
            target = hit.point;
            popupPanel.gameObject.SetActive(true);
            playerController.enabled = false;
        }
    }

    public void YesClicked() {
        popupPanel.gameObject.SetActive(false);
        playerController.Walk(target);
    }

    public void NoClicked() {
        popupPanel.gameObject.SetActive(false);
        playerController.enabled = true;
    }
    */
}