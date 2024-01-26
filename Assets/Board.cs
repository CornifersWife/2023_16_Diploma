using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public List<MinionCardData> playerMinions = new List<MinionCardData>(5);
    public List<MinionCardData> opponentMinions = new List<MinionCardData>(5);
    
    public GameObject minionCardPrefab; // Reference to the minion card prefab
    public Transform playerMinionArea; // Parent transform for player minions
    public Transform opponentMinionArea; // Parent transform for opponent minions

    public int cardSpacing = 1;
    
    public Hero playerHero;
    public Hero opponentHero;

    public void AddMinionToBoard(MinionCardData minion, bool isPlayerSide) {
        List<MinionCardData> side = isPlayerSide ? playerMinions : opponentMinions;
        Transform parentTransform = isPlayerSide ? playerMinionArea : opponentMinionArea;

        if (side.Count < 5) {
            side.Add(minion);
            // Instantiate the minion card prefab and set up its display
            GameObject minionObj = Instantiate(minionCardPrefab, parentTransform);
            CardDisplay cardDisplay = minionObj.GetComponent<CardDisplay>();
            cardDisplay.SetupCard(minion);

            // Position the minion on the board visually
            UpdateMinionPositions(isPlayerSide);
        } else {
            Debug.Log("No more space on the board.");
        }
    }

    public void MinionsAttack(bool isPlayerSide) {
        List<MinionCardData> attackers = isPlayerSide ? playerMinions : opponentMinions;
        List<MinionCardData> targets = !isPlayerSide ? playerMinions : opponentMinions;
        
        
    }

    private void UpdateMinionPositions(bool isPlayerSide) {
        List<MinionCardData> side = isPlayerSide ? playerMinions : opponentMinions;
        Transform parentTransform = isPlayerSide ? playerMinionArea : opponentMinionArea;
        
        for (int i = 0; i < side.Count; i++) {
            // Calculate the position for each minion
            Vector3 minionPos = new Vector3(i * cardSpacing, 0, 0);
            parentTransform.GetChild(i).localPosition = minionPos;
        }
    }

    // Other methods to handle minion removal, attacking, etc.
}