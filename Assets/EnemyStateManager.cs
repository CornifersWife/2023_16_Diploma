using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour {
    [SerializeField] private List<EnemySM> enemyList = new List<EnemySM>();
    
    private static EnemyStateManager Instance = null;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void ChangeEnemyState(EnemySM enemy, BaseState state) {
        
    }
}
