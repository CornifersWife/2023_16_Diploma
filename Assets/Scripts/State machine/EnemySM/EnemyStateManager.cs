using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour, ISaveable {
    private List<EnemySM> enemyList = new List<EnemySM>();
    private EnemySM currentEnemy;
    private GameObject[] allEnemies;

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
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in allEnemies) {
            if (enemy.TryGetComponent<EnemySM>(out var enemySm))
                enemyList.Add(enemySm);
        }
    }

    public void ChangeEnemyState(EnemyState state) {
        currentEnemy.ChangeState(state);
    }

    public void SetCurrentEnemy(EnemySM enemy) {
        currentEnemy = enemy;
    }

    public Enemy GetCurrentEnemy() {
        return currentEnemy.GetEnemy();
    }

    public void PopulateSaveData(SaveDataOld saveDataOld) {
        EnemySaveDataOld enemySaveDataOld = (EnemySaveDataOld)saveDataOld;
        foreach (GameObject enemy in allEnemies) {
            EnemySaveDataOld.EnemyData enemyData = new EnemySaveDataOld.EnemyData();
            float[] pos = new float[3];
            Vector3 position = enemy.transform.position;
            pos[0] = position.x;
            pos[1] = position.y;
            pos[2] = position.z;
            enemyData.position = pos;
            enemyData.isActive = enemy.activeSelf;
            enemyData.state = (int)enemy.GetComponent<EnemySM>().GetState();
            enemySaveDataOld.enemyDatas.Add(enemyData);
        }
    }

    public void LoadSaveData(SaveDataOld saveDataOld) {
        EnemySaveDataOld enemySaveDataOld = (EnemySaveDataOld)saveDataOld;
        List<EnemySaveDataOld.EnemyData> enemyDatas = enemySaveDataOld.enemyDatas;
        
        for (int i = 0; i < enemyDatas.Count; i++) {
            allEnemies[i].SetActive(enemyDatas[i].isActive);
            Vector3 pos = new Vector3(enemyDatas[i].position[0], enemyDatas[i].position[1], enemyDatas[i].position[2]);
            allEnemies[i].transform.position = pos;
            allEnemies[i].GetComponent<EnemySM>().ChangeState((EnemyState)enemyDatas[i].state);
        }
    }
}