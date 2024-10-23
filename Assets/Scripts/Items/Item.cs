using Events;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public abstract class Item: MonoBehaviour {
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;

    public string GetName() {
        return itemName;
    }

    public Sprite GetSprite() {
        return sprite;
    }
    
    public void SetName(string name) {
        itemName = name;
    }

    public void SetSprite(Sprite sprite) {
        this.sprite = sprite;
    }
}
