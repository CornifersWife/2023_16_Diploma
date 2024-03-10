using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Name : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float timeToWait = 1f;
    [SerializeField] private string messageToShow;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Active");
        StopAllCoroutines();
        StartCoroutine(MessageShowTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Not active");
        StopAllCoroutines();
        ShowNameManager.OnMouseLoseFocus();
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
