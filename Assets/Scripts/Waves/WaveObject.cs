using System;
using UnityEngine;

[Serializable]
public class WaveObject
{
    [SerializeField] SpawnObject toSpawn;
    [SerializeField] int proportionalProbability;

    public SpawnObject ToSpawn { get => toSpawn; }
    public int ProportionalProbability { get => proportionalProbability; }
}