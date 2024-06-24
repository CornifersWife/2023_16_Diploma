using CardBattles.CardScripts.CardDatas;
using UnityEngine;

namespace CardBattles.CardScripts {
    public class Spell : Card {
        public override void Initialize(CardData cardData,bool isPlayersCard) {
            base.Initialize(cardData,isPlayersCard);
            if (cardData is not SpellData spellData) {
                Debug.LogError("Invalid data type passed to Spell.Initialize");
            }
            else {
                cardDisplay.SetCardData(spellData);
            }
        }
    }
}