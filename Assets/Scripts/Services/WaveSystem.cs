using System;
using System.Collections;
using UnityEngine;
using Fusion;

public class WaveSystem : NetworkBehaviour 
{    
    [SerializeField] private Wave[] waves;
    [SerializeField] private float minSpawnDistanceToPlayer = 3;
    [SerializeField] private float minSpawnDistanceToBounds = 1;

    [Networked(OnChanged = nameof(ChangedReadyPlayers))] private int ReadyPlayersCount { get; set; } = 0;

    public bool IsReady { get; private set; }
    public WavePool Pool { get; private set; }

    private int currentWave = -1;
    private bool executingWave;

    public event Action<int> OnPlayersReadyChanged;
    public event Action OnGameStarted;
    public event Action OnSpawnStarted;
    public event Action OnRestStarted;    
    public event Action<float> OnTimerChanged;
    public event Action OnWaveEnd;


    private ScoreCounter scoreCounter;
    private Character player;
    private Bounds positionBounds;

    public void InitDependecies(Bounds positionBounds, Character player,ScoreCounter scoreCounter)
    {
        this.player = player;
        this.positionBounds = positionBounds;
        this.scoreCounter = scoreCounter;

        Pool = new WavePool();
    }

    public void ChangeReadyState()
    {
        if (IsReady)
            RPC_RemoveReady();
        else
            RPC_AddReady();

        IsReady = !IsReady;
    }

    private static void ChangedReadyPlayers(Changed<WaveSystem> waveSystem)
    {
        waveSystem.Behaviour.OnPlayersReadyChanged?.Invoke(waveSystem.Behaviour.ReadyPlayersCount);
    }    
    
    [Rpc(sources: RpcSources.All,targets: RpcTargets.StateAuthority)]
    private void RPC_AddReady()
    {
        if (HasStateAuthority)
        {
            ReadyPlayersCount++;

            if (ReadyPlayersCount == FusionConnect.PlayersCount)
                RPC_StartGame();
        }

    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    private void RPC_RemoveReady()
    {
        if (HasStateAuthority)
            ReadyPlayersCount--;
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    private void RPC_StartGame()
    {
        OnGameStarted?.Invoke();

        if (HasStateAuthority)
        {
            RPC_StartWave();
            Runner.SessionInfo.IsOpen = false;
        }

    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    private void RPC_StartWave()
    {
        currentWave++;
        if (HasStateAuthority)
            RPC_StartRest();
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    private void RPC_StartRest()
    {
        StartCoroutine(Rest());
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    private void RPC_ExecuteWave()
    {
        StartCoroutine(WaveTimer());
        StartCoroutine(ExecuteWave());
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    private void RPC_EndWave()
    {
        OnWaveEnd?.Invoke();

        if (HasStateAuthority)
        {
            if (currentWave + 1 < waves.Length)
                RPC_StartWave();
            else
                RPC_EndGame();
        }

    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    private void RPC_EndGame()
    {
        foreach (var record in scoreCounter.Records)
        {
            Debug.Log(record.Key + ": " + record.Value.Kills + "(kills) " + record.Value.Damage + "(damage)");
        }
        throw new NotImplementedException();
    }


    private IEnumerator Rest()
    {
        OnRestStarted();
        OnTimerChanged(waves[currentWave].StartDelay);

        float elapsedSeconds = 0;
        while (elapsedSeconds < waves[currentWave].StartDelay)
        {
            yield return new WaitForSeconds(1);
            elapsedSeconds++;
            OnTimerChanged(waves[currentWave].StartDelay - elapsedSeconds);
        }

        if (HasStateAuthority)
            RPC_ExecuteWave();
    }

    private IEnumerator ExecuteWave()
    {
        int wave = currentWave;
        float timeToNextSpawn = 1 / waves[currentWave].SpawnRate;

        while (executingWave && wave == currentWave)
        {
            if(HasStateAuthority)
                SpawnRandom();
            yield return new WaitForSeconds(timeToNextSpawn);
        }
    }

    private IEnumerator WaveTimer()
    {
        executingWave = true;
        OnSpawnStarted();
        OnTimerChanged(waves[currentWave].Duration);

        float elapsedSeconds = 0;
        while (elapsedSeconds < waves[currentWave].Duration)
        {
            yield return new WaitForSeconds(1);
            elapsedSeconds++;
            OnTimerChanged(waves[currentWave].Duration - elapsedSeconds);
        }

        if (HasStateAuthority)
            RPC_EndWave();

    }

    private void SpawnRandom()
    {
        if (waves[currentWave].WaveObjects.Length == 0)
            return;

        int randomRange = 0;
        foreach(WaveObject waveObject in waves[currentWave].WaveObjects)
        {
            randomRange += waveObject.ProportionalProbability;
        }

        int random = UnityEngine.Random.Range(0, randomRange);

        int bound = waves[currentWave].WaveObjects[0].ProportionalProbability;
        for(int i = 0; i < waves[currentWave].WaveObjects.Length; i++)
        {
            if(random < bound)
            {
                WaveObject waveObject = waves[currentWave].WaveObjects[i];
                Spawn(waveObject);
                break;
            }
            bound += waves[currentWave].WaveObjects[i + 1].ProportionalProbability;
        }
    }

    private void Spawn(WaveObject waveObject)
    {
        IPooledWaveObject spawned = Pool.Get(waveObject);
        Locate(spawned);
    }

    private void Locate(IPooledWaveObject spawned)
    {
        Vector2 position;
        do
        {
            float x = UnityEngine.Random.Range(positionBounds.min.x + minSpawnDistanceToBounds, positionBounds.max.x - minSpawnDistanceToBounds);
            float y = UnityEngine.Random.Range(positionBounds.min.y + minSpawnDistanceToBounds, positionBounds.max.y - minSpawnDistanceToBounds);
            position = new Vector2(x, y);

        } while ((position - (Vector2)player.transform.position).magnitude < minSpawnDistanceToPlayer);

        spawned.Locate(position);
    }
}
