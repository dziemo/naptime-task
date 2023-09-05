using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public event Action<int> OnDamageTaken;

    [SerializeField]
    private int maxHealth;

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    public void Initialize()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        OnDamageTaken?.Invoke(--currentHealth);
    }
}
