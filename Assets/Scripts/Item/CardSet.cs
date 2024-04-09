
using UnityEngine;

public class CardSet : Item {
    [SerializeField]private CardSetData cards;
    private bool unlocked;

    public CardSetData GetCardSetData() {
        return cards;
    }
}
