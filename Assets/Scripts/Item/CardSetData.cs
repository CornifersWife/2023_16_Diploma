using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Set", menuName = "Cards/CardSet")]
public class CardSetData :ScriptableObject {
    public List<BaseCardData> cards;
    public Color setColor = Color.white; 

}