/*using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Scenes.Irys_is_doing_her_best.Scripts;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
[Obsolete]
public class BoardOld : MonoBehaviour {
    public int maxMinions = 5;

    public CardSpotOld[] playerMinions;
    public CardSpotOld[] enemyMinions;

    public GameObject cardSpot;
    public GameObject minionCardPrefab; //check if still needed
    public Transform playerMinionArea;
    public Transform opponentMinionArea;

    public float cardSpacing = 1;
    public float delayBetweenAttacks = 0.3f;

    [FormerlySerializedAs("playerHero")] public HeroOld playerHeroOld;

    [FormerlySerializedAs("opponentHero")] public HeroOld opponentHeroOld;


    private void Awake() {
        playerMinions = new CardSpotOld[maxMinions];
        enemyMinions = new CardSpotOld[maxMinions];
        GenerateBoardSpaces();
    }

    public void GenerateBoardSpaces() {
        GeneratePlayerBoardSpaces();
        GenerateEnemyBoardSpaces();
    }

    public void GenerateEnemyBoardSpaces() {
        for (int i = 0; i < maxMinions; i++) {
            GameObject cardSpotobj = Instantiate(cardSpot, opponentMinionArea);
            cardSpotobj.GetComponent<CardSpotOld>().isPlayers = false;
            cardSpotobj.transform.position += new Vector3(i * cardSpacing, 0, 0);
            cardSpotobj.name = "EnemyCardSpot_" + i;
            cardSpotobj.transform.rotation *= Quaternion.Euler(0, 180, 0);
            enemyMinions[i] = cardSpotobj.GetComponent<CardSpotOld>();
        }
    }

    public void GeneratePlayerBoardSpaces() {
        for (int i = 0; i < maxMinions; i++) {
            GameObject cardSpotobj = Instantiate(cardSpot, playerMinionArea);
            cardSpotobj.transform.position += new Vector3(i * cardSpacing, 0, 0);
            cardSpotobj.name = "PlayerCardSpot_" + i;
            playerMinions[i] = cardSpotobj.GetComponent<CardSpotOld>();
        }
    }

    public bool HasEmptySpace(bool isPlayerSide) {
        CardSpotOld[] boardSide = isPlayerSide ? playerMinions : enemyMinions;
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
        CardSpotOld[] attackers = isPlayerSide ? playerMinions : enemyMinions;
        CardSpotOld[] targetted = !isPlayerSide ? playerMinions : enemyMinions;
        HeroOld targetHeroOld = !isPlayerSide ? playerHeroOld : opponentHeroOld;
        for (int i = 0; i < attackers.Length; i++) {
            if (attackers[i].IsEmpty()) continue;
            /*yield return new WaitForSeconds(delayBetweenAttacks);
            try {
                var attacker = (MinionCardData)attackers[i].CardOld.cardData;
                if (targetted[i].IsEmpty()) {
                    attacker.Attack(targetHeroOld);
                    continue;
                }

                var target = (MinionCardData)targetted[i].CardOld.cardData;
                attacker.Attack(target);
            }
            catch (Exception e) {
                Debug.Log(e);
                
            }#1#
            
        }

        return null;
    }

    public List<IDamageable> GetMinons(bool isPLayerSide) {
        var tmp = isPLayerSide ? playerMinions : enemyMinions;
        List<IDamageable> tmpOutput = new List<IDamageable>();
        for (int i = 0; i < tmp.Length; i++) {
            if (tmp[i].IsEmpty()) continue;
            //tmpOutput.Add((MinionCardData)tmp[i].CardOld.cardData);
        }

        return null;
    }
}*/