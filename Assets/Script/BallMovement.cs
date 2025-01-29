using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text AIScore;
    [SerializeField] private float delayTime = 2f;
    [SerializeField] private int winScore = 5;  

    private Rigidbody2D rb;
    private int hitCounter = 0;
    private bool playerLostLastPoint = false;
    private bool gameEnded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", delayTime);
    }

    private void FixedUpdate()
    {
        if (gameEnded) return;
        SetBallSpeed();
    }

    private void StartBall()
    {
        if (gameEnded) return;

        float xDirection = playerLostLastPoint ? -1 : 1;
        rb.velocity = new Vector2(xDirection, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall(bool playerLost)
    {
        if (gameEnded) return;

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
        if (gameEnded) return;

        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection = transform.position.x > 0 ? -1 : 1;
        float yDirection = Mathf.Clamp((ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y, -1, 1);

        rb.velocity = new Vector2(xDirection, yDirection == 0 ? 0.25f : yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameEnded) return;

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AI"))
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameEnded) return;

        if (collision.gameObject.CompareTag("GoalPlayer"))
        {
            UpdateScore(AIScore, false);
        }
        else if (collision.gameObject.CompareTag("GoalAI"))
        {
            UpdateScore(playerScore, true);
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
        int playerPoints = int.Parse(playerScore.text);
        int AIPoints = int.Parse(AIScore.text);

        if (playerPoints >= winScore)
        {
            DisplayWinner("Player");
        }
        else if (AIPoints >= winScore)
        {
            DisplayWinner("AI");
        }
    }

    private void DisplayWinner(string winner)
    {
        Debug.Log(winner + " Wins!");
        rb.velocity = Vector2.zero;
        gameEnded = true;
        rb.isKinematic = true;  

        SceneManager.LoadSceneAsync(3);
    }
}
