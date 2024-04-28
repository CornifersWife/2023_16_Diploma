using System.Collections.Generic;
using UnityEngine;

public interface IEffect {
    void ExecuteEffect(BaseCardData sourceCard);
}

public abstract class Effect : IEffect {
    protected List<GameObject> targets;
    public abstract void ExecuteEffect(BaseCardData sourceCard);
    public abstract void SetTargets(BaseCardData sourceCard);
}

