using Esper.ESave;
using Esper.ESave.SavableObjects;
using UnityEngine;

public class SavePlayerData : MonoBehaviour {
    private const string PlayerTransformID = "PlayerTransform";
    
    private SaveFileSetup saveFileSetup;
    private SaveFile saveFile;

    private Transform playerTransform;
    
    private void Awake() {
        saveFileSetup = GetComponent<SaveFileSetup>();
        saveFile = saveFileSetup.GetSaveFile();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (saveFile.isOperationOngoing) {
            saveFile.operation.onOperationEnded.AddListener(LoadPlayer);
        }
        else {
            LoadPlayer();
        }
    }
    
    public void LoadPlayer() {
        if (saveFile.HasData(PlayerTransformID)) {
            SavableTransform savableTransform = saveFile.GetTransform(PlayerTransformID);
            playerTransform.CopyTransformValues(savableTransform);
        }
    
        Debug.Log("Loaded player");
    }
    
    
    public void SavePlayer() {
        saveFile.AddOrUpdateData(PlayerTransformID, playerTransform);
        saveFile.Save();
    
        Debug.Log("Saved player");
    }
}
