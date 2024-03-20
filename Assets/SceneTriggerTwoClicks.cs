using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneTriggerTwoClicks : MonoBehaviour
{
    // Click on door
    // if player is far away automatically go to door
    // if player is near click again to enter door
    
    [SerializeField] private string loadName;
    [SerializeField] private string unloadName;
    [SerializeField] private float detectionRadius;
    public GameObject player;
    [SerializeField] private InputAction mouseClickAction;
    
    private Camera mainCamera;
    private int clickableLayer;
    private PlayerController playerController;
    private Coroutine coroutine;

    private int clickNumber = 0;
    private void Awake()
    {
        mainCamera = Camera.main;
        clickableLayer = LayerMask.NameToLayer("Clickable");
        playerController = player.GetComponent<PlayerController>();
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
    
    private void SwitchScene()
    {
        if (loadName != "")
            SceneSwitcher.Instance.LoadScene(loadName);
        if(unloadName != "")
            SceneSwitcher.Instance.UnloadScene(unloadName);
    }

    private void Clicked(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(clickableLayer) == 0)
        {
            clickNumber += 1;
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);
            if (clickNumber == 1 && playerDistance > detectionRadius)
            {
                playerController.enabled = false;
                coroutine = StartCoroutine(MovePlayer(hit.point));
            }

            if (clickNumber == 2)
            {
                SwitchScene();
            }
        }
        else
        {
            clickNumber = 0;
        }
    }

    private IEnumerator MovePlayer(Vector3 target)
    {
        yield return StartCoroutine(playerController.PlayerMove(target, 1f));
        playerController.enabled = true;
    }
}
