using UnityEngine;

public class Hero : MonoBehaviour, IDamageable {
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
        Debug.Log("Critical error hero died but nothing happened");
    }
}