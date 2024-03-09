using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public int maxMinions = 5;
    public MinionCardData[] playerMinions;
    public MinionCardData[] opponentMinions;

    public GameObject cardSpot;
    public GameObject minionCardPrefab; // Reference to the minion card prefab
    public Transform playerMinionArea; // Parent transform for player minions
    public Transform opponentMinionArea; // Parent transform for opponent minions

    public float cardSpacing = 1;
    public float delayBetweenAttacks = 0.3f;

    public Hero playerHero;
    public Hero opponentHero;
    //TODO add board space prefab and change algorith generating spaces
    //public BoardSpace boardSpace;
    public static int indentifier = 1;


    private void Awake() {
        playerMinions = new MinionCardData[maxMinions];
        opponentMinions = new MinionCardData[maxMinions];
        GenerateBoardSpaces();
    }

    public void GenerateBoardSpaces() {
        for (int i = 0; i < maxMinions; i++) {
            GameObject cardSpotobj = Instantiate(cardSpot, playerMinionArea);
            cardSpotobj.transform.position += new Vector3(i * cardSpacing, 0, 0);
        }
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
                GameManager.Instance.RegisterMinionDisplay(minion, cardDisplay);
                // Position the minion on the board visually
                UpdateMinionPositions(isPlayerSide);
                return;
            }
        }
        Debug.Log("No more space on the board.");
    }

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
            minionObj.transform.position = toggle.position + new Vector3(0, 1, 0); //display slightly above toggle
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
            GameObject minionObj = Instantiate(minionCardPrefab, parentTransform);
            minionObj.name = "Minion_" + index.ToString(); // Unique identifier
            CardDisplay cardDisplay = minionObj.GetComponent<CardDisplay>();
            cardDisplay.SetupCard(minion);

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
        StartCoroutine(AttackCoroutine(isPlayerSide));
    }

    private IEnumerator AttackCoroutine(bool isPlayerSide) {
        MinionCardData[] attackers = isPlayerSide ? playerMinions : opponentMinions;
        MinionCardData[] targetted = !isPlayerSide ? playerMinions : opponentMinions;
        Hero targetHero = !isPlayerSide ? playerHero : opponentHero;
        for (int i = 0; i < attackers.Length; i++) {
            if (attackers[i] is null) continue;
            yield return new WaitForSeconds(delayBetweenAttacks);
            MinionCardData attacker = attackers[i];
            if (targetted[i] is not null)
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
            if (side[i] is not null) {
                Transform minionTransform = parentTransform.Find("Minion_" + i.ToString());
                if (minionTransform is not null) {
                    Vector3 minionPos = new Vector3(i * cardSpacing, 0, 0);
                    minionTransform.localPosition = minionPos;
                }
            }
        }
    }


    // Other methods to handle minion removal, attacking, etc.
}