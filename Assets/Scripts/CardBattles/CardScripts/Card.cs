using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CardBattles.CardScripts.Additional;
using CardBattles.CardScripts.CardDatas;
using CardBattles.CardScripts.Effects.Structure;
using CardBattles.Enums;
using CardBattles.Interfaces;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;

namespace CardBattles.CardScripts {
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public abstract class Card : MonoBehaviour, IHasCost {
        public CardDisplay cardDisplay;
        public CardAnimation cardAnimation;
        public CardDragging cardDragging;

        public string CardName { get; set; }
        public string Description { get; private set; }
        public string FlavourText { get; private set; }
        public List<AdditionalProperty> properties;
        public CardSet CardSet { get; private set; }
        public List<EffectTargetPair> OnPlayEffects { get; private set; }

        [CanBeNull] protected CardSpot isPlacedAt = null;
        [NonSerialized] public bool isPlayers;

        [ShowNativeProperty] private bool IsPlayers => isPlayers;


        private void Awake() {
            cardDisplay = GetComponent<CardDisplay>();
            cardAnimation = GetComponent<CardAnimation>();
            cardDragging = GetComponent<CardDragging>();
        }

        public virtual void Initialize(CardData cardData, bool isPlayersCard) {
            this.isPlayers = isPlayersCard;
            cardAnimation.isPlayers = isPlayersCard;
            CardName = cardData.cardName;
            name = CardName;
            FlavourText = cardData.flavourText;
            Description = cardData.description;
            properties = cardData.properties;
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
            cardDragging.enabled = true;
            cardDisplay.IsDrawn();
        }

        public void AssignCardSpot(CardSpot cardSpot) {
            isPlacedAt = cardSpot;
            cardDisplay.IsInPlay(cardSpot is not null);
        }
        
        
        public IEnumerator Move(Vector3 vector3) {
            yield return StartCoroutine(cardAnimation.MoveTo(vector3));
        }

        public IEnumerator DrawAnimation(Vector3 finalPosition) {
            yield return cardAnimation.DrawAnimation(finalPosition);
        }
    }
}