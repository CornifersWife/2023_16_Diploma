
public static class SaveManager {
    private static SaveSystem saveSystem = new JSONSaveSystem();

    public static void SavePlayer<T>(string relativePath, T data) {
        saveSystem.SaveData(relativePath, data);
    }

    public static T LoadPlayer<T>(string relativePath) {
        return saveSystem.LoadData<T>(relativePath);
    }
    
}
