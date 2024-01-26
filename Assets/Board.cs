using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public int maxMinions = 5;
    public MinionCardData[] playerMinions;
    public MinionCardData[] opponentMinions;

    public GameObject minionCardPrefab; // Reference to the minion card prefab
    public Transform playerMinionArea; // Parent transform for player minions
    public Transform opponentMinionArea; // Parent transform for opponent minions

    public int cardSpacing = 1;
    
    public Hero playerHero;
    public Hero opponentHero;


    private void Awake() {
        playerMinions = new MinionCardData[maxMinions];
        opponentMinions = new MinionCardData[maxMinions];
    }

    public void AddMinionToBoard(MinionCardData minion, bool isPlayerSide) {
        MinionCardData[] side = isPlayerSide ? playerMinions : opponentMinions;
        Transform parentTransform = isPlayerSide ? playerMinionArea : opponentMinionArea;

        for (int i = 0; i < side.Length; i++) {
            if (side[i] == null) {
                side[i] = minion;
                // Instantiate the minion card prefab and set up its display
                GameObject minionObj = Instantiate(minionCardPrefab, parentTransform);
                CardDisplay cardDisplay = minionObj.GetComponent<CardDisplay>();
                cardDisplay.SetupCard(minion);

                // Position the minion on the board visually
                UpdateMinionPositions(isPlayerSide);
                return;
            }
        }
        Debug.Log("No more space on the board.");
    }

    public void MinionsAttack(bool isPlayerSide) {
        MinionCardData[] attackers = isPlayerSide ? playerMinions : opponentMinions;
        MinionCardData[] targetted = !isPlayerSide ? playerMinions : opponentMinions;
        Hero targetHero = !isPlayerSide ? playerHero : opponentHero;
        for (int i = 0; i < attackers.Length; i++) {
            if(attackers[i]==null) continue;
            
            MinionCardData attacker = attackers[i];
            if(targetted[i]!=null)
                attacker.Attack(targetted[i]);
            else {
                attacker.Attack(targetHero);
            }
        }
    }

    private void UpdateMinionPositions(bool isPlayerSide) {
        MinionCardData[] side = isPlayerSide ? playerMinions : opponentMinions;
        Transform parentTransform = isPlayerSide ? playerMinionArea : opponentMinionArea;

        for (int i = 0; i < side.Length; i++) {
            if (parentTransform.childCount > i) {
                Vector3 minionPos = new Vector3(i * cardSpacing, 0, 0);
                parentTransform.GetChild(i).localPosition = minionPos;
            }
        }
    }

    // Other methods to handle minion removal, attacking, etc.
}