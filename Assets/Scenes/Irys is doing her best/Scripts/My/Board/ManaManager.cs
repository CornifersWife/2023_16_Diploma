using System;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class ManaManager : MonoBehaviour {
       [SerializeField]
        public int maxMana = 2;

        [Min(0)]
        private int currentMana;
        
        public int CurrentMana {
            get {
                manaDisplay.currentMana = currentMana;
                return currentMana;
            }
            set {
                manaDisplay.currentMana = value;
                currentMana = value;
            }
        }

        private ManaDisplay manaDisplay;
        private void Awake() {
            CurrentMana = maxMana;
            manaDisplay = GetComponent<ManaDisplay>();
        }

        private bool CanUseMana(IHasCost cost) {
            return CurrentMana >= cost.GetCost();
        }

        private void UseMana(IHasCost cost) {
            if (!CanUseMana(cost)) {
                Debug.LogException(new ArgumentException($"Not enough mana to play a card\ncost: {cost.GetCost()}   mana: {currentMana}"),(Object)cost);
            }
            CurrentMana -= 1;
        }

        public bool TryUseMana(IHasCost cost) {
            if (!CanUseMana(cost)) {
                ShowLackOfMana();
                return false;
            }
            UseMana(cost);
            return true;
        }

        private void ShowLackOfMana() {
            manaDisplay.ShowLackOfMana();
        }
        
    }
}