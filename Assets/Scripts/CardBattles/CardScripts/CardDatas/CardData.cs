using System;
using System.Collections.Generic;
using CardBattles.Enums;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardBattles.CardScripts.CardDatas {
    [Serializable]
    public abstract class CardData : ScriptableObject {
        [BoxGroup("Card")] [SerializeField, ShowAssetPreview]
        public Sprite sprite;

        [BoxGroup("Card")] [SerializeField] public string cardName;

        [BoxGroup("Card")] [TextArea] [SerializeField]
        public string description;

        [BoxGroup("Card")] [TextArea] [SerializeField]
        public string flavourText;

        [BoxGroup("Data")] [SerializeField] 
        public List<AdditionalProperty> properties = new List<AdditionalProperty>();

        [BoxGroup("Data")] [SerializeField]
        public TriggerEffectDictionary effectDictionary = new TriggerEffectDictionary();
    }
}