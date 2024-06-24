using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CardBattles.CardScripts.Additional;
using CardBattles.CardScripts.CardDatas;
using CardBattles.CardScripts.Effects;
using CardBattles.Enums;
using CardBattles.Interfaces;
using CardBattles.Managers;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;

namespace CardBattles.CardScripts {
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public abstract class Card : MonoBehaviour, IHasCost {
        [NonSerialized] private EffectManager.EffectDelegate effectDelegate;

        [NonSerialized] public CardDisplay cardDisplay;
        [NonSerialized] public CardAnimation cardAnimation;
        [NonSerialized] public CardDragging cardDragging;

        [BoxGroup("Card")] public string cardName;
        [BoxGroup("Card"), ResizableTextArea] public string description;
        [BoxGroup("Card"), ResizableTextArea] public string flavourText;
        [BoxGroup("Data")] public List<AdditionalProperty> properties;

        [NonSerialized] public CardSet cardSet;

        [BoxGroup("Data")] public TriggerEffectDictionary effectDictionary;


        [CanBeNull] protected CardSpot isPlacedAt = null;

        [Foldout("Additional")] [ShowNonSerializedField] [NonSerialized]
        public bool isPlayers;


        private bool HasCardSet() => cardSet is not null;

        private bool HasCardSpot() => isPlacedAt is not null;

        private void Awake() {
            cardDisplay = GetComponent<CardDisplay>();
            cardAnimation = GetComponent<CardAnimation>();
            cardDragging = GetComponent<CardDragging>();
        }

        public virtual void Initialize(CardData cardData, bool isPlayersCard) {
            isPlayers = isPlayersCard;
            cardAnimation.isPlayers = isPlayersCard;
            cardName = cardData.cardName;
            name = cardName;
            flavourText = cardData.flavourText;
            description = cardData.description;
            properties = cardData.properties;
            effectDictionary = cardData.effectDictionary;
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

        public void DoEffect(EffectTrigger effectTrigger) {
            EffectTargetValue efv;
            if (!effectDictionary.TryGetValue(effectTrigger, out var value))
                return;
            efv = value;
            var targets = GetTargets(efv.targetType);
            EffectManager.effectDictionary[efv.effect](targets, efv.value);
        }

        private List<GameObject> GetTargets(TargetType targetType) {
            return BoardManager.Instance.GetTargets(targetType, this);
        }
    }
}