using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    
    void Start() {
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
}
