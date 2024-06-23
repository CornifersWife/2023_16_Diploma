/*using System;
using Scenes.Irys_is_doing_her_best.Scripts;
using Scenes.Irys_is_doing_her_best.Scripts.My.Interfaces;
using UnityEngine;
[Obsolete]
public class HeroOld : MonoBehaviour{
    public int maxHealth = 20;
    public int currentHealth;
    private void Awake() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if (!IsAlive()) {
            Death();
        }
    }

    public int GetPower() {
        return 0;
    }

    public void Heal(int healAmount) {
        throw new System.NotImplementedException();
    }

    public void Die() {
        throw new System.NotImplementedException();
    }

    public void HighlightAsPossibleTarget(bool on) {
        throw new System.NotImplementedException();
    }


    public Vector3 GetPosition() {
        return transform.position;
    }

    public bool IsAlive() {
        return currentHealth > 0;
    }

    private void Death() {
        if (CompareTag("Player")) {
            GameManager.Instance.Lose();
            return;
        }

        if (CompareTag("Enemy")) {
            GameManager.Instance.Win();
            return;
        }
        Debug.Log("Critical error heroOld died but nothing happened");
    }
}*/