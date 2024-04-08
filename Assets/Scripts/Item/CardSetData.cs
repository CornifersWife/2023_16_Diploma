using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Set", menuName = "Cards/CardSet")]
public class CardSetData :ScriptableObject {
    public string displayName = "nodisplaynameset";
    [HideInInspector]public List<BaseCardData> cards;
    public Color setColor = Color.white; 

}