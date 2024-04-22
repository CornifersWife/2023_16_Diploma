using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour {

    public static UIManager Instance = null;

    [SerializeField] private GameObject UIGameObject;
    private int activeUIOnStart;
    private bool isOpen;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        UIGameObject = this.gameObject;
    }

    private void Start() {
        activeUIOnStart = SetUICountOnStart();
    }
    
    private int SetUICountOnStart() {
        int count = 0;
        foreach (Transform uiChild in UIGameObject.transform) {
            if (uiChild.gameObject.activeSelf) {
                count++;
            }
        }
        return count;
    }

    public int GetCurrentUICount() {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        PointerEventData eventData = new(EventSystem.current)
        {
            position = mousePos
        };
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count;
    }

    public int GetUICountOnStart() {
        return activeUIOnStart;
    }

    public bool IsOpen() {
        return isOpen;
    }

    public void SetIsOpen(bool value) {
        isOpen = value;
    }
}