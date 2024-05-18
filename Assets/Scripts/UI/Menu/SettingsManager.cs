using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle mouseToggle;
    [SerializeField] private Toggle keyboardToggle;
    
    
    void Start() {
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
        InputManager.Instance.KeyboardControls = keyboardToggle.isOn;
        if(keyboardToggle.isOn)
            InputManager.Instance.EnableKeyboardInput();
        else {
            InputManager.Instance.DisableKeyboardInput();
        }
    }

    public void SetMouseControls() {
        InputManager.Instance.MouseControls = mouseToggle.isOn;
        if(keyboardToggle.isOn)
            InputManager.Instance.EnableMouseInput();
        else {
            InputManager.Instance.DisableMouseInput();
        }
    }
}
