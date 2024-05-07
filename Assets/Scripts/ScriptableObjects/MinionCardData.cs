using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "New MinionCard", menuName = "Cards/Minion")]
public class MinionCardData : BaseCardData, IDamageable, IActivatable {
    public int power;
    [HideInInspector] public int currentHealth;
    public int maxHealth;
    public event Action<int> OnHealthChanged;
    public event Action<IDamageable> OnAttack;
    public event Action<MinionCardData> OnDeath;
    [SerializeField] private bool hasDivineShield = false;


    public delegate Vector3
        RequestPositionDelegate(); //TODO added by chat-bot, need to think if its needed and if it is i need to understand why

    public event RequestPositionDelegate OnRequestPosition;
    
    public List<Effect> onPlayEffects;
    public List<Effect> onActivationEffects;
    public List<Effect> onDeathEffects;


    public bool HasDivineShield {
        get { return hasDivineShield; }
        set {
            //TODO add animation
            hasDivineShield = value;
        }
    }

    public void TakeDamage(int amount) {
        if (HasDivineShield) {
            HasDivineShield = false;
            return;
        }

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

    public void Heal(int healAmount) {
        throw new NotImplementedException();
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

    public override void Play() {
        if (onPlayEffects.Any())
            foreach (var effect in onPlayEffects) {
                effect.ExecuteEffect(this);
            }
    }

    public void Activate() {
        if (onPlayEffects.Any())
            foreach (var effect in onActivationEffects) {
                effect.ExecuteEffect(this);
            }
    }

    private void Death() {
        OnDeath?.Invoke(this);
        if (onDeathEffects.Any())
            foreach (var effect in onDeathEffects) {
                effect.ExecuteEffect(this);
            }
    }
}