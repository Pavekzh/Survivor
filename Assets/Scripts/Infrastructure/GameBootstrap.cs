using System;
using UnityEngine;

public class GameBootstrap:MonoBehaviour
{
    [Header("Systems")]    
    [SerializeField] private WaveSystem waveSystem;
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private AxesInputDetector axesInputDetector;
    [SerializeField] private Camera camera;
    [SerializeField] private CameraFollow cameraFollow;    
    [SerializeField] private BoxCollider2D moveBoundaries;
    [SerializeField] private Transform bulletsParent;
    [Header("Character")]
    [SerializeField] private Character character;
    [Header("Wave objects")]
    [SerializeField] private SpawnObjectFactory itemsFactory;
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private Transform enemiesParent;
    [SerializeField] private SpawnObject[] enemiesObjects;
    [SerializeField] private SpawnObject[] itemsObjects;

    private void Awake()
    {
        InitCharacter();
        InitAxesInput();
        InitCameraFollow();
        InitWaves();

        InitEnemyFactory();
        InitEnemies();
        InitItems();

    }

    private void InitAxesInput()
    {
        axesInputDetector.InitDepenpencies(character, camera);
    }

    private void InitCameraFollow()
    {
        cameraFollow.InitDependencies(moveBoundaries);
    }

    private void InitCharacter()
    {
        character.InitDependencies(inputDetector,moveBoundaries.bounds);
        Gun gun = character.GetComponent<Gun>();
        gun.InitDependencies(bulletsParent);
    }

    private void InitWaves()
    {
        waveSystem.InitDependecies(moveBoundaries.bounds, character);
    }

    private void InitEnemyFactory()
    {
        enemyFactory.InitDependencies(this);
    }

    private void InitEnemies()
    {
        foreach(SpawnObject enemy in enemiesObjects)
        {
            enemy.InitDependencies(enemyFactory,enemiesParent);
        }
    }

    private void InitItems()
    {
        foreach (SpawnObject item in itemsObjects)
        {
            item.InitDependencies(itemsFactory,itemsParent);
        }
    }

    public void InitEnemy(Enemy enemy)
    {
        enemy.InitDependecies(character, moveBoundaries.bounds,waveSystem);
        Gun gun = enemy.Weapon as Gun;
        if (gun != null)
            gun.InitDependencies(bulletsParent);
    }
}

