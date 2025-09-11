using UnityEngine;
using UnityEngine.Pool;

public class PooledChunk : MonoBehaviour
{
    private IObjectPool<PooledChunk> pool;

    public void SetPool(IObjectPool<PooledChunk> pool)
    {
        this.pool = pool;
    }

    public void ReturnToPool()
    {
        // Il LevelGenerator 
        pool.Release(this);
    }
}
