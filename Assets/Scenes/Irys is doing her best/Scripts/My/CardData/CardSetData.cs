using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My;
using ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardOld Set", menuName = "Cards/CardSet")]
public class CardSetData :ScriptableObject {
    public string displayName = "nodisplaynameset";
    [HideInInspector]public List<CardData> cards;
    public Color setColor = Color.white; 
}