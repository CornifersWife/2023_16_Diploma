using System.Collections.Generic;

public class LevelSaveData : SaveData{
    [System.Serializable]
    public struct FogData {
        public bool isActive;
    }

    public List<FogData> fogDatas = new List<FogData>();
    
    public struct WallData {
        public bool isActive;
    }

    public List<WallData> wallDatas = new List<WallData>();
}