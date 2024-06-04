using System;
using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject {
    [SerializeField] private string enemyName;
    [SerializeField] private List<CardSetData> deck = new List<CardSetData>();
    [SerializeField] private EnemyState state;
    
    public void ChangeState(EnemyState enemyState) {
        state = enemyState;
    }

    public List<CardSetData> GetDeck() {
        return deck;
    }

    public EnemyState GetState() {
        return state;
    }
}
