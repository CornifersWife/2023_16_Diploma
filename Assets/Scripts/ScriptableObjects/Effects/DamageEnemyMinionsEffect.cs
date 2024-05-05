using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageAllEnemyMinions", menuName = "Effects/Damage/AllEnemyMinions")]
public class DamageEnemyMinionsEffect : Effect {
    public int damage = 1;

    public override void ExecuteEffect(BaseCardData sourceCard) {
        Debug.Log("execute eff");
        var targets = SetTargets(sourceCard);
        if (!targets.Any())
            return;
        foreach (var target in targets) {
            target.TakeDamage(damage);
        }
    }

    public override List<IDamageable> SetTargets(BaseCardData sourceCard) {
        var targets = GameManager.Instance.GetEnemyMinions(sourceCard.belongsToPlayer);
        return targets;
    }

    public string Display(List<IDamageable> xd) {
        var x = "";
        foreach (var e in xd) {
            x += e.ToString();
        }

        return x;
    }
}