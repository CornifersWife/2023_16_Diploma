using System.Collections.Generic;

public class InventorySaveData : SaveData{
    [System.Serializable]
    public struct ItemData {
        public int index;
        public Item item;
    }

    public List<ItemData> itemDatas = new List<ItemData>();
    public List<ItemData> deckDatas = new List<ItemData>();
    public List<ItemData> cardSetDatas = new List<ItemData>();
}