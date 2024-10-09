using UnityEngine;

[DefaultExecutionOrder(-100)] // so that the input manager loads before keyboard and mouse managers 
public class InputManager : MonoBehaviour {
    public static InputManager Instance = null;

    public PlayerControls playerControls;

    [SerializeField] private MouseInputManager mouseInputManager;
    [SerializeField] private KeyboardInputManager keyboardInputManager;

    public bool MouseControls { get; set; } = true;
    public bool KeyboardControls { get; set; } = true;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
        DontDestroyOnLoad(this);
        playerControls = new PlayerControls();
    }

    public void DisableAllInput() {
        DisableMouseInput();
        DisableKeyboardInput();
    }

    public void EnableAllInput() {
        if(MouseControls)
            EnableMouseInput();
        if(KeyboardControls)
            EnableKeyboardInput();
    }

    public void DisableAllMovement() {
        DisableMouseInput();
        DisableKeyboardMovement();
    }

    public void EnableAllMovement() {
        if(MouseControls)
            EnableMouseInput();
        if(KeyboardControls)
            EnableKeyboardMovement();
    }

    public void EnableMouseInput() {
        mouseInputManager.EnableMouseControls();
    }

    public void DisableMouseInput() {
        mouseInputManager.DisableMouseControls();
    }
    
    public void EnableKeyboardInput() {
        keyboardInputManager.EnableKeyboardControls();
    }

    public void DisableKeyboardInput() {
        keyboardInputManager.DisableKeyboardControls();
    }

    private void EnableKeyboardMovement() {
        keyboardInputManager.EnableMovement();
    }

    private void DisableKeyboardMovement() {
        keyboardInputManager.DisableMovement();
    }

    public void DisablePause() {
        keyboardInputManager.DisablePause();
    }
    
    public void EnablePause() {
        keyboardInputManager.EnablePause();
    }
}