using System.Collections.Generic;
using CardBattles.Enums;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

namespace CardBattles.CardScripts.CardDatas {
    public abstract class CardData : ScriptableObject {
        
        [SerializeField] [HideInInspector] public CardSetData cardSet;

        [BoxGroup("Card")] [SerializeField] public string cardName;

        [BoxGroup("Card")] [SerializeField, ShowAssetPreview]
        public Sprite sprite;

        [BoxGroup("Card")] [TextArea] [SerializeField]
        public string description;

        [BoxGroup("Card")] [TextArea] [SerializeField]
        public string flavourText;

        [BoxGroup("Data")] [SerializeField] public List<AdditionalProperty> properties = new List<AdditionalProperty>();

        [BoxGroup("Data")] [SerializeField]
        private TriggerEffectDictionary effectDictionary = new TriggerEffectDictionary();

        public TriggerEffectDictionary EffectDictionary {
            get {
                var copyEffectDictionary = new TriggerEffectDictionary();
                foreach (var effectTrigger in effectDictionary.Keys) {
                    copyEffectDictionary.Add(effectTrigger, effectDictionary[effectTrigger].Copy());
                }

                return copyEffectDictionary;
            }

            set => effectDictionary = value;
        }
    };
}