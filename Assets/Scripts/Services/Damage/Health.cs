using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;

    private float health;

    public float MaxHealth { get => maxHealth; }

    public event Action<float> ChangedCurrentHalth;

    public float CurrentHealth 
    { 
        get => health; 
        private set
        {
            health = value;
            ChangedCurrentHalth?.Invoke(health);
        }
    }

    private void Start()
    {
        if (maxHealth <= 0)
            Debug.LogError("Max health set to negative or zero");

        CurrentHealth = maxHealth;
    }    
    
    public void RecoverHealth()
    {
        this.CurrentHealth = maxHealth;
    }

    public void Heal(float points)
    {
        CurrentHealth += points;

        if (CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;
    }

    public float TakeDamage(float damage)
    {
        float healthBefore = CurrentHealth;

        if (CurrentHealth > damage)
            CurrentHealth -= damage;
        else
            CurrentHealth = 0;

        float healthAfter = CurrentHealth;

        return healthBefore - healthAfter;
    }    
    

}

