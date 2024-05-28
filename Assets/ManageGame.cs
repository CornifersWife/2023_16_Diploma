using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour {
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject npc;
    [SerializeField] private GameObject npc2;
    [SerializeField] private GameObject enemy;
    public bool IsStarted => SceneManager.GetActiveScene().name == "beta-release";
    public static ManageGame Instance = null;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name == "") {
            
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
