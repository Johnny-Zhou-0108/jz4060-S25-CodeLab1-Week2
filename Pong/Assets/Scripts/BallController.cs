using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController Instance;
    
    public float ballSpeed = 5f;

    // Direction
    private Vector2 ballDirection;
    private float randomX;
    private float randomY;
    private float xNew;

    public bool gameStarted = false;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
        // The Y-Speed cannot be 0, otherwise it will never reach the board
        if (Random.Range(0, 2) == 0)
        {
            randomY = -1f;
        }
        else
        {
            randomY = 1f;
        }
        randomX = Random.Range(-1f, 1f);

        // Normalize the direction so that it's more accurate when it comes to the speed multiply
        ballDirection = new Vector2(randomX, randomY).normalized;
    }

    void Update()
    {
        if (gameStarted)
        {
            transform.Translate(ballDirection * ballSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Board"))
        {
            ballDirection.y = -ballDirection.y;

            // New x direction is ball's x position - board's x position
            xNew = transform.position.x - collision.transform.position.x;
            ballDirection.x = xNew;
            
            ballDirection = ballDirection.normalized;

            GameManager.Instance.score++;
            GameManager.Instance.UpdateScoreDisplay(GameManager.Instance.score);
        }
        else if (collision.gameObject.CompareTag("Wall_Up"))
        {
            ballDirection.y = -ballDirection.y;
        }
        else if (collision.gameObject.CompareTag("Wall_LR"))
        {
            ballDirection.x = -ballDirection.x;
        }
        else if (collision.gameObject.CompareTag("Obstacles"))
        {
            ballDirection.y = -ballDirection.y;

            xNew = transform.position.x - collision.transform.position.x;
            ballDirection.x = xNew;
            
            ballDirection = ballDirection.normalized;
            
            ballSpeed ++;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            //Debug.Log("Game Over");
            GameManager.Instance.EndGame(GameManager.Instance.score);
            ballSpeed = 0f;
        }
    }

    public void StartBall()
    {
        gameStarted = true;
    }
}
