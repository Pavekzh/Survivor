﻿using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int bulletsPerShot = 3;
    [SerializeField] private float spread = 30;

    private float bulletsOffset { get => spread / bulletsPerShot; }

    public void InitSettings(ShotgunFactory settings)
    {
        this.bulletsPerShot = settings.BulletsPerShot;
        this.spread = settings.Spread;
    }

    public override void Attack()
    {
        if (!limitedMagazine || BulletsLeft > 0)
        {
            if (bulletsPerShot == 1 || spread == 0)
                LaunchBullet(attackDirection);
            else
            {
                Vector2 bulletDirection = GetRotatedDirection(attackDirection, -(spread / 2));
                LaunchBullet(bulletDirection);
                for (int i = 1; i < bulletsPerShot; i++)
                {                    
                    bulletDirection = GetRotatedDirection(bulletDirection, bulletsOffset);
                    LaunchBullet(bulletDirection);
                }

            }
            BulletsLeft--;
        }
    }

    protected Vector2 GetRotatedDirection(Vector2 direction, float angle)
    {
        Vector2 result;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        result = rotation * direction;

        return result;
    }
}