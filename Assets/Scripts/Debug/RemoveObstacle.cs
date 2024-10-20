using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveObstacle : MonoBehaviour, IPointerClickHandler {
    public GameObject obstacle;


    public void OnPointerClick(PointerEventData eventData) {
        obstacle.SetActive(false);
    }
}
