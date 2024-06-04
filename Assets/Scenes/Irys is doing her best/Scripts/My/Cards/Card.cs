using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My.Card;
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using Scenes.Irys_is_doing_her_best.Scripts.My.Enums;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public abstract class Card : MonoBehaviour {
        protected CardDisplay cardDisplay;

        public string CardName { get; private set; }
        public string Description { get; private set; }
        public HashSet<AdditionalProperty> Properties { get; private set; }
        public CardSet CardSet { get; private set; }
        public List<EffectTargetPair> OnPlayEffects { get; private set; }

        private void Awake() {
            cardDisplay = GetComponent<CardDisplay>();
        }
        public virtual void Initialize(CardData cardData) {
            CardName = cardData.cardName;
            this.name = CardName;
            Description = cardData.description;
            Properties = cardData.Properties;
            OnPlayEffects = cardData.OnPlayEffects;
        }
    }
}