using System.Collections.Generic;
using System.ComponentModel;
using Scenes.Irys_is_doing_her_best.Scripts.My.Enums;
using Scenes.Irys_is_doing_her_best.Scripts.My.Structure;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas {
    public abstract class CardData : ScriptableObject {
        
        [SerializeField] public string cardName;
        [TextArea]
        [SerializeField] public string description;
        [TextArea]
        [SerializeField] public string flavourText;

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