using UnityEngine;
using Fusion;
using System;

public class GameBootstrap:MonoBehaviour
{
    public static GameBootstrap Instance { get; private set; }

    [Header("Systems")]
    [SerializeField] private WaveSystem waveSystem;
    [SerializeField] private TargetDesignator targetDesignator;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private LoseDetection loseDetection;    
    [SerializeField] private FusionLeave fusionLeave;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Camera camera;
    [SerializeField] private CameraFollow cameraFollow;    
    [SerializeField] private BoxCollider2D moveBoundaries;
    [SerializeField] private Transform bulletsParent;

    [Header("Character systems")]        
    [SerializeField] private InputDetector mockInputDetector;
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private AxesInputDetector axesInputDetector;
    [SerializeField] private PlayerFactory playerFactory;
    [SerializeField] private float playerSpawnRadius;
    [SerializeField] private PlayerUsername playerUsername;
    [SerializeField] private GunSelector gunSelector;
    [SerializeField] private ChoosePlayer choosePlayer;

    [Header("UI")]    
    [SerializeField] private GameUI gameUI;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private WaveSystemUI waveSystemUI;
    [SerializeField] private GameOverUI gameOverUI;

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
        InitLoseDetection();
        InitFusionLeave();

        InitWavesUI();
        InitGameOverUI();
        InitGameUI();
        InitPlayerUI();

        InitEnemies();
        InitItems();
    }

    private void InitCharacter()
    {
        character = playerFactory.Create(network, choosePlayer.GetChoosed(), playerSpawnRadius);
    }    
    
    private void InitGunSelector()
    {
        gunSelector.InitDependencies(character, this);
    }

    private void InitGameOverUI()
    {
        gameOverUI.InitDependencies(scoreCounter,fusionLeave);
    }

    private void InitWavesUI()
    {
        waveSystemUI.InitDependencies(waveSystem);
    }

    private void InitGameUI()
    {
        gameUI.InitDependencies(fusionLeave,character,scoreCounter);
    }

    private void InitPlayerUI()
    {
        playerUI.InitDependencies(character.Health);
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
        waveSystem.InitDependecies(moveBoundaries.bounds, character,targetDesignator,gameOverUI);
    }

    private void InitLoseDetection()
    {
        loseDetection.InitDependecies(character, gameOverUI, waveSystem);
    }

    private void InitFusionLeave()
    {
        network.AddCallbacks(fusionLeave);
        fusionLeave.InitDependencies(network, loseDetection, targetDesignator, waveSystem, sceneLoader);
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
    //Public Injectors, because of network spawn
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

    public void InitGunForCharacter(Character character,Gun gun,bool isCharacterRemote)
    {
        if (!isCharacterRemote)
            playerUI.InitDependencies(gun);

        gun.InitDependencies(character);
        gun.InitDependencies(bulletsParent, isCharacterRemote);
    }

}

