using System;
using UnityEngine;
[Obsolete]
public class ActionPointManager : MonoBehaviour {
    public int maxAP  = 3;
    public int CurrentAP { get; private set; }
    public event Action OnAPChanged;

    private void Start() {
        CurrentAP = 0;
        OnAPChanged?.Invoke(); 
    }

    
    public void UseActionPoint() {
        if (CanUseAP()) {
            CurrentAP -= 1;
            OnAPChanged?.Invoke(); // Invoke event after AP changes
            return;
        }
        Debug.Log("CANT USE AP");
    }

    public bool CanUseAP() {
        return CanUseAP(1);
    }

    public bool CanUseAP(int cost) {
        return CurrentAP >= cost;
    }

    public void StartRound() {
        CurrentAP = maxAP;
        OnAPChanged?.Invoke();
    }
}