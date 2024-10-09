using NaughtyAttributes;
using UnityEngine;

namespace CardBattles.Interfaces.InterfaceObjects {
    // ReSharper disable once InconsistentNaming
    public class PlayerEnemyMonoBehaviour : MonoBehaviour {
        
        public void SetTag(bool isPlayers) {
            tag = isPlayers ? "Player" : "Enemy";
        }
        private bool IsPlayersMethod() {
            return CompareTag("Player");
        }
        [ShowNativeProperty] public bool IsPlayers => IsPlayersMethod();

    }
}