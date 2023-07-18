using System.Collections.Generic;
using UnityEngine;

public class WavePool
{
    List<IPooledWaveObject> pool = new List<IPooledWaveObject>();

    public IPooledWaveObject Get(WaveObject waveObject, Vector2 position)
    {
        IPooledWaveObject result;
        if (TryGet(waveObject.ToSpawn.Type,out result))
        {
            result.OnGet(position);

            return result;
        }
        else
        {
            result = waveObject.ToSpawn.Spawn();

            pool.Add(result);
            result.InPool = true;
            result.OnGet(position);

            return result;
        }
    }

    public void Release(IPooledWaveObject pooled)
    {
        pooled.OnRelease();
    }

    public void AddRemoteCreated(IPooledWaveObject toPool)
    {
        pool.Add(toPool);
        toPool.InPool = true;
    }

    private bool TryGet(string type,out IPooledWaveObject result)
    {
        foreach(IPooledWaveObject pooled in pool)
        {
            if (pooled.Type == type && pooled.IsActive == false)
            {
                result = pooled;
                return true;
            }
        }
        result = null;
        return false;
    }
}