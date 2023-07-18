using System.Collections.Generic;

public class WavePool
{
    List<IPooledWaveObject> pool = new List<IPooledWaveObject>();

    public IPooledWaveObject Get(WaveObject waveObject)
    {
        IPooledWaveObject result;
        if (TryGet(waveObject.ToSpawn.Type,out result))
        {
            result.OnGet();
            result.IsActive = true;

            return result;
        }
        else
        {
            result = waveObject.ToSpawn.Spawn();

            pool.Add(result);
            result.InPool = true;
            result.OnGet();
            result.IsActive = true;

            return result;
        }
    }

    public void Release(IPooledWaveObject pooled)
    {
        pooled.OnRelease();
        pooled.IsActive = false;
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