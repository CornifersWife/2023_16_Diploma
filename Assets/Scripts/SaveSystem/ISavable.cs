namespace SaveSystem {
    public interface ISavable {
        void PopulateSaveData(SaveData.SaveData saveData);
        void LoadSaveData(SaveData.SaveData saveData);
    }
}