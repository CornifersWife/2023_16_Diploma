using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowName: MonoBehaviour
{
    [SerializeField] private float timeToWait = 0.5f;
    [SerializeField] private string messageToShow;
    [SerializeField] private RectTransform nameWindow;
    [SerializeField] private TextMeshProUGUI nameText;
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
        HideMessage();
    }

    void Update()
    {
        CheckPlayerNear();
        if (_playerNear && IsMouseOver())
        {
            _hoverTimer += Time.deltaTime;
            if (_hoverTimer >= timeToWait)
            {
                MoveTextNearCursor();
                ShowMessage();
            }
        }
        else
        {
            _hoverTimer = 0f;
            HideMessage();
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
    
    private void MoveTextNearCursor()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        nameWindow.sizeDelta = new Vector2(nameText.preferredWidth > 100 ? 100 : nameText.preferredWidth, nameText.preferredHeight);
        nameWindow.transform.position = new Vector2(mousePosition.x + nameWindow.sizeDelta.x/2, mousePosition.y);
    }
    
    private void ShowMessage()
    {
        nameText.text = messageToShow;
        nameWindow.gameObject.SetActive(true);
    }

    private void HideMessage()
    {
        nameText.text = messageToShow;
        nameWindow.gameObject.SetActive(false);
    }
}