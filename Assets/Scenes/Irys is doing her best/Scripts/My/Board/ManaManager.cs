using System;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class ManaManager : MonoBehaviour {
        public int MaxMana = 2;
        [Min(0)]
        public int CurrentMana;

        private void Awake() {
            CurrentMana = MaxMana;
        }

        public bool CanUseMana(IHasCost hasCost) {
            return CurrentMana >= hasCost.GetCost();
        }

        public void UseMana(IHasCost hasCost) {
            if (!CanUseMana(hasCost)) {
                Debug.LogException(new ArgumentException($"Not enough mana to play a card\ncost: {hasCost.GetCost()}   mana: {CurrentMana}"),(Object)hasCost);
            }

            CurrentMana -= 1;
        }
        
        
    }
}