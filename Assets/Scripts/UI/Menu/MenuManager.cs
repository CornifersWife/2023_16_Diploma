#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject mainView;
    [SerializeField] private GameObject optionsView;
    [SerializeField] private GameObject creditsView;
    
    private void Awake() {
        mainView.SetActive(true);
        optionsView.SetActive(false);
        creditsView.SetActive(false);
    }

    #region  Main View
    public void StartClicked(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void ContinueClicked() {
        //TODO Load latest save
    }

    public void OptionsClicked() {
        mainView.SetActive(false);
        optionsView.SetActive(true);
    }

    public void CreditsClicked() {
        mainView.SetActive(false);
        creditsView.SetActive(true);
    }

    public void ExitClicked() {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    #endregion

    public void BackClicked() {
        Awake();
    }
}
