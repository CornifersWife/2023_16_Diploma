using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour {
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject npc;
    [SerializeField] private GameObject npc2;
    [SerializeField] private EnemySM enemy;
    [SerializeField] private GameObject wall;
    [SerializeField] private ParticleSystem removablePS;
    [SerializeField] private List<CardSet> cardSets;
        
    public bool IsStarted => SceneManager.GetActiveScene().name is "beta-release" or "beta-release-2" or "New Scene";
    public static ManageGame Instance = null;
    
    public bool IsAfterTutorial { get; set; }
    public bool IsAfterSecondFight { get; set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start() {
        if (SceneManager.GetActiveScene().name == "Irys playspace")
            return;
        //LoadSettings();
        /*foreach (CardSet cardSet in cardSets) {
            InventoryController.Instance.AddItem(cardSet);
        }
        */
    }
    
    private void Update() {
        if (IsAfterTutorial && SceneManager.GetActiveScene().name == "beta-release") {
            enemy.GetEnemy().ChangeState(EnemyState.Undefeated);
            IsAfterTutorial = false;
        }
        
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

    public void SaveInventory() {
        InventorySaveData inventorySaveData = new InventorySaveData();
        inventoryController.PopulateSaveData(inventorySaveData);
        SaveManager.SaveGame(SaveManager.inventorySavePath, inventorySaveData);
    }

    public void LoadInventory() {
        if (SaveManager.inventorySaveExists) {
            InventorySaveData inventorySaveData = SaveManager.LoadGame<InventorySaveData>(SaveManager.inventorySavePath);
            inventoryController.LoadSaveData(inventorySaveData);
        }
    }

}
