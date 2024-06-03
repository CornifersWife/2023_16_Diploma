using System.Collections.Generic;

public class NPCSaveData : SaveData{
    [System.Serializable]
    public struct NPCData {
        public float[] position;
        public bool isActive;
        public string prefab;
    }
    
    public List<NPCData> npcDatas = new List<NPCData>();
}