using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour {
    [SerializeField] private SettingsManager settingsManager;
    public bool IsStarted => SceneManager.GetActiveScene().name == "beta-release";
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
        SaveManager.SaveGame(SaveManager.settingsSavePath, settingsSaveData);
    }

    public void LoadSettings() {
        if (SaveManager.settingsSaveExists) {
            SettingsSaveData settingsSaveData = SaveManager.LoadGame<SettingsSaveData>(SaveManager.settingsSavePath);
            settingsManager.LoadSaveData(settingsSaveData);
        }
    }

}
