using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
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

[CreateAssetMenu(fileName = "SilenceEnemyMinions", menuName = "Effects/Silence/EnemyMinions")]
public class DamageAllMinionsEffect : Effect {
    public int damage = 1;

    public override void ExecuteEffect(BaseCardData sourceCard) {
        Debug.Log("Executing Silence Enemy Minions EffectTargetPair");
        var targets = SetTargets(sourceCard);
        foreach (var target in targets) {
            target.TakeDamage(damage);
        }
    }

    public override List<IDamageable> SetTargets(BaseCardData sourceCard) {
        return GameManager.Instance.GetEnemyMinions(sourceCard.belongsToPlayer);
    }
}

[CreateAssetMenu(fileName = "HealAllAllies", menuName = "Effects/Heal/AllAllies")]
public class HealAllAlliesEffect : Effect {
    public int healAmount = 2;

    public override void ExecuteEffect(BaseCardData sourceCard) {
        Debug.Log("Executing Heal All Allies EffectTargetPair");
        var targets = SetTargets(sourceCard);
        foreach (var target in targets) {
            target.Heal(healAmount);
        }
    }

    public override List<IDamageable> SetTargets(BaseCardData sourceCard) {
        return GameManager.Instance.GetAllies(sourceCard.belongsToPlayer);
    }
}

[CreateAssetMenu(fileName = "DamageAllHeroes", menuName = "Effects/Damage/AllHeroes")]
public class DamageEnemyHeroEffect : Effect {
    public int damage = 3;

    public override void ExecuteEffect(BaseCardData sourceCard) {
        Debug.Log("Executing Damage All Heroes EffectTargetPair");
        var targets = SetTargets(sourceCard);
        foreach (var target in targets) {
            target.TakeDamage(damage);
        }
    }

    public override List<IDamageable> SetTargets(BaseCardData sourceCard) {
        return new List<IDamageable>() { GameManager.Instance.GetEnemyHero(sourceCard) };
    }
}