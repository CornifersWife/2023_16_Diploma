using UnityEngine;


public abstract class BaseCardData : ScriptableObject {
    //public int id;
    [HideInInspector]public int cost = 1;
    public string cardName;
    public Sprite cardImage;
    //effect?
    public object Owner { get; set; }
}

