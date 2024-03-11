using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Name : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float timeToWait = 0.5f;
    [SerializeField] private string messageToShow;
    [SerializeField] private GameObject player;

    private bool _playerIsNear = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_playerIsNear)
        {
            StopAllCoroutines();
            StartCoroutine(MessageShowTimer());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        ShowNameManager.OnMouseLoseFocus();
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player nearby");
            _playerIsNear = true;
        }
    }
	
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player far away");
            _playerIsNear = false;
        }
    }

    private void ShowMessage()
    {
        ShowNameManager.OnMouseHover(messageToShow, Mouse.current.position.ReadValue());
    }

    private IEnumerator MessageShowTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}
