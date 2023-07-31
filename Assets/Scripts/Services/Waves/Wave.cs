using UnityEngine;
using System;

[CreateAssetMenu(fileName ="Wave",menuName ="ScriptableObjects/Wave")]
[Serializable]
public class Wave : ScriptableObject
{    
    [SerializeField] float startDelay = 30;
    [SerializeField] float duration = 60;
    [SerializeField] float spawnRate = 2;
    [SerializeField] WaveObject[] waveObjects;

    public float StartDelay { get => startDelay; }
    public float Duration { get => duration; }
    public float SpawnRate { get => spawnRate; }
    public WaveObject[] WaveObjects { get => waveObjects; }

}