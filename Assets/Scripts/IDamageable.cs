using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int amount);
    Vector3 GetPosition(); // Method to get the position for targeting purposes
    bool IsAlive(); // Method to check if the entity is still alive
    int GetPower();
}