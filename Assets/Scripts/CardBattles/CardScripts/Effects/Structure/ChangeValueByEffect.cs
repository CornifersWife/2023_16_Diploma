using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Structure {
    public abstract class ChangeValueByEffect : EffectBase {
        [FormerlySerializedAs("Amount")]
        [Min(1)]
        [SerializeField]
        public int amount;
        
    }
}