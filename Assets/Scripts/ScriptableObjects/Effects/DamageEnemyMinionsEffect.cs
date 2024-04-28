using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageAllEnemyMinions", menuName = "Effects/Damage/AllEnemyMinions")]
public class DamageEnemyMinionsEffect : Effect {
    public int damage = 1;

    public override void ExecuteEffect(BaseCardData sourceCard) {
        var tmp = SetTargets(sourceCard);
        if (!tmp.Any())
            return;
        foreach (var target in tmp) {
            target.TakeDamage(damage);
        }
    }

    public override List<IDamageable> SetTargets(BaseCardData sourceCard) {
        var targets = GameManager.Instance.GetEnemyMinions(sourceCard.belongsToPlayer);
        Debug.Log(sourceCard + "    "+sourceCard.belongsToPlayer +"\n" + Display(targets));
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