using UnityEngine;

public class Item: MonoBehaviour {
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;

    public string GetName() {
        return itemName;
    }

    public Sprite GetSprite() {
        return sprite;
    }
}
