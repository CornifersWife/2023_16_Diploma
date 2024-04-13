
using UnityEngine;

public class LoadHandler : MonoBehaviour {

    public static LoadHandler Instance = null;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }         
        else if (Instance != this) {
            Destroy(gameObject);
        }          
        DontDestroyOnLoad(gameObject);
    }
}
