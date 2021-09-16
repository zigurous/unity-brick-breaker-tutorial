using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }

    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        NewRound();
    }

    private void NewRound()
    {
        // Restore all the bricks
        for (int i = 0; i < this.bricks.Length; i++) {
            this.bricks[i].Restore();
        }

        ResetRound();
    }

    private void ResetRound()
    {
        // Reset the paddle
        this.paddle.rigidbody.velocity = Vector2.zero;
        this.paddle.transform.position = new Vector2(0f, this.paddle.transform.position.y);

        // Reset the ball
        this.ball.rigidbody.velocity = Vector2.zero;
        this.ball.transform.position = Vector2.zero;
        this.ball.SetRandomTrajectory();
    }

    private void Miss()
    {
        this.lives--;

        if (this.lives > 0) {
            Invoke(nameof(ResetRound), 1f);
        } else {
            Invoke(nameof(NewGame), 1f);
        }
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;

        if (Cleared()) {
            Invoke(nameof(NewRound), 1f);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy) {
                return false;
            }
        }

        return true;
    }

}
