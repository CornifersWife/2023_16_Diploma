using System.Collections;
using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using Scenes.Irys_is_doing_her_best.Scripts.My.Enums;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using Scenes.Irys_is_doing_her_best.Scripts.My.Structure;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public abstract class Card : MonoBehaviour,IHasCost,IMoveable {
        public CardDisplay cardDisplay;
        public CardAnimation cardAnimation;
        public CardDragging cardDragging;
        
        public string CardName { get; private set; }
        public string Description { get; private set; }
        public string FlavourText { get; private set; }
        public List<AdditionalProperty> properties;
        public CardSet CardSet { get; private set; }
        public List<EffectTargetPair> OnPlayEffects { get; private set; }

        public bool isPlayers;

        private void Awake() {
            cardDisplay = GetComponent<CardDisplay>();
            cardAnimation = GetComponent<CardAnimation>();
            cardDragging = GetComponent<CardDragging>();
        }
        
        public virtual void Initialize(CardData cardData, bool isPlayers) {
            this.isPlayers = isPlayers;
            cardAnimation.isPlayers = isPlayers;
            CardName = cardData.cardName;
            name = CardName;
            FlavourText = cardData.flavourText;
            Description = cardData.description;
            properties = cardData.Properties;
            OnPlayEffects = cardData.OnPlayEffects;
        }

        public virtual int GetCost() {
            if (properties.Contains(AdditionalProperty.FreeToPlay)) {
                return 0;
            }
            return 1;
        }

        

        //TODO CHANGE ITS NAME
        public void IsDrawn() {
            cardDisplay.ChangeCardVisible(isPlayers);
            cardDragging.enabled = true;
        }

        public IEnumerator Move(Vector3 vector3) {
            yield return StartCoroutine(cardAnimation.MoveTo(vector3));
        }
        public IEnumerator DrawAnimation(Vector3 finalPosition) {
            yield return cardAnimation.DrawAnimation(finalPosition);
        }
        
    }
}