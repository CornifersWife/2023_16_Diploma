using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IPointerClickHandler {
    public string sceneName;
    
    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene(sceneName);
    }
}
