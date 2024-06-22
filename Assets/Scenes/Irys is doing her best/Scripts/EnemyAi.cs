using System;
using Scenes.Irys_is_doing_her_best.Scripts.My.Board;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts {
    public class EnemyAi : MonoBehaviour {
        private CharacterManager character;
        private void Awake() {
            character = GetComponent<CharacterManager>();
        }
        
        
    }
}
