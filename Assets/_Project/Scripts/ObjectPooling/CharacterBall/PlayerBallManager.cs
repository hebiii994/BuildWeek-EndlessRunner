using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBallManager : MonoBehaviour
{
    [Header("Riferimenti")]
    public Transform hitPoint;
    public Camera cam;
    public PlayerController playerController;

    [Header("Tiro")]
    [SerializeField] private float extraBallSpeed = 10f; // aggiunta oltre alla velocità del player
    [SerializeField] private float frontCamDistance = 15f; // distanza fissa davanti alla camera


    [Header("Pool")]
    public GameObject ballPrefab;
    public int maxBalls = 5;
    private ObjectPool<BallController> ballPool;
    public int currentBalls;
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
        // Converto il click in un punto davanti alla camera
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = frontCamDistance; // distanza fissa davanti alla camera
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        // Direzione dalla mano (hitPoint) al punto cliccato
        Vector3 dir = (worldPos - hitPoint.position).normalized;

        // Prendo una palla dal pool
        BallController ball = ballPool.Get();

        // Posiziono la palla davanti al player
        ball.transform.position = hitPoint.position;

        // Ignora collisione col player
        Collider playerCol = playerController.GetComponent<CharacterController>();
        Collider ballCol = ball.GetComponent<Collider>();
        if (playerCol != null && ballCol != null)
            Physics.IgnoreCollision(ballCol, playerCol, true);

        // Lancia la palla con velocità = velocità del player + extra
        ball.Launch(dir, this, playerController.forwardSpeed + extraBallSpeed);

        currentBalls--;
    }


    public void ReturnBall(BallController ball)
    {
        ballPool.Release(ball);
        currentBalls = Mathf.Min(currentBalls + 1, maxBalls);
    }
}
