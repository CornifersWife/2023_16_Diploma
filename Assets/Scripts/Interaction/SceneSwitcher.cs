using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    public static SceneSwitcher Instance = null;
    [SerializeField] private GameObject player;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject); 
    }

    public void LoadScene(string sceneName) {
        player.SetActive(sceneName == "beta-release");
        if(!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void UnloadScene(string sceneName) {
        if(SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
    }
}
