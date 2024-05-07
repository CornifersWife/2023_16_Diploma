using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour {
    [SerializeField] private List<CardSet> currentCardSets;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private GameObject unlockableNPC;
    [SerializeField] private GameObject unlockableCard;

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
        if (enemies[0].GetState() == EnemyState.Defeated) {
            unlockableNPC.SetActive(true);
            enemies[1].ChangeState(EnemyState.Undefeated);
        }

        if (enemies[1].GetState() == EnemyState.Defeated) {
            unlockableCard.SetActive(true);
        }
    }

}
