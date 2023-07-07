using UnityEngine;

class GameBootstrap:MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private AxesInputDetector axesInputDetector;
    [SerializeField] private Camera camera;
    [SerializeField] private CameraFollow cameraFollow;    
    [SerializeField] private BoxCollider2D moveBoundaries;
    [SerializeField] private Transform BulletsParent;
    [Header("Character")]
    [SerializeField] private Character character;
    [Header("Enemy")]
    [SerializeField] private Enemy[] enemies;

    private void Awake()
    {
        InitCharacter();
        InitAxesInput();
        InitCameraFollow();
        InitEnemies();
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
        character.InitDependencies(inputDetector,moveBoundaries);
        Gun gun = character.GetComponent<Gun>();
        gun.InitDependencies(BulletsParent);
    }

    private void InitEnemies()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.InitDependecies(character,moveBoundaries);

            Gun gun = enemy.GetComponent<Gun>();
            if (gun != null)
                gun.InitDependencies(BulletsParent);
        }
    }

}

