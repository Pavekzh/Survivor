using System;
using System.Collections;
using UnityEngine;

public class WaveSystem : MonoBehaviour 
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private float minSpawnDistanceToPlayer = 3;
    [SerializeField] private float minSpawnDistanceToBounds = 1;

    private Character player;
    private Bounds positionBounds;
    private int currentWave = -1;
    private Coroutine executingWave;

    public event Action OnWaveEnd;

    public void InitDependecies(Bounds positionBounds, Character player)
    {
        this.player = player;
        this.positionBounds = positionBounds;
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

            yield return StartCoroutine(WaitingWave());
            Debug.Log("Wait time out");

            executingWave = StartCoroutine(ExecuteWave());
            StartCoroutine(WaveTimer());
        }
        else
            GameEnded();
    }

    private IEnumerator WaitingWave()
    {
        yield return new WaitForSeconds(waves[currentWave].StartDelay);
    }

    private IEnumerator ExecuteWave()
    {
        float timeToNextSpawn = 1 / waves[currentWave].SpawnRate;

        while (true)
        {
            SpawnRandom();
            yield return new WaitForSeconds(timeToNextSpawn);
        }
    }

    private IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(waves[currentWave].Duration);
        StopCoroutine(executingWave);
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
        throw new NotImplementedException();
    }

}
