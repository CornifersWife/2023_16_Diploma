using System.Collections.Generic;
using Scenes.Irys_is_doing_her_best.Scripts.My.CardDatas;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using Scenes.Irys_is_doing_her_best.Scripts.My.Structure;
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Cards {
    public class Minion : Card, IAttacker {
        private int attack;

        public int Attack {
            get => attack;
            set {
                if (value < 0)
                    attack = 0;
                attack = 0;
            }
        }

        private int maxHealth;

        public int MaxHealth {
            get => maxHealth;
            set {
                maxHealth = value;
                if(CurrentHealth>maxHealth)
                   CurrentHealth = value;
            }
        }


        private int currentHealth;

        public int CurrentHealth {
            get => currentHealth;
            set {
                currentHealth = value > MaxHealth ? MaxHealth : value;

                if (currentHealth <= 0) {
                    Die();
                }
            }
        }

        public List<EffectTargetPair> OnDeathEffects { get; private set; }

        

        public override void Initialize(CardData cardData) {
            base.Initialize(cardData);
            
            if (cardData is not MinionData minionData) {
                Debug.LogError("Invalid data type passed to Minion.Initialize");
            }
            else {
                Attack = minionData.Attack;
                MaxHealth = minionData.MaxHealth;
                CurrentHealth = MaxHealth;
                OnDeathEffects = minionData.OnDeathEffects;
                cardDisplay.SetCardData(minionData);
            }
        }

        public int GetAttack() => Attack;
        public void ChangeAttackBy(int amount) => Attack += amount;

        public void TakeDamage(int amount) {
            amount = amount > 0 ? amount : 0;
            CurrentHealth -= amount;
        }

        public void Heal(int amount) {
            amount = amount > 0 ? amount : 0;
            CurrentHealth += amount;
        }

        public void Die() {
            // Handle death logic
        }
    }
}