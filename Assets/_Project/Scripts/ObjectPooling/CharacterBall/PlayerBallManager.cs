using UnityEngine;
using UnityEngine.Pool;

public class PlayerBallManager : MonoBehaviour
{
    [Header("Riferimenti")]
    public Transform hitPoint;
    public Camera cam;
    public PlayerController playerController;

    [Header("Pool")]
    public GameObject ballPrefab;
    public int maxBalls = 5;
    private ObjectPool<BallController> ballPool;

    private int currentBalls;
    public int CurrentBalls => currentBalls;


    [Header("Cooldown")]
    [SerializeField] private float shootCooldown = 0.5f; // mezzo secondo tra un tiro e l'altro
    private float lastShootTime = 0f;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        ballPool = new ObjectPool<BallController>(() =>
        {
            BallController ball = Instantiate(ballPrefab).GetComponent<BallController>();
            ball.gameObject.SetActive(false);
            return ball;
        }, ball =>
        {
            ball.gameObject.SetActive(true);
        }, ball =>
        {
            ball.gameObject.SetActive(false);
        }, ball =>
        {
            Destroy(ball.gameObject);
        }, false, maxBalls, maxBalls);


        currentBalls = maxBalls;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentBalls > 0)
        {
            // Controllo cooldown
            if (Time.time - lastShootTime < shootCooldown)
                return;

            // Controllo: ignora click nella parte bassa dello schermo
            float screenLimit = Screen.height * 0.25f; // 25% parte bassa
            if (Input.mousePosition.y < screenLimit)
                return;

            ShootBall();
            lastShootTime = Time.time; // aggiorna l'ultimo tiro
        }
    }

    private void ShootBall()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 dir = (hit.point - hitPoint.position).normalized;
            BallController ball = ballPool.Get();

            // Posiziona la palla
            ball.transform.position = hitPoint.position + dir * 0.5f; // mezzo metro davanti al player

            // Ignora collisione con il player
            Collider playerCol = playerController.GetComponent<CharacterController>();
            Collider ballCol = ball.GetComponent<Collider>();
            if (playerCol != null && ballCol != null)
                Physics.IgnoreCollision(ballCol, playerCol, true);

            // Lancia la palla
            ball.Launch(dir, this, playerController.forwardSpeed);
            currentBalls--;
        }
    }


    public void ReturnBall(BallController ball)
    {
        ballPool.Release(ball);
        currentBalls = Mathf.Min(currentBalls + 1, maxBalls);
    }
}
