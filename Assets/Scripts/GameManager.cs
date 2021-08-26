using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text playerNameBar;

    public static GameManager instance { get; private set; }

    public bool isGameActive { get; private set; } = true;
    
    private float defaultHealth;

    public void UpdateHealthIndicator(int currentHealth, bool isDefaultHealth = false)
    {
        if (isDefaultHealth)
        {
            defaultHealth = currentHealth;
        }
        
        healthBarSlider.value = currentHealth / defaultHealth;
    }

    public void TriggerGameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        UpdatePlayerNameBar();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Scenes/TitleScreen");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Awake()
    {
        instance = this;
        UpdatePlayerNameBar();
    }

    void Update()
    {
        HandleReturningToMenu();
    }

    private void UpdatePlayerNameBar()
    {
        string playerName = GameSession.instance == null 
            ? "player" 
            : GameSession.instance.playerName;

        playerNameBar.text = isGameActive 
            ? $"Run, {playerName}, run!" 
            : $"Try again, {playerName}!";
    }

    private void HandleReturningToMenu()
    {
        if (isGameActive && Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }
}
