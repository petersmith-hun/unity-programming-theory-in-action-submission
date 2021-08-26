using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Awake()
    {
        instance = this;
        UpdatePlayerNameBar();
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
}
