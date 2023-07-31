using UnityEngine;

public enum AttackMode
{
    Nearest,
    MultiTarget,
    UndefinedSingle
}

public class MeleeWeapon : MonoBehaviour,IWeapon
{
    [SerializeField] private float weaponRange;
    [SerializeField] private float reloadTime;
    [SerializeField] private float damage;
    [SerializeField] private AttackMode mode;
    [SerializeField] private LayerMask targetLayers;

    private Vector2 attackDirection;

    public float ReloadTime => reloadTime;
    public float WeaponRange => weaponRange;
    public Vector2 AttackDirection { get => attackDirection; set => attackDirection = value; }


    private void MakeDamageSingle(Collider2D target)
    {
        IDamageHandler handler = target.gameObject.GetComponent<IDamageHandler>();
        if (handler != null)
            handler.HandleDamage(damage,"");
    }

    private void MakeDamageMulti(Collider2D[] targets)
    {
        foreach (Collider2D target in targets)
            MakeDamageSingle(target);

    }

    public void Attack()
    {
        Collider2D[] hurtColliders = Physics2D.OverlapCircleAll(transform.position, WeaponRange, targetLayers);

        if (hurtColliders.Length == 0)
            return;

        if (mode == AttackMode.MultiTarget)
            MakeDamageMulti(hurtColliders);
        else if (mode == AttackMode.UndefinedSingle)
            MakeDamageSingle(hurtColliders[0]);
        else
        {
            float minDistance = (hurtColliders[0].transform.position - transform.position).magnitude;
            int nearestIndex = 0;

            for (int i = 1; i < hurtColliders.Length; i++)
            {
                float distance = (hurtColliders[i].transform.position - transform.position).magnitude;
                if (distance < minDistance)
                    nearestIndex = i;
            }

            MakeDamageSingle(hurtColliders[nearestIndex]);

        }

    }
}