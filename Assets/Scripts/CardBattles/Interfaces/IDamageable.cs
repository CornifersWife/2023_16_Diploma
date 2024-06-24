
using UnityEngine;

namespace CardBattles.Interfaces {
    public interface IDamageable {
        public void TakeDamage(int amount);
        public void Heal(int amount);
        public void Die();
        public Transform GetTransform();
    }
}