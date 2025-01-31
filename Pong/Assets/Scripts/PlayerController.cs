using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    public float moveSpeed = 5f;
    public float boundary = 8.4f;

    public Transform player;
    
    public KeyCode playerMoveLeftKey = KeyCode.A;
    public KeyCode playerMoveRightKey = KeyCode.D;


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
    }
    
    void Update()
    {
        Vector3 playerPosition = player.position;
        
        if (Input.GetKey(playerMoveLeftKey))
        {
            playerPosition.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(playerMoveRightKey))
        {
            playerPosition.x += moveSpeed * Time.deltaTime;
        }
        
        // Clamp Player position
        playerPosition.x = Mathf.Clamp(playerPosition.x, -boundary, boundary);
        player.position = playerPosition;

    }
}