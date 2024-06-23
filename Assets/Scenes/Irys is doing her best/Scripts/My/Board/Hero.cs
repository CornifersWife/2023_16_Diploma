using System;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
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

        public Action<int> currentHealthAction;
        public Action takeDamageAction;

        
        [SerializeField]
        
        private int currentHealth;

        private int CurrentHealth {
            get => currentHealth;
            set {
                currentHealth = value > MaxHealth ? MaxHealth : value;
                
                currentHealthAction.Invoke(currentHealth);
                if (currentHealth <= 0) {
                    Die();
                }
            }
        }

        public bool HasFullHp => CurrentHealth == MaxHealth;

        
        private bool isPlayers;

        private void Awake() {
            isPlayers = CompareTag("Player");
        }

        public void TakeDamage(int amount) {
            CurrentHealth -= amount;
            takeDamageAction.Invoke();
        }

        public void Heal(int amount) {
            CurrentHealth += amount;
        }

        public void Die() {
            death.Invoke(isPlayers);
        }

        public Vector3 GetPosition() {
            return transform.position;
        }
    }
}