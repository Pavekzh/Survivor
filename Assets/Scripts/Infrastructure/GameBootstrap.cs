using UnityEngine;
using Fusion;
using System;

public class GameBootstrap:MonoBehaviour
{
    public static GameBootstrap Instance { get; private set; }

    [Header("Systems")]
    [SerializeField] private PlayerFactory playerFactory;
    [SerializeField] private float playerSpawnRadius;
    [SerializeField] private PlayerUsername playerUsername;
    [SerializeField] private GunSelector gunSelector;
    [SerializeField] private ChoosePlayer choosePlayer;
    [SerializeField] private WaveSystem waveSystem;
    [SerializeField] private TargetDesignator targetDesignator;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private InputDetector mockInputDetector;
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private AxesInputDetector axesInputDetector;
    [SerializeField] private Camera camera;
    [SerializeField] private CameraFollow cameraFollow;    
    [SerializeField] private BoxCollider2D moveBoundaries;
    [SerializeField] private Transform bulletsParent;
    [Header("UI")]
    [SerializeField] private WaveSystemUI waveSystemUI;
    [Header("Wave objects")]
    [SerializeField] private WaveObjectFactory itemsFactory;
    [SerializeField] private WaveObjectFactory enemyFactory;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private Transform enemiesParent;
    [SerializeField] private SpawnObject[] enemiesObjects;
    [SerializeField] private SpawnObject[] itemsObjects;

    private Character character;
    private NetworkRunner network;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Scene has more than one GameBootstrap components");
        Instance = this;

        network = FindObjectOfType<NetworkRunner>();

        InitCharacter();
        InitGunSelector();
        InitAxesInput();
        InitCameraFollow();
        InitWaves();

        InitWavesUI();

        InitEnemies();
        InitItems();
    }

    private void InitCharacter()
    {
        character = playerFactory.Create(network, choosePlayer.GetChoosed(), playerSpawnRadius);
    }    
    
    private void InitGunSelector()
    {
        gunSelector.InitDependencies(character, bulletsParent);
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
        waveSystem.InitDependecies(moveBoundaries.bounds, character,scoreCounter,targetDesignator);
    }

    private void InitEnemies()
    {
        enemyFactory.InitDependencies(network);

        foreach (SpawnObject enemy in enemiesObjects)
        {
            enemy.InitDependencies(enemyFactory);
        }
    }

    private void InitItems()
    {
        itemsFactory.InitDependencies(network);

        foreach (SpawnObject item in itemsObjects)
        {
            item.InitDependencies(itemsFactory);
        }
    }

    public void InitItem(Item item)
    {
        item.InitDependencies(waveSystem,itemsParent);
    }

    public void InitEnemy(Enemy enemy)
    {
        enemy.InitDependecies(targetDesignator, moveBoundaries.bounds,waveSystem,scoreCounter,enemiesParent);
        enemy.Weapon.InitDependencies(enemy);
        Gun gun = enemy.Weapon as Gun;
        if (gun != null)
            gun.InitDependencies(bulletsParent,false);
    }

    public void InitCharacter(Character character)
    {
        targetDesignator.AddPlayer(character);

        if (this.character == null)
            character.InitDependencies(inputDetector, moveBoundaries.bounds,playerUsername.GetUsername());
        else
        {
            character.InitDependencies(mockInputDetector, moveBoundaries.bounds,"Player view");
            gunSelector.InstantiateRemotePlayerGun(character);
        }
    }

}

