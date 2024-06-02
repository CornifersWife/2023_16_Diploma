using System.Collections.Generic;
using UnityEngine;

public class InventorySaveData : SaveData{
    [System.Serializable]
    public struct ItemData {
        public int index;
        public string name;
        public string image;
    }
    
    public struct CardSetItemData {
        public int index;
        public string name;
        public string image;
        public string color;
        public int[] cardCosts;
        public string[] cardNames;
        public bool[] cardBelongsToPlayer;
        public string[] cardImages;
    }

    public List<ItemData> itemDatas = new List<ItemData>();
    public List<CardSetItemData> deckDatas = new List<CardSetItemData>();
    public List<CardSetItemData> cardSetDatas = new List<CardSetItemData>();
}