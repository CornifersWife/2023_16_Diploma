using Events;
using QuestSystem;
using UnityEngine;

public class CollectItemWithIdQuestStep: QuestStep {
    [SerializeField] private string itemToCollectId;
    
    private void OnEnable() {
        GameEventsManager.Instance.ItemEvents.OnItemWithIdCollected += ItemCollected;
    }

    private void OnDisable() {
        GameEventsManager.Instance.ItemEvents.OnItemWithIdCollected -= ItemCollected;
    }

    private void ItemCollected(string itemId) {
        if (itemId == itemToCollectId) {
            FinishQuestStep(); 
        }
    }
}