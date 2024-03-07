using System;

using UnityEngine;


[CreateAssetMenu(fileName = "New MinionCard", menuName = "Card/Minion")]
public class MinionCardData : BaseCardData,IDamageable {
    public int power;
    public int currentHealth;
    public int maxHealth;
    public event Action<int> OnHealthChanged;
    public event Action<Vector3> OnAttack;
    public event Action<MinionCardData> OnDeath;

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth);
        if (!IsAlive()) {
            Death();
        }
    }

    public void Attack(MinionCardData target) {
        if (target is not null) {
            //OnAttack?.Invoke(target.transform.position);
            // target is minionCardData, which has no position
            
            //TODO: figure out how to check position of target
            
            target.TakeDamage(power);
            TakeDamage(target.power);
            return;
        }
    }

    public void Attack(Hero hero) {
        OnAttack?.Invoke(hero.transform.position);
        hero.TakeDamage(power);
    }

    public Vector3 GetPosition()
    {
        // This method needs to communicate with the MonoBehaviour that represents this card in the game world
        // Since ScriptableObjects do not have a transform, you might handle this with an event or a direct method call to the CardDisplay
        throw new System.NotImplementedException("GetPosition needs to be implemented through game object representation.");
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
    
    public void Death()
    {
        OnDeath?.Invoke(this);
    }

}
