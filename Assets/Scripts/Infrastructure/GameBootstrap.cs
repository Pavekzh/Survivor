using UnityEngine;
using Fusion;


public class GameBootstrap:MonoBehaviour
{
    public static GameBootstrap Instance { get; private set; }

    [Header("Systems")]
    [SerializeField] private PlayerFactory playerFactory;
    [SerializeField] private float playerSpawnRadius;
    [SerializeField] private GunSelector gunSelector;
    [SerializeField] private ChoosePlayer choosePlayer;
    [SerializeField] private WaveSystem waveSystem;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private AxesInputDetector axesInputDetector;
    [SerializeField] private Camera camera;
    [SerializeField] private CameraFollow cameraFollow;    
    [SerializeField] private BoxCollider2D moveBoundaries;
    [SerializeField] private Transform bulletsParent;
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
    private NetworkRunner network;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Scene has more than one GameBootstrap components");
        Instance = this;

        network = FindObjectOfType<NetworkRunner>();

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
        character = playerFactory.Create(network, choosePlayer.GetChoosed(), playerSpawnRadius);

        GunSettings gunSetts = gunSelector.GetGun();
        Gun gun;

        character.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = gunSetts.Sprite;
        gun = gunSetts.InstantiateGun(character.gameObject);

        character.InitDependencies(inputDetector, moveBoundaries.bounds, gun);
        gun.InitDependencies(character);
        gun.InitDependencies(bulletsParent);
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

    public void InitCharacter(Character character)
    {
        GunSettings gunSetts = gunSelector.GetGun();
        Gun gun;

        character.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = gunSetts.Sprite;
        gun = gunSetts.InstantiateGun(character.gameObject);

        character.InitDependencies(null, moveBoundaries.bounds, gun);
        gun.InitDependencies(character);
        gun.InitDependencies(bulletsParent);
    }

}

