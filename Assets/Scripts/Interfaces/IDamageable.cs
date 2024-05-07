using UnityEngine;

public interface IDamageable {
    void TakeDamage(int amount);
    Vector3 GetPosition(); 
    bool IsAlive(); 
    int GetPower();

    void Heal(int healAmount);
}