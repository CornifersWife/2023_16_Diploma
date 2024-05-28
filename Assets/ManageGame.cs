using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour {
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject npc;
    [SerializeField] private GameObject npc2;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject wall;
    [SerializeField] private ParticleSystem removablePS;
    [SerializeField] private List<CardSet> cardSets;
        
    public bool IsStarted => SceneManager.GetActiveScene().name == "beta-release";
    public static ManageGame Instance = null;
    
    public bool IsAfterTutorial { get; set; }
    public bool IsAfterFirstFight { get; set; }
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
        LoadSettings();
        foreach (CardSet cardSet in cardSets) {
            InventoryController.Instance.AddItem(cardSet);
        }
    }
    
    private void Update() {
        if (IsAfterTutorial && SceneManager.GetActiveScene().name == "beta-release") {
            enemy.GetComponent<EnemySM>().GetEnemy().ChangeState(EnemyState.Undefeated);
            IsAfterTutorial = false;
        }
        
        Debug.Log(IsAfterFirstFight);

        if (IsAfterFirstFight && SceneManager.GetActiveScene().name == "beta-release") {
            Debug.Log("Fight won");
            enemy.GetComponent<EnemySM>().GetEnemy().ChangeState(EnemyState.Defeated);
            Destroy(enemy);
            wall.SetActive(false);
            Destroy(removablePS);
            IsAfterFirstFight = false;
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

}
