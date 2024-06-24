using System.Collections.Generic;
using CardBattles.CardScripts.Effects.Structure;
using UnityEngine;

namespace CardBattles.CardScripts.CardDatas {
    public class MinionData : CardData {
        [Min(0)] public int Attack;

        [Min(1)] public int MaxHealth = 1;

        [SerializeField] private List<EffectTargetPair> onDeathEffects = new List<EffectTargetPair>();

        public List<EffectTargetPair> OnDeathEffects {
            get => onDeathEffects;
            set => onDeathEffects = value;
        }
    }
}