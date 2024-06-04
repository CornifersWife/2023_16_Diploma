using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My.Enums;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas {
    public abstract class CardData : ScriptableObject {
        
        [SerializeField] public string cardName;
        [SerializeField] public string description;
        

        private HashSet<AdditionalProperty> properties = new HashSet<AdditionalProperty>();
        public HashSet<AdditionalProperty> Properties {
            get => properties;
            set => properties = value;
        }

        /*[SerializeField]
        private CardSet cardSet;
        public CardSet CardSet {
            get => cardSet;
            set => cardSet = value;
        }*/
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