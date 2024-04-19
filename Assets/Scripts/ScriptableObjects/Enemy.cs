using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject {
    [SerializeField] private string enemyName;
    public EnemyState state;

    public void ChangeState(EnemyState enemyState) {
        state = enemyState;
    }
}
