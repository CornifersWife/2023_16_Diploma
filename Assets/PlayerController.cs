using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private InputAction mouseClickAction;
    private Camera _mainCamera;
    private CharacterController _characterController;
    
    private Coroutine _coroutine;
    private Vector3 _targetPosition;
    private int _groundLayer;

    void Awake()
    {
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void OnEnable()
    {
        mouseClickAction.Enable();
        mouseClickAction.performed += Move;
    }

    private void OnDisable()
    {
        mouseClickAction.performed -= Move;
        mouseClickAction.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(_groundLayer) == 0)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(PlayerMove(hit.point));
            _targetPosition = hit.point;
        }
    }

    private IEnumerator PlayerMove(Vector3 target)
    {
        // Maintain distance from player to floor
        float playerDistanceToFloor = transform.position.y - target.y;
        target.y += playerDistanceToFloor;
        
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 direction = target - transform.position;
            Vector3 movement = moveSpeed * Time.deltaTime * direction.normalized;
            _characterController.Move(movement);

            direction.y = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), rotationSpeed *Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_targetPosition, 0.3f);
    }
}
