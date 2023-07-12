using UnityEngine;
using UnityEngine.Pool;

public class Gun : Weapon
{
    [SerializeField] protected float damage = 10;
    [SerializeField] protected int magazineSize = 15;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected int defaultPoolSize = 10;

    protected int bulletsLeft;
    protected bool limitedMagazine = true;

    protected Transform bulletsParent;

    protected ObjectPool<Bullet> bulletsPool;

    public void InitDependencies(Transform bulletsParent)
    {
        this.bulletsParent = bulletsParent;
    }

    public override void RecoverWeapon()
    {
        base.RecoverWeapon();
        bulletsLeft = magazineSize;
    }

    public void AddMagazine()
    {
        bulletsLeft += magazineSize;
    }

    public void AddBullets(int amount)
    {
        bulletsLeft += amount;
    }

    private void Start()
    {
        if (bulletPrefab.GetComponent<Bullet>() == null)
            Debug.LogError("Bullet prefab must have Bullet component");

        if (magazineSize == -1)
            limitedMagazine = false;
        else
            bulletsLeft = magazineSize;

        bulletsPool = new ObjectPool<Bullet>(() => Instantiate(bulletPrefab, bulletsParent).GetComponent<Bullet>(), bullet => bullet.gameObject.SetActive(true), bullet => bullet.gameObject.SetActive(false), null, false, defaultPoolSize);
    }

    protected override void Attack(Vector2 direction)
    {
        if(!limitedMagazine || bulletsLeft > 0)
        {
            LaunchBullet(direction);
            bulletsLeft--;
        }
    }
    
    protected void LaunchBullet(Vector2 direction)
    {
        Bullet bullet = bulletsPool.Get();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        bullet.Launch(direction, owner.ID, bulletsPool, damage, WeaponRange);
    }
}

