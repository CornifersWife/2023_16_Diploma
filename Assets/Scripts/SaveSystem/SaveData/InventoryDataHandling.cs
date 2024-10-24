using System.Collections.Generic;
using System.Linq;
using Esper.ESave;
using Items;
using UnityEngine;

namespace SaveSystem.SaveData {
    public class InventoryDataHandling:MonoBehaviour, ISavable {
        [SerializeField] private GameObject itemList;
        [SerializeField] private GameObject deckList;
        [SerializeField] private GameObject cardSetList;
        
        private List<ItemSlot> allItems = new List<ItemSlot>();
        private List<ItemSlot> allCardSets = new List<ItemSlot>();
        private List<ItemSlot> allDeckCardSets = new List<ItemSlot>();

        private const string ItemSaveID = "Inventory items";
        private const string CardSetSaveID = "Inventory Card Set";
        private const string DeckSaveID = "Inventory deck";

        private void Awake() {
            IEnumerable<ItemSlot> objects = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<ItemSlot>();
            List<ItemSlot> allItemSlots = new List<ItemSlot>(objects);

            foreach (ItemSlot itemSlot in allItemSlots) {
                if (itemSlot.GetParentName() == itemList.name) {
                    allItems.Add(itemSlot);
                }
                else if (itemSlot.GetParentName() == deckList.name) {
                    allDeckCardSets.Add(itemSlot);
                }
                else {
                    allCardSets.Add(itemSlot);
                }
            }
        }
        
        
        public void PopulateSaveData(SaveFile saveFile) {
            saveFile.AddOrUpdateData(ItemSaveID, ItemSaveID);
            foreach (ItemSlot itemSlot in allItems) {
                string itemSlotID = itemSlot.GetID();
                
                if (saveFile.HasData(itemSlotID))
                    saveFile.DeleteData(itemSlotID);
                saveFile.AddOrUpdateData(itemSlotID, itemSlot.IsOccupied());
                
                if (!itemSlot.IsOccupied()) {
                    continue;
                }
                
                CollectibleItem item = (CollectibleItem)itemSlot.GetItem();
                string itemName = item.GetName();
                Sprite itemSprite = item.GetSprite();
                CollectibleItemData itemData = item.GetItemData();
                string id = itemData.itemID;
                
                saveFile.AddOrUpdateData(itemSlotID + "_item", id);
                saveFile.AddOrUpdateData(id + "_name", itemName);
                //saveFile.AddOrUpdateData(id + "_sprite", itemSprite);
            }
        }

        public void LoadSaveData(SaveFile saveFile) {
            if(!saveFile.HasData(ItemSaveID))
                return;
            
            foreach (ItemSlot itemSlot in allItems) {
                string itemSlotID = itemSlot.GetID();
                itemSlot.SetIsOccupied(saveFile.GetData<bool>(itemSlotID));

                if (itemSlot.IsOccupied()) {
                    CollectibleItemData itemData = ScriptableObject.CreateInstance<CollectibleItemData>();
                    string id = saveFile.GetData<string>(itemSlotID + "_item");
                    itemData.itemID = id;
                    GameObject itemObject = new GameObject();
                    itemObject.AddComponent<DraggableItem>();
                    CollectibleItem item = itemObject.AddComponent<CollectibleItem>();
                    item.SetItemData(itemData);
                    item.SetName(saveFile.GetData<string>(id + "_name"));
                    //item.SetSprite(saveFile.GetData<Sprite>(id + "_sprite"));
                    
                    itemSlot.AddItem(item);
                }
            }
        }
    }
}