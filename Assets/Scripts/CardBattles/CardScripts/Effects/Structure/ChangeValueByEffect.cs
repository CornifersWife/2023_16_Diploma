using UnityEngine;
using UnityEngine.Serialization;

namespace CardBattles.CardScripts.Effects.Structure {
    public abstract class ChangeValueByEffect : EffectBase {
        [FormerlySerializedAs("Amount")]
        [Min(1)]
        [SerializeField]
        public int amount;
        
    }
}