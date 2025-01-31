using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI EndingText;
    
    public GameObject restartButton;
    public GameObject startButton;
    public GameObject PlayerController;
    
    public int score = 0;
    public int scoreThreshold = 3;
    public BallController ballController;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        restartButton.SetActive(false);
        if (BallController.Instance.gameStarted == false)
        {
            startButton.SetActive(true);
            PlayerController.GetComponent<PlayerController>().enabled = false;
        }
    }

    void Update()
    {
        // Check if the score threshold is reached
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int requiredScore = (currentSceneIndex + 1) * scoreThreshold;

        if (score >= requiredScore)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void UpdateScoreDisplay(int playerScore)
    {
        scoreBoard.text = playerScore.ToString();

    }

    public void EndGame(int playerScore)
    {
        EndingText.text = "Score: " + playerScore;
        PlayerController.GetComponent<PlayerController>().enabled = false;
    }

    // dont know how to reset the variables inside the nondestroyable singleton
    public void RestartGame()
    {
        score = 0;
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        PlayerController.GetComponent<PlayerController>().enabled = true;
        ballController.StartBall();
    }
    
}
