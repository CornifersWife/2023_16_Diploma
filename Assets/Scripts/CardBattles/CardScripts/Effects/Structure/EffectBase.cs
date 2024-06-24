using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardBattles.CardScripts.Effects.Structure {
    [Serializable]
    public abstract class EffectBase : ScriptableObject, IEffect {
        public abstract void ApplyEffect(ICollection<GameObject> targets);
    }
}