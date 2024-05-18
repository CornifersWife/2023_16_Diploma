using System;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance = null;

    public PlayerControls playerControls;

    [SerializeField] private MouseInputManager mouseInputManager;
    [SerializeField] private KeyboardInputManager keyboardInputManager;
    
    public bool MouseControls { get; set; }
    public bool KeyboardControls { get; set; }
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

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
}