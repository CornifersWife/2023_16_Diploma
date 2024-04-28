using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect {
    void ExecuteEffect(BaseCardData sourceCard);
}

public abstract class Effect : ScriptableObject, IEffect {
    public ITargetSelector targetSelector;
    public abstract void ExecuteEffect(BaseCardData sourceCard);
}


[CreateAssetMenu(fileName = "DamageEffect", menuName = "Effects/DamageEffect")]
public class DamageEffect : Effect {
    [SerializeField] private int damageAmount;

    public override void ExecuteEffect(BaseCardData sourceCard) {
        List<IDamageable> targets = targetSelector.GetTargets(sourceCard);
        foreach (var target in targets) {
            target.TakeDamage(damageAmount);
        }
    }

 
}