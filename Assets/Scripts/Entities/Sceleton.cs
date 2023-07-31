using UnityEngine;

public class Sceleton:Enemy
{
    [SerializeField] private Gun weapon;

    public Gun Weapon { get => weapon; }

    public override float AttackRange => weapon.WeaponRange - rangeOffset;
    public override float ReloadTime => weapon.ReloadTime;

    public override Vector2 TargetDirection { get => weapon.AttackDirection; set => weapon.AttackDirection = value; }

    protected override void Attack()
    {
        Debug.LogError(gameObject.name + "Launch");
        weapon.Attack();
    }
}