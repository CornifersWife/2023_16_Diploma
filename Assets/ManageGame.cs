using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private List<CardSet> currentCardSets;
    [SerializeField] private List<EnemySM> enemies;
    [SerializeField] private GameObject unlockableNPC;
    [SerializeField] private GameObject unlockableCard;

    [SerializeField] private Transform waypoint3;
    [SerializeField] private Transform waypoint4;

    private static ManageGame Instance = null;
    private PlayerStats playerStats;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        playerStats = new PlayerStats();
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
            enemies[0].ChangeState(EnemyState.Locked);
            enemies[0].gameObject.SetActive(false);
            enemies[1].ChangeState(EnemyState.Undefeated);
        }

        if (enemies[1].GetEnemy().GetState() == EnemyState.Defeated) {
            unlockableCard.SetActive(true);
            enemies[1].ChangeState(EnemyState.Locked);
            enemies[1].gameObject.SetActive(false);
        }
    }

    public void Save() {
        SaveManager.SavePlayer("/player.json", player);
    }

    public void Load() {
        Destroy(player);
        SaveManager.LoadPlayer("/player.json", Instantiate(player));
    }

}
