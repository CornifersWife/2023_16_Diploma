using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowName: MonoBehaviour
{
    [SerializeField] private float timeToWait = 0.5f;
    [SerializeField] private string messageToShow;
    [SerializeField] private float detectionRadius = 3f;
    [SerializeField] private GameObject player;
    
    private Camera _camera;
    private bool _playerNear = false;
    private float _hoverTimer = 0f;

    void Awake()
    {
        _camera = Camera.main;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckPlayerNear();
        if (_playerNear && IsMouseOver())
        {
            _hoverTimer += Time.deltaTime;
            if (_hoverTimer >= timeToWait)
            {
                ShowMessage();
            }
        }
        else
        {
            _hoverTimer = 0f;
            ShowNameManager.OnMouseLoseFocus();
        }
    }

    void CheckPlayerNear()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        if (distanceToPlayer <= detectionRadius)
        {
            _playerNear = true;
        }
        else
        {
            _playerNear = false;
        }
    }

    bool IsMouseOver()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }
    
    private void ShowMessage()
    {
        ShowNameManager.OnMouseHover(messageToShow, Mouse.current.position.ReadValue());
    }
}