using System;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class ManaManager : MonoBehaviour {
        private ManaDisplay manaDisplay;

        [SerializeField]
        public int maxMana = 2;

        [Min(0)]
        private int currentMana;
        
        private int CurrentMana {
            get {
                manaDisplay.CurrentMana = currentMana;
                return currentMana;
            }
            set {
                manaDisplay.CurrentMana = value;
                currentMana = value;
            }
        }

        private void Awake() {
            manaDisplay = GetComponentInChildren<ManaDisplay>();
            manaDisplay.UpdateMaxMana(maxMana);
            CurrentMana = maxMana;
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

        public void RefreshMana() {
            CurrentMana = maxMana;
        }        
        private void ShowLackOfMana() {
            StartCoroutine(manaDisplay.ShowLackOfMana());
        }
        
    }
}