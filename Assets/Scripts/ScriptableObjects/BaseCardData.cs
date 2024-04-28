using UnityEngine;


public abstract class BaseCardData : ScriptableObject {
    [HideInInspector]public int cost = 1;
    public string cardName;
    public Sprite cardImage;
    [HideInInspector] public bool belongsToPlayer;

    public object GetCardSetCards() {
        throw new System.NotImplementedException();
    }
}

