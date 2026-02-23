using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game States")]
    public bool isGameStarted = false;
    public bool isGameOver = false;

    [Header("Score Settings")]
    public float coinScore = 0;
    private float distanceScore;

    [Header("UI Referances")]
    public GameObject startScreen;
    public GameObject gameUI; // The Score Text (hidden at start)
    public GameObject gameOverPanel;

    public TMP_Text scoreText;
    public TMP_Text coinText;
    public TMP_Text finalScoreText; // inside game over panel

    [Header("Game Settings")]
    public Transform player;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        isGameStarted = false;
        isGameOver = false;
        
        // Show Start Screen, Hide Game UI
        if (startScreen != null) startScreen.SetActive(true);
        if (gameUI != null) gameUI.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // 1. WAITING TO START
        if (!isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
            return;
        }

        // 2. GAME RUNNING
        if (!isGameOver && !isGameOver && player != null)
        {
            distanceScore = player.position.z;
            
            // Update UI
            if (scoreText != null) scoreText.text = ((int)distanceScore).ToString() + "m";
            if (coinText != null) coinText.text = "Coins: " + coinScore.ToString();
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        if (startScreen != null) startScreen.SetActive(false);
        if (gameUI != null) gameUI.SetActive(true);
    }

    public void AddCoin()
    {
        coinScore++;
        // Play coin sound here later
    }

    public void EndGame()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameOverPanel != null) 
        {
            gameOverPanel.SetActive(true);
            // Show total score
            finalScoreText.text = "Distance: " + (int)distanceScore + "\nCoins: " + coinScore;
        }
            
        // Hide Game UI
        if (gameUI != null) gameUI.SetActive(false);

        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}