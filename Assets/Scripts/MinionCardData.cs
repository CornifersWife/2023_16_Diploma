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
    public void Attack(GameObject target) {
        MinionCardData targetMinion = target.GetComponent<MinionCardData>();
        if (targetMinion != null) {
            targetMinion.TakeDamage(power);
            TakeDamage(targetMinion.power);
            return;
        }
        Hero targetHero = target.GetComponent<Hero>();
        if (targetHero != null) {
            targetHero.TakeDamage(power);
            return;
        }
    }

    public void Death() {
        
    }
}
