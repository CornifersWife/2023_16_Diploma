public class PlayerSaveData : SaveData {
    [System.Serializable]
    public struct PlayerData {
        public float[] position;
    }

    public PlayerData playerData;
}