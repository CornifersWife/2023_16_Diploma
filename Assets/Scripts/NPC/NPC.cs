using Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NPC {
    public abstract class NPC : MonoBehaviour, IInteractable, IPointerClickHandler {
        
        public void OnPointerClick(PointerEventData eventData) {
            Interact();
        }
        
        public abstract void Interact();
    }
}