using SaveSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IPointerClickHandler {
    public string sceneName;
    
    public void OnPointerClick(PointerEventData eventData) {
        if(sceneName != "OverworldTesting")
            SaveManager.Instance.SaveGame();
        SceneManager.LoadScene(sceneName);
    }
}
