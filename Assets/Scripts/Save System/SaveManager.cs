
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public static class SaveManager {
    private static SaveSystem saveSystem = new JSONSaveSystem();

    public static bool SaveGame<T>(string relativePath, T data) {
        return saveSystem.SaveData(relativePath, data);
    }

    public static T LoadGame<T>(string relativePath) {
        return saveSystem.LoadData<T>(relativePath);
    }

    public static bool SavePlayer(string relativePath, GameObject player) {
        PlayerStats playerStats = new PlayerStats();
        Vector3 position = player.transform.position;
        playerStats.position[0] = position[0];
        playerStats.position[1] = position[1];
        playerStats.position[2] = position[2];

        return saveSystem.SaveData(relativePath, playerStats);
    }

    public static bool SaveEnemies() {
        return false;
    }
    
    public static bool SaveNPCs() {
        return false;
    }
    
    public static bool SaveInventory() {
        return false;
    }
    
    public static bool SaveSettings() {
        return false;
    }
    
    public static void LoadPlayer(string relativePath, GameObject player) {
        PlayerStats playerStats = saveSystem.LoadData<PlayerStats>(relativePath);
        Vector3 pos = new Vector3(playerStats.position[0], playerStats.position[1], playerStats.position[2]);
        player.transform.position = pos;
        player.GetComponent<MouseInputManager>().enabled = true;
        player.GetComponent<NavMeshAgent>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        
        Camera.main.transform.parent.GetComponent<CameraFollow>().SetTarget(player);
    }

    public static void LoadEnemies() {
        
    }
    
    public static void LoadNPCs() {
        
    }
    
    public static void LoadInventory() {
        
    }
    
    public static void LoadSettings() {
        
    }
}
