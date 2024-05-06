using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour {
    private List<EnemySM> enemyList = new List<EnemySM>();
    private Enemy currentEnemy;
    
    public static EnemyStateManager Instance = null;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        DontDestroyOnLoad(this);
        LoadAllEnemies();
    }

    private void LoadAllEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            enemyList.Add(enemy.GetComponent<EnemySM>());
        }
    }

    public void ChangeEnemyState(EnemyState state) {
        currentEnemy.ChangeState(state);
    }

    public void SetCurrentEnemy(Enemy enemy) {
        currentEnemy = enemy;
    }
    
    public Enemy GetCurrentEnemy() {
        return currentEnemy;
    }
    
}