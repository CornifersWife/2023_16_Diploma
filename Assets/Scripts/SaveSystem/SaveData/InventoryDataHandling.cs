using System.Collections.Generic;
using Esper.ESave;
using Items;
using UnityEngine;

namespace SaveSystem.SaveData {
    public class InventoryDataHandling:MonoBehaviour, ISavable {
        private List<ItemSlot> allItems;
        private List<ItemSlot> allCardSets;
        private List<ItemSlot> allDeckCardSets;

        private const string ItemSaveID = "Inventory items";
        private const string CardSetSaveID = "Inventory Card Set";
        private const string DeckSaveID = "Inventory deck";

        private void Start() {
            allItems = InventoryController.Instance.GetItemSlots();
            allCardSets = InventoryController.Instance.GetCardSetSlots();
            allDeckCardSets = InventoryController.Instance.GetDeckSlots();
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