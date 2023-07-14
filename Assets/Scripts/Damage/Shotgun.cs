using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int bulletsPerShot = 3;
    [SerializeField] private float spread = 30;

    private float bulletsOffset { get => spread / bulletsPerShot; }

    public void InitSettings(ShotgunSettings settings)
    {
        this.bulletsPerShot = settings.BulletsPerShot;
        this.spread = settings.Spread;
    }

    protected override void Attack(Vector2 direction)
    {
        if (!limitedMagazine || bulletsLeft > 0)
        {
            if (bulletsPerShot == 1 || spread == 0)
                LaunchBullet(direction);
            else
            {
                Vector2 bulletDirection = GetRotatedDirection(direction, -(spread / 2));
                LaunchBullet(bulletDirection);
                for (int i = 1; i < bulletsPerShot; i++)
                {                    
                    bulletDirection = GetRotatedDirection(bulletDirection, bulletsOffset);
                    LaunchBullet(bulletDirection);
                }

            }
            bulletsLeft--;
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