using System;
using UnityEngine;

public abstract class Damager:MonoBehaviour
{
    [SerializeField] float damage;

    public float Damage { get => damage; }

}
