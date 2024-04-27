using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance = null;

    public PlayerControls playerControls;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        playerControls = new PlayerControls();
    }
}