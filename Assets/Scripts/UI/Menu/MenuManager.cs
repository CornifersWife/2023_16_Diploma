#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject mainView;
    [SerializeField] private GameObject optionsView;
    [SerializeField] private GameObject creditsView;

    private GameObject audioVideoPanel;
    private GameObject controlsPanel;
    
    private void Awake() {
        audioVideoPanel = optionsView.transform.GetChild(0).gameObject;
        controlsPanel = optionsView.transform.GetChild(1).gameObject;
        
        mainView.SetActive(true);
        optionsView.SetActive(false);
        creditsView.SetActive(false);
    }

    private void Start() {
        
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
        audioVideoPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ControlsClicked() {
        audioVideoPanel.SetActive(false);
        controlsPanel.SetActive(true);
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
