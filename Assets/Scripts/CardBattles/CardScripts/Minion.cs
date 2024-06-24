using System;
using CardBattles.CardScripts.CardDatas;
using CardBattles.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace CardBattles.CardScripts {
    public class Minion : Card, IAttacker {
        [Space(30), Header("Minion")] 
        [BoxGroup("Data")]
        [ShowNonSerializedField]
        private int attack;

        private int Attack {
            get => attack;
            set {
                if (value < 0) {
                    attack = 0;
                    return;
                }
                attack = value;
            }
        }

        [ShowNonSerializedField]
        [BoxGroup("Data")]
        private int maxHealth;

        private int MaxHealth {
            get => maxHealth;
            set {
                maxHealth = value;
                if(CurrentHealth>maxHealth)
                   CurrentHealth = value;
            }
        }


        [ShowNonSerializedField]
        [BoxGroup("Data")]
        private int currentHealth;

        private int CurrentHealth {
            get => currentHealth;
            set {
                currentHealth = value > MaxHealth ? MaxHealth : value;

                if (currentHealth <= 0) {
                    Die();
                }
            }
        }

        public override void Initialize(CardData cardData,bool isPlayersCard) {
            base.Initialize(cardData,isPlayersCard);
            
            if (cardData is not MinionData minionData) {
                Debug.LogError("Invalid data type passed to Minion.Initialize");
            }
            else {
                Attack = minionData.Attack;
                MaxHealth = minionData.MaxHealth;
                CurrentHealth = MaxHealth;
                cardDisplay.SetCardData(minionData);
            }
        }

        public Action<Vector3, IDamageable> action;
        public int GetAttack() => Attack;
        public void ChangeAttackBy(int amount) => Attack += amount;
        
        

        public void TakeDamage(int amount) {
            string debugout = $"{name}: {currentHealth}";
            amount = amount > 0 ? amount : 0;
            CurrentHealth -= amount;
            Debug.Log(debugout + $"--> {currentHealth}");
        }

        public void Heal(int amount) {
            amount = amount > 0 ? amount : 0;
            CurrentHealth += amount;
        }
        
        public void Die() {
            Destroy(gameObject);
        }

        public Transform GetTransform() {
            return transform;
        }

        public void AttackTarget(IDamageable target) {
            StartCoroutine(
                cardAnimation.AttackAnimation(
                    this, target));
        }

        private void OnDestroy() {
            if (isPlacedAt is not null) {
                if (isPlacedAt.card is not null) {
                    isPlacedAt.card = null;
                }

                isPlacedAt = null;
            }
        }
    }
}