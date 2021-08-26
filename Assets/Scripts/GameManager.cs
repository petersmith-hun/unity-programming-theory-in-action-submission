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

    // ENCAPSULATION
    public static GameManager instance { get; private set; }

    // ENCAPSULATION
    public bool isGameActive { get; private set; } = true;
    
    private float defaultHealth = 0.0f;

    // ENCAPSULATION
    // Default health should be set only once then its value must be locked.
    public void SetDefaultHealth(int defaultHealth)
    {
        if (this.defaultHealth == 0.0f)
        {
            this.defaultHealth = defaultHealth;
        }
        else
        {
            Debug.LogWarning("Default health value cannot be modified once set");
        }
    }

    // ABSTRACTION
    // Health indicator's value should be calculated based on the current and max health value.
    public void UpdateHealthIndicator(int currentHealth)
    {
        healthBarSlider.value = currentHealth / defaultHealth;
    }

    // ABSTRACTION
    // Triggering a game over consists of multiple steps:
    //  - Flagging the game inactive
    //  - Showing the game over screen
    //  - Updating the player name bar
    public void TriggerGameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        UpdatePlayerNameBar();
        UnlockCursor();
    }

    // ABSTRACTION
    // Moving back to the menu is essentially loading the title screen scene.
    public void BackToMenu()
    {
        SceneManager.LoadScene("Scenes/TitleScreen");
    }

    // ABSTRACTION
    // Retrying is essentially reloading the main scene.
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Awake()
    {
        instance = this;
        UpdatePlayerNameBar();
        LockCursor();
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
            UnlockCursor();
            BackToMenu();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
