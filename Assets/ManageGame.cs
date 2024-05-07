using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour {
    [SerializeField] private List<CardSet> currentCardSets;
    [SerializeField] private List<EnemySM> enemies;
    [SerializeField] private GameObject unlockableNPC;
    [SerializeField] private GameObject unlockableCard;

    [SerializeField] private Transform waypoint3;
    [SerializeField] private Transform waypoint4;

    private static ManageGame Instance = null;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        if (currentCardSets != null) {
            foreach (CardSet cardSet in currentCardSets) {
                InventoryController.Instance.AddItem(cardSet);
            }
        }
    }

    private void Update() {
        if (enemies[0].GetEnemy().GetState() == EnemyState.Defeated) {
            unlockableNPC.SetActive(true);
            enemies[1].ChangeState(EnemyState.Undefeated);
        }

        if (enemies[1].GetEnemy().GetState() == EnemyState.Defeated) {
            unlockableCard.SetActive(true);
        }
    }

}
