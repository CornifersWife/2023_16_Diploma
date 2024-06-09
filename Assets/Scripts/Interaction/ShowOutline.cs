using UnityEngine;
using UnityEngine.EventSystems;

public class ShowOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Outline outlineScript;

    private void Awake() {
        outlineScript = GetComponent<Outline>();
        outlineScript.enabled = false;
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        outlineScript.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        outlineScript.enabled = false;
    }
}
