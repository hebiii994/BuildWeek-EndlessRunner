using UnityEngine;
using UnityEngine.Pool;


public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [Header("Prefab da 'Poolare'")]
    [SerializeField] private PooledChunk _chunkPrefab;
    [SerializeField] private PooledObstacle _obstaclePrefab;
    [SerializeField] private PooledCoin _coinPrefab;


    public IObjectPool<PooledChunk> ChunkPool { get; private set; }
    public IObjectPool<PooledObstacle> ObstaclePool { get; private set; }
    public IObjectPool<PooledCoin> CoinPool { get; private set; }

    private void Awake()
    {
        Instance = this;
        SetupPools();
    }

    private void SetupPools()
    {
        // Creiamo la pool per i pezzi di percorso (Chunk)
        ChunkPool = new ObjectPool<PooledChunk>(
            createFunc: () => Instantiate(_chunkPrefab),
            actionOnGet: (chunk) => chunk.gameObject.SetActive(true),
            actionOnRelease: (chunk) => chunk.gameObject.SetActive(false),
            actionOnDestroy: (chunk) => Destroy(chunk.gameObject),
            collectionCheck: false, 
            defaultCapacity: 10,
            maxSize: 20
        );

        // Creiamo la pool per gli ostacoli
        ObstaclePool = new ObjectPool<PooledObstacle>(
            createFunc: () => Instantiate(_obstaclePrefab),
            actionOnGet: (obstacle) => obstacle.gameObject.SetActive(true),
            actionOnRelease: (obstacle) => obstacle.gameObject.SetActive(false),
            actionOnDestroy: (obstacle) => Destroy(obstacle.gameObject),
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: 50
        );

        // Creiamo la pool per le monete/scudetti
        CoinPool = new ObjectPool<PooledCoin>(
            createFunc: () => Instantiate(_coinPrefab),
            actionOnGet: (coin) => coin.gameObject.SetActive(true),
            actionOnRelease: (coin) => coin.gameObject.SetActive(false),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            collectionCheck: false,
            defaultCapacity: 50,
            maxSize: 200
        );
    }
}
