using UnityEngine;

public class EnemySM : StateMachine {
    [HideInInspector]
    public LockedState lockedState;
    [HideInInspector]
    public UnbeatenState unbeatenState;
    [HideInInspector]
    public BeatenState beatenState;

    private bool isLocked, isBeaten;
    
    private void Awake() {
        lockedState = new LockedState(this);
        unbeatenState = new UnbeatenState(this);
        beatenState = new BeatenState(this);
    }
    
    protected override BaseState GetInitialState() {
        return lockedState;
    }

    public bool IsLocked() {
        return isLocked;
    }

    public void SetLocked(bool value) {
        isLocked = value;
    }
    
    public bool IsBeaten() {
        return isBeaten;
    }

    public void SetBeaten(bool value) {
        isBeaten = value;
    }
}
