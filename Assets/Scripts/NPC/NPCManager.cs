using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour, ISaveable {
    [SerializeField] private List<GameObject> allNPCs;
    
    public void PopulateSaveData(SaveData saveData) {
        NPCSaveData npcSaveData = (NPCSaveData)saveData;
        foreach (GameObject npc in allNPCs) {
            NPCSaveData.NPCData npcData = new NPCSaveData.NPCData();
            float[] pos = new float[3];
            Vector3 position = npc.transform.position;
            pos[0] = position.x;
            pos[1] = position.y;
            pos[2] = position.z;
            npcData.position = pos;
            npcData.isActive = npc.activeSelf;
            npcSaveData.npcDatas.Add(npcData);
        }
    }

    public void LoadSaveData(SaveData saveData) {
        NPCSaveData npcSaveData = (NPCSaveData)saveData;
        List<NPCSaveData.NPCData> npcDatas = npcSaveData.npcDatas;
        
        for (int i = 0; i < npcDatas.Count; i++) {
            allNPCs[i].SetActive(npcDatas[i].isActive);
            Vector3 pos = new Vector3(npcDatas[i].position[0], npcDatas[i].position[1], npcDatas[i].position[2]);
            allNPCs[i].transform.position = pos;
        }
    }
}
