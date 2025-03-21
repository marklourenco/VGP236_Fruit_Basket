using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public enum GameState
{
    Play,
    Pause
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance;  } }

    [SerializeField]
    private TMP_Text scoreText = null;
    [SerializeField]
    private TMP_Text scoreTextTitle = null;
    private int score = 0;

    private float timer = 0.0f;

    private bool alreadyRewarded = false;

    // Player
    [SerializeField]
    private GameObject player;

    // Title Screen
    [SerializeField]
    private GameObject titleScreen;

    // Play Screen
    [SerializeField]
    private GameObject playScreen;

    // Continue Screen
    [SerializeField]
    private GameObject continueScreen;

    // Leaderboards UI update
    [Header("Leaderboard UI")]
    [SerializeField]
    private GameObject leaderboardUI;
    [SerializeField]
    private TMP_Text[] namesText;
    [SerializeField]
    private TMP_Text[] scoresText;
    [SerializeField]
    private GameObject HighScoreButton;

    // Game State
    private GameState gameState = GameState.Pause;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            AddScore(0);
        }
    }

    private void Start()
    {
        leaderboardUI.SetActive(false);
        playScreen.SetActive(false);
        continueScreen.SetActive(false);
        player.SetActive(false);
        titleScreen.SetActive(true);

    }

    // Timer
    private void Update()
    {
        if (gameState == GameState.Play)
        {
            timer += Time.deltaTime;
        }
    }
    public float GetTimer()
    {
        return timer;
    }

    // Score
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score:  " + score.ToString();
        scoreTextTitle.text = "Previous Score:  " + score.ToString();
        Debug.Log(amount);
    }
    public int GetScore()
    {
        return score;
    }

    // Game Over
    public void GameOver()
    {
        gameState = GameState.Pause;

        continueScreen.SetActive(true);
        player.SetActive(false);
        titleScreen.SetActive(false);
        playScreen.SetActive(true);
        leaderboardUI.SetActive(false);

        SpawnManager.Instance.DeleteAllSpawnedObjects();
        // Where you lose:
        SendScoreToLeaderboard();
    }

    // Game State
    public GameState GetGameState()
    {
        return gameState;
    }

    public void PauseGame()
    {
        gameState = GameState.Pause;
    }

    // Leaderboard
    private void SendScoreToLeaderboard()
    {
        UGSManager.Instance.AddScore("HighestScore", score);
    }

    public void LoadLeaderboard()
    {
        UGSManager.Instance.GetScores("HighestScore");
    }

    public void ShowLeaderboardUI(List<LeaderboardEntry> entries)
    {
        player.SetActive(false);
        continueScreen.SetActive(false);
        titleScreen.SetActive(false);
        playScreen.SetActive(false);
        leaderboardUI.SetActive(true);

        for (int i = 0; i < scoresText.Length; i++)
        {
            if (entries.Count <= i)
            {
                scoresText[i].text = "";
                namesText[i].text = "";
            }
            else
            {
                scoresText[i].text = entries[i].Score.ToString();
                namesText[i].text = entries[i].PlayerName.ToString().Split('#')[0];
            }
        }
    }

    // Title
    public void ShowTitleUI()
    {
        player.SetActive(false);
        continueScreen.SetActive(false);
        leaderboardUI.SetActive(false);
        playScreen.SetActive(false);
        titleScreen.SetActive(true);

        alreadyRewarded = false;
    }

    // Play Screen
    public void ShowPlayUI()
    {

        player.SetActive(true);
        continueScreen.SetActive(false);
        titleScreen.SetActive(false);
        leaderboardUI.SetActive(false);
        playScreen.SetActive(true);
    }

    // Begin Play
    public void BeginGame()
    {
        ShowPlayUI();

        score = 0;
        timer = 0.0f;
        SpawnManager.Instance.ResetSpeed();

        scoreText.text = "Score:  " + score.ToString();

        gameState = GameState.Play;
    }

    public void Continue()
    {
        ShowPlayUI();
        gameState = GameState.Play;
    }

    // Rewarded Ad Flip
    public void FlipRewarded()
    {
        alreadyRewarded = !alreadyRewarded;
    }

    public bool GetRewarded()
    {
        return alreadyRewarded;
    }

    // Quit
    public void QuitGame()
    {
        Application.Quit();
    }
}
