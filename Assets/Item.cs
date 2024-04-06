using UnityEngine;

public class Item {
    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;

    public string GetName() {
        return name;
    }

    public Sprite GetSprite() {
        return sprite;
    }
}
