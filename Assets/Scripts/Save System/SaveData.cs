using System.Collections.Generic;

[System.Serializable]
public class SaveData {
    [System.Serializable]
    public struct PlayerData {
        public float[] position;
    }

    public PlayerData playerData;

    [System.Serializable]
    public struct EnemyData {
        public int state;
        public float[] position;
        public bool isActive;
    }
    
    public List<EnemyData> enemyData = new List<EnemyData>();
    
    [System.Serializable]
    public struct NPCData {
        public int state;
        public float[] position;
        public bool isActive;
    }
    
    public List<NPCData> npcData = new List<NPCData>();

    [System.Serializable]
    public struct InventoryItemData {
        
    }

    public List<InventoryItemData> itemData = new List<InventoryItemData>();
}