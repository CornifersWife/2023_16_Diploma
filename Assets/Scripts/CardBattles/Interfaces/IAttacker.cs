namespace Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces {
    public interface IAttacker : IDamageable {
        public int GetAttack();
        public void ChangeAttackBy(int amount);
        public void AttackTarget(IDamageable target);
    }
}