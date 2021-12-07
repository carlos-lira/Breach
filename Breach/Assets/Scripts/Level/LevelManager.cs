using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one player in scene");
            return;
        }
        instance = this;
    }
    #endregion

    [Header("Settings")]
    [SerializeField] private bool isPaused = false;
    [SerializeField] private float speed = 1f;
    
    [Header("Dependencies")]
    public Camera mainCamera;
    public Camera finishCamera;
    public GameObject inGameUI;
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public GameObject levelClearedUI;

    private bool checkForWinner = false;
    private bool isGameOver = false;


    private void Start()
    {
        checkForWinner = false;
        mainCamera.enabled = true;
        finishCamera.enabled = false;
        speed = 1f;

        ShowInGameUI();
    }

    private void Update()
    {
        if (checkForWinner && !isGameOver)
        { 
            var enemies = FindObjectsOfType<Enemy>();

            if (enemies.Length == 0)
            {
                //Last wave has already spawned and there are no enemies left. The game is over and the user won.
                GameOver(levelCleared: true);
            }
        }
    }

    public void GameOver(bool levelCleared = false)
    {
        Debug.Log("THE GAME IS OVER!");
        isGameOver = true;
        if (levelCleared)
        {
            ShowLevelClearedUI();
            GameManager.instance.SaveProgress();
            Debug.Log("USER WON");
        }
        else
        {
            ShowGameOverUI();
            Debug.Log("USER LOST");
        }
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            ShowPauseUI();
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            ShowInGameUI();
            isPaused = false;
            Time.timeScale = speed;
        }
    }

    public void IncreaseSpeed()
    {
        speed = (speed + 1) % 4;
        if (speed == 0)
            speed = 1;
        Time.timeScale = speed;
    }

    public void ResumeGame()
    {
        ShowInGameUI();
        isPaused = false;
        Time.timeScale = speed;
    }

    public void SwapCamera()
    {
        mainCamera.enabled = !mainCamera.enabled;
        finishCamera.enabled = !finishCamera.enabled;
    }

    //This function will be called from the WaveSpawner when the last wave has spawned
    public void CheckForWinner()
    {
        checkForWinner = true;
    }

    private void ShowInGameUI() 
    { 
        inGameUI.SetActive(true);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        levelClearedUI.SetActive(false);
    }

    private void ShowPauseUI() 
    {
        inGameUI.SetActive(false);
        pauseUI.SetActive(true);
        gameOverUI.SetActive(false);
        levelClearedUI.SetActive(false);
    }

    private void ShowGameOverUI() 
    {
        inGameUI.SetActive(false);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(true);
        levelClearedUI.SetActive(false);
    }

    private void ShowLevelClearedUI() 
    {
        inGameUI.SetActive(false);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        levelClearedUI.SetActive(true);
    }

}
