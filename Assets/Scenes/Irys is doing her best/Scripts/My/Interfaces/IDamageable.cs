
namespace Scenes.Irys_is_doing_her_best.Scripts {
    public interface IDamageable {
        void TakeDamage(int amount);
        void Heal(int amount);
        void Die();
    }
}