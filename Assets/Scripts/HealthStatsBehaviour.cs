using System;
using Interfaces;
using UnityEngine;

public class HealthStatsBehaviour : MonoBehaviour, IHealView
{
    public event Action<float, float> OnHealthChangeEvent;
    public event Action OnDeathEvent;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    private bool _isDeath;
        
    public void SpendHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - value, 0, MaxHealth);
        OnHealthChangeEvent?.Invoke(CurrentHealth, MaxHealth);
            
        if (CurrentHealth <= 0) Death();
    }
 
    public void ReplenishHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);
        OnHealthChangeEvent?.Invoke(CurrentHealth, MaxHealth);
    }
 
    public void SetMaxHealth(float value)
    {
        MaxHealth = value;
        CurrentHealth = MaxHealth;
        OnHealthChangeEvent?.Invoke(CurrentHealth, MaxHealth);
    }
 
    private void Death()
    {
        if (_isDeath) return;
        _isDeath = true;
        OnDeathEvent?.Invoke();
    }
}