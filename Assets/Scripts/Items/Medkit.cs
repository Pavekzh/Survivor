using UnityEngine;

public class Medkit : Item
{
    [SerializeField] private float points;

    protected override void Execute(Collider2D founder)
    {
        Health health = founder.GetComponent<Health>();

        if (health != null)
            health.Heal(points);
        else
            Debug.LogError("Object that found medkit must have Health component");
    }
}