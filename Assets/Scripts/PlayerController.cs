using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private static readonly float playerWalkingSpeed = 20.0f;
    private static readonly float playerRunningSpeed = 50.0f;
    private static readonly float playerRotationSpeed = 2.0f;
    
    private Animator playerAnimator;

    private float verticalPlayerInput;
    private float horizontalPlayerInput;
    private bool isPlayerRunning;
    private float currentAnimationSpeed = 0.0f;
    private float currentPlayerMovementSpeed = 0.0f;
    private int health = 1000;

    public void DamagePlayer(int damage)
    {
        health -= damage;
        gameManager.UpdateHealthIndicator(health);

        if (health <= 0)
        {
            gameManager.TriggerGameOver();
            TriggerPlayerDeathAnimation();
        }
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        gameManager.UpdateHealthIndicator(health, true);
    }

    void Update()
    {
        GatherInput();
        HandlePlayerMovement();
        TriggerPlayerMovementAnimation();
    }

    private void GatherInput()
    {
        if (!GameManager.instance.isGameActive)
        {
            return;
        }
        
        verticalPlayerInput = Input.GetAxis("Vertical");
        horizontalPlayerInput = Input.GetAxis("Horizontal");
        isPlayerRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void HandlePlayerMovement()
    {
        currentPlayerMovementSpeed = isPlayerRunning 
            ? playerRunningSpeed 
            : playerWalkingSpeed;
        transform.Translate(Vector3.forward * currentPlayerMovementSpeed * Time.deltaTime * verticalPlayerInput, Space.Self);
        transform.Rotate(Vector3.up * horizontalPlayerInput * playerRotationSpeed);
    }

    private void TriggerPlayerMovementAnimation()
    {
        if (verticalPlayerInput != 0.0f)
        {
            currentAnimationSpeed = isPlayerRunning ? 1.0f : 0.3f;
        }
        else
        {
            currentAnimationSpeed = 0.0f;
        }

        playerAnimator.SetFloat("Speed_f", currentAnimationSpeed);
    }

    private void TriggerPlayerDeathAnimation()
    {
        playerAnimator.SetInteger("DeathType_int", 1);
        playerAnimator.SetBool("Death_b", true);
    }
}
