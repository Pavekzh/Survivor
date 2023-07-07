using System;
using UnityEngine;

public class Health:MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] int weaponLayer;

    private float health;

    public float CurrentHealth { get => health; }

    private void Start()
    {
        if (maxHealth <= 0)
            Debug.LogError("Max health set to negative or zero");

        health = maxHealth;
    }

    public bool IsDamager(GameObject obj)
    {
        return obj.layer == weaponLayer;
    }

    public void TakeDamage(Damager damager)
    {
        float damage = damager.Damage;

        if (health > damage)
            health -= damage;
        else
            health = 0;
    }
}

