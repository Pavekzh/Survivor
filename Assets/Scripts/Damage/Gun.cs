using UnityEngine;
using UnityEngine.Pool;

public class Gun : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int defaultPoolSize = 10;
    [SerializeField] private int magazineSize = 15;

    private int bulletsLeft;
    private bool limitedMagazine = true;

    private Transform bulletsParent;

    ObjectPool<Bullet> bulletsPool;

    public void InitDependencies(Transform bulletsParent)
    {
        this.bulletsParent = bulletsParent;
    }

    private void Start()
    {
        if (bulletPrefab.GetComponent<Bullet>() == null)
            Debug.LogError("Bullet prefab must have Bullet component");

        if (magazineSize == -1)
            limitedMagazine = false;
        else
            bulletsLeft = magazineSize;

        bulletsPool = new ObjectPool<Bullet>(() => Instantiate(bulletPrefab, bulletsParent).GetComponent<Bullet>(), bullet => bullet.gameObject.SetActive(true), bullet => bullet.gameObject.SetActive(false), null, true, defaultPoolSize);
    }

    protected override void Attack(Vector2 direction)
    {
        if(!limitedMagazine || bulletsLeft > 0)
        {
            Bullet bullet = bulletsPool.Get();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            bullet.Launch(direction, bulletsPool,WeaponRange);

            bulletsLeft--;
        }
    }
}

