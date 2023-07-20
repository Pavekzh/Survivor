using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;

    private float health;

    public float CurrentHealth { get => health; }

    private void Start()
    {
        if (maxHealth <= 0)
            Debug.LogError("Max health set to negative or zero");

        health = maxHealth;
    }    
    
    public void RecoverHealth()
    {
        this.health = maxHealth;
    }

    public void Heal(float points)
    {
        health += points;

        if (health > maxHealth)
            health = maxHealth;
    }

    public float TakeDamage(float damage)
    {
        float healthBefore = health;

        if (health > damage)
            health -= damage;
        else
            health = 0;

        float healthAfter = health;

        return healthBefore - healthAfter;
    }    
    

}

