using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PauseManager : MonoBehaviour {
    [SerializeField] private GameObject pauseView;
    private PostProcessVolume postProcessVolume;

    public static PauseManager Instance = null;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
        pauseView.SetActive(false);
        postProcessVolume = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessVolume>();
    }

    public void Open() {
        pauseView.SetActive(true);
        postProcessVolume.enabled = true;
        UIManager.Instance.SetIsOpen(true);
        Time.timeScale = 0;
    }

    public void Close() {
        pauseView.SetActive(false);
        postProcessVolume.enabled = false;
        UIManager.Instance.SetIsOpen(false);
        Time.timeScale = 1;
    }

    public void ContinueClicked() {
        Close();
    }

    public void SaveClicked() {
        //TODO save
    }

    public void OptionsClicked() {
        pauseView.SetActive(false);
        
    }

    public void ExitClicked() {
        SceneSwitcher.Instance.LoadScene("Main Menu");
    }
}
