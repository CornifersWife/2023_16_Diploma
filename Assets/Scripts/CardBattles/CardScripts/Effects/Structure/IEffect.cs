using System.Collections.Generic;
using UnityEngine;

namespace CardBattles.CardScripts.Effects.Structure {
    public interface IEffect {
        void ApplyEffect(ICollection<GameObject> targets);
    }
}