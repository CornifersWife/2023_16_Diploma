using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My.Structure;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas {
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