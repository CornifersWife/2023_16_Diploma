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
    
    //Add minion on board by active toggle
    public bool AddMinionToBoard(MinionCardData minion, int toggleIndex, Transform toggle) {
        MinionCardData[] side = playerMinions;
        Transform parentTransform = playerMinionArea;
        
        if (side[toggleIndex] == null) {
            side[toggleIndex] = minion;
            // Instantiate the minion card prefab and set up its display
            GameObject minionObj = Instantiate(minionCardPrefab, parentTransform);
            CardDisplay cardDisplay = minionObj.GetComponent<CardDisplay>();
            cardDisplay.SetupCard(minion);

            // Position the minion on the board visually
            minionObj.transform.position = toggle.position + new Vector3(0, 1, 0);//display slightly above toggle
            return true;
        } 
        Debug.Log("Space is occupied");
        return false;
    }

    public void AddMinionToBoard(MinionCardData minion, bool isPlayerSide, int index) {
        MinionCardData[] side = isPlayerSide ? playerMinions : opponentMinions;
        Transform parentTransform = isPlayerSide ? playerMinionArea : opponentMinionArea;


        if (side[index] is null) {
            side[index] = minion;
            // Instantiate the minion card prefab and set up its display
            GameObject minionObj = Instantiate(minionCardPrefab, parentTransform);
            CardDisplay cardDisplay = minionObj.GetComponent<CardDisplay>();
            cardDisplay.SetupCard(minion);

            // Position the minion on the board visually
            UpdateMinionPositions(isPlayerSide);
            return;
        }

        Debug.Log("No more space on this spot.");
    }

    public bool HasEmptySpace(bool isPlayerSide) {
        MinionCardData[] boardSide = isPlayerSide ? playerMinions : opponentMinions;
        for (int i = 0; i < boardSide.Length; i++) {
            if (boardSide[i] is null)
                return true;
        }

        return false;
    }

    public void MinionsAttack(bool isPlayerSide) {
        MinionCardData[] attackers = isPlayerSide ? playerMinions : opponentMinions;
        MinionCardData[] targetted = !isPlayerSide ? playerMinions : opponentMinions;
        Hero targetHero = !isPlayerSide ? playerHero : opponentHero;
        for (int i = 0; i < attackers.Length; i++) {
            if (attackers[i] is null) continue;

            MinionCardData attacker = attackers[i];
            if (targetted[i] is null)
                attacker.Attack(targetted[i]);
            else {
                attacker.Attack(targetHero);
            }
        }
    }

    private void UpdateMinionPositions(bool isPlayerSide) {
        MinionCardData[] side = isPlayerSide ? playerMinions : opponentMinions;
        Transform parentTransform = isPlayerSide ? playerMinionArea : opponentMinionArea;

        int childIndex = 0;
        for (int i = 0; i < side.Length; i++) {
            if (side[i] is not null) {
                Vector3 minionPos = new Vector3(i * cardSpacing, 0, 0);
                if (parentTransform.childCount > childIndex) {
                    parentTransform.GetChild(childIndex).localPosition = minionPos;
                    childIndex++;
                }
            }
        }
    }

    // Other methods to handle minion removal, attacking, etc.
}