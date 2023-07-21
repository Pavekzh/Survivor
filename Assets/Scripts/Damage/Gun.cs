using System;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : Weapon
{
    [SerializeField] protected float damage = 10;
    [SerializeField] protected int magazineSize = 15;
    [SerializeField] protected GameObject defaultBulletPrefab;
    [SerializeField] protected GameObject blankBulletPrefab;
    [SerializeField] protected int defaultPoolSize = 10;

    private int bulletsLeft;
    protected bool limitedMagazine = true;
    protected bool useBlankBullets;

    public event Action<int> ChangedBulletsLeft;

    protected GameObject bulletPrefab
    {
        get
        {
            if (useBlankBullets)
                return blankBulletPrefab;
            else
                return defaultBulletPrefab;
        }
    }

    protected int BulletsLeft 
    { 
        get => bulletsLeft; 
        set
        {
            bulletsLeft = value;             
            ChangedBulletsLeft?.Invoke(bulletsLeft);
        }

    }

    protected Transform bulletsParent;

    protected ObjectPool<Bullet> bulletsPool;

    public void InitSettings(GunFactory settings)
    {
        this.reloadTime = settings.ReloadTime;
        this.weaponRange = settings.WeaponRange;
        this.damage = settings.Damage;
        this.magazineSize = settings.MagazineSize;
        this.defaultBulletPrefab = settings.BulletPrefab;
        this.blankBulletPrefab = settings.BlankBulletPrefab;
        this.defaultPoolSize = settings.DefaultPoolSize;

    }

    public void InitDependencies(Transform bulletsParent,bool useBlankBullets)
    {
        this.bulletsParent = bulletsParent;
        this.useBlankBullets = useBlankBullets;
    }

    public override void RecoverWeapon()
    {
        base.RecoverWeapon();
        BulletsLeft = magazineSize;
    }

    public void AddMagazine()
    {
        BulletsLeft += magazineSize;
    }

    public void AddBullets(int amount)
    {
        BulletsLeft += amount;
    }

    private void Start()
    {

        if (bulletPrefab.GetComponent<Bullet>() == null)
            Debug.LogError("Bullet prefab must have Bullet component");

        if (magazineSize == -1)
            limitedMagazine = false;
        else
            BulletsLeft = magazineSize;

        bulletsPool = new ObjectPool<Bullet>(() => Instantiate(bulletPrefab, bulletsParent).GetComponent<Bullet>(), bullet => bullet.gameObject.SetActive(true), bullet => bullet.gameObject.SetActive(false), null, false, defaultPoolSize);
    }

    protected override void Attack(Vector2 direction)
    {
        if(!limitedMagazine || BulletsLeft > 0)
        {
            LaunchBullet(direction);
            BulletsLeft--;
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

