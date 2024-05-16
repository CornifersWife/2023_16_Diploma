using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer SFXMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    
    void Start() {
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(ResolutionHandler.Options);
        resolutionDropdown.value = ResolutionHandler.CurrentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        fullscreenToggle.GetComponent<Toggle>().isOn = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen;
    }
    
    public void SetMusicVolume(float volume) {
        musicMixer.SetFloat("volume", volume);
    }
    
    public void SetSFXVolume(float volume) {
        SFXMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreenMode = isFullscreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
    }

    public void SetResolution() {
        ResolutionHandler.ChangeResolution(resolutionDropdown.value);
    }
}
