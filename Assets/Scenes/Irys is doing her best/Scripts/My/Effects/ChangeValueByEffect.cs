using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    public abstract class ChangeValueByEffect : EffectBase {
        [Range(1,int.MaxValue)]
        [SerializeField] public int amount;
    }
}