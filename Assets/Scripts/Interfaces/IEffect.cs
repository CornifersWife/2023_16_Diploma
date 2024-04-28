using System.Collections.Generic;
using UnityEngine;

public interface IEffect {
    void ExecuteEffect(BaseCardData sourceCard);
}

public abstract class Effect : ScriptableObject, IEffect {
    public abstract void ExecuteEffect(BaseCardData sourceCard);
    public abstract List<IDamageable> SetTargets(BaseCardData sourceCard);
}

