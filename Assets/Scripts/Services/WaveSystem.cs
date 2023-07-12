using System;
using System.Collections;
using UnityEngine;

public class WaveSystem : MonoBehaviour 
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private float minSpawnDistanceToPlayer = 3;
    [SerializeField] private float minSpawnDistanceToBounds = 1;

    private int currentWave = -1;
    private bool executingWave;

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
    }

    private void Start()
    {
        StartCoroutine(StartNewWave());
    }

    private IEnumerator StartNewWave()
    {
        if (currentWave < waves.Length-1)
        {
            Debug.Log("Started new wave");
            currentWave++;

            yield return StartCoroutine(RestTimer());
            Debug.Log("Wait time out");
        
            StartCoroutine(WaveTimer());
            StartCoroutine(ExecuteWave());
        }
        else
            GameEnded();
    }

    private IEnumerator RestTimer()
    {
        OnRestStarted?.Invoke();
        OnTimerChanged?.Invoke(waves[currentWave].StartDelay);

        float elapsedSeconds = 0;
        while(elapsedSeconds < waves[currentWave].StartDelay)
        {
            yield return new WaitForSeconds(1);            
            elapsedSeconds++;
            OnTimerChanged(waves[currentWave].StartDelay - elapsedSeconds);

        }
    }

    private IEnumerator ExecuteWave()
    {
        float timeToNextSpawn = 1 / waves[currentWave].SpawnRate;

        while (executingWave)
        {
            SpawnRandom();
            yield return new WaitForSeconds(timeToNextSpawn);
        }
    }

    private IEnumerator WaveTimer()
    {
        executingWave = true;
        OnSpawnStarted?.Invoke();
        OnTimerChanged?.Invoke(waves[currentWave].Duration);

        float elapsedSeconds = 0;
        while (elapsedSeconds < waves[currentWave].Duration)
        {
            yield return new WaitForSeconds(1);            
            elapsedSeconds++;
            OnTimerChanged(waves[currentWave].Duration - elapsedSeconds);

        }

        executingWave = false;
        WaveEnded();
        StartCoroutine(StartNewWave());

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
        GameObject spawned = waveObject.ToSpawn.Pool.Get().GameObject;
        Locate(spawned);
    }

    private void Locate(GameObject spawned)
    {
        Vector3 position;
        do
        {
            float x = UnityEngine.Random.Range(positionBounds.min.x + minSpawnDistanceToBounds, positionBounds.max.x - minSpawnDistanceToBounds);
            float y = UnityEngine.Random.Range(positionBounds.min.y + minSpawnDistanceToBounds, positionBounds.max.y - minSpawnDistanceToBounds);
            position = new Vector3(x, y, spawned.transform.position.z);

        } while ((position - player.transform.position).magnitude < minSpawnDistanceToPlayer);

        spawned.transform.position = position;
    }

    private void WaveEnded()
    {
        Debug.Log("WaveEnd");
        OnWaveEnd?.Invoke();
    }

    private void GameEnded()
    {
        Debug.Log("Waves end");
        foreach (var record in scoreCounter.Records)
        {
            Debug.Log(record.Key + ": " + record.Value.Kills + "(kills) " + record.Value.Damage + "(damage)");
        }
        throw new NotImplementedException();
    }

}
