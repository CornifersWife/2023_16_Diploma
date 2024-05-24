public static class SaveManager {
    private static SaveSystem saveSystem = new JSONSaveSystem();
    
    public static bool SaveGame<T>(string relativePath, T data) {
        return saveSystem.SaveData(relativePath, data);
    }

    public static T LoadGame<T>(string relativePath) {
        return saveSystem.LoadData<T>(relativePath);
    }
}
