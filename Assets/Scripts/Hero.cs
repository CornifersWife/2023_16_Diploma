using UnityEngine;

public class Hero : MonoBehaviour, IDamageable
{
    public int maxHealth = 20;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public int GetPower() {
        return 0;
    }
    private void Death()
    {
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}