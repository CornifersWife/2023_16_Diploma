using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MinionCard", menuName = "Card/Minion")]
public class MinionCardData : BaseCardData {
    public int power;
    public int currentHealth;
    public int maxHealth;

    private void Awake() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Death();
        }
    }
    public void Attack(MinionCardData target) {
        if (target != null) {
            target.TakeDamage(power);
            TakeDamage(target.power);
            return;
        }
    }

    public void Attack(Hero hero) {
        hero.TakeDamage(power);
    }

    public void Death() {
        
    }
}
