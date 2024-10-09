namespace CardBattles.Interfaces {
    public interface IAttacker : IDamageable {
        public int GetAttack();
        public void ChangeAttackBy(int amount);
        public void AttackTarget(IDamageable target);
    }
}