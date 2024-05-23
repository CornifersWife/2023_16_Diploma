
public abstract class SaveSystem {
    public abstract bool SaveData<T>(string relativePath, T data);

    public abstract T LoadData<T>(string relativePath);
}
