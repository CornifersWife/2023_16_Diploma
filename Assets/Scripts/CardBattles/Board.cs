using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Board : MonoBehaviour {
    public int maxMinions = 5;

    public CardSpot[] playerMinions;
    public CardSpot[] enemyMinions;

    public GameObject cardSpot;
    public GameObject minionCardPrefab; //check if still needed
    public Transform playerMinionArea;
    public Transform opponentMinionArea;

    public float cardSpacing = 1;
    public float delayBetweenAttacks = 0.3f;

    public Hero playerHero;

    public Hero opponentHero;


    private void Awake() {
        playerMinions = new CardSpot[maxMinions];
        enemyMinions = new CardSpot[maxMinions];
        GenerateBoardSpaces();
    }

    public void GenerateBoardSpaces() {
        GeneratePlayerBoardSpaces();
        GenerateEnemyBoardSpaces();
    }

    public void GenerateEnemyBoardSpaces() {
        for (int i = 0; i < maxMinions; i++) {
            GameObject cardSpotobj = Instantiate(cardSpot, opponentMinionArea);
            cardSpotobj.GetComponent<CardSpot>().isPlayers = false;
            cardSpotobj.transform.position += new Vector3(i * cardSpacing, 0, 0);
            cardSpotobj.name = "EnemyCardSpot_" + i;
            cardSpotobj.transform.rotation *= Quaternion.Euler(0, 180, 0);
            enemyMinions[i] = cardSpotobj.GetComponent<CardSpot>();
        }
    }

    public void GeneratePlayerBoardSpaces() {
        for (int i = 0; i < maxMinions; i++) {
            GameObject cardSpotobj = Instantiate(cardSpot, playerMinionArea);
            cardSpotobj.transform.position += new Vector3(i * cardSpacing, 0, 0);
            cardSpotobj.name = "PlayerCardSpot_" + i;
            playerMinions[i] = cardSpotobj.GetComponent<CardSpot>();
        }
    }

    public bool HasEmptySpace(bool isPlayerSide) {
        CardSpot[] boardSide = isPlayerSide ? playerMinions : enemyMinions;
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
        CardSpot[] attackers = isPlayerSide ? playerMinions : enemyMinions;
        CardSpot[] targetted = !isPlayerSide ? playerMinions : enemyMinions;
        Hero targetHero = !isPlayerSide ? playerHero : opponentHero;
        for (int i = 0; i < attackers.Length; i++) {
            if (attackers[i].IsEmpty()) continue;
            yield return new WaitForSeconds(delayBetweenAttacks);
            try {
                var attacker = (MinionCardData)attackers[i].Card.cardData;
                if (targetted[i].IsEmpty()) {
                    attacker.Attack(targetHero);
                    continue;
                }

                var target = (MinionCardData)targetted[i].Card.cardData;
                attacker.Attack(target);
            }
            catch (Exception e) {
                Debug.Log(e);
                
            }
            
        }
    }

    public List<IDamageable> GetMinons(bool isPLayerSide) {
        var tmp = isPLayerSide ? playerMinions : enemyMinions;
        List<IDamageable> tmpOutput = new List<IDamageable>();
        for (int i = 0; i < tmp.Length; i++) {
            if (tmp[i].IsEmpty()) continue;
            tmpOutput.Add((MinionCardData)tmp[i].Card.cardData);
        }

        return tmpOutput;
    }
}