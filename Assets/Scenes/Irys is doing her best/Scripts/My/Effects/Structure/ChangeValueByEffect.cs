using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Structure {
    public abstract class ChangeValueByEffect : EffectBase {
        [Min(1)]
        [SerializeField] public int amount;
    }
}