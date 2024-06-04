
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using UnityEngine;

public class CardSet : Item {
    [SerializeField] private CardSetData cards;
    private bool unlocked;

    public CardSetData GetCardSetData() {
        return cards;
    }
}
