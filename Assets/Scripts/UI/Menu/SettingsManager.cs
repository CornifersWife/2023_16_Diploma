using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle mouseToggle;
    [SerializeField] private Toggle keyboardToggle;
    [SerializeField] private PlayerController playerController;
    
    private PlayerControls playerControls;
    
    void Start() {
        playerControls = InputManager.Instance.playerControls;
        
        ResolutionHandler.SetUpResolutions();
        
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(ResolutionHandler.Options);
        resolutionDropdown.value = ResolutionHandler.CurrentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        fullscreenToggle.GetComponent<Toggle>().isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;
    }

    public void SetFullscreen() {
        Screen.fullScreenMode = fullscreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void SetResolution() {
        ResolutionHandler.ChangeResolution(resolutionDropdown.value);
    }

    public void SetKeyboardControls() {
        if(keyboardToggle.isOn)
            playerControls.PlayerActionMap.Move.Enable();
        else {
            playerControls.PlayerActionMap.Move.Disable();
        }
    }

    public void SetMouseControls() {
        if(mouseToggle.isOn)
            playerController.Enable(true);
        else {
            playerController.Enable(false);
            playerController.Stop();
        }
    }
}
