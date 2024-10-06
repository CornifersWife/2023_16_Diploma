using Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractableObject : MonoBehaviour, IInteractable, IPointerClickHandler{
    public abstract void Interact();
    
    public void OnPointerClick(PointerEventData eventData) {
        Interact();
    }
}
