using System.Collections.Generic;

public class NPCSaveData : SaveData{
    [System.Serializable]
    public struct NPCData {
        public int state;
        public float[] position;
        public bool isActive;
    }
    
    public List<NPCData> npcData = new List<NPCData>();
}