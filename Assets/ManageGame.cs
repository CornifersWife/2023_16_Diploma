using UnityEngine;
using UnityEngine.Windows;

public class ManageGame : MonoBehaviour {
    [SerializeField] private SettingsManager settingsManager;
    public bool IsStarted { get; private set; }
    public static ManageGame Instance = null;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        LoadSettings();
    }

    public void SaveSettings() {
        SettingsSaveData settingsSaveData = new SettingsSaveData();
        settingsManager.PopulateSaveData(settingsSaveData);
        SaveManager.SaveGame("/settings.json", settingsSaveData);
    }

    public void LoadSettings() {
        if (File.Exists(Application.persistentDataPath + "/settings.json")) {
            SettingsSaveData settingsSaveData = SaveManager.LoadGame<SettingsSaveData>("/settings.json");
            settingsManager.LoadSaveData(settingsSaveData);
        }
    }

}
