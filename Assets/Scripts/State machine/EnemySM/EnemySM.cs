using UnityEngine;

public class EnemySM : StateMachine {
    [HideInInspector]
    public LockedState lockedState;
    [HideInInspector]
    public UnbeatenState unbeatenState;
    [HideInInspector]
    public BeatenState beatenState;
    
    [SerializeField] private Enemy enemy;

    [SerializeField] private Material unbeatenMaterial;
    [SerializeField] private Material beatenMaterial;

    private bool isLocked, isBeaten;
    private EnemyPopup enemyPopup;
    private Renderer objectRenderer;
    
    private void Awake() {
        enemyPopup = GetComponent<EnemyPopup>();
        objectRenderer = GetComponent<Renderer>();
        
        lockedState = new LockedState(this);
        unbeatenState = new UnbeatenState(this);
        beatenState = new BeatenState(this);
    }
    
    protected override BaseState GetInitialState() {
        return GetStateFromEnemy();
    }

    public void ChangeState(EnemyState state) {
        enemy.ChangeState(state);
        ChangeState(GetStateFromEnemy());
    }

    private BaseState GetStateFromEnemy() {
        switch (enemy.state) {
            case (EnemyState)0:
                return lockedState;
            case (EnemyState)1: 
                return unbeatenState;
            case (EnemyState)2:
                return beatenState;
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

    public EnemyPopup GetEnemyPopup() {
        return enemyPopup;
    }

    public Enemy GetEnemy() {
        return enemy;
    }

    public Material GetUnbeatenMaterial() {
        return unbeatenMaterial;
    }
    
    public Material GetBeatenMaterial() {
        return beatenMaterial;
    }

    public Renderer GetRenderer() {
        return objectRenderer;
    }
}
