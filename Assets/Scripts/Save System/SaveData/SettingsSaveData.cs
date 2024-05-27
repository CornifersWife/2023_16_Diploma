using UnityEngine;

public class SettingsSaveData : SaveData {
    [System.Serializable]
    public struct SettingsData {
        public float musicVolume;
        public float sfxVolume;
        public Resolution[] resolutions;
        public int resolutionIndex;
        public bool isFullscreen;
        public bool isMouse;
        public bool isKeyboard;
    }

    public SettingsData settingsData;
}