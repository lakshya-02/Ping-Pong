using UnityEngine;

public class BallCollisionSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a paddle or a wall
        if (collision.gameObject.CompareTag("Paddle") || collision.gameObject.CompareTag("Wall"))
        {
            audioSource.Play();
        }
    }
}
