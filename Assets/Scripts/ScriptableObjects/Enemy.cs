using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject {
    [SerializeField] private string enemyName;
    [SerializeField] private List<CardSetData> deck = new List<CardSetData>();
    public EnemyState state;

    public void ChangeState(EnemyState enemyState) {
        state = enemyState;
    }

    public List<CardSetData> GetDeck()
    {
        return deck;
    }
}
