using System.Collections;
using UnityEngine;

public class Hero : MonoBehaviour, IDamageable
{
    public int maxHealth = 20;
    public int currentHealth;
    public float moveDistance = 0.5f;
    public float animationDuration = 0.1f;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        StartCoroutine(AnimateTakingDamage());
        if (currentHealth <= 0)
        {
            Death();
        }
        
    }

    public IEnumerator AnimateTakingDamage() {
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition - new Vector3(0, 0, moveDistance); 

        float elapsedTime = 0;
        while (elapsedTime < animationDuration / 2) {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / (animationDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0;

        while (elapsedTime < animationDuration / 2) {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, (elapsedTime / (animationDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
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