using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
    public int maxHealth =20;
    public int currentHealth;
    private void Awake() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Death();
        }
    }
    public void Death() {
        GetComponent<Hero>().tag = "Dead";
    }
}
