using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    public abstract class EffectBase : ScriptableObject, IEffect {
        public abstract void ApplyEffect(ICollection<GameObject> targets);
    }
}