using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My.Structure;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My {
    [CreateAssetMenu(fileName = "Change Attack By Effect",menuName = "Effects/Change Attack By Effect")]
    public class ChangeAttackByEffect : ChangeValueByEffect {
        public override void ApplyEffect(ICollection<GameObject> targets) {
            throw new System.NotImplementedException();
        }
    }
}