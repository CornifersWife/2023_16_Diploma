using System.Collections.Generic;
using UnityEngine;

namespace CardBattles.CardScripts.CardDatas {
    [CreateAssetMenu(fileName = "New CardSet", menuName = "Cards/CardSet")]
    public class CardSetData :ScriptableObject {
        public string displayName = "No Display Name";
        [HideInInspector]public List<CardData> cards = new List<CardData>();
        public Color setColor = Color.white; 
    }
}