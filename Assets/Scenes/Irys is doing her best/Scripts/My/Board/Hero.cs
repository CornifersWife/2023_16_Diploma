using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Board {
    public class Hero : MonoBehaviour,IDamageable {

        public Action<bool> death;
        
        [SerializeField]
        private int maxHealth = 30;

        public int MaxHealth {
            get => maxHealth;
            set {
                maxHealth = value;
                if(CurrentHealth>maxHealth)
                    CurrentHealth = value;
            }
        }

        [SerializeField]
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

        private bool isPlayers;

        private void Awake() {
            isPlayers = CompareTag("Player");
        }

        public void TakeDamage(int amount) {
            CurrentHealth -= amount;
        }

        public void Heal(int amount) {
            CurrentHealth += amount;
        }

        public void Die() {
            death.Invoke(isPlayers);
        }
    }
}