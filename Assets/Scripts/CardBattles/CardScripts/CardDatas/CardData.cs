using System.Collections.Generic;
using CardBattles.CardScripts.Effects.Structure;
using CardBattles.Enums;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardBattles.CardScripts.CardDatas {
    public abstract class CardData : ScriptableObject {
        
        [SerializeField] public string cardName;
        [TextArea]
        [SerializeField] public string description;
        [TextArea]
        [SerializeField] public string flavourText;

        [SerializeField]
        [EnumFlags]
        public List<AdditionalProperty> properties = new List<AdditionalProperty>();
        
        [SerializeField]
        private Sprite sprite; 
        public Sprite Sprite {
            get => sprite;
            set => sprite = value;
        }

        [SerializeField]
        private List<EffectTargetPair> onPlayEffects = new List<EffectTargetPair>();
        public List<EffectTargetPair> OnPlayEffects {
            get => onPlayEffects;
            set => onPlayEffects = value;
        }
    }
}