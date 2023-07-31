using UnityEngine;

public interface IWeapon
{

    float ReloadTime { get; }
    float WeaponRange { get; }
    Vector2 AttackDirection { get; }

    void Attack();
}

