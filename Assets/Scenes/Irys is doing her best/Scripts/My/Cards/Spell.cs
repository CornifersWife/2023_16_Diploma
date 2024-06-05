using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class Spell : Card {
        public override void Initialize(CardData cardData,bool isPlayer) {
            base.Initialize(cardData,isPlayer);
            if (cardData is not SpellData spellData) {
                Debug.LogError("Invalid data type passed to Spell.Initialize");
            }
            else {
                cardDisplay.SetCardData(spellData);
            }
        }
    }
}