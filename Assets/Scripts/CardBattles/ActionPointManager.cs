using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ActionPointManager : MonoBehaviour {
    public int maxAP = 3;
    public int currentAP;
    public TMP_Text APCount;

    private void Start() {
        currentAP = 0;
        APCount.text = "AP: " + currentAP;
    }

    
    public bool UseActionPoint() {
        if (CanUseAP()) {
            currentAP--;
            return true;
        }
        return false;
    }

    public bool CanUseAP() {
        return CanUseAP(1);
    }

    //here in case we want to add something that costs more or less
    public bool CanUseAP(int cost) {
        return currentAP >= cost;
    }

    public void StartRound() {
        currentAP = maxAP;
    }
}