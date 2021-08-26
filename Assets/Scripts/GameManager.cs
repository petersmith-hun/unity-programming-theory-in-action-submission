using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;

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

    void Awake()
    {
        instance = this;
    }
}
