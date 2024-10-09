using UnityEngine;

public class EnemySM : StateMachine {
    [HideInInspector]
    public LockedState lockedState;
    [HideInInspector]
    public UndefeatedState undefeatedState;
    [HideInInspector]
    public DefeatedState defeatedState;
    
    [SerializeField] private Enemy enemy;

    [SerializeField] private Material undefeatedMaterial;
    [SerializeField] private Material defeatedMaterial;

    private bool isLocked, isBeaten;
    private Renderer objectRenderer;
    
    private void Awake() {
        objectRenderer = GetComponent<Renderer>();
        
        lockedState = new LockedState(this);
        undefeatedState = new UndefeatedState(this);
        defeatedState = new DefeatedState(this);
    }
    
    protected override BaseState GetInitialState() {
        return GetStateFromEnemy();
    }

    public void ChangeState(EnemyState state) {
        enemy.ChangeState(state);
        ChangeState(GetStateFromEnemy());
    }

    private BaseState GetStateFromEnemy() {
        switch (enemy.GetState()) {
            case EnemyState.Locked:
                return lockedState;
            case EnemyState.Undefeated: 
                return undefeatedState;
            case EnemyState.Defeated:
                return defeatedState;
            default:
                return lockedState;
        }
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

    public Enemy GetEnemy() {
        return enemy;
    }

    public Material GetUndefeatedMaterial() {
        return undefeatedMaterial;
    }
    
    public Material GetDefeatedMaterial() {
        return defeatedMaterial;
    }

    public Renderer GetRenderer() {
        return objectRenderer;
    }
}
