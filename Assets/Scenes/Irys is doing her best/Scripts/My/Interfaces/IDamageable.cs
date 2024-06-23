
using UnityEngine;

namespace Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces {
    public interface IDamageable {
        public void TakeDamage(int amount);
        public void Heal(int amount);
        public void Die();
        public Vector3 GetPosition();
    }
}