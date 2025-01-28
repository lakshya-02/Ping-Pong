using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallMulti : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text playerOneScore;
    [SerializeField] private Text playerTwoScore;
    [SerializeField] private float delayTime = 2f;
    [SerializeField] private int winScore = 5;  // Number of points to win the game

    private int hitCounter;
    private Rigidbody2D rb;
    private bool playerLostLastPoint;
    private bool gameEnded = false;  // Flag to stop game updates once a winner is declared

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", delayTime);
    }

    private void FixedUpdate()
    {
        if (gameEnded) return;  // Prevent updates if the game has ended
        SetBallSpeed();
    }

    private void StartBall()
    {
        if (gameEnded) return;  // Prevent starting the ball if the game has ended

        float xDirection = playerLostLastPoint ? -1 : 1;
        rb.velocity = new Vector2(xDirection, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall(bool playerLost)
    {
        if (gameEnded) return;  // Prevent resetting the ball if the game has ended

        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        hitCounter = 0;
        playerLostLastPoint = playerLost;
        Invoke("StartBall", delayTime);
    }

    private void SetBallSpeed()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void PlayerBounce(Transform myObject)
    {
        if (gameEnded) return;  // Prevent bouncing if the game has ended

        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection = transform.position.x > 0 ? -1 : 1;
        float yDirection = Mathf.Clamp((ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y, -1, 1);

        rb.velocity = new Vector2(xDirection, yDirection == 0 ? 0.25f : yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameEnded) return;  // Prevent collisions if the game has ended

        if (collision.gameObject.name == "Player" || collision.gameObject.name == "Player2")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameEnded) return;  // Prevent scoring if the game has ended

        if (transform.position.x > 0)
        {
            UpdateScore(playerOneScore, true);
        }
        else if (transform.position.x < 0)
        {
            UpdateScore(playerTwoScore, false);
        }

        CheckWinner();
    }

    private void UpdateScore(Text scoreText, bool playerScored)
    {
        scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
        ResetBall(!playerScored);
    }

    private void CheckWinner()
    {
        int playerOnePoints = int.Parse(playerOneScore.text);
        int playerTwoPoints = int.Parse(playerTwoScore.text);

        if (playerOnePoints >= winScore)
        {
            DisplayWinner("Player 1");
        }
        else if (playerTwoPoints >= winScore)
        {
            DisplayWinner("Player 2");
        }
    }

    private void DisplayWinner(string winner)
    {
        Debug.Log(winner + " Wins!");
        rb.velocity = Vector2.zero;
        gameEnded = true;  // Set gameEnded to true to prevent further updates
        rb.isKinematic = true;  // Disable ball physics to ensure it stops moving

        // Load the game-over scene
        SceneManager.LoadSceneAsync(3);
    }
}
