using System;
using UnityEngine;

public class GameBootstrap:MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private WaveSystem waveSystem;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private AxesInputDetector axesInputDetector;
    [SerializeField] private Camera camera;
    [SerializeField] private BoxCollider2D moveBoundaries;
    [SerializeField] private Transform bulletsParent;
    [Header("Player")]    
    [SerializeField] private PlayerFactory playerFactory;
    [SerializeField] private GunSelector gunSelector;
    [SerializeField] private ChoosePlayer choosePlayer;
    [SerializeField] private CameraFollow cameraFollow;  
    [Header("UI")]
    [SerializeField] private WaveSystemUI waveSystemUI;
    [Header("Wave objects")]
    [SerializeField] private SpawnObjectFactory itemsFactory;
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private Transform enemiesParent;
    [SerializeField] private SpawnObject[] enemiesObjects;
    [SerializeField] private SpawnObject[] itemsObjects;

    private Character character;

    private void Awake()
    {
        InitCharacter();
        InitAxesInput();
        InitCameraFollow();
        InitWaves();

        InitWavesUI();

        InitEnemyFactory();
        InitEnemies();
        InitItems();
    }

    private void InitCharacter()
    {
        Gun gun;
        if (playerFactory.InstantiatePlayer(choosePlayer.GetChoosed(), gunSelector.GetGun(), out character, out gun))
        {
            character.InitDependencies(inputDetector, moveBoundaries.bounds, gun);
            gun.InitDependencies(character);
            gun.InitDependencies(bulletsParent);
        }
        else
            Debug.LogError("There was an error instantiating player");

    }

    private void InitWavesUI()
    {
        waveSystemUI.InitDependencies(waveSystem);
    }

    private void InitAxesInput()
    {
        axesInputDetector.InitDepenpencies(character, camera);
    }

    private void InitCameraFollow()
    {
        cameraFollow.InitDependencies(character.transform,moveBoundaries);
    }

    private void InitWaves()
    {
        waveSystem.InitDependecies(moveBoundaries.bounds, character,scoreCounter);
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
        enemy.InitDependecies(character, moveBoundaries.bounds,waveSystem,scoreCounter);
        enemy.Weapon.InitDependencies(enemy);
        Gun gun = enemy.Weapon as Gun;
        if (gun != null)
            gun.InitDependencies(bulletsParent);
    }
}

