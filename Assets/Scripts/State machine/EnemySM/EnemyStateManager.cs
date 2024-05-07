using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour {
    private List<EnemySM> enemyList = new List<EnemySM>();
    private Enemy currentEnemy;

    public static EnemyStateManager Instance;

    private void Awake() {
        if (Instance is not null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        DontDestroyOnLoad(this);
        LoadAllEnemies();
    }

    private void LoadAllEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            if (enemy.TryGetComponent<EnemySM>(out var enemySm))
                enemyList.Add(enemySm);
        }
    }

    public void ChangeEnemyState(EnemyState state) {
        Debug.Log(currentEnemy);
        currentEnemy.ChangeState(state);
    }

    public void SetCurrentEnemy(Enemy enemy) {
        currentEnemy = enemy;
    }

    public Enemy GetCurrentEnemy() {
        return currentEnemy;
    }
}