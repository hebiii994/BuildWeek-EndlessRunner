using UnityEngine;
using UnityEngine.Pool;

public class PooledCoin : MonoBehaviour
{
    private IObjectPool<PooledCoin> pool;

    public void SetPool(IObjectPool<PooledCoin> pool)
    {
        this.pool = pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Scudetto raccolto!");
            // UIManager.Instance.AddCoin();

            // Una volta raccolto, lo scudetto torna da solo nella pool.
            pool.Release(this);
        }
    }
}