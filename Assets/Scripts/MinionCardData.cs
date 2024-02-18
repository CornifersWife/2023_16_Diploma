using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "New MinionCard", menuName = "Card/Minion")]
public class MinionCardData : BaseCardData {
    public int power;
    public int currentHealth;
    public int maxHealth;
    public event Action<int> OnHealthChanged;
    public event Action<Vector3> OnAttack;
    

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0) {
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

    public void Death() {
        
    }
}
