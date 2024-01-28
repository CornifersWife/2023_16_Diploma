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
    

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Death();
        }
        //event wyslij informacje o aktualizacji zycia do displaycard
    }
    public void Attack(MinionCardData target) {
        if (target is not null) {
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
