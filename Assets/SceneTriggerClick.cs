using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneTriggerClick : MonoBehaviour
{
    // Click on door then button pops up click that and it automatically goes to the door
    
    [SerializeField] private string loadName;
    [SerializeField] private string unloadName;
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private InputAction mouseClickAction;
    private Camera mainCamera;
    public GameObject player;

    private int clickableLayer;
    private PlayerController playerController;
    private Vector3 target;
    private void Awake()
    {
        mainCamera = Camera.main;
        clickableLayer = LayerMask.NameToLayer("Clickable");
        playerController = player.GetComponent<PlayerController>();
        popupPanel.gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        mouseClickAction.Enable();
        mouseClickAction.performed += Clicked;
    }

    private void OnDisable()
    {
        mouseClickAction.performed -= Clicked;
        mouseClickAction.Disable();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (loadName != "")
                SceneSwitcher.Instance.LoadScene(loadName);
            if(unloadName != "")
                SceneSwitcher.Instance.UnloadScene(unloadName);
        }
    }

    private void Clicked(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(clickableLayer) == 0)
        {
            target = hit.point;
            popupPanel.gameObject.SetActive(true);
            playerController.enabled = false;
        }
    }

    public void YesClicked()
    {
        popupPanel.gameObject.SetActive(false);
        StartCoroutine(playerController.PlayerMove(target));
    }

    public void NoClicked()
    {
        popupPanel.gameObject.SetActive(false);
        playerController.enabled = true;
    }
}