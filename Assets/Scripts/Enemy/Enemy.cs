using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DeckManager enemyDeckManager;
    public HandManager enemyHandManager;
    public CardPositionManager cardPositionManager;

    private Dictionary<Vector3, bool> positionAvailabilityMap;

    public void PlayCard()
    {
        //move this to card position manager if possible
        //------------------------------------------------
        positionAvailabilityMap = new Dictionary<Vector3, bool>();

        for (int i = 0; i < cardPositionManager._opponentPositions.Count; i++)
        {
            positionAvailabilityMap.Add(cardPositionManager._opponentPositions[i], true);
        }
        //------------------------------------------------
        
        System.Random random = new System.Random();
        if (enemyHandManager.hand.Count != 0)
        {
            //take random index
            int index = random.Next(0, enemyHandManager.hand.Count);
            CardDisplay playedCard = enemyHandManager.hand[index];
            enemyHandManager.hand.Remove(enemyHandManager.hand[index]);
            
            List<Vector3> aSpots = AvailableSpots();
            //check available spots
            if (aSpots.Count != 0)
            {
                //take random position on board
                Vector3 pos = aSpots[random.Next(0, aSpots.Count())];
                playedCard.transform.position = pos;

                positionAvailabilityMap[pos] = false;
            }
        }
    }

    private List<Vector3> AvailableSpots()
    {
        List<Vector3> res = new List<Vector3>();
        foreach (Vector3 v in positionAvailabilityMap.Keys)
        {
            if (positionAvailabilityMap[v])
            {
                res.Add(v);
            }
        }
        return res;
    }
}
