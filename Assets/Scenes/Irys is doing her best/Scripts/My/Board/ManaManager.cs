using System;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class ManaManager : MonoBehaviour {
        [SerializeField]
        public int MaxMana = 2;
        [Min(0)]
        public int currentMana;

        private void Awake() {
            currentMana = MaxMana;
        }

        private bool CanUseMana(IHasCost cost) {
            return currentMana >= cost.GetCost();
        }

        private void UseMana(IHasCost cost) {
            if (!CanUseMana(cost)) {
                Debug.LogException(new ArgumentException($"Not enough mana to play a card\ncost: {cost.GetCost()}   mana: {currentMana}"),(Object)cost);
            }

            currentMana -= 1;
        }

        public bool TryUseMana(IHasCost cost) {
            if (!CanUseMana(cost)) {
                ShowLackOfMana();
                return false;
            }
            UseMana(cost);
            return true;
        }
        public void ShowLackOfMana() {
            //TODO
        }
        
    }
}