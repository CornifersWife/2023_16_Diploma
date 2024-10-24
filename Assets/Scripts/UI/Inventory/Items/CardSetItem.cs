
using CardBattles.CardScripts.CardDatas;
using UnityEngine;

public class CardSetItem : Item {
    [SerializeField] private CardSetData cardSetData;
    private bool unlocked;

    public CardSetData GetCardSetData() {
        return cardSetData;
    }
    
    public void SetCardSetData(CardSetData cardSetData) {
        this.cardSetData = cardSetData;
    }
}
