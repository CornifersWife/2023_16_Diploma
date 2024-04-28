using System.Collections.Generic;

public interface ITargetSelector {
    List<IDamageable> GetTargets(BaseCardData sourceCard);
}
public class AllTargetsSelector : ITargetSelector {
    public List<IDamageable> GetTargets(BaseCardData sourceCard) {
        return GameManager.Instance.GetAllDamageable();
    }
}

public class AllEnemiesSelector : ITargetSelector {
    public List<IDamageable> GetTargets(BaseCardData sourceCard) {
        return GameManager.Instance.GetEnemies(sourceCard.Owner);
    }
}

public class EnemyMinionsSelector : ITargetSelector {
    public List<IDamageable> GetTargets(BaseCardData sourceCard) {
        return GameManager.Instance.GetEnemyMinions(sourceCard.Owner);
    }
}

public class YourMinionsSelector : ITargetSelector {
    public List<IDamageable> GetTargets(BaseCardData sourceCard) {
        return GameManager.Instance.GetYourMinions(sourceCard.Owner);
    }
}

public class SameCardSetSelector : ITargetSelector {
    public List<IDamageable> GetTargets(BaseCardData sourceCard) {
        return GameManager.Instance.GetMinionsInSameSet(sourceCard);
    }
}

public class YourHeroSelector : ITargetSelector {
    public List<IDamageable> GetTargets(BaseCardData sourceCard) {
        return new List<IDamageable> { GameManager.Instance.GetYourHero(sourceCard.Owner) };
    }
}

public class BothHeroesSelector : ITargetSelector {
    public List<IDamageable> GetTargets(BaseCardData sourceCard) {
        return GameManager.Instance.GetAllHeroes();
    }
}
