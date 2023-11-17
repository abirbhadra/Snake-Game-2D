using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    [SerializeField]
    private PlayerType playerType;
    public float score = 0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ShieldPowerUpUI;
    public TextMeshProUGUI SpeedPowerUpUI;
    public TextMeshProUGUI HighScoreUI;
    public Transform segmentPrefab;
    public Vector2Int direction = Vector2Int.right;
    public float speed = 15f;
    public float speedMultiplier = 1f;
    public int initialSize = 4;
    public bool moveThroughWalls = false;
    private bool canReset = true;
    public string sceneName = "Menu";
    public List<Transform> segments = new List<Transform>();
    private Vector2Int input;
    private float nextUpdate;
    public Button Resume;
    public Button Restart;
    public Button Menu;
    public Button Pause;
    public Button RestartPause;
    public Button GameOverMenu;
    public Button WinnerRestart;
    public Button WinnerMenu;

    public WinnerUI winnerUI;
    public GameOver gameOver;
    public PauseMenu pauseMenu;
    
    private void Start()
    {
        WinnerMenu.onClick.AddListener(MenuPauseButton);
        WinnerRestart.onClick.AddListener(RestartButton);
        Resume.onClick.AddListener(ResumeButton);
        Pause.onClick.AddListener(PauseButton);
        Restart.onClick.AddListener(RestartButton);
        Menu.onClick.AddListener(MenuPauseButton);
        RestartPause.onClick.AddListener(RestartButton);
        GameOverMenu.onClick.AddListener(GameOverMenuButton);
        ResetState();
    }


    private void Update()
    {


        scoreText.text = "Score: " + score;

        HighScoreUI.text = "Score: " + score;

        if (playerType == PlayerType.Player1 )
        {
            if (direction.x != 0f)
            {
                if (Input.GetKeyDown(KeyCode.W)) // Input.GetKeyDown(KeyCode.UpArrow)) 
                {
                    input = Vector2Int.up;
                } else if (Input.GetKeyDown(KeyCode.S)) // Input.GetKeyDown(KeyCode.DownArrow)) 
                {
                    input = Vector2Int.down;
                }
            }
            // Only allow turning left or right while moving in the y-axis
            else if (direction.y != 0f)
            {
                if (Input.GetKeyDown(KeyCode.D))// Input.GetKeyDown(KeyCode.RightArrow))
                {
                    input = Vector2Int.right;
                } else if (Input.GetKeyDown(KeyCode.A)) //Input.GetKeyDown(KeyCode.LeftArrow) 
                {
                    input = Vector2Int.left;
                }
            }
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(direction) - 90);

        }
        else if (playerType == PlayerType.Player2)
            {
            if (direction.x != 0f)
            {
                if ( Input.GetKeyDown(KeyCode.UpArrow)) 
                {
                    input = Vector2Int.up;
                }
                else if ( Input.GetKeyDown(KeyCode.DownArrow)) 
                {
                    input = Vector2Int.down;
                }
            }
            // Only allow turning left or right while moving in the y-axis
            else if (direction.y != 0f)
            {
                if ( Input.GetKeyDown(KeyCode.RightArrow))
                {
                    input = Vector2Int.right;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
                {
                    input = Vector2Int.left;
                }
            }
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(direction) - 90);
        }
    }

    
    private void GameOverMenuButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    private void PauseButton()
    {
        Time.timeScale = 0;
        pauseMenu.EnableImage();
    }
    private void ResumeButton()
    {
        pauseMenu.DisableImage();
        Time.timeScale = 1;
    }
    private void RestartButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void MenuPauseButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(sceneName);
    }
   

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void FixedUpdate()
    {
        // Wait until the next update before proceeding
        if (Time.time < nextUpdate) {
            return;
        }

        // Set the new direction based on the input
        if (input != Vector2Int.zero) {
            direction = input;
        }

        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }
        
        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        int x = Mathf.RoundToInt(transform.position.x) + direction.x;
        int y = Mathf.RoundToInt(transform.position.y) + direction.y;
        transform.position = new Vector2(x, y);

        


        // Set the next update time based on the speed
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }

    public void Grow()
    {
        
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }
    // Method to reduce the size of the snake
    public void ReduceSize()
    {
        // Get the last segment and destroy it
        Transform lastSegment = segments[segments.Count - 1];
        Destroy(lastSegment.gameObject);

        // Remove the last segment from the list
        segments.RemoveAt(segments.Count - 1);
    }
    public void ResetState()
    {
        if (playerType == PlayerType.Player1)
        {
            direction = Vector2Int.right;
            transform.position = Vector3.zero;

            // Start at 1 to skip destroying the head
            for (int i = 1; i < segments.Count; i++)
            {
                Destroy(segments[i].gameObject);
            }

            // Clear the list but add back this as the head
            segments.Clear();
            segments.Add(transform);

            // -1 since the head is already in the list
            for (int i = 0; i < initialSize - 1; i++)
            {
                Grow();
            }
        }
        else if (playerType == PlayerType.Player2)
        {
            direction = Vector2Int.left;
            transform.position = new Vector3(0,2,0);

            // Start at 1 to skip destroying the head
            for (int i = 1; i < segments.Count; i++)
            {
                Destroy(segments[i].gameObject);
            }

            // Clear the list but add back this as the head
            segments.Clear();
            segments.Add(transform);

            // -1 since the head is already in the list
            for (int i = 0; i < initialSize - 1; i++)
            {
                Grow();
            }
        }
    }

    public bool Occupies(int x, int y)
    {
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y) {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (canReset) // Only perform the reset if canReset is true
        {
            if (other.gameObject.CompareTag("Food"))
            {
                score += 10;
                Grow();
            }
            
            
            else if (other.gameObject.CompareTag("shield"))
            {
                string powerUPText = "!! SHIELD RECIEVED !!";
                DisplayShieldPopUp(powerUPText);
                // Handle shield power-up collision
                // For example, disable the reset function
                canReset = false;
            }
            else if(other.gameObject.CompareTag("Speed"))
            {
                string speedpowerUPText = "!! 2X SPEED OBTAINED !!";
                DisplaySpeedPopUp(speedpowerUPText);
                speed *= 2;
                StartCoroutine(ResetSpeedAfterDelay(5f));
            }
            else if (other.gameObject.CompareTag("FoodBurner"))
            {
                score -= 5;
                // Check if the snake size is greater than 6 before reducing it
                if (segments.Count > 6)
                {
                    // Reduce snake size by 1
                    ReduceSize();
                }
            }
            
        }
        

        if (other.gameObject.CompareTag("Wall"))
        {

            Debug.Log("nothing");
            Debug.Log("movethroughwall1"+moveThroughWalls);
            if (!moveThroughWalls)
            {
                moveThroughWalls = true;
                Debug.Log("check colliding wall");
                Traverse(other.transform);
                
            }
            else
            {
                moveThroughWalls = false;
                
            }
        }

        // Check if the snake collides with itself and enemy

        if (playerType == PlayerType.Player1)
        {
            // Check if the snake collides with itself
            if (other.gameObject.CompareTag("Obstacle"))
            {
                Time.timeScale = 0;
                gameOver.EnableImage();
                // If it does, enable the reset function
                canReset = true;
                ResetState();
            }
            else if (other.gameObject.CompareTag("Obstacle1"))
            {
                Time.timeScale = 0;
                winnerUI.EnableImage();               
                winnerUI.DisableImageRed();
                winnerUI.EnableImageGreen();
                // If it does, enable the reset function
                canReset = true;
                ResetState();
            }
        }
        else if (playerType == PlayerType.Player2)
        {
            // Check if the snake collides with itself
            if (other.gameObject.CompareTag("Obstacle1"))
            {
                Time.timeScale = 0;
                gameOver.EnableImage();
                // If it does, enable the reset function
                canReset = true;
                ResetState();
            }
            else if (other.gameObject.CompareTag("Obstacle"))
            {
                Time.timeScale = 0;
                gameOver.DisableImage();
                winnerUI.EnableImage();
                
                Debug.Log("green");
                winnerUI.EnableImageGreen();
                winnerUI.DisableImageRed();
                // If it does, enable the reset function
                canReset = true;
                ResetState();
            }
        }


    }
    
    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the speed to its original value
        speed /= 2;
    }

    private void Traverse(Transform wall)
    {

        
        Vector3 position = transform.position;

        if (direction.x != 0f)
        {
            position.x = Mathf.RoundToInt(-wall.position.x + direction.x);
        }
        else if (direction.y != 0f)
        {
            position.y = Mathf.RoundToInt(-wall.position.y + direction.y);
        }
        Debug.Log(position);
        Debug.Log("2"+moveThroughWalls);
        transform.position = position;
        
        
    }

    private void DisplayShieldPopUp(string shieldpowerUpText)
    {
        
        ShieldPowerUpUI.gameObject.SetActive(true);
        ShieldPowerUpUI.SetText(shieldpowerUpText);
        StartCoroutine(DisableShieldPowerUpPopUP(5));
    }
    private IEnumerator DisableShieldPowerUpPopUP(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        ShieldPowerUpUI.gameObject.SetActive(false);
    }

    private void DisplaySpeedPopUp(string speedpowerUpText)
    {

        SpeedPowerUpUI.gameObject.SetActive(true);
        SpeedPowerUpUI.SetText(speedpowerUpText);
        StartCoroutine(DisableSpeedPowerUpPopUP(5));
    }
    private IEnumerator DisableSpeedPowerUpPopUP(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        SpeedPowerUpUI.gameObject.SetActive(false);
    }



}
public enum PlayerType
{
    Player1,
    Player2
}
