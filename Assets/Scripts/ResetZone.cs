using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ResetZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        Ball ball = other.gameObject.GetComponent<Ball>();

        if (gameManager != null) {
            gameManager.Miss();
        } else if (ball != null) {
            ball.ResetBall();
        }
    }

}
