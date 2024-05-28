using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour, ISaveable {
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle mouseToggle;
    [SerializeField] private Toggle keyboardToggle;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;
    [SerializeField] private GameObject audioVideoPanel;
    [SerializeField] private GameObject controlsPanel;

    private float currentMusicVolume = 0.5f;
    private float currentSFXVolume = 0.5f;
    private Resolution[] resolutions;
    private static List<string> options;
    private int currentResolutionIndex;
    private bool isFullscreen = true;
    private bool isMouse = true;
    private bool isKeyboard = true;
    
    void Start() {
        //if (SaveManager.settingsSaveExists)
            //return;
        SetUpResolutions();
    }

    public void SetFullscreen() {
        isFullscreen = fullscreenToggle.isOn;
        Screen.fullScreenMode = fullscreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void SetResolution() {
        ChangeResolution(resolutionDropdown.value);
    }

    public void SetKeyboardControls() {
        InputManager.Instance.KeyboardControls = keyboardToggle.isOn;
        isKeyboard = keyboardToggle.isOn;
        if(keyboardToggle.isOn)
            InputManager.Instance.EnableKeyboardInput();
        else {
            InputManager.Instance.DisableKeyboardInput();
        }
    }

    public void SetMouseControls() {
        InputManager.Instance.MouseControls = mouseToggle.isOn;
        isMouse = mouseToggle.isOn;
        if(keyboardToggle.isOn)
            InputManager.Instance.EnableMouseInput();
        else {
            InputManager.Instance.DisableMouseInput();
        }
    }
    
    public void SetMusicVolume(float volume) {
        currentMusicVolume = volume;
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    
    public void SetSFXVolume(float volume) {
        currentSFXVolume = volume;
        sfxMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    private void SetUpResolutions() {
        resolutions = Screen.resolutions;
        options = new List<string>();
        
        for (int i = 0; i <resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (!SaveManager.settingsSaveExists) {
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) {
                    currentResolutionIndex = i;
                }
            }
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void ChangeResolution(int resolutionIndex) {
        currentResolutionIndex = resolutionIndex;
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        Resolution resolution = resolutions[resolutionIndex];
        if(!resolution.Equals(Screen.currentResolution)) 
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void PopulateSaveData(SaveData saveData) {
        ((SettingsSaveData)saveData).settingsData.musicVolume = currentMusicVolume;
        ((SettingsSaveData)saveData).settingsData.sfxVolume = currentSFXVolume;
        ((SettingsSaveData)saveData).settingsData.resolutions = resolutions;
        ((SettingsSaveData)saveData).settingsData.resolutionIndex = currentResolutionIndex;
        ((SettingsSaveData)saveData).settingsData.isFullscreen = isFullscreen;
        ((SettingsSaveData)saveData).settingsData.isMouse = isMouse;
        ((SettingsSaveData)saveData).settingsData.isKeyboard = isKeyboard;
    }

    public void LoadSaveData(SaveData saveData) {
        SetMusicVolume(((SettingsSaveData)saveData).settingsData.musicVolume);
        musicSlider.value = currentMusicVolume;
        SetSFXVolume(((SettingsSaveData)saveData).settingsData.sfxVolume);
        sfxSlider.value = currentSFXVolume;
        
        currentResolutionIndex = ((SettingsSaveData)saveData).settingsData.resolutionIndex;
        resolutions = ((SettingsSaveData)saveData).settingsData.resolutions;
        SetUpResolutions();
        
        fullscreenToggle.isOn = ((SettingsSaveData)saveData).settingsData.isFullscreen;
        SetFullscreen();
        
        mouseToggle.isOn = ((SettingsSaveData)saveData).settingsData.isMouse;
        SetMouseControls();
        keyboardToggle.isOn = ((SettingsSaveData)saveData).settingsData.isKeyboard;
        SetKeyboardControls();
    }
}
