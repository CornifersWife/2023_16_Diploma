using System.Collections.Generic;

public class EnemySaveData : SaveData {
    [System.Serializable]
    public struct EnemyData {
        public int state;
        public float[] position;
        public bool isActive;
    }
    
    public List<EnemyData> enemyDatas = new List<EnemyData>();
}