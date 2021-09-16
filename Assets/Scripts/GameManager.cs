using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }

    const int NUM_LEVELS = 2;

    public int level = 1;
    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        // Load the win scene if you have beaten all of the levels.
        if (level > NUM_LEVELS) {
            SceneManager.LoadScene("Winner");
        } else {
            SceneManager.LoadScene("Level" + level);
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
    }

    public void Miss()
    {
        this.lives--;

        if (this.lives > 0) {
            ResetLevel();
        } else {
            GameOver();
        }
    }

    private void ResetLevel()
    {
        this.paddle.ResetPaddle();
        this.ball.ResetBall();

        // for (int i = 0; i < this.bricks.Length; i++) {
        //     this.bricks[i].ResetBrick();
        // }
    }

    private void GameOver()
    {
        // SceneManager.LoadScene("GameOver");

        NewGame();
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;

        if (Cleared()) {
            LoadLevel(this.level + 1);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable) {
                return false;
            }
        }

        return true;
    }

}
