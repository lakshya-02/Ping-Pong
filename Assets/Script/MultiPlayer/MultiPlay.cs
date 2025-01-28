using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlay : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private bool isSecondPlayer;

    private Rigidbody2D rb;
    private Vector2 playerMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isSecondPlayer)
        {
            SecondPlayerControl();
        }
        else
        {
            PlayerControl();
        }
    }

    private void PlayerControl()
    {
        float moveY = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveY = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }

        playerMove = new Vector2(0, moveY);
    }

    private void SecondPlayerControl()
    {
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        playerMove = new Vector2(0, moveY);
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMove * movementSpeed;  // Apply the velocity to move the paddle
    }
}
