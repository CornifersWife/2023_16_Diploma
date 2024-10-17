using System.Collections.Generic;
using Esper.ESave;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour, ISavable {
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

    private bool isResolutionLoaded = false;

    private const string MusicVolumeSaveID = "MusicVolume";
    private const string SFXVolumeSaveID = "SFXVolume";
    private const string ResolutionSaveID = "ResolutionId";
    private const string FullscreenSaveID = "FullscreenId";
    private const string MouseSaveID = "Mouse";
    private const string KeyboardSaveID = "Keyboard";
    
    void Start() {
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
        Debug.Log(InputManager.Instance);
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

            if (!isResolutionLoaded) {
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

   public void PopulateSaveData(SaveFile saveFile) {
        saveFile.AddOrUpdateData(MusicVolumeSaveID, currentMusicVolume);
        saveFile.AddOrUpdateData(SFXVolumeSaveID, currentSFXVolume);
        saveFile.AddOrUpdateData(ResolutionSaveID, currentResolutionIndex);
        saveFile.AddOrUpdateData(FullscreenSaveID, isFullscreen);
        saveFile.AddOrUpdateData(MouseSaveID, isMouse);
        saveFile.AddOrUpdateData(KeyboardSaveID, isKeyboard);
    }

    public void LoadSaveData(SaveFile saveFile) {
        if (saveFile.HasData(MusicVolumeSaveID)) {
            SetMusicVolume(saveFile.GetData<float>(MusicVolumeSaveID));
            musicSlider.value = currentMusicVolume;
            SetSFXVolume(saveFile.GetData<float>(SFXVolumeSaveID));
            sfxSlider.value = currentSFXVolume;
        
            currentResolutionIndex = saveFile.GetData<int>(ResolutionSaveID);
            isResolutionLoaded = true;
            SetUpResolutions();
            ChangeResolution(currentResolutionIndex);
        
            fullscreenToggle.isOn = saveFile.GetData<bool>(FullscreenSaveID);
            SetFullscreen();
        
            mouseToggle.isOn = saveFile.GetData<bool>(MouseSaveID);
            SetMouseControls();
            keyboardToggle.isOn = saveFile.GetData<bool>(KeyboardSaveID);
            SetKeyboardControls();
        }
    }
}
