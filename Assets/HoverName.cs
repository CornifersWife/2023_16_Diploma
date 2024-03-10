using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Name : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float timeToWait = 0.5f;
    [SerializeField] private string messageToShow;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(MessageShowTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
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
