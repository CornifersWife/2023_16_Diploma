using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager Instance = null;
    
    private bool isOpen;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public bool IsOpen() {
        return isOpen;
    }

    public void SetIsOpen(bool value) {
        isOpen = value;
    }
}