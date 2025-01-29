using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private bool isAI;
    [SerializeField] private GameObject ball;  // Assign the ball in the inspector for AI tracking

    private Rigidbody2D rb;
    private Vector2 playerMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Ensure the AI has a ball reference
        if (isAI && ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
        }
    }

    private void Update()
    {
        if (isAI)
        {
            AIControl();
        }
        else
        {
            PlayerControl();
        }
    }

    private void PlayerControl()
    {
        float moveY = Input.GetAxisRaw("Vertical"); // Smoother input handling
        playerMove = new Vector2(0, moveY);
    }

    private void AIControl()
    {
        if (ball == null) return; // Prevent errors if ball is missing

        float ballY = ball.transform.position.y;
        float paddleY = transform.position.y;

        float moveThreshold = 0.2f; // Reduce jitter by adding a dead zone

        if (ballY > paddleY + moveThreshold)
        {
            playerMove = new Vector2(0, 1);
        }
        else if (ballY < paddleY - moveThreshold)
        {
            playerMove = new Vector2(0, -1);
        }
        else
        {
            playerMove = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMove * movementSpeed;
    }
}
