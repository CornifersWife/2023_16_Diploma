using System;
using System.Runtime.CompilerServices;
using UnityEngine;


[CreateAssetMenu(fileName = "New MinionCard", menuName = "Card/Minion")]
public class MinionCardData : BaseCardData, IDamageable {
    public int power;
    public int currentHealth;
    public int maxHealth;
    public event Action<int> OnHealthChanged;
    public event Action<IDamageable> OnAttack;
    public event Action<MinionCardData> OnDeath;
    public delegate Vector3 RequestPositionDelegate();
    public event RequestPositionDelegate OnRequestPosition;


    public MinionCardData Clone() {
        var clone = ScriptableObject.CreateInstance<MinionCardData>();
        clone.power = this.power;
        clone.currentHealth = this.currentHealth;
        clone.maxHealth = this.maxHealth;
        clone.id = id;
        clone.cost = cost;
        clone.cardName = cardName;
        clone.cardImage = cardImage;
        // Copy over other necessary data here
        return clone;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth);
        if (!IsAlive()) {
            Death();
        }
    }

    public void Attack(IDamageable target) {
        if (target is not null) {
            OnAttack?.Invoke(target);
            target.TakeDamage(power);
            TakeDamage(target.GetPower());
        }
    }


    public int GetPower() {
        return power;
    }

    public Vector3 GetPosition() {
        if (OnRequestPosition != null) {
            return OnRequestPosition.Invoke();
        }
        else {
            Debug.Log("no information of position of target");
            return Vector3.zero;
        }
    }

    public bool IsAlive() {
        return currentHealth > 0;
    }

    public void Death() {
        OnDeath?.Invoke(this);
    }
}