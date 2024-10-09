using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    public abstract class CardData : ScriptableObject {
        
        [SerializeField]
        private string name;
        public string Name {
            get => name;
            set => name = value;
        }

        [SerializeField]
        private string description;
        public string Description {
            get => description;
            set => description = value;
        }

        [SerializeField]
        private HashSet<AdditionalProperty> properties = new HashSet<AdditionalProperty>();
        public HashSet<AdditionalProperty> Properties {
            get => properties;
            set => properties = value;
        }

        [SerializeField]
        private CardSet cardSet;
        public CardSet CardSet {
            get => cardSet;
            set => cardSet = value;
        }
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